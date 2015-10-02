//////////////////////////////////////////////////////////////////////////
// Script Name:  Duck Duck's Lumberjacker                               //
// Verision : 1.0a                                                      //
// Versiion Date: 9/7/15                                                //
// Release Date:                                                        //
// Special Thanks:  Crome for examples and ScriptSDK                    //
// Authorized Release Locations:  www.scriptuo.com ; www.rebirthuo.com  //
//////////////////////////////////////////////////////////////////////////

using System;
using System.Threading;
using System.Windows.Forms;
using ScriptDotNet2;
using ScriptSDK;
using ScriptSDK.Items;
using ScriptSDK.Mobiles;

namespace ScriptGUI
{
    public partial class Lumberjacker : Form
    {
        public static string Method = "Not Set";

        public Lumberjacker()
        {
            
            InitializeComponent();
                       
        }
        public static Item Housecontainer;
        public Serial AxeSerial;
        public DateTime Endtime;
        public static int Minutes; //Minutes to Run
        public static int Homerune; // Rune Home
        public static int Bankrune;  //  Bank Rune
        public static int Firstrune; // First Lumberjacking Rune
        public static int Lastrune; // Last Lumberjacking Rune
        public static bool Osi; // OSI = True; RebirthUO = False;
        public static int Runetouse; //current rune on recall
        public static uint Runebook;
        public static int Treearea; //  Area to look for tree
        public static int maxweight;


        public bool SetInputs()
        {
            maxweight = Stealth.Default.GetSelfMaxWeight() - 30;
            Osi = comboBox1.Text == @"OSI";
            if (endtimebox == null || homerunebox == null || bankrunebox == null || firstrunebox == null ||
                lastrunebox == null)
            {
                return false;
            }
            if (!int.TryParse(endtimebox.Text, out Minutes)) return false;
            if (!int.TryParse(homerunebox.Text, out Homerune)) return false;
            if (!int.TryParse(bankrunebox.Text, out Bankrune)) return false;
            if (!int.TryParse(treeareatbox.Text, out Treearea)) return false;
            return int.TryParse(firstrunebox.Text, out Firstrune) && int.TryParse(lastrunebox.Text, out Lastrune);
        }
        public void Setup()
        {

            if (SetInputs())
            {
                RecallSetup();
                RunebookSetup();
                AxeSerial = AxeSetup();
                recallstatus.Text = Travel.Recall(Runebook, Homerune, Method, Osi) ? "Recalled" : "Recall Failed";
                ContainerSetup();
                Lumbermethod.Unload(Housecontainer);
                Invoke((MethodInvoker) delegate { Text = PlayerMobile.GetPlayer().Name + @" - Lumberjacker"; });
            }
            else MessageBox.Show(@"You missed some required fields or didn't enter in digits in those fields");
        }
        private static void ContainerSetup()
        {
            MessageBox.Show(@"Select your Container");
            Housecontainer = new Item(new Serial(Getcontainer()));
            Housecontainer.DoubleClick();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Endtime = DateTime.Now.AddMinutes((Minutes));
            Start();
            MessageBox.Show(string.Format("We have ran for {0} minutes. Thank you! ", endtimebox.Text));
            
        }

        private void Start()
        {
            if (!backgroundWorker1.IsBusy) backgroundWorker1.RunWorkerAsync();
                    
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
            Lumbermethod main = new Lumbermethod();
            #region Get Axe Info
            DialogResult axedialogResult = MessageBox.Show(@"Do you have your Axe Equipped?", @"Getting Axe ID", MessageBoxButtons.YesNo);
            switch (axedialogResult)
            {
                case DialogResult.Yes:
                    axe = main.GetAxe(PlayerMobile.GetPlayer());
                    axetextbox.Text = axe.ToString();
                    break;
                case DialogResult.No:
                    MessageBox.Show(@"Equip your Axe and start script again.");
                    Close();
                    break;
            }
            return axe;

            #endregion
        }
        private void RunebookSetup()
        {
            Travel travel = new Travel();
            #region Get Runebook Info
            DialogResult runebookDialogResult = MessageBox.Show(@"Do you have only 1 runebook in your backpack?", @"Getting Axe ID", MessageBoxButtons.YesNo);
            if (runebookDialogResult == DialogResult.Yes)
            {
                Runebook = travel.SetRunebookId();
                Runebooktbox.Text = Runebook == 0 ? "Error" : Runebook.ToString();
            }
            else if (runebookDialogResult == DialogResult.No)
            {
                MessageBox.Show(@"Get your runebooks correct and start script again.");
                Close();
            }
            #endregion
        }
        private static uint Getcontainer()
        {
            Stealth.Default.ClientRequestObjectTarget();
            while (!Stealth.Default.ClientTargetResponsePresent())
            {
                Thread.Sleep(50);
            }
            var container = Stealth.Default.ClientTargetResponse().ID;
            return container;
        }
        private void RecallSetup()
        {
            #region Recall Method
            var recallDialogResult =
                MessageBox.Show("Choose whether to recall or sacred journey \n Yes = Recall \n No = Sacred Journey",
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
        public string GetMethod()
        {
            return Method;
        }

        private void startsetup_Click(object sender, EventArgs e)
        {
            Setup();
            Controls.Remove(startsetup);
            Controls.Add(lumberjackbutton);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (Endtime > DateTime.Now)
            {

                for (Runetouse = Firstrune; Runetouse < Lastrune + 1; Runetouse++)
                {
                    Invoke((MethodInvoker) delegate { gumptext.Text = string.Format("Recalling to spot {0}", Runetouse); }) ;
                    if (!Travel.Recall(Runebook, Runetouse, Method, Osi))
                    {
                        recallstatus.Text = Method + @" Failed. Trying Next Rune";
                        continue;
                    }
                    Lumbermethod.Lumberjack(AxeSerial, Treearea);
                    if (Endtime > DateTime.Now) break;
                    Invoke((MethodInvoker)
                        delegate { gumptext.Text = "getting next rune"; }) ;
                }
            }
            Lumbermethod.Unload(Housecontainer);



        }
    }

}
