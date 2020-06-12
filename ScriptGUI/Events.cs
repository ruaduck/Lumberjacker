using StealthAPI;

namespace TLumberjack
{
    class LumberEvents
    {
        public static void OnClilocSpeech(object sender, ClilocSpeechEventArgs e)
        {

            if (e.Text.Contains("There's not enough wood here to harvest.")) Lumberjacker.Speechhit = true;
            if (e.Text.Contains("Thou art too encumbered to move.")) Lumberjacker.Encumbered = true;
            if (e.Text.Contains("You must wait to perform another action.")) Lumberjacker.Actionperform = true;
        }
        public static void Buffsystem(object sender, Buff_DebuffSystemEventArgs e)
        {
            //MessageBox.Show(String.Format("{0}",(Buffs)e.ObjectId));
        }
        public static void speech(object sender, SpeechEventArgs e)
        {
            //MessageBox.Show(e.Text);

        }
    }
}
