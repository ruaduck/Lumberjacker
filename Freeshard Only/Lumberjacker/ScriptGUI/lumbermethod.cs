using System;
using System.Collections.Generic;
using System.Linq;
using ScriptSDK;
using ScriptSDK.API;
using ScriptSDK.Attributes;
using ScriptSDK.Data;
using ScriptSDK.Engines;
using ScriptSDK.Items;
using ScriptSDK.Mobiles;

namespace ScriptGUI
{
    class Lumbermethod
    {
        public static int Reg;
        public static int Oak;
        public static int Ash;
        public static int Yew;
        public static int Hw;
        public static int Blood;
        public static string Journal;
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
            var logs = Scanner.Find<Item>((ushort)Logs, 0xFFFF, Stealth.Client.GetBackpackID(), true);
            foreach (var log in logs.Where(log => (myaxe.DoubleClick()) && (targethelper.WaitForTarget(2000))))
                Stealth.Client.TargetToObject(log.Serial.Value);
            
        }
        private static bool Checkweight()
        {
            var weight = Stealth.Client.GetSelfWeight();
            return weight >= Lumberjacker.Maxweight;
        }

        public static void Unload(Item mycontainer)
        {
            Stealth.Client.Wait(1000);
            Setvariables();
            mycontainer.DoubleClick();
            Stealth.Client.Wait(1000);
            var logs = Scanner.Find<Item>((ushort) Logs, 0xFFFF, Stealth.Client.GetBackpackID(), true);
            foreach (var log in logs)
            {
                Stealth.Client.MoveItem(log.Serial.Value, log.Amount, mycontainer.Serial.Value, 0, 0, 0);
                Stealth.Client.Wait(1000);
                switch (log.Color)
                {
                    case 0x7DA:
                        Oak += log.Amount;
                        break;
                    case 0x4A7:
                        Ash += log.Amount;
                        break;
                    case 0x4A8:
                        Yew += log.Amount;
                        break;
                    case 0x4A9:
                        Hw += log.Amount;
                        break;
                    case 0x4AA:
                        Blood += log.Amount;
                        break;
                    default:
                        Reg += log.Amount;
                        break;
                }

            }
            var board = Scanner.Find<Item>((ushort)Boards, 0xFFFF, Stealth.Client.GetBackpackID(), true);
            foreach (var log in board)
            {
                Stealth.Client.MoveItem(log.Serial.Value, log.Amount, mycontainer.Serial.Value, 0, 0, 0);
                Stealth.Client.Wait(1000);
                switch (log.Color)
                {
                    case 0x7DA:
                        Oak += log.Amount;
                        break;
                    case 0x4A7:
                        Ash += log.Amount;
                        break;
                    case 0x4A8:
                        Yew += log.Amount;
                        break;
                    case 0x4A9:
                        Hw += log.Amount;
                        break;
                    case 0x4AA:
                        Blood += log.Amount;
                        break;
                    default:
                        Reg += log.Amount;
                        break;
                }

            }
            foreach (var move in Extras.Select(extra => Scanner.Find<Item>(extra, 0xFFFF, Stealth.Client.GetBackpackID(), true)).SelectMany(moves => moves))
            {
                Stealth.Client.MoveItem(move.Serial.Value, move.Amount, mycontainer.Serial.Value, 0, 0, 0);
                Stealth.Client.Wait(1000);
            }
            Lumberjacker.backgroundWorker3.RunWorkerAsync();
        }

        public static void Lumberjack(Serial axeserial, int distance)
        {
            var myaxe = new UOEntity(new Serial(axeserial.Value));
            TileReader.Initialize(); //Initialize the TileReader
            var trees = TileReader.GetLumberSpots(distance); //Search all Trees in Range of 1 Tile  
            var targethelper = TargetHelper.GetTarget(); // Assign the TargetHelper refeence
            foreach (var tree in trees) //iterate through all results
            {
                Stealth.Client.newMoveXY(tree.X, tree.Y, true, 1, true); // Move to Tree
                for (var i = 0; i < 25; i++) // Do 25 times or until weight full
                {
                    if (Lumberjacker.Endtime < DateTime.Now) Lumberjacker.backgroundWorker1.CancelAsync();
                    if (Lumberjacker.backgroundWorker1.CancellationPending) break;
                    if (Checkweight())
                    {
                        Lumberjacker.Gohomeandunload();
                        Stealth.Client.newMoveXY(tree.X, tree.Y, true, 1, true);
                    }
                    if ((myaxe.DoubleClick()) && (targethelper.WaitForTarget(2000)))
                        // try to doubleclick and wait until tárget cursor appear
                        targethelper.TargetTo(tree.Tile, new Point3D(tree.X, tree.Y, tree.Z)); //target the tree
                    Stealth.Client.Wait(1100); //wait 1 second
                    if (!Lumberjacker.Speechhit) continue;
                    Lumberjacker.Speechhit = false;
                    break;
                    //Journal = JournalHelper.GetJournal().LastMessage;
                    //if (Journal.Contains("not enough wood ") || Journal.Contains("cannot be seen")) break;
                }
                LogToBoard(myaxe);
                if (Lumberjacker.Endtime < DateTime.Now) Lumberjacker.backgroundWorker1.CancelAsync();
                if (Lumberjacker.backgroundWorker1.CancellationPending) break;
            }
        }

        public Serial GetAxe(PlayerMobile mobile)
        {
            var axe = mobile.Paperdoll.TwoHanded;
            return axe.Serial;
        }
    }
}
