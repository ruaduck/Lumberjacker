using System;
using System.Linq;
using ScriptSDK.Gumps;
using StealthAPI;
using ScriptSDK.Items;
using ScriptSDK.Mobiles;

namespace TLumberjack
{
    class Travel
    {

        public static bool Recall(Item runebookserial, int bookspot, string recalltype, bool osi)
        {
            var RUOconfig = new RuneBookConfig()
            {
                ScrollOffset = 2,
                DropOffset = 3,
                DefaultOffset = 4,
                RecallOffset = 5,
                GateOffset = 6,
                SacredOffset = 7,
                Jumper = 6
            };
            var OSIconfig = new RuneBookConfig()
            {
                ScrollOffset = 10,
                DropOffset = 200,
                DefaultOffset = 300,
                RecallOffset = 50,
                GateOffset = 100,
                SacredOffset = 75,
                Jumper = 1
            };
            Runebook RUOrb = new Runebook(runebookserial.Serial.Value, RUOconfig);
            Runebook OSIrb = new Runebook(runebookserial.Serial.Value, OSIconfig, "OSI");
            Stealth.Client.AddToSystemJournal(string.Format("Recalling to spot {0} using {1}", bookspot, recalltype));
            //Gump runegump;
            runebookserial.DoubleClick();
            var loc1 = PlayerMobile.GetPlayer().Location;// LOC before recall
            //runebookserial.DoubleClick(); // Open Runebook
            Stealth.Client.Wait(1000);
            Gump g;
            if (osi)
            {
                g = Gump.GetGump(0x0059); //OSI
                if (g == null)
                {
                    Stealth.Client.AddToSystemJournal("Gump is Null");
                }
                else
                {
                    var ScrollOffset = 10;
                    var DropOffset = 200;
                    var DefaultOffset = 300;
                    var RecallOffset = 50;
                    var GateOffset = 100;
                    var SacredOffset = 75;
                    var Jumper = 1;

                    var any = false;
                    foreach (var e in g.Buttons)
                    {
                        if (!e.PacketValue.Equals(RecallOffset + bookspot-1) && !e.Graphic.Released.Equals(2103) &&
                            !e.Graphic.Pressed.Equals(2104)) continue;
                        any = true;
                        if (recalltype == "Recall")
                        {
                            Stealth.Client.AddToSystemJournal(String.Format("{0} is my packet value",RecallOffset + (bookspot-1)));
                            var recallButton = g.Buttons.First(i => i.PacketValue.Equals(RecallOffset + (bookspot-1)));
                            recallButton.Click();
                            break;
                        }
                    }
                }
                //OSIrb.Parse();
                //if (recalltype == "Recall")
                //{
                //    OSIrb.Entries[bookspot - 1].Recall();
                //}
                //else
                //{
                //    OSIrb.Entries[bookspot - 1].Sacred();
                //}
            }
            else
            {
                g = Gump.GetGump(0x554B87F3); //Freeshard
                if (g == null)
                {
                    Stealth.Client.AddToSystemJournal("Gump is Null");
                }
                else
                {
                    var ScrollOffset = 2;
                    var DropOffset = 3;
                    var DefaultOffset = 4;
                    var RecallOffset = 5;
                    var GateOffset = 6;
                    var SacredOffset = 7;
                    var Jumper = 6;
                    var any = false;
                    int go;
                    go = RecallOffset + ((bookspot - 1) * Jumper);
                    foreach (var e in g.Buttons)
                    {                       
                        if (!e.PacketValue.Equals(go) || !e.Graphic.Released.Equals(2103) ||
                            !e.Graphic.Pressed.Equals(2104)) continue;
                        any = true;
                        if (recalltype == "Recall")
                        {
                            var recallButton =
                                g.Buttons.First(i => i.PacketValue.Equals(go));
                            recallButton.Click();
                        }
                        else
                        {

                        }
                    }
                }
                //RUOrb.Parse();
                //if (recalltype == "Recall")
                //{
                //    RUOrb.Entries[bookspot - 1].Recall();
                //}
                //else
                //{
                //    RUOrb.Entries[bookspot - 1].Sacred();
                //}
            } 
            Stealth.Client.Wait(!osi ? 2000 : 3500);
            var loc2 = PlayerMobile.GetPlayer().Location; // LOC after recall
            return loc1 != loc2; // Compare Locs to see if you moved.
        }

        //private static Gump GetRunebookGump(uint gumpType, uint runebookSerial)
        //{
        //    if ((GumpHelper.GetGumpIndex(gumpType)) > -1)
        //    {
        //        GumpHelper.CloseGump(gumpType, false);
        //    }

        //    var runebook = new Item(new Serial(runebookSerial));
        //    runebook.DoubleClick();
        //    while ((GumpHelper.GetGumpIndex(gumpType)) < 0)
        //    {
        //        Thread.Sleep(50);
        //    }

        //    var g = new Gump(GumpHelper.GetGump(gumpType, false));
        //    return g;
        //}
    }
}
