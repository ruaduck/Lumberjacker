using System;
using System.Collections.Generic;
using System.Linq;
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
        public static List<ushort> Extras = new List<ushort>();
        public static List<ushort> Lumbers = new List<ushort>();
        public static UOEntity Theaxe;




        public static void Setvariables()
        {
            Extras.Add((ushort)LumberItems.Fungi); 
            Extras.Add((ushort)LumberItems.Bark); 
            Extras.Add((ushort)LumberItems.Switch); 
            Extras.Add((ushort)LumberItems.Plant); 
            Extras.Add((ushort)LumberItems.Amber);
            Lumbers.Add((ushort)Lumber.Boards);
            Lumbers.Add((ushort)Lumber.Logs);
        }
        private static void LogToBoard()
        {
            var player = PlayerMobile.GetPlayer();
            var logs = Scanner.Find<Item>((ushort)Lumber.Logs, 0xFFFF, Stealth.Client.GetBackpackID(), true);
            foreach (var log in logs)
            {
                Stealth.Client.AddToSystemJournal(String.Format("Converting {0} Logs to boards",(LumberColor)log.Color));
                player.Paperdoll.TwoHanded.DoubleClick();
                var target = new EntityTarget(1000);
                target.Action(log);               
            }
            
        }
        private static bool Checkweight()
        {
            var max = Stealth.Client.GetSelfMaxWeight();
            var weight = Stealth.Client.GetSelfWeight();
            if (weight < (max - Lumberjacker.MaxWeight)) return false;
            LogToBoard();
            Stealth.Client.Wait(1000);
            weight = Stealth.Client.GetSelfWeight();
            return weight >= (max - Lumberjacker.MaxWeight);
        }

        private static void LumberCount (Item log)
        {
            var color = Stealth.Client.GetColor(log.Serial.Value);
            switch (color)
            {
                case (ushort)LumberColor.Reg:
                    Reg += log.Amount;
                    break;
                case (ushort)LumberColor.Oak:
                    Oak += log.Amount;
                    break;
                case (ushort)LumberColor.Ash:
                    Ash += log.Amount;
                    break;
                case (ushort)LumberColor.Yew:
                    Yew += log.Amount;
                    break;
                case (ushort)LumberColor.HeartWood:
                    Hw += log.Amount;
                    break;
                case (ushort)LumberColor.Bloodwood:
                    Blood += log.Amount;
                    break;
                case (ushort)LumberColor.Frostwood:
                    Frost += log.Amount;
                    break;
            }
            Stealth.Client.AddToSystemJournal(string.Format("Dropping off {0} {1} lumber",log.Amount, (LumberColor)color));
        }
        private static void LumberItemCount(Item item)
        {
            switch (item.ObjectType)
            {
                case (ushort)LumberItems.Fungi:
                    Fungi += item.Amount;
                    break;
                case (ushort)LumberItems.Bark: //Bark
                    Bark += item.Amount;
                    break;
                case (ushort)LumberItems.Switch: //Switch
                    Switch += item.Amount;
                    break;
                case (ushort)LumberItems.Plant: //Plant
                    Plant += item.Amount;
                    break;
                case (ushort)LumberItems.Amber: //Amber
                    Amber += item.Amount;
                    break;
            }
            Stealth.Client.AddToSystemJournal(string.Format("Dropping off {0} {1}.", item.Amount, (LumberItems)item.ObjectType));
        }
        private static void MoveLogs(uint container)
        {
            var logs = Scanner.Find<Item>((ushort)Lumber.Logs, 0xFFFF, Stealth.Client.GetBackpackID(), true);
            foreach (var log in logs)
            {
                LumberCount(log);
                Stealth.Client.MoveItem(log.Serial.Value, log.Amount, container, 0, 0, 0);
                Stealth.Client.Wait(1000);
                while (Lumberjacker.Actionperform)
                    Moveagain(log, container);
            }
        }
        private static void MoveBoards(uint container)
        {
            var board = Scanner.Find<Item>((ushort)Lumber.Boards, 0xFFFF, Stealth.Client.GetBackpackID(), true);
            foreach (var log in board)
            {
                LumberCount(log);
                Stealth.Client.MoveItem(log.Serial.Value, log.Amount, container, 0, 0, 0);
                Stealth.Client.Wait(1000);
                while (Lumberjacker.Actionperform)
                    Moveagain(log, container);
            }
        }
        private static void MoveItems(List<ushort> Items, uint tocontainer, uint fromcontainer, bool countitems)
        {
            foreach (var item in Items.Select(extra => ushort.Parse(extra.ToString())).Select(move => Scanner.Find<Item>(move, 0xFFFF, fromcontainer, true)).SelectMany(item => item))
            {
                if (countitems)
                {
                    if (Enum.IsDefined(typeof(LumberItems), (int)item.ObjectType))
                    {
                        LumberItemCount(item);
                    }
                    else LumberCount(item);
                }
                Stealth.Client.MoveItem(item.Serial.Value, item.Amount, tocontainer, 0, 0, 0);
                Stealth.Client.Wait(1000);
                while (Lumberjacker.Actionperform)
                    Moveagain(item, tocontainer);
            }
        }
        public static void Unload(Item mycontainer)
        {
            Stealth.Client.Wait(1000);
            Setvariables();
            Stealth.Client.newMoveXY((ushort)mycontainer.Location.X, (ushort)mycontainer.Location.Y, true, 1, true);

            //MoveLogs(mycontainer);
            //MoveBoards(mycontainer);
            MoveItems(Extras, mycontainer.Serial.Value, Stealth.Client.GetBackpackID(),true);
            MoveItems(Lumbers, mycontainer.Serial.Value, Stealth.Client.GetBackpackID(),true);
            if (Lumberjacker.Beetle)
            {
                PlayerMobile.GetPlayer().DoubleClick();
                Lumberjacker.BeetleContainer.DoubleClick();
                MoveItems(Extras, mycontainer.Serial.Value,Lumberjacker.BeetleContainer.Serial.Value,true);
                MoveItems(Lumbers, mycontainer.Serial.Value, Lumberjacker.BeetleContainer.Serial.Value,true);
                Lumberjacker.BlueBeetle.DoubleClick();
            }

            if (Lumberjacker.backgroundWorker3 != null)
            {
                Lumberjacker.backgroundWorker3.RunWorkerAsync();
            }
        }

        private static void Moveagain(Item item, uint container)
        {
            Lumberjacker.Actionperform = false;
            Stealth.Client.MoveItem(item.Serial.Value, item.Amount, container, 0, 0, 0);
            Stealth.Client.Wait(1100);
        }
        public static void Lumberjack(Serial axeserial, int distance)
        {
            Setvariables();
            var myaxe = new UOEntity(new Serial(axeserial.Value));
            var trees = TileReader.GetLumberSpots(distance); //Search all Trees in Range of *Distance* Tiles
            foreach (var tree in trees) //iterate through all results
            {
                Chop(myaxe, tree);
                if (Lumberjacker.Endtime < DateTime.Now) Lumberjacker.backgroundWorker1.CancelAsync();
                if (Lumberjacker.backgroundWorker1.CancellationPending) break;
            }
        }

        public Serial GetAxe(PlayerMobile mobile)
        {
            Theaxe = mobile.Paperdoll.TwoHanded;
            return Theaxe.Serial;
        }
        private static void Chop(UOEntity axe, StaticItemRealXY tree)
        {
            Stealth.Client.newMoveXY(tree.X, tree.Y, true, 1, true); // Move to Tree
            for (var i = 0; i < 25; i++) // Do 25 times or until weight full
            {
                if (Lumberjacker.Endtime < DateTime.Now) Lumberjacker.backgroundWorker1.CancelAsync();
                if (Lumberjacker.backgroundWorker1.CancellationPending) break;
                if (Checkweight())
                {
                    if (Lumberjacker.Beetle)
                    {
                        PlayerMobile.GetPlayer().DoubleClick();
                        Lumberjacker.BeetleContainer.DoubleClick();
                        Stealth.Client.Wait(1100); //wait 1 second
                        MoveItems(Extras, Lumberjacker.BeetleContainer.Serial.Value, Stealth.Client.GetBackpackID(), false);
                        MoveItems(Lumbers, Lumberjacker.BeetleContainer.Serial.Value, Stealth.Client.GetBackpackID(), false);
                        Lumberjacker.BlueBeetle.DoubleClick();
                        Stealth.Client.Wait(1100); //wait 1 second
                        if (Checkweight())
                        {
                            Lumberjacker.Gohomeandunload();
                            Stealth.Client.newMoveXY(tree.X, tree.Y, true, 1, true);
                        }
                    }
                    else
                    {
                        Lumberjacker.Gohomeandunload();
                        Stealth.Client.newMoveXY(tree.X, tree.Y, true, 1, true);
                    }
                }
                if ((axe.DoubleClick()) && (TargetHelper.GetTarget().WaitForTarget(2000)))
                    // try to doubleclick and wait until tárget cursor appear
                    TargetHelper.GetTarget().TargetTo(tree.Tile, new Point3D(tree.X, tree.Y, tree.Z)); //target the tree
                Stealth.Client.Wait(1100); //wait 1 second
                if (!Lumberjacker.Speechhit) continue;
                Lumberjacker.Speechhit = false;
                break;
            }
        }
    }
    
}
