using System;
using System.Linq;
using ScriptSDK.Gumps;
using StealthAPI;
using ScriptSDK.Items;
using ScriptSDK.Mobiles;
using System.Threading;

namespace TLumberjack
{
    class Travel
    {

        public static bool Recall(int bookspot, string recalltype, bool osi)
        {
            
            Stealth.Client.AddToSystemJournal(string.Format("Recalling to spot {0} using {1}", bookspot, recalltype));
            var loc1 = PlayerMobile.GetPlayer().Location;// LOC before recall
            Stealth.Client.Wait(1000);
            while (!Lumberjacker.Runebook.Entries[bookspot - 1].Recall()) Thread.Sleep(50);               
            Stealth.Client.Wait(!osi ? 2000 : 3500);
            return loc1 != PlayerMobile.GetPlayer().Location; // Compare Locs to see if you moved.
        }
    }
}
