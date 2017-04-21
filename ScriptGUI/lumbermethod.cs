using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ScriptSDK;
using StealthAPI;
using ScriptSDK.Attributes;
using ScriptSDK.Data;
using ScriptSDK.Engines;
using ScriptSDK.Items;
using ScriptSDK.Mobiles;
using ScriptSDK.Targets;

namespace TLumberjack
{
    class Lumbermethod
    {
        public static int Reg;
        public static int Oak;
        public static int Ash;
        public static int Yew;
        public static int Hw;
        public static int Blood;
        public static int Frost;
        public static int Amber;
        public static int Bark;
        public static int Switch;
        public static int Plant;
        public static int Fungi;
        public static uint Boards = 0x1BD7;
        public static uint Logs = 0x1BDD;
        public static List<ushort> Extras = new List<ushort>();
        public static UOEntity Theaxe;
        public static int weight;
        public static void Setvariables()
        {
            Extras.Add(0x3191); //Fungi
            Extras.Add(0x318f); //Bark
            Extras.Add(0x2F5F); //Switch
            Extras.Add(0x3190); //Plant
            Extras.Add(0x3199); //Amber
            
        }
        private static void LogToBoard()
        {
            Stealth.Client.AddToSystemJournal("Converting Logs to boards");
            var logs = Scanner.Find<Item>((ushort)Logs, 0xFFFF, Stealth.Client.GetBackpackID(), true);
            foreach (var log in logs)
            {
                var player = PlayerMobile.GetPlayer();
                player.Paperdoll.TwoHanded.DoubleClick();
                var target = new EntityTarget(1000);
                target.Action(log);               
            }
            
        }
        private static bool Checkweight()
        {

            weight = Stealth.Client.GetSelfWeight();
            if (weight < Lumberjacker.Maxweight) return weight >= Lumberjacker.Maxweight;
            LogToBoard();
            Stealth.Client.Wait(1000);
            weight = Stealth.Client.GetSelfWeight();
            return weight >= Lumberjacker.Maxweight;
        }

