using System;
using System.Collections.Generic;
using System.Linq;
using ScriptDotNet2;
using ScriptSDK;
using ScriptSDK.Engines;
using ScriptSDK.Items;
using ScriptSDK.Mobiles;

namespace ScriptGUI
{
    class Lumbermethod
    {
        
        public static uint Boards = 0x1BD7;
        public static uint Logs = 0x1BDD;
        public static List<ushort> Extras = new List<ushort>();
        public static ushort[] Hues;

        public static void Setvariables()
        {
            Hues = new ushort[]{0x0, 0x7DA, 0x4A7, 0x4A8, 0x4A9, 0x4AA};// Reg,Oak,Ash,Yew,Heartwood,Bloodwood
            Extras.Add(0x3191); //Fungi
            Extras.Add(0x318f); //Bark
            Extras.Add(0x2F5F); //Switch
            Extras.Add(0x3190); //Plant
            Extras.Add(0x3199); //Amber
            
        }
        private static void LogToBoard(UOEntity myaxe)
        {
            var targethelper = TargetHelper.GetTarget();
            var logs = Scanner.Find<Item>((ushort)Logs, 0xFFFF, Stealth.Default.GetBackpackID(), true);
            foreach (var log in logs.Where(log => (myaxe.DoubleClick()) && (targethelper.WaitForTarget(2000))))
                Stealth.Default.TargetToObject(log.Serial.Value);
            
        }
        private static bool Checkweight()
        {
            var weight = Stealth.Default.GetSelfWeight();
            return weight >= Lumberjacker.maxweight;
        }

        public static void Unload(Item mycontainer)
        {
            Stealth.Default.Wait(1000);
            Setvariables();
            mycontainer.Use();
            var logs = Scanner.Find<Item>((ushort) Logs, 0xFFFF, Stealth.Default.GetBackpackID(), true);
            foreach (var log in logs)
            {
                Stealth.Default.MoveItem(log.Serial.Value, log.Amount, mycontainer.Serial.Value, 0, 0, 0);
                Stealth.Default.Wait(1000);

            }
            var board = Scanner.Find<Item>((ushort)Boards, 0xFFFF, Stealth.Default.GetBackpackID(), true);
            foreach (var log in board)
            {
                Stealth.Default.MoveItem(log.Serial.Value, log.Amount, mycontainer.Serial.Value, 0, 0, 0);
                Stealth.Default.Wait(1000);

            }
            foreach (var move in Extras.Select(extra => Scanner.Find<Item>(extra, 0xFFFF, Stealth.Default.GetBackpackID(), true)).SelectMany(moves => moves))
            {
                Stealth.Default.MoveItem(move.Serial.Value, move.Amount, mycontainer.Serial.Value, 0, 0, 0);
                Stealth.Default.Wait(1000);
            }
        }

        public static void Lumberjack(Serial axeserial, int distance)
        {
            var myaxe = new UOEntity(new Serial(axeserial.Value));
            TileReader.Initialize(); //Initialize the TileReader
            var trees = TileReader.GetLumberSpots(distance); //Search all Trees in Range of 1 Tile  
            var targethelper = TargetHelper.GetTarget(); // Assign the TargetHelper refeence
            foreach (var tree in trees) //iterate through all results
            {
                Stealth.Default.newMoveXY(tree.X, tree.Y, true, 1, true); // Move to Tree

                for (var i = 0; i < 25; i++) // Do 10 times or until weight full
                {
                    if (Lumberjacker.Endtime < DateTime.Now) Lumberjacker.backgroundWorker1.CancelAsync();
                    if (Lumberjacker.backgroundWorker1.CancellationPending) break;
                    if (Checkweight())
                    {
                        Lumberjacker.Gohomeandunload();
                        Stealth.Default.newMoveXY(tree.X, tree.Y, true, 1, true);
                    }
                    if ((myaxe.DoubleClick()) && (targethelper.WaitForTarget(2000)))
                        // try to doubleclick and wait until tárget cursor appear
                        targethelper.TargetTo(tree.Tile, new Point3D(tree.X, tree.Y, tree.Z)); //target the tree
                    Stealth.Default.Wait(1000); //wait 1 second
                    var journal = JournalHelper.GetJournal();
                    if (journal.LastMessage.Contains("not enough wood ") || journal.LastMessage.Contains("cannot be seen")) break;

                    if (!Checkweight()) continue;
                    LogToBoard(myaxe);
                }
                if (Lumberjacker.Endtime < DateTime.Now) Lumberjacker.backgroundWorker1.CancelAsync();
                if (Lumberjacker.backgroundWorker1.CancellationPending) break;
            }
        }

        public Serial GetAxe(PlayerMobile mobile)
        {
            var axe = mobile.Paperdoll.Shield;
            return axe.Serial;
        }
    }
}
