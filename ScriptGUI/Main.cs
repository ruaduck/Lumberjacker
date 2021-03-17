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


namespace TLumberjack
{
    public partial class Lumberjacker : Form
    {
        public static string Method = "Not Set";
        public static Item Housecontainer;
        public Serial AxeSerial;
        public static DateTime Endtime;
        public static DateTime Starttime;
        public static bool Osi; // OSI = True; RebirthUO = False;
        public static int Runetouse; //current rune on recall
        public static Runebook Runebook;
        public static Item bankstorage;
        public static bool Speechhit;
        public static bool Encumbered;
        public static bool Actionperform;
        public static bool Loadused;
        public static bool Beetle;
        public static bool BankCrystal;
        public static Mobile BlueBeetle;
        public static ScriptSDK.Items.Container BeetleContainer;
        public static int Minutes { get; set; }       
        public static int Homerune { get; set; }        
        public static int Bankrune { get; set; }        
        public static int Firstrune { get; set; }        
        public static int Lastrune { get; set; }        
        public static int MaxWeight { get; set; }        
        public static int Treearea { get; set; }
        public static BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        public static BackgroundWorker backgroundWorker3 = new BackgroundWorker();
        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork +=
                new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker3.DoWork +=
                 new DoWorkEventHandler(backgroundWorker3_DoWork);
        }
        public Lumberjacker()
        {

            if (UacHelper.IsProcessElevated)
            {
                InitializeComponent();
                InitializeBackgroundWorker();
                #region Event Listeners
                Stealth.Client.Speech += LumberEvents.speech;
                Stealth.Client.ClilocSpeech += LumberEvents.OnClilocSpeech;
                Stealth.Client.Buff_DebuffSystem += LumberEvents.Buffsystem;
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
                #region Use Bank Crystal?
                var BankCrystalDialogResult =
                MessageBox.Show("Do you have a Bank Crystal?",
                    @"Bank Crystal Method", MessageBoxButtons.YesNo);
                switch (BankCrystalDialogResult)
                {
                    case DialogResult.Yes:
                        BankCrystal = true;
                        Stealth.Client.SendTextToUO("Bank");
                        BankContainer();
                        break;
                    case DialogResult.No:
                        BankCrystal = false;
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
                lastrunebox == null) { return false; }
            return true;
        }
        public void Setup()
        {
            Minutes = Convert.ToInt32(endtimebox.Text);
            Homerune = Convert.ToInt32(homerunebox.Text);
            Bankrune = Convert.ToInt32(bankrunebox.Text);
            Firstrune = Convert.ToInt32(firstrunebox.Text);
            Lastrune = Convert.ToInt32(lastrunebox.Text);
            MaxWeight = Convert.ToInt32(maxweighttbox.Text);
            Treearea = Convert.ToInt32(treeareatbox.Text);
            if (!backgroundWorker2.IsBusy) backgroundWorker2.RunWorkerAsync();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (SetInputs())
            {
                Minutes = Convert.ToInt32(endtimebox.Text);
                Homerune = Convert.ToInt32(homerunebox.Text);
                Bankrune = Convert.ToInt32(bankrunebox.Text);
                Firstrune = Convert.ToInt32(firstrunebox.Text);
                Lastrune = Convert.ToInt32(lastrunebox.Text);
                MaxWeight = Convert.ToInt32(maxweighttbox.Text);
                Treearea = Convert.ToInt32(treeareatbox.Text);
                if (!Osi)
                {
                    label24.Text = "Ebony";
                    label23.Text = "Mahogany";
                    label22.Text = "Pine";
                    label21.Text = "Zircote";
                    label20.Text = "Bamboo";
                }
                else
                {
                    label24.Text = "Amber";
                    label23.Text = "Fungi";
                    label22.Text = "Bark";
                    label21.Text = "Switch";
                    label20.Text = "Plant";
                }
                //Runebook.Parse();
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
            if (BankCrystal)
            {
                Stealth.Client.SendTextToUO("Bank");
                Lumbermethod.BankUnload(bankstorage);
            }
            else
            {
                while (!GoHome())
                {
                    Thread.Sleep(50);
                }

                Lumbermethod.Unload(Housecontainer);
                Travel.Recall(Runetouse, Method, Osi);
            }
        }
        RuneBookConfig OSIconfig = new RuneBookConfig()
        {
            ScrollOffset = 10,
            DropOffset = 200,
            DefaultOffset = 300,
            RecallOffset = 50,
            GateOffset = 100,
            SacredOffset = 75,
            Jumper = 1
        };
        RuneBookConfig UOEVO = new RuneBookConfig()
        {
            ScrollOffset = 2,
            DropOffset = 3,
            DefaultOffset = 4,
            RecallOffset = 5,
            GateOffset = 6,
            SacredOffset = 7,
            Jumper = 6
        };
        private static bool GoHome()
        {
            return Travel.Recall(Homerune, Method, Osi);
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
        public static void BankContainer()
        {
            MessageBox.Show(@"Select your Container in your Bank");
            bankstorage = new Item(new Serial(Getitem()));
            bankstorage.DoubleClick();
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
            if (Osi)
            {
                MessageBox.Show(@"Select your Runebook");
                Runebook = new Runebook(new Serial(Getitem()), UOEVO);
                Runebook.Parse();
                #region Get Runebook Info
                Invoke((MethodInvoker)
                            delegate { Runebooktbox.Text = Runebook.Serial.Value.ToString(); });
                #endregion
            }
            else
            {
                MessageBox.Show(@"Select your Runebook");
                Runebook = new Runebook(new Serial(Getitem()), UOEVO, 0x554B87F3);
                Runebook.Parse();
                #region Get Runebook Info
                Invoke((MethodInvoker)
                            delegate { Runebooktbox.Text = Runebook.Serial.Value.ToString(); });
                #endregion
            }
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
                    if (!Travel.Recall(Runetouse, Method, Osi))
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
            if (BankCrystal)
            {
                Stealth.Client.SendTextToUO("Bank");
                Lumbermethod.BankUnload(bankstorage);
            }
            else
            {
                Lumbermethod.Unload(Housecontainer);
            }
            MessageBox.Show(string.Format("We have ran for {0} minutes. Thank you! ", endtimebox.Text));
            Invoke((MethodInvoker)
                delegate
                {
                    savebutton.Enabled = true;
                    lumberjackbutton.Enabled = true;
                });
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
                        recallstatus.Text = Travel.Recall(Homerune, Method, Osi)
                            ? "Recalled"
                            : "Recall Failed";
                    });
                ContainerSetup();
                Lumbermethod.Unload(Housecontainer);
                Invoke((MethodInvoker)delegate
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
            int _count;
            if (Osi)
            {
               _count = Lumbermethod.Oak + Lumbermethod.Ash + Lumbermethod.Yew + Lumbermethod.Hw + Lumbermethod.Blood +
                            Lumbermethod.Frost + Lumbermethod.Reg;
            }
            else
            {
                _count = Lumbermethod.Oak + Lumbermethod.Ash + Lumbermethod.Yew + Lumbermethod.Hw + Lumbermethod.Blood +
                            Lumbermethod.Frost + Lumbermethod.Reg + Lumbermethod.Bamboo + Lumbermethod.Ebony + 
                            Lumbermethod.Mahogany + Lumbermethod.Pine + Lumbermethod.Zircote;
            }
            var timespan = DateTime.Now.Subtract(Starttime);
            var span = (int)timespan.TotalMinutes;
            if (span < 1) span = 1;
            var avg = _count / span * 60;

            return avg;
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
               { if (Osi)
                   {
                       oakbox.Text = Lumbermethod.Oak.ToString();
                       ashbox.Text = Lumbermethod.Ash.ToString();
                       yewbox.Text = Lumbermethod.Yew.ToString();
                       hwbox.Text = Lumbermethod.Hw.ToString();
                       bloodbox.Text = Lumbermethod.Blood.ToString();
                       regbox.Text = Lumbermethod.Reg.ToString();
                       frostbox.Text = Lumbermethod.Frost.ToString();
                       amberbox.Text = Lumbermethod.Amber.ToString(); //Amber
                       fungibox.Text = Lumbermethod.Fungi.ToString(); //Fungi
                       barkbox.Text = Lumbermethod.Bark.ToString(); //Bark
                       switchbox.Text = Lumbermethod.Switch.ToString(); //Switch
                       plantbox.Text = Lumbermethod.Plant.ToString(); //Plant
                       avghr.Text = Avgperhour().ToString();
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
                       amberbox.Text = Lumbermethod.Ebony.ToString(); //Amber
                       fungibox.Text = Lumbermethod.Mahogany.ToString(); //Fungi
                       barkbox.Text = Lumbermethod.Pine.ToString(); //Bark
                       switchbox.Text = Lumbermethod.Zircote.ToString(); //Switch
                       plantbox.Text = Lumbermethod.Bamboo.ToString(); //Plant
                       avghr.Text = Avgperhour().ToString();
                   }
               });

            }
            else
            {
                if (Osi)
                {
                    oakbox.Text = Lumbermethod.Oak.ToString();
                    ashbox.Text = Lumbermethod.Ash.ToString();
                    yewbox.Text = Lumbermethod.Yew.ToString();
                    hwbox.Text = Lumbermethod.Hw.ToString();
                    bloodbox.Text = Lumbermethod.Blood.ToString();
                    regbox.Text = Lumbermethod.Reg.ToString();
                    frostbox.Text = Lumbermethod.Frost.ToString();
                    amberbox.Text = Lumbermethod.Amber.ToString(); //Amber
                    fungibox.Text = Lumbermethod.Fungi.ToString(); //Fungi
                    barkbox.Text = Lumbermethod.Bark.ToString(); //Bark
                    switchbox.Text = Lumbermethod.Switch.ToString(); //Switch
                    plantbox.Text = Lumbermethod.Plant.ToString(); //Plant
                    avghr.Text = Avgperhour().ToString();
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
                    amberbox.Text = Lumbermethod.Ebony.ToString(); //Amber
                    fungibox.Text = Lumbermethod.Mahogany.ToString(); //Fungi
                    barkbox.Text = Lumbermethod.Pine.ToString(); //Bark
                    switchbox.Text = Lumbermethod.Zircote.ToString(); //Switch
                    plantbox.Text = Lumbermethod.Bamboo.ToString(); //Plant
                    avghr.Text = Avgperhour().ToString();
                }
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
                Invoke((MethodInvoker)delegate
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
                   if (runebook != null) Runebook = new Runebook(uint.Parse(runebook),OSIconfig);
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
                    if (axetextbox.Text != null) AxeSerial = new Item(Convert.ToUInt32(axetextbox.Text, 16)).Serial;
                    if (housecontainer != null) Housecontainer = new Item(Convert.ToUInt32(housecontainer, 16));
                    if (Runebooktbox.Text != null) Runebook = new Runebook(Convert.ToUInt32(Runebooktbox.Text, 16),OSIconfig);

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