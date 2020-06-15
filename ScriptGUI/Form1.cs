﻿//////////////////////////////////////////////////////////////////////////
// Script Name:  Duck Duck's Lumberjacker                               //
// Verision : 1.0a                                                      //
// Versiion Date: 9/7/15                                                //
// Release Date:                                                        //
// Special Thanks:  Crome for examples and ScriptSDK; Unisharp for      //
//                  journal scanning                                    //
// Authorized Release Locations:  www.scriptuo.com ; www.rebirthuo.com  //
//////////////////////////////////////////////////////////////////////////

using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using TLumberjack.Properties;
using StealthAPI;
using ScriptSDK;
using ScriptSDK.Engines;
using ScriptSDK.Items;
using ScriptSDK.Mobiles;
//using ScriptDotNet2;
//using ScriptDotNet2.Services;

namespace TLumberjack
{
    public partial class Lumberjacker : Form
    {
        public static string Method = "Not Set";
        public static Item Housecontainer;
        public Serial AxeSerial;
        public static DateTime Endtime;
        public static DateTime Starttime;
        public static int Minutes; //Minutes to Run
        public static int Homerune; // Rune Home
        public static int Bankrune;  //  Bank Rune
        public static int Firstrune; // First Lumberjacking Rune
        public static int Lastrune; // Last Lumberjacking Rune
        public static bool Osi; // OSI = True; RebirthUO = False;
        public static int Runetouse; //current rune on recall
        public static Item Runebook;
        public static int Treearea; //  Area to look for tree
        public static bool Speechhit;
        public static bool Encumbered;
        public static bool Actionperform;
        public static bool Loadused;
        public static bool Beetle;
        public static Mobile BlueBeetle;
        public static ScriptSDK.Items.Container BeetleContainer;

        public static int MaxWeight
        {
            get { return Convert.ToInt32(maxweighttbox.Text); }
        }


        public Lumberjacker() 
        {

            if (UacHelper.IsProcessElevated)
            {
                InitializeComponent();
                #region Event Listeners
                Stealth.Client.Speech += LumberEvents.speech;
                Stealth.Client.ClilocSpeech += LumberEvents.OnClilocSpeech;
                Stealth.Client.Buff_DebuffSystem += LumberEvents.Buffsystem;
                //ScriptDotNet2.Stealth.Default.GetService<IEventSystemService>().ClilocSpeech += OnClilocSpeech;
                #endregion
                #region Buttons
                maxweighttbox.Text = Convert.ToString(30);
                cancelbutton.Enabled = false;
                lumberjackbutton.Enabled = false;
                loadbutton.Enabled = true;
                savebutton.Enabled = false;
                startsetup.Enabled = false;
                #endregion
                #region Use Beetle?
                var BeetleDialogResult =
                MessageBox.Show("Do you have a Blue Beetle?",
                    @"Beetle Method", MessageBoxButtons.YesNo);
                switch (BeetleDialogResult)
                {
                    case DialogResult.Yes:
                        Beetle = true;
                        BeetleSetup();
                        break;
                    case DialogResult.No:
                        Beetle = false;
                        break;
                }
                #endregion
            }
            else
            {
                MessageBox.Show("Please run as administrator.");
                Environment.Exit(1);
            }
        }
        public bool SetInputs()
        {
            Invoke((MethodInvoker)
                delegate { Osi = comboBox1.Text == @"OSI"; });
            if (endtimebox == null || homerunebox == null || bankrunebox == null || firstrunebox == null ||
                lastrunebox == null)
            {
                return false;
            }
            if (!int.TryParse(endtimebox.Text, out Minutes)) return false;
            if (!int.TryParse(endtimebox.Text, out Minutes)) return false;
            if (!int.TryParse(homerunebox.Text, out Homerune)) return false;
            if (!int.TryParse(bankrunebox.Text, out Bankrune)) return false;
            if (!int.TryParse(treeareatbox.Text, out Treearea)) return false;
            return int.TryParse(firstrunebox.Text, out Firstrune) && int.TryParse(lastrunebox.Text, out Lastrune);
        }
        public void Setup()
        {
            if (!backgroundWorker2.IsBusy)backgroundWorker2.RunWorkerAsync();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (SetInputs())
            {
                Starttime = DateTime.Now;
                Endtime = DateTime.Now.AddMinutes((Minutes));
                Start();
                cancelbutton.Enabled = true;
                lumberjackbutton.Enabled = false;
                loadbutton.Enabled = false;
                savebutton.Enabled = false;
            }
            else MessageBox.Show(@"You missed some required fields or didn't enter in digits in those fields");
        }

