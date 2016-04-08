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
            var loc1 = PlayerMobile.GetPlayer().Location;// LOC before recall
            //runebookserial.DoubleClick(); // Open Runebook
            Stealth.Client.Wait(1000);
            if (osi)
            {
                OSIrb.Parse();
                if (recalltype == "Recall")
                {
                    OSIrb.Entries[bookspot - 1].Recall();
                }
                else
                {
                    OSIrb.Entries[bookspot - 1].Sacred();
                }
            }
            else
            {
                RUOrb.Parse();
                if (recalltype == "Recall")
                {
                    RUOrb.Entries[bookspot - 1].Recall();
                }
                else
                {
                    RUOrb.Entries[bookspot - 1].Sacred();
                }
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