        public static void Unload(Item mycontainer)
        {
            Stealth.Client.Wait(1000);
            Setvariables();
            Stealth.Client.newMoveXY((ushort)mycontainer.Location.X, (ushort)mycontainer.Location.Y, true, 1, true);
            var logs = Scanner.Find<Item>((ushort) Logs, 0xFFFF, Stealth.Client.GetBackpackID(), true);
            foreach (var log in logs)
            {
                var color = Stealth.Client.GetColor(log.Serial.Value);
                switch (color)
                {
                    case 0x0:
                        Reg += log.Amount;
                        Stealth.Client.AddToSystemJournal("Dropping off Regular Logs");
                        break;
                    case 0x7DA:
                        Oak += log.Amount;
                        Stealth.Client.AddToSystemJournal("Dropping off Oak Logs");
                        break;
                    case 0x4A7:
                        Ash += log.Amount;
                        Stealth.Client.AddToSystemJournal("Dropping off Ash Logs");
                        break;
                    case 0x4A8:
                        Yew += log.Amount;
                        Stealth.Client.AddToSystemJournal("Dropping off Yew Logs");
                        break;
                    case 0x4A9:
                        Hw += log.Amount;
                        Stealth.Client.AddToSystemJournal("Dropping off Heartwood Logs");
                        break;
                    case 0x4AA:
                        Blood += log.Amount;
                        Stealth.Client.AddToSystemJournal("Dropping off Bloodwood Logs");
                        break;
                    case 0x047F:
                        Frost += log.Amount;
                        Stealth.Client.AddToSystemJournal("Dropping off Frostwood Logs");
                        break;
                }
                Stealth.Client.MoveItem(log.Serial.Value, log.Amount, mycontainer.Serial.Value, 0, 0, 0);
                Stealth.Client.Wait(1000);
                while (Lumberjacker.Actionperform)
                    moveagain(log, mycontainer);
            }
            var board = Scanner.Find<Item>((ushort)Boards, 0xFFFF, Stealth.Client.GetBackpackID(), true);
            foreach (var log in board)
            {

                switch ((uint)log.Color)
                {
                    case 0x0:
                        Reg += log.Amount;
                        Stealth.Client.AddToSystemJournal("Dropping off Regular Boards");
                        break;
                    case 0x7DA:
                        Oak += log.Amount;
                        Stealth.Client.AddToSystemJournal("Dropping off Oak Boards");
                        break;
                    case 0x4A7:
                        Ash += log.Amount;
                        Stealth.Client.AddToSystemJournal("Dropping off Ash Boards");
                        break;
                    case 0x4A8:
                        Yew += log.Amount;
                        Stealth.Client.AddToSystemJournal("Dropping off Yew Boards");
                        break;
                    case 0x4A9:
                        Hw += log.Amount;
                        Stealth.Client.AddToSystemJournal("Dropping off Heartwood Boards");
                        break;
                    case 0x4AA:
                        Blood += log.Amount;
                        Stealth.Client.AddToSystemJournal("Dropping off Bloodwood Boards");
                        break;
                    case 0x047F:
                        Frost += log.Amount;
                        Stealth.Client.AddToSystemJournal("Dropping off Frostwood Boards");
                        break;
                }
                Stealth.Client.MoveItem(log.Serial.Value, log.Amount, mycontainer.Serial.Value, 0, 0, 0);
                Stealth.Client.Wait(1000);
                while (Lumberjacker.Actionperform)
                    moveagain(log, mycontainer);
            }
            foreach (var item1 in Extras.Select(extra => ushort.Parse(extra.ToString())).Select(move => Scanner.Find<Item>(move, 0xFFFF, Stealth.Client.GetBackpackID(), true)).SelectMany(item => item))
            {
                switch (item1.ObjectType)
                {
                    case 0x3191:
                        Fungi += item1.Amount;
                        Stealth.Client.AddToSystemJournal("Dropping off Luminscent Fungi");
                        break;
                    case 0x318f: //Bark
                        Bark += item1.Amount;
                        Stealth.Client.AddToSystemJournal("Dropping off Bark");
                        break;
                    case 0x2F5F: //Switch
                        Switch += item1.Amount;
                        Stealth.Client.AddToSystemJournal("Dropping off Switch");
                        break;
                    case 0x3190: //Plant
                        Plant += item1.Amount;
                        Stealth.Client.AddToSystemJournal("Dropping off Parasitic Plant");
                        break;
                    case 0x3199: //Amber
                        Amber += item1.Amount;
                        Stealth.Client.AddToSystemJournal("Dropping off Amber");
                        break;
                }
                Stealth.Client.MoveItem(item1.Serial.Value, item1.Amount, mycontainer.Serial.Value, 0, 0, 0);
                Stealth.Client.Wait(1000);
                while (Lumberjacker.Actionperform)
                    moveagain(item1, mycontainer);
            }
            if (Lumberjacker.backgroundWorker3 != null) Lumberjacker.backgroundWorker3.RunWorkerAsync();
        }

        public static void moveagain(Item item, Item container)
        {
            Lumberjacker.Actionperform = false;
            Stealth.Client.MoveItem(item.Serial.Value, item.Amount, container.Serial.Value, 0, 0, 0);
            Stealth.Client.Wait(1100);
        }
        public static void Lumberjack(Serial axeserial, int distance)
        {
            var myaxe = new UOEntity(new Serial(axeserial.Value));
            var trees = TileReader.GetLumberSpots(distance); //Search all Trees in Range of *Distance* Tiles
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
                }
                if (Lumberjacker.Endtime < DateTime.Now) Lumberjacker.backgroundWorker1.CancelAsync();
                if (Lumberjacker.backgroundWorker1.CancellationPending) break;
            }
        }

        public Serial GetAxe(PlayerMobile mobile)
        {
            Theaxe = mobile.Paperdoll.TwoHanded;
            return Theaxe.Serial;
        }
    }
    
}