        private static void Start()
        {
            TileReader.Initialize(); //Initialize the TileReader
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        public static void Gohomeandunload()
        {

            while (!GoHome())
            {
                Thread.Sleep(50);
            }

            Lumbermethod.Unload(Housecontainer);
            Travel.Recall(Runebook, Runetouse, Method, Osi);
        }

        private static bool GoHome()
        {
            return Travel.Recall(Runebook, Homerune, Method, Osi);
        }

        #region Setups
        private Serial AxeSetup()
        {
            Serial axe = null;
            var main = new Lumbermethod();
            #region Get Axe Info
            DialogResult axedialogResult = MessageBox.Show(@"Do you have your Axe Equipped?", @"Getting Axe ID", MessageBoxButtons.YesNo);
            switch (axedialogResult)
            {
                case DialogResult.Yes:
                    axe = main.GetAxe(PlayerMobile.GetPlayer());
                    Invoke((MethodInvoker)
                        delegate { axetextbox.Text = axe.ToString(); });
                    break;
                case DialogResult.No:
                    MessageBox.Show(@"Equip your Axe and start script again.");
                    Close();
                    break;
            }
            return axe;

            #endregion
        }
        public static void ContainerSetup()
        {
            MessageBox.Show(@"Select your Container");
            Housecontainer = new Item(new Serial(Getitem()));
            Housecontainer.DoubleClick();
        }
        private void BeetleSetup()
        {
            PlayerMobile.GetPlayer().DoubleClick();
            MessageBox.Show(@"Lets setup your Beetle");
            BlueBeetle = new Mobile(new Serial(Getitem()));
            BeetleContainer = BlueBeetle.Paperdoll.Backpack;
            BeetleContainer.DoubleClick();
            BlueBeetle.DoubleClick();

        }
        private void RunebookSetup()
        {
            MessageBox.Show(@"Select your Runebook");
            Runebook = new Item(new Serial(Getitem()));
            #region Get Runebook Info
            Invoke((MethodInvoker)
                        delegate { Runebooktbox.Text = Runebook.Serial.Value.ToString(); });
            #endregion
        }
        private static uint Getitem()
        {
            Stealth.Client.ClientRequestObjectTarget();
            while (!Stealth.Client.ClientTargetResponsePresent())
            {
                Thread.Sleep(50);
            }
            var myitem = Stealth.Client.ClientTargetResponse().ID;
            return myitem;
        }
        private static void RecallSetup()
        {
            #region Recall Method
            var recallDialogResult =
                MessageBox.Show(Resources.Lumberjacker_RecallSetup_,
                    @"Recall Method", MessageBoxButtons.YesNo);
            switch (recallDialogResult)
            {
                case DialogResult.Yes:
                    Method = "Recall";
                    break;
                case DialogResult.No:
                    Method = "Sacred Journey";
                    break;
            }
            #endregion
            
        }

        #endregion
        private void startsetup_Click(object sender, EventArgs e)
        {
            startsetup.Enabled = false;
            Setup();    
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
           Lumberjackloop();
        }

        private void Lumberjackloop()
        {
            while (Endtime > DateTime.Now)
            {

                for (Runetouse = Firstrune; Runetouse < Lastrune + 1; Runetouse++)
                {
                    Invoke((MethodInvoker)delegate { statustext.Text = string.Format("Recalling to spot {0}", Runetouse); });
                    if (!Travel.Recall(Runebook, Runetouse, Method, Osi))
                    {
                        Invoke((MethodInvoker)
                        delegate
                        {
                            recallstatus.Text = Method + @" Failed. Trying Next Rune";
                        });
                            continue;
                        
                    }
                    Lumbermethod.Lumberjack(AxeSerial, Treearea);
                    if (Endtime < DateTime.Now) backgroundWorker1.CancelAsync();
                    if (backgroundWorker1.CancellationPending) break;
                    Invoke((MethodInvoker)
                        delegate { statustext.Text = @"getting next rune"; });
                }
                if (backgroundWorker1.CancellationPending) break;
            }
            while (!GoHome())
            {
                Thread.Sleep(50);
            }
            Lumbermethod.Unload(Housecontainer);
            MessageBox.Show(string.Format("We have ran for {0} minutes. Thank you! ", endtimebox.Text));
            Invoke((MethodInvoker)
                delegate {
                    savebutton.Enabled = true;
                    lumberjackbutton.Enabled = true; });
            Clearcounts();
        }

        private static void Clearcounts()
        {
            Lumbermethod.Oak = 0;
            Lumbermethod.Ash = 0;
            Lumbermethod.Yew = 0;
            Lumbermethod.Hw = 0;
            Lumbermethod.Blood = 0;
            Lumbermethod.Reg = 0;
            Lumbermethod.Frost = 0;
            Lumbermethod.Amber = 0;
            Lumbermethod.Fungi = 0;
            Lumbermethod.Bark = 0;
            Lumbermethod.Switch = 0;
            Lumbermethod.Plant = 0;
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            if (SetInputs())
            {
                
                RecallSetup();
                RunebookSetup();
                AxeSerial = AxeSetup();
                Invoke((MethodInvoker)
                    delegate
                    {
                        recallstatus.Text = Travel.Recall(Runebook, Homerune, Method, Osi)
                            ? "Recalled"
                            : "Recall Failed";
                    });
                ContainerSetup();
                Lumbermethod.Unload(Housecontainer);
                Invoke((MethodInvoker) delegate
                {
                    Text = PlayerMobile.GetPlayer().Name + @" - Lumberjacker";
                    lumberjackbutton.Enabled = true;
                    savebutton.Enabled = true;
                });
            }

            else
            {
                Invoke((MethodInvoker)delegate
                {
                    startsetup.Enabled = true;
                });
                MessageBox.Show(@"You missed some required fields or didn't enter in digits in those fields");
            }
        }

        

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            cancelbutton.Enabled = false;
            lumberjackbutton.Enabled = true;
        }

