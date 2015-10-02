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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // Runebooktbox
            // 
            this.Runebooktbox.Location = new System.Drawing.Point(14, 72);
            this.Runebooktbox.Name = "Runebooktbox";
            this.Runebooktbox.ReadOnly = true;
            this.Runebooktbox.Size = new System.Drawing.Size(100, 22);
            this.Runebooktbox.TabIndex = 0;
            // 
            // recallstatus
            // 
            this.recallstatus.Location = new System.Drawing.Point(161, 131);
            this.recallstatus.Name = "recallstatus";
            this.recallstatus.ReadOnly = true;
            this.recallstatus.Size = new System.Drawing.Size(100, 22);
            this.recallstatus.TabIndex = 2;
            // 
            // lumberjackbutton
            // 
            this.lumberjackbutton.Location = new System.Drawing.Point(75, 125);
            this.lumberjackbutton.Name = "lumberjackbutton";
            this.lumberjackbutton.Size = new System.Drawing.Size(130, 23);
            this.lumberjackbutton.TabIndex = 4;
            this.lumberjackbutton.Text = "Lumberjack";
            this.lumberjackbutton.UseVisualStyleBackColor = true;
            this.lumberjackbutton.Click += new System.EventHandler(this.button3_Click);
            // 
            // axetextbox
            // 
            this.axetextbox.Location = new System.Drawing.Point(161, 72);
            this.axetextbox.Name = "axetextbox";
            this.axetextbox.ReadOnly = true;
            this.axetextbox.Size = new System.Drawing.Size(100, 22);
            this.axetextbox.TabIndex = 5;
            // 
            // gumptext
            // 
            this.gumptext.Location = new System.Drawing.Point(14, 131);
            this.gumptext.Name = "gumptext";
            this.gumptext.ReadOnly = true;
            this.gumptext.Size = new System.Drawing.Size(100, 22);
            this.gumptext.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Gump ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Runebook ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(158, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Axe ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(161, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Last Recall Status";
            // 
            // startsetup
            // 
            this.startsetup.Location = new System.Drawing.Point(76, 169);
            this.startsetup.Name = "startsetup";
            this.startsetup.Size = new System.Drawing.Size(130, 23);
            this.startsetup.TabIndex = 11;
            this.startsetup.Text = "Setup";
            this.startsetup.UseVisualStyleBackColor = true;
            this.startsetup.Click += new System.EventHandler(this.startsetup_Click);
            // 
            // endtimebox
            // 
            this.endtimebox.Location = new System.Drawing.Point(14, 206);
            this.endtimebox.Name = "endtimebox";
            this.endtimebox.Size = new System.Drawing.Size(45, 22);
            this.endtimebox.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(73, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Minutes to Run";
            // 
            // homerunebox
            // 
            this.homerunebox.Location = new System.Drawing.Point(14, 235);
            this.homerunebox.Name = "homerunebox";
            this.homerunebox.Size = new System.Drawing.Size(45, 22);
            this.homerunebox.TabIndex = 14;
            // 
            // bankrunebox
            // 
            this.bankrunebox.Location = new System.Drawing.Point(14, 264);
            this.bankrunebox.Name = "bankrunebox";
            this.bankrunebox.Size = new System.Drawing.Size(45, 22);
            this.bankrunebox.TabIndex = 15;
            // 
            // firstrunebox
            // 
            this.firstrunebox.Location = new System.Drawing.Point(14, 293);
            this.firstrunebox.Name = "firstrunebox";
            this.firstrunebox.Size = new System.Drawing.Size(45, 22);
            this.firstrunebox.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(73, 235);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "Home Rune";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(73, 264);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 17);
            this.label7.TabIndex = 18;
            this.label7.Text = "Bank Rune";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(73, 293);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 17);
            this.label8.TabIndex = 19;
            this.label8.Text = "First Tree Rune";
            // 
            // lastrunebox
            // 
            this.lastrunebox.Location = new System.Drawing.Point(14, 322);
            this.lastrunebox.Name = "lastrunebox";
            this.lastrunebox.Size = new System.Drawing.Size(45, 22);
            this.lastrunebox.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(73, 322);
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
            this.comboBox1.Location = new System.Drawing.Point(76, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 22;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // treeareatbox
            // 
            this.treeareatbox.Location = new System.Drawing.Point(13, 351);
            this.treeareatbox.Name = "treeareatbox";
            this.treeareatbox.Size = new System.Drawing.Size(46, 22);
            this.treeareatbox.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(73, 351);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 17);
            this.label10.TabIndex = 24;
            this.label10.Text = "Tree Area";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // Lumberjacker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 393);
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
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

