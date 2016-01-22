namespace Poker
{
    partial class Form1
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
            this.buttonFold = new System.Windows.Forms.Button();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.buttonCall = new System.Windows.Forms.Button();
            this.buttonRaise = new System.Windows.Forms.Button();
            this.PlayerTurnTimer = new System.Windows.Forms.ProgressBar();
            this.tableChips = new System.Windows.Forms.TextBox();
            this.buttonAddChips = new System.Windows.Forms.Button();
            this.tableAdd = new System.Windows.Forms.TextBox();
            this.tableChipsBot5 = new System.Windows.Forms.TextBox();
            this.tableChipsBot4 = new System.Windows.Forms.TextBox();
            this.tableChipsBot3 = new System.Windows.Forms.TextBox();
            this.tableChipsBot2 = new System.Windows.Forms.TextBox();
            this.tableChipsBot1 = new System.Windows.Forms.TextBox();
            this.tablePot = new System.Windows.Forms.TextBox();
            this.buttonOptions = new System.Windows.Forms.Button();
            this.buttonBigBlind = new System.Windows.Forms.Button();
            this.tableSmallBlind = new System.Windows.Forms.TextBox();
            this.buttonSmallBlind = new System.Windows.Forms.Button();
            this.tableBigBlind = new System.Windows.Forms.TextBox();
            this.botFiveStatus = new System.Windows.Forms.Label();
            this.botFourStatus = new System.Windows.Forms.Label();
            this.botThreeStatus = new System.Windows.Forms.Label();
            this.botOneStatus = new System.Windows.Forms.Label();
            this.playerStatus = new System.Windows.Forms.Label();
            this.botTwoStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableRaise = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonFold
            // 
            this.buttonFold.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonFold.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFold.Location = new System.Drawing.Point(335, 660);
            this.buttonFold.Name = "buttonFold";
            this.buttonFold.Size = new System.Drawing.Size(130, 62);
            this.buttonFold.TabIndex = 0;
            this.buttonFold.Text = "Fold";
            this.buttonFold.UseVisualStyleBackColor = true;
            this.buttonFold.Click += new System.EventHandler(this.BFold_Click);
            // 
            // buttonCheck
            // 
            this.buttonCheck.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCheck.Location = new System.Drawing.Point(494, 660);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(134, 62);
            this.buttonCheck.TabIndex = 2;
            this.buttonCheck.Text = "Check";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.BCheck_Click);
            // 
            // buttonCall
            // 
            this.buttonCall.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCall.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCall.Location = new System.Drawing.Point(667, 661);
            this.buttonCall.Name = "buttonCall";
            this.buttonCall.Size = new System.Drawing.Size(126, 62);
            this.buttonCall.TabIndex = 3;
            this.buttonCall.Text = "Call";
            this.buttonCall.UseVisualStyleBackColor = true;
            this.buttonCall.Click += new System.EventHandler(this.BCall_Click);
            // 
            // buttonRaise
            // 
            this.buttonRaise.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonRaise.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRaise.Location = new System.Drawing.Point(835, 661);
            this.buttonRaise.Name = "buttonRaise";
            this.buttonRaise.Size = new System.Drawing.Size(124, 62);
            this.buttonRaise.TabIndex = 4;
            this.buttonRaise.Text = "Raise";
            this.buttonRaise.UseVisualStyleBackColor = true;
            this.buttonRaise.Click += new System.EventHandler(this.BRaise_Click);
            // 
            // PlayerButtonTimer
            // 
            this.PlayerTurnTimer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.PlayerTurnTimer.BackColor = System.Drawing.SystemColors.Control;
            this.PlayerTurnTimer.Location = new System.Drawing.Point(335, 631);
            this.PlayerTurnTimer.Maximum = 1000;
            this.PlayerTurnTimer.Name = "PlayerButtonTimer";
            this.PlayerTurnTimer.Size = new System.Drawing.Size(667, 23);
            this.PlayerTurnTimer.TabIndex = 5;
            this.PlayerTurnTimer.Value = 1000;
            this.PlayerTurnTimer.Click += new System.EventHandler(this.PlayerButtonTimer_Click);
            // 
            // tableChips
            // 
            this.tableChips.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tableChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableChips.Location = new System.Drawing.Point(755, 553);
            this.tableChips.Name = "tableChips";
            this.tableChips.Size = new System.Drawing.Size(163, 23);
            this.tableChips.TabIndex = 6;
            this.tableChips.Text = "Chips : 0";
            // 
            // buttonAddChips
            // 
            this.buttonAddChips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddChips.Location = new System.Drawing.Point(12, 697);
            this.buttonAddChips.Name = "buttonAddChips";
            this.buttonAddChips.Size = new System.Drawing.Size(75, 25);
            this.buttonAddChips.TabIndex = 7;
            this.buttonAddChips.Text = "AddChips";
            this.buttonAddChips.UseVisualStyleBackColor = true;
            this.buttonAddChips.Click += new System.EventHandler(this.BAdd_Click);
            // 
            // tableAdd
            // 
            this.tableAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tableAdd.Location = new System.Drawing.Point(93, 700);
            this.tableAdd.Name = "tableAdd";
            this.tableAdd.Size = new System.Drawing.Size(125, 20);
            this.tableAdd.TabIndex = 8;
            // 
            // tableChipsBot5
            // 
            this.tableChipsBot5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableChipsBot5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableChipsBot5.Location = new System.Drawing.Point(1012, 553);
            this.tableChipsBot5.Name = "tableChipsBot5";
            this.tableChipsBot5.Size = new System.Drawing.Size(152, 23);
            this.tableChipsBot5.TabIndex = 9;
            this.tableChipsBot5.Text = "Chips : 0";
            // 
            // tableChipsBot4
            // 
            this.tableChipsBot4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableChipsBot4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableChipsBot4.Location = new System.Drawing.Point(970, 81);
            this.tableChipsBot4.Name = "tableChipsBot4";
            this.tableChipsBot4.Size = new System.Drawing.Size(123, 23);
            this.tableChipsBot4.TabIndex = 10;
            this.tableChipsBot4.Text = "Chips : 0";
            // 
            // tableChipsBot3
            // 
            this.tableChipsBot3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableChipsBot3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableChipsBot3.Location = new System.Drawing.Point(755, 81);
            this.tableChipsBot3.Name = "tableChipsBot3";
            this.tableChipsBot3.Size = new System.Drawing.Size(125, 23);
            this.tableChipsBot3.TabIndex = 11;
            this.tableChipsBot3.Text = "Chips : 0";
            // 
            // tableChipsBot2
            // 
            this.tableChipsBot2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableChipsBot2.Location = new System.Drawing.Point(276, 81);
            this.tableChipsBot2.Name = "tableChipsBot2";
            this.tableChipsBot2.Size = new System.Drawing.Size(133, 23);
            this.tableChipsBot2.TabIndex = 12;
            this.tableChipsBot2.Text = "Chips : 0";
            this.tableChipsBot2.TextChanged += new System.EventHandler(this.tbBotChips2_TextChanged);
            // 
            // tableChipsBot1
            // 
            this.tableChipsBot1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tableChipsBot1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableChipsBot1.Location = new System.Drawing.Point(181, 553);
            this.tableChipsBot1.Name = "tableChipsBot1";
            this.tableChipsBot1.Size = new System.Drawing.Size(142, 23);
            this.tableChipsBot1.TabIndex = 13;
            this.tableChipsBot1.Text = "Chips : 0";
            // 
            // tablePot
            // 
            this.tablePot.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tablePot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tablePot.Location = new System.Drawing.Point(606, 212);
            this.tablePot.Name = "tablePot";
            this.tablePot.Size = new System.Drawing.Size(125, 23);
            this.tablePot.TabIndex = 14;
            this.tablePot.Text = "0";
            // 
            // buttonOptions
            // 
            this.buttonOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOptions.Location = new System.Drawing.Point(12, 12);
            this.buttonOptions.Name = "buttonOptions";
            this.buttonOptions.Size = new System.Drawing.Size(75, 36);
            this.buttonOptions.TabIndex = 15;
            this.buttonOptions.Text = "BB/SB";
            this.buttonOptions.UseVisualStyleBackColor = true;
            this.buttonOptions.Click += new System.EventHandler(this.BOptions_Click);
            // 
            // buttonBigBlind
            // 
            this.buttonBigBlind.Location = new System.Drawing.Point(12, 254);
            this.buttonBigBlind.Name = "buttonBigBlind";
            this.buttonBigBlind.Size = new System.Drawing.Size(75, 23);
            this.buttonBigBlind.TabIndex = 16;
            this.buttonBigBlind.Text = "Big Blind";
            this.buttonBigBlind.UseVisualStyleBackColor = true;
            this.buttonBigBlind.Click += new System.EventHandler(this.BBB_Click);
            // 
            // tableSmallBlind
            // 
            this.tableSmallBlind.Location = new System.Drawing.Point(12, 228);
            this.tableSmallBlind.Name = "tableSmallBlind";
            this.tableSmallBlind.Size = new System.Drawing.Size(75, 20);
            this.tableSmallBlind.TabIndex = 17;
            this.tableSmallBlind.Text = "250";
            this.tableSmallBlind.TextChanged += new System.EventHandler(this.tbSB_TextChanged);
            // 
            // buttonSmallBlind
            // 
            this.buttonSmallBlind.Location = new System.Drawing.Point(12, 199);
            this.buttonSmallBlind.Name = "buttonSmallBlind";
            this.buttonSmallBlind.Size = new System.Drawing.Size(75, 23);
            this.buttonSmallBlind.TabIndex = 18;
            this.buttonSmallBlind.Text = "Small Blind";
            this.buttonSmallBlind.UseVisualStyleBackColor = true;
            this.buttonSmallBlind.Click += new System.EventHandler(this.BSB_Click);
            // 
            // tableBigBlind
            // 
            this.tableBigBlind.Location = new System.Drawing.Point(12, 283);
            this.tableBigBlind.Name = "tableBigBlind";
            this.tableBigBlind.Size = new System.Drawing.Size(75, 20);
            this.tableBigBlind.TabIndex = 19;
            this.tableBigBlind.Text = "500";
            // 
            // botFiveStatus
            // 
            this.botFiveStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.botFiveStatus.Location = new System.Drawing.Point(1012, 579);
            this.botFiveStatus.Name = "botFiveStatus";
            this.botFiveStatus.Size = new System.Drawing.Size(152, 32);
            this.botFiveStatus.TabIndex = 26;
            // 
            // botFourStatus
            // 
            this.botFourStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.botFourStatus.Location = new System.Drawing.Point(970, 107);
            this.botFourStatus.Name = "botFourStatus";
            this.botFourStatus.Size = new System.Drawing.Size(123, 32);
            this.botFourStatus.TabIndex = 27;
            // 
            // botThreeStatus
            // 
            this.botThreeStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.botThreeStatus.Location = new System.Drawing.Point(755, 107);
            this.botThreeStatus.Name = "botThreeStatus";
            this.botThreeStatus.Size = new System.Drawing.Size(125, 32);
            this.botThreeStatus.TabIndex = 28;
            // 
            // botOneStatus
            // 
            this.botOneStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.botOneStatus.Location = new System.Drawing.Point(181, 579);
            this.botOneStatus.Name = "botOneStatus";
            this.botOneStatus.Size = new System.Drawing.Size(142, 32);
            this.botOneStatus.TabIndex = 29;
            // 
            // playerStatus
            // 
            this.playerStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerStatus.Location = new System.Drawing.Point(755, 579);
            this.playerStatus.Name = "playerStatus";
            this.playerStatus.Size = new System.Drawing.Size(163, 32);
            this.playerStatus.TabIndex = 30;
            // 
            // botTwoStatus
            // 
            this.botTwoStatus.Location = new System.Drawing.Point(276, 107);
            this.botTwoStatus.Name = "botTwoStatus";
            this.botTwoStatus.Size = new System.Drawing.Size(133, 32);
            this.botTwoStatus.TabIndex = 31;
            this.botTwoStatus.Click += new System.EventHandler(this.botTwoStatus_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(654, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pot";
            // 
            // tableRaise
            // 
            this.tableRaise.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tableRaise.Location = new System.Drawing.Point(965, 703);
            this.tableRaise.Name = "tbRaise";
            this.tableRaise.Size = new System.Drawing.Size(108, 20);
            this.tableRaise.TabIndex = 0;
            this.tableRaise.TextChanged += new System.EventHandler(this.tbRaise_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Poker.Properties.Resources.poker_table___Copy;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.tableRaise);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.botTwoStatus);
            this.Controls.Add(this.playerStatus);
            this.Controls.Add(this.botOneStatus);
            this.Controls.Add(this.botThreeStatus);
            this.Controls.Add(this.botFourStatus);
            this.Controls.Add(this.botFiveStatus);
            this.Controls.Add(this.tableBigBlind);
            this.Controls.Add(this.buttonSmallBlind);
            this.Controls.Add(this.tableSmallBlind);
            this.Controls.Add(this.buttonBigBlind);
            this.Controls.Add(this.buttonOptions);
            this.Controls.Add(this.tablePot);
            this.Controls.Add(this.tableChipsBot1);
            this.Controls.Add(this.tableChipsBot2);
            this.Controls.Add(this.tableChipsBot3);
            this.Controls.Add(this.tableChipsBot4);
            this.Controls.Add(this.tableChipsBot5);
            this.Controls.Add(this.tableAdd);
            this.Controls.Add(this.buttonAddChips);
            this.Controls.Add(this.tableChips);
            this.Controls.Add(this.PlayerTurnTimer);
            this.Controls.Add(this.buttonRaise);
            this.Controls.Add(this.buttonCall);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.buttonFold);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "GLS Texas Poker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Layout_Change);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonFold;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.Button buttonCall;
        private System.Windows.Forms.Button buttonRaise;
        private System.Windows.Forms.Button buttonAddChips;
        private System.Windows.Forms.Button buttonSmallBlind;
        private System.Windows.Forms.Button buttonBigBlind;
        private System.Windows.Forms.Button buttonOptions;

        private System.Windows.Forms.ProgressBar PlayerTurnTimer;
        private System.Windows.Forms.TextBox tablePot;
        private System.Windows.Forms.TextBox tableSmallBlind;
        private System.Windows.Forms.TextBox tableBigBlind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tableRaise;
        private System.Windows.Forms.TextBox tableChips;
        private System.Windows.Forms.TextBox tableAdd;

        private System.Windows.Forms.TextBox tableChipsBot1;
        private System.Windows.Forms.TextBox tableChipsBot2;
        private System.Windows.Forms.TextBox tableChipsBot3;
        private System.Windows.Forms.TextBox tableChipsBot4;
        private System.Windows.Forms.TextBox tableChipsBot5;

        private System.Windows.Forms.Label botOneStatus;
        private System.Windows.Forms.Label botTwoStatus;
        private System.Windows.Forms.Label botThreeStatus;
        private System.Windows.Forms.Label playerStatus;
        private System.Windows.Forms.Label botFourStatus;
        private System.Windows.Forms.Label botFiveStatus;


    }
}