        private static int Avgperhour()
        {
            var count = Lumbermethod.Oak + Lumbermethod.Ash + Lumbermethod.Yew + Lumbermethod.Hw + Lumbermethod.Blood +
                        Lumbermethod.Frost + Lumbermethod.Reg;
            var timespan = DateTime.Now.Subtract ( Starttime );
            var span = (int)timespan.TotalMinutes;
            if (span < 1) span = 1;
            var avg = (count / span) * 60;

            return avg;
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {

            if (InvokeRequired)
            {
                Invoke((MethodInvoker) delegate
                {
                    oakbox.Text = Lumbermethod.Oak.ToString();
                    ashbox.Text = Lumbermethod.Ash.ToString();
                    yewbox.Text = Lumbermethod.Yew.ToString();
                    hwbox.Text = Lumbermethod.Hw.ToString();
                    bloodbox.Text = Lumbermethod.Blood.ToString();
                    regbox.Text = Lumbermethod.Reg.ToString();
                    frostbox.Text = Lumbermethod.Frost.ToString();
                    amberbox.Text = Lumbermethod.Amber.ToString();
                    fungibox.Text = Lumbermethod.Fungi.ToString();
                    barkbox.Text = Lumbermethod.Bark.ToString();
                    switchbox.Text = Lumbermethod.Switch.ToString();
                    plantbox.Text = Lumbermethod.Plant.ToString();
                    avghr.Text = Avgperhour().ToString();
                });
             
            }
            else
            {
                oakbox.Text = Lumbermethod.Oak.ToString();
                ashbox.Text = Lumbermethod.Ash.ToString();
                yewbox.Text = Lumbermethod.Yew.ToString();
                hwbox.Text = Lumbermethod.Hw.ToString();
                bloodbox.Text = Lumbermethod.Blood.ToString();
                regbox.Text = Lumbermethod.Reg.ToString();
                frostbox.Text = Lumbermethod.Frost.ToString();
                amberbox.Text = Lumbermethod.Amber.ToString();
                fungibox.Text = Lumbermethod.Fungi.ToString();
                barkbox.Text = Lumbermethod.Bark.ToString();
                switchbox.Text = Lumbermethod.Switch.ToString();
                plantbox.Text = Lumbermethod.Plant.ToString();
                avghr.Text = Avgperhour().ToString();
            }
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
                    FileStream file = new FileStream(string.Format("{0}.txt", PlayerMobile.GetPlayer().Name),
                        FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(file);
                    sw.WriteLine(endtimebox.Text);
                    sw.WriteLine(homerunebox.Text);
                    sw.WriteLine(bankrunebox.Text);
                    sw.WriteLine(firstrunebox.Text);
                    sw.WriteLine(lastrunebox.Text);
                    sw.WriteLine(treeareatbox.Text);
                    sw.WriteLine(comboBox1.Text);
                    sw.WriteLine(Runebook.Serial);
                    sw.WriteLine(axetextbox.Text);
                    sw.WriteLine(Housecontainer.Serial);
                    sw.WriteLine(Method);
                    sw.Close();
            Stealth.Client.AddToSystemJournal("Save file written");
            loadbutton.Enabled = false;
        }

        private void loadbutton_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker) delegate
                {
                    FileStream file = new FileStream(string.Format("{0}.txt", PlayerMobile.GetPlayer().Name),
                        FileMode.OpenOrCreate, FileAccess.Read);
                    StreamReader sr = new StreamReader(file);
                    endtimebox.Text = sr.ReadLine();
                    homerunebox.Text = sr.ReadLine();
                    bankrunebox.Text = sr.ReadLine();
                    firstrunebox.Text = sr.ReadLine();
                    lastrunebox.Text = sr.ReadLine();
                    treeareatbox.Text = sr.ReadLine();
                    comboBox1.Text = sr.ReadLine();
                    var runebook = sr.ReadLine();
                    var axeSerial = sr.ReadLine();
                    var housecontainer = sr.ReadLine();
                    sr.Close();
                    if (runebook != null) Runebook = new Item(uint.Parse(runebook));
                    if (axeSerial != null) Lumbermethod.Theaxe = new UOEntity(uint.Parse(axeSerial));
                    if (housecontainer != null) Housecontainer = new Item(uint.Parse(housecontainer));
                    AxeSerial = Lumbermethod.Theaxe.Serial;
                    Runebooktbox.Text = Runebook.Serial.ToString();
                    axetextbox.Text = AxeSerial.ToString();
                });
            }
            else
            {
                var tempname = PlayerMobile.GetPlayer().Name;
                FileStream file = new FileStream(string.Format("{0}.txt", tempname),
                FileMode.OpenOrCreate, FileAccess.Read);
                using (var sr = new StreamReader(file))
                {
                    endtimebox.Text = sr.ReadLine();
                    homerunebox.Text = sr.ReadLine();
                    bankrunebox.Text = sr.ReadLine();
                    firstrunebox.Text = sr.ReadLine();
                    lastrunebox.Text = sr.ReadLine();
                    treeareatbox.Text = sr.ReadLine();
                    comboBox1.Text = sr.ReadLine();
                    Runebooktbox.Text = sr.ReadLine().ToString();
                    axetextbox.Text = sr.ReadLine().ToString();
                    var housecontainer = sr.ReadLine().ToString();
                    Method = sr.ReadLine().ToString();
                    if (axetextbox.Text != null) AxeSerial = new Item(Convert.ToUInt32(axetextbox.Text,16)).Serial;
                    if (housecontainer != null) Housecontainer = new Item(Convert.ToUInt32(housecontainer,16));
                    if (Runebooktbox.Text != null) Runebook = new Item(Convert.ToUInt32(Runebooktbox.Text,16));
               
                }
            }
            lumberjackbutton.Enabled = true;
            startsetup.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)
                delegate { Osi = comboBox1.Text == @"OSI"; });
        }

        private void endtimebox_TextChanged(object sender, EventArgs e)
        {
            startsetup.Enabled = true;
        }

    }

}
