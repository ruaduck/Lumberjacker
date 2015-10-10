namespace ScriptGUI
{
    partial class Lumberjacker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Runebooktbox = new System.Windows.Forms.TextBox();
            this.recallstatus = new System.Windows.Forms.TextBox();
            this.lumberjackbutton = new System.Windows.Forms.Button();
            this.axetextbox = new System.Windows.Forms.TextBox();
            this.gumptext = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.startsetup = new System.Windows.Forms.Button();
            this.endtimebox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.homerunebox = new System.Windows.Forms.TextBox();
            this.bankrunebox = new System.Windows.Forms.TextBox();
            this.firstrunebox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lastrunebox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.treeareatbox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.cancelbutton = new System.Windows.Forms.Button();
            this.regbox = new System.Windows.Forms.TextBox();
            this.oakbox = new System.Windows.Forms.TextBox();
            this.ashbox = new System.Windows.Forms.TextBox();
            this.yewbox = new System.Windows.Forms.TextBox();
            this.bloodbox = new System.Windows.Forms.TextBox();
            this.hwbox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // Runebooktbox
            // 
            this.Runebooktbox.Location = new System.Drawing.Point(49, 74);
            this.Runebooktbox.Name = "Runebooktbox";
            this.Runebooktbox.ReadOnly = true;
            this.Runebooktbox.Size = new System.Drawing.Size(100, 22);
            this.Runebooktbox.TabIndex = 0;
            // 
            // recallstatus
            // 
            this.recallstatus.Location = new System.Drawing.Point(196, 133);
            this.recallstatus.Name = "recallstatus";
            this.recallstatus.ReadOnly = true;
            this.recallstatus.Size = new System.Drawing.Size(100, 22);
            this.recallstatus.TabIndex = 2;
            // 
            // lumberjackbutton
            // 
            this.lumberjackbutton.Location = new System.Drawing.Point(222, 168);
            this.lumberjackbutton.Name = "lumberjackbutton";
            this.lumberjackbutton.Size = new System.Drawing.Size(130, 23);
            this.lumberjackbutton.TabIndex = 4;
            this.lumberjackbutton.Text = "Lumberjack";
            this.lumberjackbutton.UseVisualStyleBackColor = true;
            this.lumberjackbutton.Click += new System.EventHandler(this.button3_Click);
            // 
            // axetextbox
            // 
            this.axetextbox.Location = new System.Drawing.Point(196, 74);
            this.axetextbox.Name = "axetextbox";
            this.axetextbox.ReadOnly = true;
            this.axetextbox.Size = new System.Drawing.Size(100, 22);
            this.axetextbox.TabIndex = 5;
            // 
            // gumptext
            // 
            this.gumptext.Location = new System.Drawing.Point(49, 133);
            this.gumptext.Name = "gumptext";
            this.gumptext.ReadOnly = true;
            this.gumptext.Size = new System.Drawing.Size(100, 22);
            this.gumptext.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Gump ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Runebook ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(193, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Axe ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(196, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Last Recall Status";
            // 
            // startsetup
            // 
            this.startsetup.Location = new System.Drawing.Point(5, 168);
            this.startsetup.Name = "startsetup";
            this.startsetup.Size = new System.Drawing.Size(130, 23);
            this.startsetup.TabIndex = 11;
            this.startsetup.Text = "Setup";
            this.startsetup.UseVisualStyleBackColor = true;
            this.startsetup.Click += new System.EventHandler(this.startsetup_Click);
            // 
            // endtimebox
            // 
            this.endtimebox.Location = new System.Drawing.Point(7, 213);
            this.endtimebox.Name = "endtimebox";
            this.endtimebox.Size = new System.Drawing.Size(45, 22);
            this.endtimebox.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(66, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Minutes to Run";
            // 
            // homerunebox
            // 
            this.homerunebox.Location = new System.Drawing.Point(7, 242);
            this.homerunebox.Name = "homerunebox";
            this.homerunebox.Size = new System.Drawing.Size(45, 22);
            this.homerunebox.TabIndex = 14;
            // 
            // bankrunebox
            // 
            this.bankrunebox.Location = new System.Drawing.Point(7, 271);
            this.bankrunebox.Name = "bankrunebox";
            this.bankrunebox.Size = new System.Drawing.Size(45, 22);
            this.bankrunebox.TabIndex = 15;
            // 
            // firstrunebox
            // 
            this.firstrunebox.Location = new System.Drawing.Point(7, 300);
            this.firstrunebox.Name = "firstrunebox";
            this.firstrunebox.Size = new System.Drawing.Size(45, 22);
            this.firstrunebox.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(66, 242);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "Home Rune";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(66, 271);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 17);
            this.label7.TabIndex = 18;
            this.label7.Text = "Bank Rune";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(66, 300);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 17);
            this.label8.TabIndex = 19;
            this.label8.Text = "First Tree Rune";
            // 
            // lastrunebox
            // 
            this.lastrunebox.Location = new System.Drawing.Point(7, 329);
            this.lastrunebox.Name = "lastrunebox";
            this.lastrunebox.Size = new System.Drawing.Size(45, 22);
            this.lastrunebox.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(66, 329);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 17);
            this.label9.TabIndex = 21;
            this.label9.Text = "Last Tree Rune";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "OSI",
            "RebirthUO"});
            this.comboBox1.Location = new System.Drawing.Point(111, 14);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 22;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // treeareatbox
            // 
            this.treeareatbox.Location = new System.Drawing.Point(6, 358);
            this.treeareatbox.Name = "treeareatbox";
            this.treeareatbox.Size = new System.Drawing.Size(46, 22);
            this.treeareatbox.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(66, 358);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 17);
            this.label10.TabIndex = 24;
            this.label10.Text = "Tree Area";
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // cancelbutton
            // 
            this.cancelbutton.Location = new System.Drawing.Point(141, 168);
            this.cancelbutton.Name = "cancelbutton";
            this.cancelbutton.Size = new System.Drawing.Size(75, 23);
            this.cancelbutton.TabIndex = 25;
            this.cancelbutton.Text = "Cancel";
            this.cancelbutton.UseVisualStyleBackColor = true;
            this.cancelbutton.Click += new System.EventHandler(this.cancelbutton_Click);
            // 
            // regbox
            // 
            this.regbox.Location = new System.Drawing.Point(199, 213);
            this.regbox.Name = "regbox";
            this.regbox.Size = new System.Drawing.Size(64, 22);
            this.regbox.TabIndex = 26;
            // 
            // oakbox
            // 
            this.oakbox.Location = new System.Drawing.Point(199, 242);
            this.oakbox.Name = "oakbox";
            this.oakbox.Size = new System.Drawing.Size(64, 22);
            this.oakbox.TabIndex = 27;
            // 
            // ashbox
            // 
            this.ashbox.Location = new System.Drawing.Point(199, 271);
            this.ashbox.Name = "ashbox";
            this.ashbox.Size = new System.Drawing.Size(64, 22);
            this.ashbox.TabIndex = 28;
            // 
            // yewbox
            // 
            this.yewbox.Location = new System.Drawing.Point(199, 300);
            this.yewbox.Name = "yewbox";
            this.yewbox.Size = new System.Drawing.Size(64, 22);
            this.yewbox.TabIndex = 29;
            // 
            // bloodbox
            // 
            this.bloodbox.Location = new System.Drawing.Point(199, 328);
            this.bloodbox.Name = "bloodbox";
            this.bloodbox.Size = new System.Drawing.Size(64, 22);
            this.bloodbox.TabIndex = 30;
            // 
            // hwbox
            // 
            this.hwbox.Location = new System.Drawing.Point(199, 359);
            this.hwbox.Name = "hwbox";
            this.hwbox.Size = new System.Drawing.Size(64, 22);
            this.hwbox.TabIndex = 31;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(269, 216);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 17);
            this.label11.TabIndex = 32;
            this.label11.Text = "Regular";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(269, 245);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 17);
            this.label12.TabIndex = 33;
            this.label12.Text = "Oak";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(269, 276);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 17);
            this.label13.TabIndex = 34;
            this.label13.Text = "Ash";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(269, 303);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(34, 17);
            this.label14.TabIndex = 35;
            this.label14.Text = "Yew";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(269, 331);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 17);
            this.label15.TabIndex = 36;
            this.label15.Text = "Bloodwood";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(269, 361);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(76, 17);
            this.label16.TabIndex = 37;
            this.label16.Text = "Heartwood";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(196, 194);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(115, 17);
            this.label17.TabIndex = 38;
            this.label17.Text = "Board/Log Count";
            // 
            // backgroundWorker3
            // 
            backgroundWorker3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker3_DoWork);
            // 
            // Lumberjacker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 393);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.hwbox);
            this.Controls.Add(this.bloodbox);
            this.Controls.Add(this.yewbox);
            this.Controls.Add(this.ashbox);
            this.Controls.Add(this.oakbox);
            this.Controls.Add(this.regbox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.treeareatbox);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lastrunebox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.firstrunebox);
            this.Controls.Add(this.bankrunebox);
            this.Controls.Add(this.homerunebox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.endtimebox);
            this.Controls.Add(this.startsetup);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gumptext);
            this.Controls.Add(this.axetextbox);
            this.Controls.Add(this.recallstatus);
            this.Controls.Add(this.Runebooktbox);
            this.Controls.Add(this.cancelbutton);
            this.Controls.Add(this.lumberjackbutton);
            this.Name = "Lumberjacker";
            this.Text = "Lumberjacker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox Runebooktbox;
        public System.Windows.Forms.TextBox recallstatus;
        private System.Windows.Forms.Button lumberjackbutton;
        public System.Windows.Forms.TextBox axetextbox;
        public System.Windows.Forms.TextBox gumptext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button startsetup;
        private System.Windows.Forms.TextBox endtimebox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox homerunebox;
        private System.Windows.Forms.TextBox bankrunebox;
        private System.Windows.Forms.TextBox firstrunebox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox lastrunebox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox treeareatbox;
        private System.Windows.Forms.Label label10;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Button cancelbutton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        public static System.ComponentModel.BackgroundWorker backgroundWorker3;
        public System.Windows.Forms.TextBox regbox;
        public System.Windows.Forms.TextBox oakbox;
        public System.Windows.Forms.TextBox ashbox;
        public System.Windows.Forms.TextBox yewbox;
        public System.Windows.Forms.TextBox bloodbox;
        public System.Windows.Forms.TextBox hwbox;
        public static System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

