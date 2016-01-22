namespace Poker
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    #endregion

    public partial class Form1 : Form
    {
        private const double FloatingPointsTolerance = 0.0000001;

        public Form1()
        {
            this.bools.Add(this.playerFinishedHisTurn);
            this.bools.Add(this.botOneFinishedTurn);
            this.bools.Add(this.botTwoFinishedTurn);
            this.bools.Add(this.botThreeFinishedTurn);
            this.bools.Add(this.botFourFinishedTurn);
            this.bools.Add(this.botFiveFinishedTurn);
            this.call = this.bigBlind;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.updates.Start();
            this.InitializeComponent();
            this.width = this.Width;
            this.height = this.Height;
            this.Shuffle();
            this.tablePot.Enabled = false;
            this.tableChips.Enabled = false;
            this.tableChipsBot1.Enabled = false;
            this.tableChipsBot2.Enabled = false;
            this.tableChipsBot3.Enabled = false;
            this.tableChipsBot4.Enabled = false;
            this.tableChipsBot5.Enabled = false;
            this.tableChips.Text = "Chips : " + this.playerChips;
            this.tableChipsBot1.Text = "Chips : " + this.botOneChips;
            this.tableChipsBot2.Text = "Chips : " + this.botTwoChips;
            this.tableChipsBot3.Text = "Chips : " + this.botThreeChips;
            this.tableChipsBot4.Text = "Chips : " + this.botFourChips;
            this.tableChipsBot5.Text = "Chips : " + this.botFiveChips;
            this.timer.Interval = 1 * 1 * 1000;
            this.timer.Tick += this.timer_Tick;
            this.updates.Interval = 1 * 1 * 100;
            this.updates.Tick += this.Update_Tick;
            this.tableBigBlind.Visible = true;
            this.tableSmallBlind.Visible = true;
            this.buttonBigBlind.Visible = true;
            this.buttonSmallBlind.Visible = true;
            this.tableBigBlind.Visible = true;
            this.tableSmallBlind.Visible = true;
            this.buttonBigBlind.Visible = true;
            this.buttonSmallBlind.Visible = true;
            this.tableBigBlind.Visible = false;
            this.tableSmallBlind.Visible = false;
            this.buttonBigBlind.Visible = false;
            this.buttonSmallBlind.Visible = false;
            this.tableRaise.Text = (this.bigBlind * 2).ToString();
        }

        private static double RoundN(int sChips, int n)
        {
            var a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }

        private async Task Shuffle()
        {
            this.bools.Add(this.playerFinishedHisTurn);
            this.bools.Add(this.botOneFinishedTurn);
            this.bools.Add(this.botTwoFinishedTurn);
            this.bools.Add(this.botThreeFinishedTurn);
            this.bools.Add(this.botFourFinishedTurn);
            this.bools.Add(this.botFiveFinishedTurn);
            this.buttonCall.Enabled = false;
            this.buttonRaise.Enabled = false;
            this.buttonFold.Enabled = false;
            this.buttonCheck.Enabled = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            var check = false;
            var backImage = new Bitmap("Assets\\Back\\Back.png");
            int horizontal = 580, vertical = 480;
            var r = new Random();
            for (this.i = this.imageLocations.Length; this.i > 0; this.i--)
            {
                var j = r.Next(this.i);
                var k = this.imageLocations[j];
                this.imageLocations[j] = this.imageLocations[this.i - 1];
                this.imageLocations[this.i - 1] = k;
            }

            for (this.i = 0; this.i < 17; this.i++)
            {
                this.deck[this.i] = Image.FromFile(this.imageLocations[this.i]);
                var charsToRemove = new[] { "Assets\\Cards\\", ".png" };
                foreach (var c in charsToRemove)
                {
                    this.imageLocations[this.i] = this.imageLocations[this.i].Replace(c, string.Empty);
                }

                this.reserve[this.i] = int.Parse(this.imageLocations[this.i]) - 1;
                this.cardHolder[this.i] = new PictureBox();
                this.cardHolder[this.i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.cardHolder[this.i].Height = 130;
                this.cardHolder[this.i].Width = 80;
                this.Controls.Add(this.cardHolder[this.i]);
                this.cardHolder[this.i].Name = "pb" + this.i;
                await Task.Delay(200);

                if (this.i < 2)
                {
                    if (this.cardHolder[0].Tag != null)
                    {
                        this.cardHolder[1].Tag = this.reserve[1];
                    }

                    this.cardHolder[0].Tag = this.reserve[0];
                    this.cardHolder[this.i].Image = this.deck[this.i];
                    this.cardHolder[this.i].Anchor = AnchorStyles.Bottom;

                    // Holder[i].Dock = DockStyle.Top;
                    this.cardHolder[this.i].Location = new Point(horizontal, vertical);
                    horizontal += this.cardHolder[this.i].Width;
                    this.Controls.Add(this.playerPanel);
                    this.playerPanel.Location = new Point(this.cardHolder[0].Left - 10, this.cardHolder[0].Top - 10);
                    this.playerPanel.BackColor = Color.DarkBlue;
                    this.playerPanel.Height = 150;
                    this.playerPanel.Width = 180;
                    this.playerPanel.Visible = false;
                }

                if (this.botOneChips > 0)
                {
                    this.foldedPlayers--;
                    if (this.i >= 2 && this.i < 4)
                    {
                        if (this.cardHolder[2].Tag != null)
                        {
                            this.cardHolder[3].Tag = this.reserve[3];
                        }

                        this.cardHolder[2].Tag = this.reserve[2];
                        if (!check)
                        {
                            horizontal = 15;
                            vertical = 420;
                        }

                        check = true;
                        this.cardHolder[this.i].Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                        this.cardHolder[this.i].Image = backImage;
                        this.cardHolder[this.i].Image = this.deck[this.i];
                        this.cardHolder[this.i].Location = new Point(horizontal, vertical);
                        horizontal += this.cardHolder[this.i].Width;
                        this.cardHolder[this.i].Visible = true;
                        this.Controls.Add(this.botOnePanel);
                        this.botOnePanel.Location = new Point(this.cardHolder[2].Left - 10, this.cardHolder[2].Top - 10);
                        this.botOnePanel.BackColor = Color.DarkBlue;
                        this.botOnePanel.Height = 150;
                        this.botOnePanel.Width = 180;
                        this.botOnePanel.Visible = false;
                        if (this.i == 3)
                        {
                            check = false;
                        }
                    }
                }

                if (this.botTwoChips > 0)
                {
                    this.foldedPlayers--;
                    if (this.i >= 4 && this.i < 6)
                    {
                        if (this.cardHolder[4].Tag != null)
                        {
                            this.cardHolder[5].Tag = this.reserve[5];
                        }

                        this.cardHolder[4].Tag = this.reserve[4];
                        if (!check)
                        {
                            horizontal = 75;
                            vertical = 65;
                        }

                        check = true;
                        this.cardHolder[this.i].Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        this.cardHolder[this.i].Image = backImage;

                        // Holder[i].Image = Deck[i];
                        this.cardHolder[this.i].Location = new Point(horizontal, vertical);
                        horizontal += this.cardHolder[this.i].Width;
                        this.cardHolder[this.i].Visible = true;
                        this.Controls.Add(this.botTwoPanel);
                        this.botTwoPanel.Location = new Point(this.cardHolder[4].Left - 10, this.cardHolder[4].Top - 10);
                        this.botTwoPanel.BackColor = Color.DarkBlue;
                        this.botTwoPanel.Height = 150;
                        this.botTwoPanel.Width = 180;
                        this.botTwoPanel.Visible = false;
                        if (this.i == 5)
                        {
                            check = false;
                        }
                    }
                }

                if (this.botThreeChips > 0)
                {
                    this.foldedPlayers--;
                    if (this.i >= 6 && this.i < 8)
                    {
                        if (this.cardHolder[6].Tag != null)
                        {
                            this.cardHolder[7].Tag = this.reserve[7];
                        }

                        this.cardHolder[6].Tag = this.reserve[6];
                        if (!check)
                        {
                            horizontal = 590;
                            vertical = 25;
                        }

                        check = true;
                        this.cardHolder[this.i].Anchor = AnchorStyles.Top;
                        this.cardHolder[this.i].Image = backImage;
                        this.cardHolder[this.i].Image = this.deck[this.i];
                        this.cardHolder[this.i].Location = new Point(horizontal, vertical);
                        horizontal += this.cardHolder[this.i].Width;
                        this.cardHolder[this.i].Visible = true;
                        this.Controls.Add(this.botThreePanel);
                        this.botThreePanel.Location = new Point(
                            this.cardHolder[6].Left - 10, 
                            this.cardHolder[6].Top - 10);
                        this.botThreePanel.BackColor = Color.DarkBlue;
                        this.botThreePanel.Height = 150;
                        this.botThreePanel.Width = 180;
                        this.botThreePanel.Visible = false;
                        if (this.i == 7)
                        {
                            check = false;
                        }
                    }
                }

                if (this.botFourChips > 0)
                {
                    this.foldedPlayers--;
                    if (this.i >= 8 && this.i < 10)
                    {
                        if (this.cardHolder[8].Tag != null)
                        {
                            this.cardHolder[9].Tag = this.reserve[9];
                        }

                        this.cardHolder[8].Tag = this.reserve[8];
                        if (!check)
                        {
                            horizontal = 1115;
                            vertical = 65;
                        }

                        check = true;
                        this.cardHolder[this.i].Anchor = AnchorStyles.Top | AnchorStyles.Right;
                        this.cardHolder[this.i].Image = backImage;

                        // Holder[i].Image = Deck[i];
                        this.cardHolder[this.i].Location = new Point(horizontal, vertical);
                        horizontal += this.cardHolder[this.i].Width;
                        this.cardHolder[this.i].Visible = true;
                        this.Controls.Add(this.botFourPanel);
                        this.botFourPanel.Location = new Point(
                            this.cardHolder[8].Left - 10, 
                            this.cardHolder[8].Top - 10);
                        this.botFourPanel.BackColor = Color.DarkBlue;
                        this.botFourPanel.Height = 150;
                        this.botFourPanel.Width = 180;
                        this.botFourPanel.Visible = false;
                        if (this.i == 9)
                        {
                            check = false;
                        }
                    }
                }

                if (this.botFiveChips > 0)
                {
                    this.foldedPlayers--;
                    if (this.i >= 10 && this.i < 12)
                    {
                        if (this.cardHolder[10].Tag != null)
                        {
                            this.cardHolder[11].Tag = this.reserve[11];
                        }

                        this.cardHolder[10].Tag = this.reserve[10];

                        if (!check)
                        {
                            horizontal = 1160;
                            vertical = 420;
                        }

                        check = true;
                        this.cardHolder[this.i].Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                        this.cardHolder[this.i].Image = backImage;
                        this.cardHolder[this.i].Image = this.deck[this.i];
                        this.cardHolder[this.i].Location = new Point(horizontal, vertical);
                        horizontal += this.cardHolder[this.i].Width;
                        this.cardHolder[this.i].Visible = true;
                        this.Controls.Add(this.botFivePanel);
                        this.botFivePanel.Location = new Point(this.cardHolder[10].Left - 10, this.cardHolder[10].Top - 10);
                        this.botFivePanel.BackColor = Color.DarkBlue;
                        this.botFivePanel.Height = 150;
                        this.botFivePanel.Width = 180;
                        this.botFivePanel.Visible = false;

                        if (this.i == 11)
                        {
                            check = false;
                        }
                    }
                }

                if (this.i >= 12)
                {
                    this.cardHolder[12].Tag = this.reserve[12];
                    if (this.i > 12)
                    {
                        this.cardHolder[13].Tag = this.reserve[13];
                    }

                    if (this.i > 13)
                    {
                        this.cardHolder[14].Tag = this.reserve[14];
                    }

                    if (this.i > 14)
                    {
                        this.cardHolder[15].Tag = this.reserve[15];
                    }

                    if (this.i > 15)
                    {
                        this.cardHolder[16].Tag = this.reserve[16];
                    }

                    if (!check)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }

                    check = true;
                    if (this.cardHolder[this.i] != null)
                    {
                        this.cardHolder[this.i].Anchor = AnchorStyles.None;
                        this.cardHolder[this.i].Image = backImage;
                        this.cardHolder[this.i].Image = this.deck[this.i];
                        this.cardHolder[this.i].Location = new Point(horizontal, vertical);
                        horizontal += 110;
                    }
                }

                if (this.botOneChips <= 0)
                {
                    this.botOneFinishedTurn = true;
                    this.cardHolder[2].Visible = false;
                    this.cardHolder[3].Visible = false;
                }
                else
                {
                    this.botOneFinishedTurn = false;
                    if (this.i == 3)
                    {
                        if (this.cardHolder[3] != null)
                        {
                            this.cardHolder[2].Visible = true;
                            this.cardHolder[3].Visible = true;
                        }
                    }
                }

                if (this.botTwoChips <= 0)
                {
                    this.botTwoFinishedTurn = true;
                    this.cardHolder[4].Visible = false;
                    this.cardHolder[5].Visible = false;
                }
                else
                {
                    this.botTwoFinishedTurn = false;

                    if (this.i == 5)
                    {
                        if (this.cardHolder[5] != null)
                        {
                            this.cardHolder[4].Visible = true;
                            this.cardHolder[5].Visible = true;
                        }
                    }
                }

                if (this.botThreeChips <= 0)
                {
                    this.botThreeFinishedTurn = true;
                    this.cardHolder[6].Visible = false;
                    this.cardHolder[7].Visible = false;
                }
                else
                {
                    this.botThreeFinishedTurn = false;

                    if (this.i == 7)
                    {
                        if (this.cardHolder[7] != null)
                        {
                            this.cardHolder[6].Visible = true;
                            this.cardHolder[7].Visible = true;
                        }
                    }
                }

                if (this.botFourChips <= 0)
                {
                    this.botFourFinishedTurn = true;
                    this.cardHolder[8].Visible = false;
                    this.cardHolder[9].Visible = false;
                }
                else
                {
                    this.botFourFinishedTurn = false;

                    if (this.i == 9)
                    {
                        if (this.cardHolder[9] != null)
                        {
                            this.cardHolder[8].Visible = true;
                            this.cardHolder[9].Visible = true;
                        }
                    }
                }

                if (this.botFiveChips <= 0)
                {
                    this.botFiveFinishedTurn = true;
                    this.cardHolder[10].Visible = false;
                    this.cardHolder[11].Visible = false;
                }
                else
                {
                    this.botFiveFinishedTurn = false;

                    if (this.i == 11)
                    {
                        if (this.cardHolder[11] != null)
                        {
                            this.cardHolder[10].Visible = true;
                            this.cardHolder[11].Visible = true;
                        }
                    }
                }

                if (this.i != 16)
                {
                    continue;
                }

                if (!this.restart)
                {
                    this.MaximizeBox = true;
                    this.MinimizeBox = true;
                }

                this.timer.Start();
            }

            if (this.foldedPlayers == 5)
            {
                // TODO replace the text with constants
                var dialogResult = MessageBox.Show(
                    "Would You Like To Play Again ?", 
                    "You Won , Congratulations ! ", 
                    MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
                this.foldedPlayers = 5;
            }

            if (this.i == 17)
            {
                this.buttonRaise.Enabled = true;
                this.buttonCall.Enabled = true;
                this.buttonRaise.Enabled = true;
                this.buttonRaise.Enabled = true;
                this.buttonFold.Enabled = true;
            }
        }

        private async Task Turns()
        {
            if (!this.playerFinishedHisTurn)
            {
                if (this.playerTurn)
                {
                    this.FixCall(this.playerStatus, ref this.playerCall, ref this.playerRaise, 1);

                    // MessageBox.Show("Player's Turn");
                    this.PlayerTurnTimer.Visible = true;
                    this.PlayerTurnTimer.Value = 1000;
                    this.t = 60;
                    this.up = 10000000;
                    this.timer.Start();
                    this.buttonRaise.Enabled = true;
                    this.buttonCall.Enabled = true;
                    this.buttonRaise.Enabled = true;
                    this.buttonRaise.Enabled = true;
                    this.buttonFold.Enabled = true;
                    this.turnCount++;
                    this.FixCall(this.playerStatus, ref this.playerCall, ref this.playerRaise, 2);
                }
            }

            if (this.playerFinishedHisTurn || !this.playerTurn)
            {
                await this.AllIn();

                if (this.playerFinishedHisTurn && !this.playerFolded)
                {
                    if (this.buttonCall.Text.Contains("All in") == false || this.buttonRaise.Text.Contains("All in") == false)
                    {
                        this.bools.RemoveAt(0);
                        this.bools.Insert(0, null);
                        this.maxLeft--;
                        this.playerFolded = true;
                    }
                }

                await this.CheckRaise(0, 0);
                this.PlayerTurnTimer.Visible = false;
                this.buttonRaise.Enabled = false;
                this.buttonCall.Enabled = false;
                this.buttonRaise.Enabled = false;
                this.buttonRaise.Enabled = false;
                this.buttonFold.Enabled = false;
                this.timer.Stop();
                this.botOneTurn = true;

                if (!this.botOneFinishedTurn)
                {
                    if (this.botOneTurn)
                    {
                        this.FixCall(this.botOneStatus, ref this.botOneCall, ref this.botOneRaise, 1);
                        this.FixCall(this.botOneStatus, ref this.botOneCall, ref this.botOneRaise, 2);
                        this.Rules(2, 3, "Bot 1", ref this.botOneType, ref this.botOnePower, this.botOneFinishedTurn);

                        // TODO replace the text with constants
                        MessageBox.Show("Bot 1's Turn");
                        this.Ai(
                            2, 
                            3, 
                            ref this.botOneChips, 
                            ref this.botOneTurn, 
                            ref this.botOneFinishedTurn, 
                            this.botOneStatus, 
                            0, 
                            this.botOnePower, 
                            this.botOneType);
                        this.turnCount++;
                        this.last = 1;
                        this.botOneTurn = false;
                        this.botTwoTurn = true;
                    }
                }

                if (this.botOneFinishedTurn && !this.botOneFolded)
                {
                    this.bools.RemoveAt(1);
                    this.bools.Insert(1, null);
                    this.maxLeft--;
                    this.botOneFolded = true;
                }

                if (this.botOneFinishedTurn || !this.botOneTurn)
                {
                    await this.CheckRaise(1, 1);
                    this.botTwoTurn = true;
                }

                if (!this.botTwoFinishedTurn)
                {
                    if (this.botTwoTurn)
                    {
                        this.FixCall(this.botTwoStatus, ref this.botTwoCall, ref this.botTwoRaise, 1);
                        this.FixCall(this.botTwoStatus, ref this.botTwoCall, ref this.botTwoRaise, 2);
                        this.Rules(4, 5, "Bot 2", ref this.botTwoType, ref this.botTwoPower, this.botTwoFinishedTurn);

                        // TODO replace the text with constants
                        MessageBox.Show("Bot 2's Turn");
                        this.Ai(
                            4, 
                            5, 
                            ref this.botTwoChips, 
                            ref this.botTwoTurn, 
                            ref this.botTwoFinishedTurn, 
                            this.botTwoStatus, 
                            1, 
                            this.botTwoPower, 
                            this.botTwoType);
                        this.turnCount++;
                        this.last = 2;
                        this.botTwoTurn = false;
                        this.botThreeTurn = true;
                    }
                }

                if (this.botTwoFinishedTurn && !this.botTwoFolded)
                {
                    this.bools.RemoveAt(2);
                    this.bools.Insert(2, null);
                    this.maxLeft--;
                    this.botTwoFolded = true;
                }

                if (this.botTwoFinishedTurn || !this.botTwoTurn)
                {
                    await this.CheckRaise(2, 2);
                    this.botThreeTurn = true;
                }

                if (!this.botThreeFinishedTurn)
                {
                    if (this.botThreeTurn)
                    {
                        this.FixCall(this.botThreeStatus, ref this.botThreeCall, ref this.botThreeRaise, 1);
                        this.FixCall(this.botThreeStatus, ref this.botThreeCall, ref this.botThreeRaise, 2);
                        this.Rules(
                            6, 
                            7, 
                            "Bot 3", 
                            ref this.botThreeType, 
                            ref this.botThreePower, 
                            this.botThreeFinishedTurn);

                        // TODO replace the text with constants
                        MessageBox.Show("Bot 3's Turn");
                        this.Ai(
                            6, 
                            7, 
                            ref this.botThreeChips, 
                            ref this.botThreeTurn, 
                            ref this.botThreeFinishedTurn, 
                            this.botThreeStatus, 
                            2, 
                            this.botThreePower, 
                            this.botThreeType);
                        this.turnCount++;
                        this.last = 3;
                        this.botThreeTurn = false;
                        this.botFourTurn = true;
                    }
                }

                if (this.botThreeFinishedTurn && !this.botThreeFolded)
                {
                    this.bools.RemoveAt(3);
                    this.bools.Insert(3, null);
                    this.maxLeft--;
                    this.botThreeFolded = true;
                }

                if (this.botThreeFinishedTurn || !this.botThreeTurn)
                {
                    await this.CheckRaise(3, 3);
                    this.botFourTurn = true;
                }

                if (!this.botFourFinishedTurn)
                {
                    if (this.botFourTurn)
                    {
                        this.FixCall(this.botFourStatus, ref this.botFourCall, ref this.botFourRaise, 1);
                        this.FixCall(this.botFourStatus, ref this.botFourCall, ref this.botFourRaise, 2);
                        this.Rules(8, 9, "Bot 4", ref this.botFourType, ref this.botFourPower, this.botFourFinishedTurn);

                        // TODO replace the text with constants
                        MessageBox.Show("Bot 4's Turn");
                        this.Ai(
                            8, 
                            9, 
                            ref this.botFourChips, 
                            ref this.botFourTurn, 
                            ref this.botFourFinishedTurn, 
                            this.botFourStatus, 
                            3, 
                            this.botFourPower, 
                            this.botFourType);
                        this.turnCount++;
                        this.last = 4;
                        this.botFourTurn = false;
                        this.botFiveTurn = true;
                    }
                }

                if (this.botFourFinishedTurn && !this.botFourFolded)
                {
                    this.bools.RemoveAt(4);
                    this.bools.Insert(4, null);
                    this.maxLeft--;
                    this.botFourFolded = true;
                }

                if (this.botFourFinishedTurn || !this.botFourTurn)
                {
                    await this.CheckRaise(4, 4);
                    this.botFiveTurn = true;
                }

                if (!this.botFiveFinishedTurn)
                {
                    if (this.botFiveTurn)
                    {
                        this.FixCall(this.botFiveStatus, ref this.botFiveCall, ref this.botFiveRaise, 1);
                        this.FixCall(this.botFiveStatus, ref this.botFiveCall, ref this.botFiveRaise, 2);
                        this.Rules(
                            10, 
                            11, 
                            "Bot 5", 
                            ref this.botFiveType, 
                            ref this.botFivePower, 
                            this.botFiveFinishedTurn);

                        // TODO replace the text with constants
                        MessageBox.Show("Bot 5's Turn");
                        this.Ai(
                            10, 
                            11, 
                            ref this.botFiveChips, 
                            ref this.botFiveTurn, 
                            ref this.botFiveFinishedTurn, 
                            this.botFiveStatus, 
                            4, 
                            this.botFivePower, 
                            this.botFiveType);
                        this.turnCount++;
                        this.last = 5;
                        this.botFiveTurn = false;
                    }
                }

                if (this.botFiveFinishedTurn && !this.botFiveFolded)
                {
                    this.bools.RemoveAt(5);
                    this.bools.Insert(5, null);
                    this.maxLeft--;
                    this.botFiveFolded = true;
                }

                if (this.botFiveFinishedTurn || !this.botFiveTurn)
                {
                    await this.CheckRaise(5, 5);
                    this.playerTurn = true;
                }

                if (this.playerFinishedHisTurn && !this.playerFolded)
                {
                    if (this.buttonCall.Text.Contains("All in") == false || this.buttonRaise.Text.Contains("All in") == false)
                    {
                        this.bools.RemoveAt(0);
                        this.bools.Insert(0, null);
                        this.maxLeft--;
                        this.playerFolded = true;
                    }
                }

                await this.AllIn();
                if (!this.restart)
                {
                    await this.Turns();
                }

                this.restart = false;
            }
        }

        private void Rules(int c1, int c2, string currentText, ref double current, ref double power, bool foldedTurn)
        {
            if (c1 == 0 && c2 == 1)
            {
            }

            // TODO Group conditions and put parenthesis
            if (!foldedTurn || c1 == 0 && c2 == 1 && this.playerStatus.Text.Contains("Fold") == false)
            {
                bool done = false, vf = false;
                var Straight1 = new int[5];
                var Straight = new int[7];
                Straight[0] = this.reserve[c1];
                Straight[1] = this.reserve[c2];
                Straight1[0] = Straight[2] = this.reserve[12];
                Straight1[1] = Straight[3] = this.reserve[13];
                Straight1[2] = Straight[4] = this.reserve[14];
                Straight1[3] = Straight[5] = this.reserve[15];
                Straight1[4] = Straight[6] = this.reserve[16];
                var a = Straight.Where(o => o % 4 == 0).ToArray();
                var b = Straight.Where(o => o % 4 == 1).ToArray();
                var c = Straight.Where(o => o % 4 == 2).ToArray();
                var d = Straight.Where(o => o % 4 == 3).ToArray();
                var straightOne = a.Select(o => o / 4).Distinct().ToArray();
                var straightTwo = b.Select(o => o / 4).Distinct().ToArray();
                var straightThree = c.Select(o => o / 4).Distinct().ToArray();
                var straightFour = d.Select(o => o / 4).Distinct().ToArray();
                Array.Sort(Straight);
                Array.Sort(straightOne);
                Array.Sort(straightTwo);
                Array.Sort(straightThree);
                Array.Sort(straightFour);

                for (this.i = 0; this.i < 16; this.i++)
                {
                    if (this.reserve[this.i] == int.Parse(this.cardHolder[c1].Tag.ToString())
                        && this.reserve[this.i + 1] == int.Parse(this.cardHolder[c2].Tag.ToString()))
                    {
                        // Pair from Hand current = 1
                        this.RPairFromHand(ref current, ref power);
                        this.RPairTwoPair(ref current, ref power);

                        this.PairOfTwo(ref current, ref power);

                        

                        this.RThreeOfAKind(ref current, ref power, Straight);

                        

                        #region Straight current = 4

                        this.RStraight(ref current, ref power, Straight);

                        #endregion

                        #region Flush current = 5 || 5.5

                        this.RFlush(ref current, ref power, ref vf, Straight1);

                        #endregion

                        #region Full House current = 6

                        this.RFullHouse(ref current, ref power, ref done, Straight);

                        #endregion

                        #region Four of a Kind current = 7

                        this.RFourOfAKind(ref current, ref power, Straight);

                        #endregion

                        #region Straight Flush current = 8 || 9

                        this.RStraightFlush(ref current, ref power, straightOne, straightTwo, straightThree, straightFour);

                        #endregion

                        #region High Card current = -1

                        this.RHighCard(ref current, ref power);

                        #endregion
                    }
                }
            }
        }

        private void RStraightFlush(ref double currentHand, ref double power, int[] straightSpades, int[] straightClubs, int[] straightDiamonds, int[] straightHearts)
        {
            if (currentHand >= -1)
            {
                if (straightSpades.Length >= 5)
                {
                    if (straightSpades[0] + 4 == straightSpades[4])
                    {
                        currentHand = 8;
                        power = straightSpades.Max() / 4 + currentHand * 100;
                        this.win.Add(new Type() { Power = power, CurrentHand = 8 });
                        this.sorted = this.win.OrderByDescending(op1 => op1.CurrentHand).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (straightSpades[0] == 0 && straightSpades[1] == 9 && straightSpades[2] == 10 && straightSpades[3] == 11 && straightSpades[0] + 12 == straightSpades[4])
                    {
                        currentHand = 9;
                        power = straightSpades.Max() / 4 + currentHand * 100;
                        this.win.Add(new Type() { Power = power, CurrentHand = 9 });
                        this.sorted =
                            this.win.OrderByDescending(op1 => op1.CurrentHand).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (straightClubs.Length >= 5)
                {
                    if (straightClubs[0] + 4 == straightClubs[4])
                    {
                        currentHand = 8;
                        power = straightClubs.Max() / 4 + currentHand * 100;
                        this.win.Add(new Type() { Power = power, CurrentHand = 8 });
                        this.sorted =
                            this.win.OrderByDescending(op1 => op1.CurrentHand).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (straightClubs[0] == 0 && straightClubs[1] == 9 && straightClubs[2] == 10 && straightClubs[3] == 11 && straightClubs[0] + 12 == straightClubs[4])
                    {
                        currentHand = 9;
                        power = straightClubs.Max() / 4 + currentHand * 100;
                        this.win.Add(new Type() { Power = power, CurrentHand = 9 });
                        this.sorted =
                            this.win.OrderByDescending(op1 => op1.CurrentHand).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (straightDiamonds.Length >= 5)
                {
                    if (straightDiamonds[0] + 4 == straightDiamonds[4])
                    {
                        currentHand = 8;
                        power = straightDiamonds.Max() / 4 + currentHand * 100;
                        this.win.Add(new Type() { Power = power, CurrentHand = 8 });
                        this.sorted =
                            this.win.OrderByDescending(op1 => op1.CurrentHand).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (straightDiamonds[0] == 0 && straightDiamonds[1] == 9 && straightDiamonds[2] == 10 && straightDiamonds[3] == 11 && straightDiamonds[0] + 12 == straightDiamonds[4])
                    {
                        currentHand = 9;
                        power = straightDiamonds.Max() / 4 + currentHand * 100;
                        this.win.Add(new Type() { Power = power, CurrentHand = 9 });
                        this.sorted =
                            this.win.OrderByDescending(op1 => op1.CurrentHand).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (straightHearts.Length >= 5)
                {
                    if (straightHearts[0] + 4 == straightHearts[4])
                    {
                        currentHand = 8;
                        power = straightHearts.Max() / 4 + currentHand * 100;
                        this.win.Add(new Type() { Power = power, CurrentHand = 8 });
                        this.sorted =
                            this.win.OrderByDescending(op1 => op1.CurrentHand).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (straightHearts[0] == 0 && straightHearts[1] == 9 && straightHearts[2] == 10 && straightHearts[3] == 11 && straightHearts[0] + 12 == straightHearts[4])
                    {
                        currentHand = 9;
                        power = straightHearts.Max() / 4 + currentHand * 100;
                        this.win.Add(new Type() { Power = power, CurrentHand = 9 });
                        this.sorted =
                            this.win.OrderByDescending(op1 => op1.CurrentHand).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        private void RFourOfAKind(ref double current, ref double power, IReadOnlyList<int> handCards)
        {
            if (current >= -1)
            {
                for (var cardsIndex = 0; cardsIndex <= 3; cardsIndex++)
                {
                    if (handCards[cardsIndex] / 4 == handCards[cardsIndex + 1] / 4 && handCards[cardsIndex] / 4 == handCards[cardsIndex + 2] / 4
                        && handCards[cardsIndex] / 4 == handCards[cardsIndex + 3] / 4)
                    {
                        current = 7;

                        // TODO group conditions and put parenthesis
                        power = (handCards[cardsIndex] / 4) * 4 + current * 100;
                        this.win.Add(new Type() { Power = power, CurrentHand = 7 });
                        this.sorted =
                            this.win.OrderByDescending(op1 => op1.CurrentHand).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (handCards[cardsIndex] / 4 != 0 || handCards[cardsIndex + 1] / 4 != 0 || handCards[cardsIndex + 2] / 4 != 0
                        || handCards[cardsIndex + 3] / 4 != 0)
                    {
                        continue;
                    }

                    current = 7;

                    // TODO group conditions and put parenthesis
                    power = 13 * 4 + current * 100;
                    this.win.Add(new Type() { Power = power, CurrentHand = 7 });
                    this.sorted =
                        this.win.OrderByDescending(op1 => op1.CurrentHand).ThenByDescending(op1 => op1.Power).First();
                }
            }
        }

        private void RFullHouse(ref double current, ref double power, ref bool done, int[] handCards)
        {
            if (!(current >= -1))
            {
                return;
            }

            this.type = power;
            for (var j = 0; j <= 12; j++)
            {
                var fh = handCards.Where(o => o / 4 == j).ToArray();
                if (fh.Length == 3 || done)
                {
                    if (fh.Length == 2)
                    {
                        if (fh.Max() / 4 == 0)
                        {
                            current = 6;

                            // TODO group conditions and put parenthesis, replace magic numbers
                            power = 13 * 2 + current * 100;
                            this.win.Add(new Type() { Power = power, CurrentHand = 6 });
                            this.sorted =
                                this.win.OrderByDescending(op1 => op1.CurrentHand)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            break;
                        }

                        if (fh.Max() / 4 > 0)
                        {
                            current = 6;

                            // TODO group conditions and put parenthesis, replace magic numbers
                            power = fh.Max() / 4 * 2 + current * 100;
                            this.win.Add(new Type() { Power = power, CurrentHand = 6 });
                            this.sorted =
                                this.win.OrderByDescending(cards => cards.CurrentHand)
                                    .ThenByDescending(cards => cards.Power)
                                    .First();
                            break;
                        }
                    }

                    if (done)
                    {
                        continue;
                    }

                    if (fh.Max() / 4 == 0)
                    {
                        power = 13;
                        done = true;
                        j = -1;
                    }
                    else
                    {
                        power = fh.Max() / 4d;
                        done = true;
                        j = -1;
                    }
                }
            }

            if (Math.Abs(current - 6) > FloatingPointsTolerance)
            {
                power = this.type;
            }
        }

        private void RFlush(ref double current, ref double powerOfHand, ref bool vf, int[] handCards)
        {
            if (!(current >= -1))
            {
                return;
            }

            var flushHearts = handCards.Where(o => o % 4 == 0).ToArray();
            var flushClubs = handCards.Where(o => o % 4 == 1).ToArray();
            var flushDiamonds = handCards.Where(o => o % 4 == 2).ToArray();
            var flushSpades = handCards.Where(o => o % 4 == 3).ToArray();

            if (flushHearts.Length == 3 || flushHearts.Length == 4)
            {
                if (this.reserve[this.i] % 4 == this.reserve[this.i + 1] % 4 && this.reserve[this.i] % 4 == flushHearts[0] % 4)
                {
                    if (this.reserve[this.i] / 4 > flushHearts.Max() / 4)
                    {
                        current = 5;

                        // TODO group conditions and put parenthesis, replace magic numbers
                        powerOfHand = this.reserve[this.i] + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                                          .OrderByDescending(cards => cards.CurrentHand)
                                          .ThenByDescending(cards => cards.Power)
                                          .First();
                        vf = true;
                    }

                    if (this.reserve[this.i + 1] / 4 > flushHearts.Max() / 4)
                    {
                        current = 5;

                        // TODO group conditions and put parenthesis, replace magic numbers
                        // TODO Code repetition - Create method and put it here
                        powerOfHand = this.reserve[this.i + 1] + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                               .OrderByDescending(cards => cards.CurrentHand)
                               .ThenByDescending(cards => cards.Power)
                               .First();
                        vf = true;
                    }

                    else if (this.reserve[this.i] / 4 < flushHearts.Max() / 4 && this.reserve[this.i + 1] / 4 < flushHearts.Max() / 4)
                    {
                        current = 5;

                        // TODO group conditions and put parenthesis, replace magic numbers
                        // TODO Code repetition - Create method and put it here
                        powerOfHand = flushHearts.Max() + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                                .OrderByDescending(cards => cards.CurrentHand)
                                .ThenByDescending(cards => cards.Power)
                                .First();
                        vf = true;
                    }
                }
            }

            if (flushHearts.Length == 4)
            {
                // different cards in hand
                if (this.reserve[this.i] % 4 != this.reserve[this.i + 1] % 4 && this.reserve[this.i] % 4 == flushHearts[0] % 4)
                {
                    if (this.reserve[this.i] / 4 > flushHearts.Max() / 4)
                    {
                        // TODO group conditions and put parenthesis, replace magic numbers
                        // TODO Code repetition - Create method and put it here
                        current = 5;
                        powerOfHand = this.reserve[this.i] + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                                .OrderByDescending(cards => cards.CurrentHand)
                                .ThenByDescending(cards => cards.Power)
                                .First();
                        vf = true;
                    }
                    else
                    {
                        current = 5;

                        // TODO group conditions and put parenthesis, replace magic numbers
                        // TODO Code repetition - Create method and put it here
                        powerOfHand = flushHearts.Max() + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                                .OrderByDescending(cards => cards.CurrentHand)
                                .ThenByDescending(cards => cards.Power)
                                .First();
                        vf = true;
                    }
                }

                if (this.reserve[this.i + 1] % 4 != this.reserve[this.i] % 4
                    && this.reserve[this.i + 1] % 4 == flushHearts[0] % 4)
                {
                    if (this.reserve[this.i + 1] / 4 > flushHearts.Max() / 4)
                    {
                        current = 5;

                        // TODO group conditions and put parenthesis, replace magic numbers
                        // TODO Code repetition - Create method and put it here
                        powerOfHand = this.reserve[this.i + 1] + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                                .OrderByDescending(cards => cards.CurrentHand)
                                .ThenByDescending(cards => cards.Power)
                                .First();
                        vf = true;
                    }
                    else
                    {
                        current = 5;

                        // TODO group conditions and put parenthesis, replace magic numbers
                        // TODO Code repetition - Create method and put it here
                        powerOfHand = flushHearts.Max() + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                                .OrderByDescending(cards => cards.CurrentHand)
                                .ThenByDescending(cards => cards.Power)
                                .First();
                        vf = true;
                    }
                }
            }

            if (flushHearts.Length == 5)
            {
                if (this.reserve[this.i] % 4 == flushHearts[0] % 4 && this.reserve[this.i] / 4 > flushHearts.Min() / 4)
                {
                    current = 5;

                    // TODO group conditions and put parenthesis, replace magic numbers
                    // TODO Code repetition - Create method and put it here
                    powerOfHand = this.reserve[this.i] + current * 100;
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                    this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                    vf = true;
                }

                if (this.reserve[this.i + 1] % 4 == flushHearts[0] % 4 && this.reserve[this.i + 1] / 4 > flushHearts.Min() / 4)
                {
                    current = 5;

                    // TODO group conditions and put parenthesis, replace magic numbers
                    // TODO Code repetition - Create method and put it here
                    powerOfHand = this.reserve[this.i + 1] + current * 100;
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                    this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                    vf = true;
                }

                else if (this.reserve[this.i] / 4 < flushHearts.Min() / 4 && this.reserve[this.i + 1] / 4 < flushHearts.Min()) // Put theese in the brackets in variables
                {
                    current = 5;
                    powerOfHand = flushHearts.Max() + current * 100;

                    // TODO group conditions and put parenthesis, replace magic numbers
                    // TODO Code repetition - Create method and put it here
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                    this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                    vf = true;
                }
            }

            if (flushClubs.Length == 3 || flushClubs.Length == 4)
            {
                if (this.reserve[this.i] % 4 == this.reserve[this.i + 1] % 4 && this.reserve[this.i] % 4 == flushClubs[0] % 4)
                {
                    if (this.reserve[this.i] / 4 > flushClubs.Max() / 4)
                    {
                        current = 5;

                        // TODO group conditions and put parenthesis, replace magic numbers
                        // TODO Code repetition - Create method and put it here
                        powerOfHand = this.reserve[this.i] + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                                .OrderByDescending(cards => cards.CurrentHand)
                                .ThenByDescending(cards => cards.Power)
                                .First();
                        vf = true;
                    }

                    if (this.reserve[this.i + 1] / 4 > flushClubs.Max() / 4)
                    {
                        current = 5;

                        // TODO group conditions and put parenthesis, replace magic numbers
                        // TODO Code repetition - Create method and put it here
                        powerOfHand = this.reserve[this.i + 1] + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                                .OrderByDescending(cards => cards.CurrentHand)
                                .ThenByDescending(cards => cards.Power)
                                .First();
                        vf = true;
                    }
                    else if (this.reserve[this.i] / 4 < flushClubs.Max() / 4 && this.reserve[this.i + 1] / 4 < flushClubs.Max() / 4)
                    {
                        current = 5;

                        // TODO group conditions and put parenthesis, replace magic numbers
                        // TODO Code repetition - Create method and put it here
                        powerOfHand = flushClubs.Max() + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                                .OrderByDescending(cards => cards.CurrentHand)
                                .ThenByDescending(cards => cards.Power)
                                .First();
                        vf = true;
                    }
                }
            }

            if (flushClubs.Length == 4)
            {
                // different cards in hand
                if (this.reserve[this.i] % 4 != this.reserve[this.i + 1] % 4 && this.reserve[this.i] % 4 == flushClubs[0] % 4)
                {
                    if (this.reserve[this.i] / 4 > flushClubs.Max() / 4)
                    {
                        current = 5;

                        // TODO group conditions and put parenthesis, replace magic numbers
                        // TODO Code repetition - Create method and put it here
                        powerOfHand = this.reserve[this.i] + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                                .OrderByDescending(cards => cards.CurrentHand)
                                .ThenByDescending(cards => cards.Power)
                                .First();
                        vf = true;
                    }
                    else
                    {
                        current = 5;

                        // TODO group conditions and put parenthesis, replace magic numbers
                        // TODO Code repetition - Create method and put it here
                        powerOfHand = flushClubs.Max() + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                        vf = true;
                    }
                }

                if (this.reserve[this.i + 1] % 4 != this.reserve[this.i] % 4
                    && this.reserve[this.i + 1] % 4 == flushClubs[0] % 4)
                {
                    if (this.reserve[this.i + 1] / 4 > flushClubs.Max() / 4)
                    {
                        current = 5;

                        // TODO group conditions and put parenthesis, replace magic numbers
                        // TODO Code repetition - Create method and put it here
                        powerOfHand = this.reserve[this.i + 1] + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                        vf = true;
                    }
                    else
                    {
                        current = 5;

                        // TODO group conditions and put parenthesis, replace magic numbers
                        // TODO Code repetition - Create method and put it here
                        powerOfHand = flushClubs.Max() + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                        vf = true;
                    }
                }
            }

            if (flushClubs.Length == 5)
            {
                if (this.reserve[this.i] % 4 == flushClubs[0] % 4 && this.reserve[this.i] / 4 > flushClubs.Min() / 4)
                {
                    current = 5;

                    // TODO group conditions and put parenthesis, replace magic numbers
                    // TODO Code repetition - Create method and put it here
                    powerOfHand = this.reserve[this.i] + current * 100;
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                    this.sorted = this.win
                        .OrderByDescending(cards => cards.CurrentHand)
                        .ThenByDescending(cards => cards.Power)
                        .First();
                    vf = true;
                }

                if (this.reserve[this.i + 1] % 4 == flushClubs[0] % 4 && this.reserve[this.i + 1] / 4 > flushClubs.Min() / 4)
                {
                    current = 5;

                    // TODO group conditions and put parenthesis, replace magic numbers
                    // TODO Code repetition - Create method and put it here
                    powerOfHand = this.reserve[this.i + 1] + current * 100;
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                    this.sorted = this.win
                        .OrderByDescending(cards => cards.CurrentHand)
                        .ThenByDescending(cards => cards.Power)
                        .First();
                    vf = true;
                }
                else if (this.reserve[this.i] / 4 < flushClubs.Min() / 4 && this.reserve[this.i + 1] / 4 < flushClubs.Min())
                {
                    current = 5;

                    // TODO group conditions and put parenthesis, replace magic numbers
                    // TODO Code repetition - Create method and put it here
                    powerOfHand = flushClubs.Max() + current * 100;
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                    this.sorted = this.win
                        .OrderByDescending(cards => cards.CurrentHand)
                        .ThenByDescending(cards => cards.Power)
                        .First();
                    vf = true;
                }
            }

            if (flushDiamonds.Length == 3 || flushDiamonds.Length == 4)
            {
                if (this.reserve[this.i] % 4 == this.reserve[this.i + 1] % 4 && this.reserve[this.i] % 4 == flushDiamonds[0] % 4)
                {
                    if (this.reserve[this.i] / 4 > flushDiamonds.Max() / 4)
                    {
                        current = 5;

                        // TODO group conditions and put parenthesis, replace magic numbers
                        // TODO Code repetition - Create method and put it here
                        powerOfHand = this.reserve[this.i] + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                        vf = true;
                    }

                    if (this.reserve[this.i + 1] / 4 > flushDiamonds.Max() / 4)
                    {
                        current = 5;

                        // TODO group conditions and put parenthesis, replace magic numbers
                        // TODO Code repetition - Create method and put it here
                        powerOfHand = this.reserve[this.i + 1] + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                        vf = true;
                    }
                    else if (this.reserve[this.i] / 4 < flushDiamonds.Max() / 4 && this.reserve[this.i + 1] / 4 < flushDiamonds.Max() / 4)
                    {
                        current = 5;

                        // TODO group conditions and put parenthesis, replace magic numbers
                        // TODO Code repetition - Create method and put it here
                        powerOfHand = flushDiamonds.Max() + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                        vf = true;
                    }
                }
            }

            if (flushDiamonds.Length == 4)
            {
                // different cards in hand
                if (this.reserve[this.i] % 4 != this.reserve[this.i + 1] % 4 && this.reserve[this.i] % 4 == flushDiamonds[0] % 4)
                {
                    if (this.reserve[this.i] / 4 > flushDiamonds.Max() / 4)
                    {
                        current = 5;

                        // TODO group conditions and put parenthesis, replace magic numbers
                        // TODO Code repetition - Create method and put it here
                        powerOfHand = this.reserve[this.i] + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                        vf = true;
                    }
                    else
                    {
                        current = 5;

                        powerOfHand = flushDiamonds.Max() + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                        vf = true;
                    }
                }

                if (this.reserve[this.i + 1] % 4 != this.reserve[this.i] % 4
                    && this.reserve[this.i + 1] % 4 == flushDiamonds[0] % 4)
                {
                    if (this.reserve[this.i + 1] / 4 > flushDiamonds.Max() / 4)
                    {
                        current = 5;

                        // TODO group conditions and put parenthesis, replace magic numbers
                        // TODO Code repetition - Create method and put it here
                        powerOfHand = this.reserve[this.i + 1] + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                        vf = true;
                    }
                    else
                    {
                        current = 5;
                        powerOfHand = flushDiamonds.Max() + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                        vf = true;
                    }
                }
            }

            // TODO group conditions and put parenthesis, replace magic numbers
            // TODO Code repetition - Create method and put it here
            if (flushDiamonds.Length == 5)
            {
                if (this.reserve[this.i] % 4 == flushDiamonds[0] % 4 && this.reserve[this.i] / 4 > flushDiamonds.Min() / 4)
                {
                    current = 5;
                    powerOfHand = this.reserve[this.i] + current * 100;
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                    this.sorted = this.win
                        .OrderByDescending(cards => cards.CurrentHand)
                        .ThenByDescending(cards => cards.Power)
                        .First();
                    vf = true;
                }

                if (this.reserve[this.i + 1] % 4 == flushDiamonds[0] % 4 && this.reserve[this.i + 1] / 4 > flushDiamonds.Min() / 4)
                {
                    current = 5;
                    powerOfHand = this.reserve[this.i + 1] + current * 100;
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                    this.sorted = this.win
                        .OrderByDescending(cards => cards.CurrentHand)
                        .ThenByDescending(cards => cards.Power)
                        .First();
                    vf = true;
                }
                else if (this.reserve[this.i] / 4 < flushDiamonds.Min() / 4 && this.reserve[this.i + 1] / 4 < flushDiamonds.Min())
                {
                    current = 5;
                    powerOfHand = flushDiamonds.Max() + current * 100;
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                    this.sorted = this.win
                        .OrderByDescending(cards => cards.CurrentHand)
                        .ThenByDescending(cards => cards.Power)
                        .First();
                    vf = true;
                }
            }

            // TODO group conditions and put parenthesis, replace magic numbers
            // TODO Code repetition - Create method and put it here
            if (flushSpades.Length == 3 || flushSpades.Length == 4)
            {
                if (this.reserve[this.i] % 4 == this.reserve[this.i + 1] % 4 && this.reserve[this.i] % 4 == flushSpades[0] % 4)
                {
                    if (this.reserve[this.i] / 4 > flushSpades.Max() / 4)
                    {
                        current = 5;
                        powerOfHand = this.reserve[this.i] + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                        vf = true;
                    }

                    if (this.reserve[this.i + 1] / 4 > flushSpades.Max() / 4)
                    {
                        current = 5;
                        powerOfHand = this.reserve[this.i + 1] + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                        vf = true;
                    }
                    else if (this.reserve[this.i] / 4 < flushSpades.Max() / 4 && this.reserve[this.i + 1] / 4 < flushSpades.Max() / 4)
                    {
                        current = 5;
                        powerOfHand = flushSpades.Max() + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                        vf = true;
                    }
                }
            }

            // TODO group conditions and put parenthesis, replace magic numbers
            // TODO Code repetition - Create method and put it here
            if (flushSpades.Length == 4)
            {
                // different cards in hand
                if (this.reserve[this.i] % 4 != this.reserve[this.i + 1] % 4 && this.reserve[this.i] % 4 == flushSpades[0] % 4)
                {
                    if (this.reserve[this.i] / 4 > flushSpades.Max() / 4)
                    {
                        current = 5;
                        powerOfHand = this.reserve[this.i] + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                        vf = true;
                    }
                    else
                    {
                        current = 5;
                        powerOfHand = flushSpades.Max() + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                        vf = true;
                    }
                }

                // TODO group conditions and put parenthesis, replace magic numbers
                // TODO Code repetition - Create method and put it here
                if (this.reserve[this.i + 1] % 4 != this.reserve[this.i] % 4
                    && this.reserve[this.i + 1] % 4 == flushSpades[0] % 4)
                {
                    if (this.reserve[this.i + 1] / 4 > flushSpades.Max() / 4)
                    {
                        current = 5;
                        powerOfHand = this.reserve[this.i + 1] + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                        vf = true;
                    }
                    else
                    {
                        current = 5;
                        powerOfHand = flushSpades.Max() + current * 100;
                        this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                        this.sorted = this.win
                            .OrderByDescending(cards => cards.CurrentHand)
                            .ThenByDescending(cards => cards.Power)
                            .First();
                        vf = true;
                    }
                }
            }

            // TODO group conditions and put parenthesis, replace magic numbers
            // TODO Code repetition - Create method and put it here
            if (flushSpades.Length == 5)
            {
                if (this.reserve[this.i] % 4 == flushSpades[0] % 4 && this.reserve[this.i] / 4 > flushSpades.Min() / 4)
                {
                    current = 5;
                    powerOfHand = this.reserve[this.i] + current * 100;
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                    this.sorted = this.win
                        .OrderByDescending(cards => cards.CurrentHand)
                        .ThenByDescending(cards => cards.Power)
                        .First();
                    vf = true;
                }

                if (this.reserve[this.i + 1] % 4 == flushSpades[0] % 4 && this.reserve[this.i + 1] / 4 > flushSpades.Min() / 4)
                {
                    current = 5;
                    powerOfHand = this.reserve[this.i + 1] + current * 100;
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                    this.sorted = this.win
                        .OrderByDescending(cards => cards.CurrentHand)
                        .ThenByDescending(cards => cards.Power)
                        .First();
                    vf = true;
                }
                else if (this.reserve[this.i] / 4 < flushSpades.Min() / 4 && this.reserve[this.i + 1] / 4 < flushSpades.Min())
                {
                    current = 5;
                    powerOfHand = flushSpades.Max() + current * 100;
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5 });
                    this.sorted = this.win
                        .OrderByDescending(cards => cards.CurrentHand)
                        .ThenByDescending(cards => cards.Power)
                        .First();
                    vf = true;
                }
            }

            // ace
            // TODO group conditions and put parenthesis, replace magic numbers
            // TODO Code repetition - Create method and put it here
            if (flushHearts.Length > 0)
            {
                if (this.reserve[this.i] / 4 == 0 && this.reserve[this.i] % 4 == flushHearts[0] % 4 && vf && flushHearts.Length > 0)
                {
                    current = 5.5;
                    powerOfHand = 13 + current * 100;
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5.5 });
                    this.sorted = this.win
                        .OrderByDescending(cards => cards.CurrentHand)
                        .ThenByDescending(cards => cards.Power)
                        .First();
                }

                if (this.reserve[this.i + 1] / 4 == 0 && this.reserve[this.i + 1] % 4 == flushHearts[0] % 4 && vf
                    && flushHearts.Length > 0)
                {
                    current = 5.5;
                    powerOfHand = 13 + current * 100;
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5.5 });
                    this.sorted = this.win
                        .OrderByDescending(cards => cards.CurrentHand)
                        .ThenByDescending(cards => cards.Power)
                        .First();
                }
            }

            if (flushClubs.Length > 0)
            {
                if (this.reserve[this.i] / 4 == 0 && this.reserve[this.i] % 4 == flushClubs[0] % 4 && vf && flushClubs.Length > 0)
                {
                    current = 5.5;
                    powerOfHand = 13 + current * 100;
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5.5 });
                    this.sorted = this.win
                        .OrderByDescending(cards => cards.CurrentHand)
                        .ThenByDescending(cards => cards.Power)
                        .First();
                }

                if (this.reserve[this.i + 1] / 4 == 0 && this.reserve[this.i + 1] % 4 == flushClubs[0] % 4 && vf
                    && flushClubs.Length > 0)
                {
                    current = 5.5;
                    powerOfHand = 13 + current * 100;
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5.5 });
                    this.sorted = this.win
                        .OrderByDescending(cards => cards.CurrentHand)
                        .ThenByDescending(cards => cards.Power)
                        .First();
                }
            }

            if (flushDiamonds.Length > 0)
            {
                if (this.reserve[this.i] / 4 == 0 && this.reserve[this.i] % 4 == flushDiamonds[0] % 4 && vf && flushDiamonds.Length > 0)
                {
                    current = 5.5;
                    powerOfHand = 13 + current * 100;
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5.5 });
                    this.sorted = this.win
                        .OrderByDescending(cards => cards.CurrentHand)
                        .ThenByDescending(cards => cards.Power)
                        .First();
                }

                if (this.reserve[this.i + 1] / 4 == 0 && this.reserve[this.i + 1] % 4 == flushDiamonds[0] % 4 && vf
                    && flushDiamonds.Length > 0)
                {
                    current = 5.5;
                    powerOfHand = 13 + current * 100;
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5.5 });
                    this.sorted = this.win
                        .OrderByDescending(cards => cards.CurrentHand)
                        .ThenByDescending(cards => cards.Power)
                        .First();
                }
            }

            if (flushSpades.Length > 0)
            {
                if (this.reserve[this.i] / 4 == 0 && this.reserve[this.i] % 4 == flushSpades[0] % 4 && vf && flushSpades.Length > 0)
                {
                    current = 5.5;
                    powerOfHand = 13 + current * 100;
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5.5 });
                    this.sorted = this.win
                        .OrderByDescending(cards => cards.CurrentHand)
                        .ThenByDescending(cards => cards.Power)
                        .First();
                }

                if (this.reserve[this.i + 1] / 4 == 0 && this.reserve[this.i + 1] % 4 == flushSpades[0] % 4 && vf)
                {
                    current = 5.5;
                    powerOfHand = 13 + current * 100;
                    this.win.Add(new Type() { Power = powerOfHand, CurrentHand = 5.5 });
                    this.sorted = this.win
                        .OrderByDescending(cards => cards.CurrentHand)
                        .ThenByDescending(cards => cards.Power)
                        .First();
                }
            }
        }
        
        // TODO group conditions and put parenthesis, replace magic numbers
        // TODO Code repetition - Create method and put it here
        // Straight is hand with distinct cards - 2,3,4,5,6,7...
        private void RStraight(ref double current, ref double power, int[] handCards)
        {
            if (current >= -1)
            {
                var distinctCards = handCards.Select(o => o / 4).Distinct().ToArray();
                for (var j = 0; j < distinctCards.Length - 4; j++)
                {
                    if (distinctCards[j] + 4 == distinctCards[j + 4])
                    {
                        if (distinctCards.Max() - 4 == distinctCards[j])
                        {
                            current = 4;
                            power = distinctCards.Max() + current * 100;
                            this.win.Add(new Type() { Power = power, CurrentHand = 4 });
                            this.sorted =
                                this.win.OrderByDescending(op1 => op1.CurrentHand)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                        }
                        else
                        {
                            current = 4;
                            power = distinctCards[j + 4] + current * 100;
                            this.win.Add(new Type() { Power = power, CurrentHand = 4 });
                            this.sorted =
                                this.win.OrderByDescending(op1 => op1.CurrentHand)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                        }
                    }

                    if (distinctCards[j] == 0 && distinctCards[j + 1] == 9 && distinctCards[j + 2] == 10 
                        && distinctCards[j + 3] == 11 && distinctCards[j + 4] == 12)
                    {
                        current = 4;
                        power = 13 + current * 100;
                        this.win.Add(new Type() { Power = power, CurrentHand = 4 });
                        this.sorted = this.win
                            .OrderByDescending(op1 => op1.CurrentHand)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }
            }
        }

        // TODO group conditions and put parenthesis, replace magic numbers
        // TODO Code repetition - Create method and put it here
        private void RThreeOfAKind(ref double current, ref double power, int[] handCards)
        {
            if (current >= -1)
            {
                // j <= 12 because there are 13 possible situation(2,3,4,5,6,7,8,9,10,J,Q,K,A)
                for (var j = 0; j <= 12; j++)
                {
                    var fh = handCards.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3)
                    {
                        if (fh.Max() / 4 == 0)
                        {
                            current = 3;
                            power = 13 * 3 + current * 100;
                            this.win.Add(new Type() { Power = power, CurrentHand = 3 });
                            this.sorted = this.win
                                .OrderByDescending(op => op.CurrentHand)
                                .ThenByDescending(op => op.Power)
                                .First();
                        }
                        else
                        {
                            current = 3;
                            power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + current * 100;
                            this.win.Add(new Type() { Power = power, CurrentHand = 3 });
                            this.sorted = this.win
                                .OrderByDescending(op => op.CurrentHand)
                                .ThenByDescending(op => op.Power)
                                .First();
                        }
                    }
                }
            }
        }

        // TODO group conditions and put parenthesis, replace magic numbers
        // TODO Code repetition - Create method and put it here
        private void PairOfTwo(ref double current, ref double power)
        {
            if (current >= -1)
            {
                var msgbox = false;
                for (var tc = 16; tc >= 12; tc--)
                {
                    var max = tc - 12;
                    if (this.reserve[this.i] / 4 == this.reserve[this.i + 1] / 4)
                    {
                        continue;
                    }

                    for (var k = 1; k <= max; k++)
                    {
                        if (tc - k < 12)
                        {
                            max--;
                        }

                        if (tc - k < 12)
                        {
                            continue;
                        }

                        // TODO Groupd conditions and insert parenthesis
                        if ((this.reserve[this.i] / 4 != this.reserve[tc] / 4
                             || this.reserve[this.i + 1] / 4 != this.reserve[tc - k] / 4)
                            && (this.reserve[this.i + 1] / 4 != this.reserve[tc] / 4
                                || this.reserve[this.i] / 4 != this.reserve[tc - k] / 4))
                        {
                            continue;
                        }

                        if (!msgbox)
                        {
                            if (this.reserve[this.i] / 4 == 0)
                            {
                                current = 2;
                                power = 13 * 4 + (this.reserve[this.i + 1] / 4) * 2 + current * 100;
                                this.win.Add(new Type() { Power = power, CurrentHand = 2 });
                                this.sorted =
                                    this.win.OrderByDescending(op => op.CurrentHand)
                                        .ThenByDescending(op => op.Power)
                                        .First();
                            }

                            if (this.reserve[this.i + 1] / 4 == 0)
                            {
                                current = 2;
                                power = 13 * 4 + (this.reserve[this.i] / 4) * 2 + current * 100;
                                this.win.Add(new Type() { Power = power, CurrentHand = 2 });
                                this.sorted = this.win
                                        .OrderByDescending(op => op.CurrentHand)
                                        .ThenByDescending(op => op.Power)
                                        .First();
                            }

                            if (this.reserve[this.i + 1] / 4 != 0 && this.reserve[this.i] / 4 != 0)
                            {
                                current = 2;
                                power = (this.reserve[this.i] / 4) * 2 + (this.reserve[this.i + 1] / 4) * 2
                                        + current * 100;
                                this.win.Add(new Type() { Power = power, CurrentHand = 2 });
                                this.sorted = this.win
                                    .OrderByDescending(op => op.CurrentHand)
                                    .ThenByDescending(op => op.Power)
                                    .First();
                            }
                        }

                        msgbox = true;
                    }
                }
            }
        }

        // TODO group conditions and put parenthesis, replace magic numbers
        // TODO Code repetition - Create method and put it here
        private void RPairTwoPair(ref double current, ref double power)
        {
            if (current >= -1)
            {
                var msgbox = false;
                var msgbox1 = false;
                for (var tc = 16; tc >= 12; tc--)
                {
                    var max = tc - 12;
                    for (var k = 1; k <= max; k++)
                    {
                        if (tc - k < 12)
                        {
                            max--;
                        }

                        if (tc - k >= 12)
                        {
                            if (this.reserve[tc] / 4 == this.reserve[tc - k] / 4)
                            {
                                if (this.reserve[tc] / 4 != this.reserve[this.i] / 4
                                    && this.reserve[tc] / 4 != this.reserve[this.i + 1] / 4 && current == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (this.reserve[this.i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            power = (this.reserve[this.i] / 4) * 2 + 13 * 4 + current * 100;
                                            this.win.Add(new Type() { Power = power, CurrentHand = 2 });
                                            this.sorted =
                                                this.win.OrderByDescending(op => op.CurrentHand)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                        }

                                        if (this.reserve[this.i] / 4 == 0)
                                        {
                                            current = 2;
                                            power = (this.reserve[this.i + 1] / 4) * 2 + 13 * 4 + current * 100;
                                            this.win.Add(new Type() { Power = power, CurrentHand = 2 });
                                            this.sorted =
                                                this.win.OrderByDescending(op => op.CurrentHand)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                        }

                                        if (this.reserve[this.i + 1] / 4 != 0)
                                        {
                                            current = 2;
                                            power = (this.reserve[tc] / 4) * 2 + (this.reserve[this.i + 1] / 4) * 2
                                                    + current * 100;
                                            this.win.Add(new Type() { Power = power, CurrentHand = 2 });
                                            this.sorted =
                                                this.win.OrderByDescending(op => op.CurrentHand)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                        }

                                        if (this.reserve[this.i] / 4 != 0)
                                        {
                                            current = 2;
                                            power = (this.reserve[tc] / 4) * 2 + (this.reserve[this.i] / 4) * 2
                                                    + current * 100;
                                            this.win.Add(new Type() { Power = power, CurrentHand = 2 });
                                            this.sorted =
                                                this.win.OrderByDescending(op => op.CurrentHand)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                        }
                                    }

                                    msgbox = true;
                                }

                                if (Math.Abs(current - (-1)) > FloatingPointsTolerance)
                                {
                                    continue;
                                }

                                if (!msgbox1)
                                {
                                    if (this.reserve[this.i] / 4 > this.reserve[this.i + 1] / 4)
                                    {
                                        if (this.reserve[tc] / 4 == 0)
                                        {
                                            current = 0;
                                            power = 13 + this.reserve[this.i] / 4 + current * 100;
                                            this.win.Add(new Type() { Power = power, CurrentHand = 1 });
                                            this.sorted =
                                                this.win.OrderByDescending(op => op.CurrentHand)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                        }
                                        else
                                        {
                                            current = 0;
                                            power = this.reserve[tc] / 4 + this.reserve[this.i] / 4 + current * 100;
                                            this.win.Add(new Type() { Power = power, CurrentHand = 1 });
                                            this.sorted =
                                                this.win.OrderByDescending(op => op.CurrentHand)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                        }
                                    }
                                    else
                                    {
                                        if (this.reserve[tc] / 4 == 0)
                                        {
                                            current = 0;
                                            power = 13 + this.reserve[this.i + 1] + current * 100;
                                            this.win.Add(new Type() { Power = power, CurrentHand = 1 });
                                            this.sorted =
                                                this.win.OrderByDescending(op => op.CurrentHand)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                        }
                                        else
                                        {
                                            current = 0;
                                            power = this.reserve[tc] / 4 + this.reserve[this.i + 1] / 4 + current * 100;
                                            this.win.Add(new Type() { Power = power, CurrentHand = 1 });
                                            this.sorted =
                                                this.win.OrderByDescending(op => op.CurrentHand)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                        }
                                    }
                                }

                                msgbox1 = true;
                            }
                        }
                    }
                }
            }
        }

        // TODO group conditions and put parenthesis, replace magic numbers
        // TODO Code repetition - Create method and put it here
        private void RPairFromHand(ref double current, ref double power)
        {
            if (!(current >= -1))
            {
                return;
            }

            var msgbox = false;
            if (this.reserve[this.i] / 4 == this.reserve[this.i + 1] / 4)
            {
                if (this.reserve[this.i] / 4 == 0)
                {
                    current = 1;
                    power = 13 * 4 + current * 100;
                    this.win.Add(new Type() { Power = power, CurrentHand = 1 });
                    this.sorted = this.win.OrderByDescending(op => op.CurrentHand).ThenByDescending(op => op.Power).First();
                }
                else
                {
                    current = 1;
                    power = (this.reserve[this.i + 1] / 4) * 4 + current * 100;
                    this.win.Add(new Type() { Power = power, CurrentHand = 1 });
                    this.sorted = this.win.OrderByDescending(op => op.CurrentHand).ThenByDescending(op => op.Power).First();
                }

                msgbox = true;
            }

            for (var tc = 16; tc >= 12; tc--)
            {
                if (this.reserve[this.i + 1] / 4 == this.reserve[tc] / 4)
                {
                    if (!msgbox)
                    {
                        if (this.reserve[this.i + 1] / 4 == 0)
                        {
                            current = 1;
                            power = 13 * 4 + this.reserve[this.i] / 4 + current * 100;
                            this.win.Add(new Type() { Power = power, CurrentHand = 1 });
                            this.sorted =
                                this.win.OrderByDescending(op => op.CurrentHand).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 1;
                            power = (this.reserve[this.i + 1] / 4) * 4 + this.reserve[this.i] / 4 + current * 100;
                            this.win.Add(new Type() { Power = power, CurrentHand = 1 });
                            this.sorted =
                                this.win.OrderByDescending(op => op.CurrentHand).ThenByDescending(op => op.Power).First();
                        }
                    }

                    msgbox = true;
                }

                if (this.reserve[this.i] / 4 != this.reserve[tc] / 4)
                {
                    continue;
                }

                if (!msgbox)
                {
                    if (this.reserve[this.i] / 4 == 0)
                    {
                        current = 1;
                        power = 13 * 4 + this.reserve[this.i + 1] / 4 + current * 100;
                        this.win.Add(new Type() { Power = power, CurrentHand = 1 });
                        this.sorted =
                            this.win.OrderByDescending(op => op.CurrentHand).ThenByDescending(op => op.Power).First();
                    }
                    else
                    {
                        current = 1;
                        power = (this.reserve[tc] / 4) * 4 + this.reserve[this.i + 1] / 4 + current * 100;
                        this.win.Add(new Type() { Power = power, CurrentHand = 1 });
                        this.sorted =
                            this.win.OrderByDescending(op => op.CurrentHand).ThenByDescending(op => op.Power).First();
                    }
                }

                msgbox = true;
            }
        }

        private void RHighCard(ref double current, ref double Power)
        {
            if (current == -1)
            {
                if (this.reserve[this.i] / 4 > this.reserve[this.i + 1] / 4)
                {
                    current = -1;
                    Power = this.reserve[this.i] / 4;
                    this.win.Add(new Type() { Power = Power, CurrentHand = -1 });
                    this.sorted =
                        this.win.OrderByDescending(op1 => op1.CurrentHand).ThenByDescending(op1 => op1.Power).First();
                }
                else
                {
                    current = -1;
                    Power = this.reserve[this.i + 1] / 4;
                    this.win.Add(new Type() { Power = Power, CurrentHand = -1 });
                    this.sorted =
                        this.win.OrderByDescending(op1 => op1.CurrentHand).ThenByDescending(op1 => op1.Power).First();
                }

                if (this.reserve[this.i] / 4 == 0 || this.reserve[this.i + 1] / 4 == 0)
                {
                    current = -1;
                    Power = 13;
                    this.win.Add(new Type() { Power = Power, CurrentHand = -1 });
                    this.sorted =
                        this.win.OrderByDescending(op1 => op1.CurrentHand).ThenByDescending(op1 => op1.Power).First();
                }
            }
        }

        private void Winner(double current, double power, string currentText, int chips, string lastly)
        {
            if (lastly == " ")
            {
                lastly = "Bot 5";
            }

            for (var j = 0; j <= 16; j++)
            {
                // await Task.Delay(5);
                if (this.cardHolder[j].Visible)
                {
                    this.cardHolder[j].Image = this.deck[j];
                }
            }

            // TODO Make a switch and replace the text with constants
            if (Math.Abs(current - this.sorted.CurrentHand) < FloatingPointsTolerance)
            {
                if (Math.Abs(power - this.sorted.Power) < FloatingPointsTolerance)
                {
                    this.winners++;
                    this.checkWinners.Add(currentText);
                    if (Math.Abs(current - (-1)) < FloatingPointsTolerance)
                    {
                        MessageBox.Show(currentText + " High Card ");
                    }

                    if (Math.Abs(current - 1) < FloatingPointsTolerance || Math.Abs(current) < FloatingPointsTolerance)
                    {
                        MessageBox.Show(currentText + " Pair ");
                    }

                    if (Math.Abs(current - 2) < FloatingPointsTolerance)
                    {
                        MessageBox.Show(currentText + " Two Pair ");
                    }

                    if (Math.Abs(current - 3) < FloatingPointsTolerance)
                    {
                        MessageBox.Show(currentText + " Three of a Kind ");
                    }

                    if (Math.Abs(current - 4) < FloatingPointsTolerance)
                    {
                        MessageBox.Show(currentText + " Straight ");
                    }

                    if (Math.Abs(current - 5) < FloatingPointsTolerance
                        || Math.Abs(current - 5.5) < FloatingPointsTolerance)
                    {
                        MessageBox.Show(currentText + " Flush ");
                    }

                    if (Math.Abs(current - 6) < FloatingPointsTolerance)
                    {
                        MessageBox.Show(currentText + " Full House ");
                    }

                    if (Math.Abs(current - 7) < FloatingPointsTolerance)
                    {
                        MessageBox.Show(currentText + " Four of a Kind ");
                    }

                    if (Math.Abs(current - 8) < FloatingPointsTolerance)
                    {
                        MessageBox.Show(currentText + " Straight Flush ");
                    }

                    if (Math.Abs(current - 9) < FloatingPointsTolerance)
                    {
                        MessageBox.Show(currentText + " Royal Flush ! ");
                    }
                }
            }

            if (currentText != lastly)
            {
                return;
            }

            // lastfixed
            if (this.winners > 1)
            {
                if (this.checkWinners.Contains("Player"))
                {
                    this.playerChips += int.Parse(this.tablePot.Text) / this.winners;
                    this.tableChips.Text = this.playerChips.ToString();

                    // pPanel.Visible = true;
                }

                if (this.checkWinners.Contains("Bot 1"))
                {
                    this.botOneChips += int.Parse(this.tablePot.Text) / this.winners;
                    this.tableChipsBot1.Text = this.botOneChips.ToString();

                    // b1Panel.Visible = true;
                }

                if (this.checkWinners.Contains("Bot 2"))
                {
                    this.botTwoChips += int.Parse(this.tablePot.Text) / this.winners;
                    this.tableChipsBot2.Text = this.botTwoChips.ToString();

                    // b2Panel.Visible = true;
                }

                if (this.checkWinners.Contains("Bot 3"))
                {
                    this.botThreeChips += int.Parse(this.tablePot.Text) / this.winners;
                    this.tableChipsBot3.Text = this.botThreeChips.ToString();

                    // b3Panel.Visible = true;
                }

                if (this.checkWinners.Contains("Bot 4"))
                {
                    this.botFourChips += int.Parse(this.tablePot.Text) / this.winners;
                    this.tableChipsBot4.Text = this.botFourChips.ToString();

                    // b4Panel.Visible = true;
                }

                if (this.checkWinners.Contains("Bot 5"))
                {
                    this.botFiveChips += int.Parse(this.tablePot.Text) / this.winners;
                    this.tableChipsBot5.Text = this.botFiveChips.ToString();

                    // b5Panel.Visible = true;
                }

                // await Finish(1);
            }

            if (this.winners == 1)
            {
                if (this.checkWinners.Contains("Player"))
                {
                    this.playerChips += int.Parse(this.tablePot.Text);

                    // await Finish(1);
                    // pPanel.Visible = true;
                }

                if (this.checkWinners.Contains("Bot 1"))
                {
                    this.botOneChips += int.Parse(this.tablePot.Text);

                    // await Finish(1);
                    // b1Panel.Visible = true;
                }

                if (this.checkWinners.Contains("Bot 2"))
                {
                    this.botTwoChips += int.Parse(this.tablePot.Text);

                    // await Finish(1);
                    // b2Panel.Visible = true;
                }

                if (this.checkWinners.Contains("Bot 3"))
                {
                    this.botThreeChips += int.Parse(this.tablePot.Text);

                    // await Finish(1);
                    // b3Panel.Visible = true;
                }

                if (this.checkWinners.Contains("Bot 4"))
                {
                    this.botFourChips += int.Parse(this.tablePot.Text);

                    // await Finish(1);
                    // b4Panel.Visible = true;
                }

                if (this.checkWinners.Contains("Bot 5"))
                {
                    this.botFiveChips += int.Parse(this.tablePot.Text);

                    // await Finish(1);
                    // b5Panel.Visible = true;
                }
            }
        }

        // TODO group conditions and put parenthesis, replace magic numbers
        // TODO Code repetition - Create method and put it here
        private async Task CheckRaise(int currentTurn, int raiseTurn)
        {
            if (this.raising)
            {
                this.turnCount = 0;
                this.raising = false;
                this.raisedTurn = currentTurn;
                this.changed = true;
            }
            else
            {
                // TODO Group conditions and put parenthesis
                if (this.turnCount >= this.maxLeft - 1 || !this.changed && this.turnCount == this.maxLeft)
                {
                    if (currentTurn == this.raisedTurn - 1 || !this.changed && this.turnCount == this.maxLeft
                        || this.raisedTurn == 0 && currentTurn == 5)
                    {
                        this.changed = false;
                        this.turnCount = 0;
                        this.raise = 0;
                        this.call = 0;
                        this.raisedTurn = 123;
                        this.rounds++;
                        if (!this.playerFinishedHisTurn)
                        {
                            this.playerStatus.Text = string.Empty;
                        }

                        if (!this.botOneFinishedTurn)
                        {
                            this.botOneStatus.Text = string.Empty;
                        }

                        if (!this.botTwoFinishedTurn)
                        {
                            this.botTwoStatus.Text = string.Empty;
                        }

                        if (!this.botThreeFinishedTurn)
                        {
                            this.botThreeStatus.Text = string.Empty;
                        }

                        if (!this.botFourFinishedTurn)
                        {
                            this.botFourStatus.Text = string.Empty;
                        }

                        if (!this.botFiveFinishedTurn)
                        {
                            this.botFiveStatus.Text = string.Empty;
                        }
                    }
                }
            }

            // TODO class name must be changed !
            if (Math.Abs(this.rounds - this.flop) < FloatingPointsTolerance)
            {
                for (var j = 12; j <= 14; j++)
                {
                    if (this.cardHolder[j].Image == this.deck[j])
                    {
                        continue;
                    }

                    this.cardHolder[j].Image = this.deck[j];
                    this.playerCall = 0;
                    this.playerRaise = 0;
                    this.botOneCall = 0;
                    this.botOneRaise = 0;
                    this.botTwoCall = 0;
                    this.botTwoRaise = 0;
                    this.botThreeCall = 0;
                    this.botThreeRaise = 0;
                    this.botFourCall = 0;
                    this.botFourRaise = 0;
                    this.botFiveCall = 0;
                    this.botFiveRaise = 0;
                }
            }

            if (Math.Abs(this.rounds - this.turn) < FloatingPointsTolerance)
            {
                for (var j = 14; j <= 15; j++)
                {
                    if (this.cardHolder[j].Image == this.deck[j])
                    {
                        continue;
                    }

                    this.cardHolder[j].Image = this.deck[j];
                    this.playerCall = 0;
                    this.playerRaise = 0;
                    this.botOneCall = 0;
                    this.botOneRaise = 0;
                    this.botTwoCall = 0;
                    this.botTwoRaise = 0;
                    this.botThreeCall = 0;
                    this.botThreeRaise = 0;
                    this.botFourCall = 0;
                    this.botFourRaise = 0;
                    this.botFiveCall = 0;
                    this.botFiveRaise = 0;
                }
            }

            if (Math.Abs(this.rounds - this.river) < FloatingPointsTolerance)
            {
                for (var j = 15; j <= 16; j++)
                {
                    if (this.cardHolder[j].Image == this.deck[j])
                    {
                        continue;
                    }

                    this.cardHolder[j].Image = this.deck[j];
                    this.playerCall = 0;
                    this.playerRaise = 0;
                    this.botOneCall = 0;
                    this.botOneRaise = 0;
                    this.botTwoCall = 0;
                    this.botTwoRaise = 0;
                    this.botThreeCall = 0;
                    this.botThreeRaise = 0;
                    this.botFourCall = 0;
                    this.botFourRaise = 0;
                    this.botFiveCall = 0;
                    this.botFiveRaise = 0;
                }
            }

            if (Math.Abs(this.rounds - this.end) < FloatingPointsTolerance && this.maxLeft == 6)
            {
                var fixedLast = "qwerty";
                if (!this.playerStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    this.Rules(0, 1, "Player", ref this.pType, ref this.playerPower, this.playerFinishedHisTurn);
                }

                if (!this.botOneStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                    this.Rules(2, 3, "Bot 1", ref this.botOneType, ref this.botOnePower, this.botOneFinishedTurn);
                }

                if (!this.botTwoStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    this.Rules(4, 5, "Bot 2", ref this.botTwoType, ref this.botTwoPower, this.botTwoFinishedTurn);
                }

                if (!this.botThreeStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    this.Rules(6, 7, "Bot 3", ref this.botThreeType, ref this.botThreePower, this.botThreeFinishedTurn);
                }

                if (!this.botFourStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    this.Rules(8, 9, "Bot 4", ref this.botFourType, ref this.botFourPower, this.botFourFinishedTurn);
                }

                if (!this.botFiveStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    this.Rules(10, 11, "Bot 5", ref this.botFiveType, ref this.botFivePower, this.botFiveFinishedTurn);
                }

                this.Winner(this.pType, this.playerPower, "Player", this.playerChips, fixedLast);
                this.Winner(this.botOneType, this.botOnePower, "Bot 1", this.botOneChips, fixedLast);
                this.Winner(this.botTwoType, this.botTwoPower, "Bot 2", this.botTwoChips, fixedLast);
                this.Winner(this.botThreeType, this.botThreePower, "Bot 3", this.botThreeChips, fixedLast);
                this.Winner(this.botFourType, this.botFourPower, "Bot 4", this.botFourChips, fixedLast);
                this.Winner(this.botFiveType, this.botFivePower, "Bot 5", this.botFiveChips, fixedLast);
                this.restart = true;
                this.playerTurn = true;
                this.playerFinishedHisTurn = false;
                this.botOneFinishedTurn = false;
                this.botTwoFinishedTurn = false;
                this.botThreeFinishedTurn = false;
                this.botFourFinishedTurn = false;
                this.botFiveFinishedTurn = false;
                if (this.playerChips <= 0)
                {
                    var f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        this.playerChips = f2.a;
                        this.botOneChips += f2.a;
                        this.botTwoChips += f2.a;
                        this.botThreeChips += f2.a;
                        this.botFourChips += f2.a;
                        this.botFiveChips += f2.a;
                        this.playerFinishedHisTurn = false;
                        this.playerTurn = true;
                        this.buttonRaise.Enabled = true;
                        this.buttonFold.Enabled = true;
                        this.buttonCheck.Enabled = true;

                        // TODO move to resourse !
                        this.buttonRaise.Text = "Raise";
                    }
                }

                this.playerPanel.Visible = false;
                this.botOnePanel.Visible = false;
                this.botTwoPanel.Visible = false;
                this.botThreePanel.Visible = false;
                this.botFourPanel.Visible = false;
                this.botFivePanel.Visible = false;
                this.playerCall = 0;
                this.playerRaise = 0;
                this.botOneCall = 0;
                this.botOneRaise = 0;
                this.botTwoCall = 0;
                this.botTwoRaise = 0;
                this.botThreeCall = 0;
                this.botThreeRaise = 0;
                this.botFourCall = 0;
                this.botFourRaise = 0;
                this.botFiveCall = 0;
                this.botFiveRaise = 0;
                this.last = 0;
                this.call = this.bigBlind;
                this.raise = 0;
                this.imageLocations = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                this.bools.Clear();
                this.rounds = 0;
                this.playerPower = 0;
                this.pType = -1;
                this.type = 0;
                this.botOnePower = 0;
                this.botTwoPower = 0;
                this.botThreePower = 0;
                this.botFourPower = 0;
                this.botFivePower = 0;
                this.botOneType = -1;
                this.botTwoType = -1;
                this.botThreeType = -1;
                this.botFourType = -1;
                this.botFiveType = -1;
                this.ints.Clear();
                this.checkWinners.Clear();
                this.winners = 0;
                this.win.Clear();
                this.sorted.CurrentHand = 0;
                this.sorted.Power = 0;
                for (var os = 0; os < 17; os++)
                {
                    this.cardHolder[os].Image = null;
                    this.cardHolder[os].Invalidate();
                    this.cardHolder[os].Visible = false;
                }

                // TODO move to const or resourse
                this.tablePot.Text = "0";
                this.playerStatus.Text = string.Empty;
                await this.Shuffle();
                await this.Turns();
            }
        }

        private void FixCall(Control status, ref int cCall, ref int cRaise, int options)
        {
            if (!(Math.Abs(this.rounds - 4) > FloatingPointsTolerance))
            {
                return;
            }

            if (options == 1)
            {
                if (status.Text.Contains("Raise"))
                {
                    var changeRaise = status.Text.Substring(6);
                    cRaise = int.Parse(changeRaise);
                }

                if (status.Text.Contains("Call"))
                {
                    var changeCall = status.Text.Substring(5);
                    cCall = int.Parse(changeCall);
                }

                if (status.Text.Contains("Check"))
                {
                    cRaise = 0;
                    cCall = 0;
                }
            }

            if (options != 2)
            {
                return;
            }

            if (Math.Abs(cRaise - this.raise) > FloatingPointsTolerance && cRaise <= this.raise)
            {
                this.call = Convert.ToInt32(this.raise) - cRaise;
            }

            if (cCall != this.call || cCall <= this.call)
            {
                this.call = this.call - cCall;
            }

            if (!(Math.Abs(cRaise - this.raise) < FloatingPointsTolerance) || !(this.raise > 0))
            {
                return;
            }

            this.call = 0;
            this.buttonCall.Enabled = false;
            this.buttonCall.Text = "Callisfuckedup";
        }

        private async Task AllIn()
        {
            if (this.playerChips <= 0 && !this.intsadded)
            {
                if (this.playerStatus.Text.Contains("Raise"))
                {
                    this.ints.Add(this.playerChips);
                    this.intsadded = true;
                }

                if (this.playerStatus.Text.Contains("Call"))
                {
                    this.ints.Add(this.playerChips);
                    this.intsadded = true;
                }
            }

            this.intsadded = false;
            if (this.botOneChips <= 0 && !this.botOneFinishedTurn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.botOneChips);
                    this.intsadded = true;
                }

                this.intsadded = false;
            }

            if (this.botTwoChips <= 0 && !this.botTwoFinishedTurn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.botTwoChips);
                    this.intsadded = true;
                }

                this.intsadded = false;
            }

            if (this.botThreeChips <= 0 && !this.botThreeFinishedTurn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.botThreeChips);
                    this.intsadded = true;
                }

                this.intsadded = false;
            }

            if (this.botFourChips <= 0 && !this.botFourFinishedTurn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.botFourChips);
                    this.intsadded = true;
                }

                this.intsadded = false;
            }

            if (this.botFiveChips <= 0 && !this.botFiveFinishedTurn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.botFiveChips);
                    this.intsadded = true;
                }
            }

            if (this.ints.ToArray().Length == this.maxLeft)
            {
                await this.Finish(2);
            }
            else
            {
                this.ints.Clear();
            }

            var abc = this.bools.Count(x => x == false);

            // TODO should get the messages from centrilized file or resource
            if (abc == 1)
            {
                var index = this.bools.IndexOf(false);
                if (index == 0)
                {
                    this.playerChips += int.Parse(this.tablePot.Text);
                    this.tableChips.Text = this.playerChips.ToString();
                    this.playerPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }

                if (index == 1)
                {
                    this.botOneChips += int.Parse(this.tablePot.Text);
                    this.tableChips.Text = this.botOneChips.ToString();
                    this.botOnePanel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }

                if (index == 2)
                {
                    this.botTwoChips += int.Parse(this.tablePot.Text);
                    this.tableChips.Text = this.botTwoChips.ToString();
                    this.botTwoPanel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }

                if (index == 3)
                {
                    this.botThreeChips += int.Parse(this.tablePot.Text);
                    this.tableChips.Text = this.botThreeChips.ToString();
                    this.botThreePanel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }

                if (index == 4)
                {
                    this.botFourChips += int.Parse(this.tablePot.Text);
                    this.tableChips.Text = this.botFourChips.ToString();
                    this.botFourPanel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }

                if (index == 5)
                {
                    this.botFiveChips += int.Parse(this.tablePot.Text);
                    this.tableChips.Text = this.botFiveChips.ToString();
                    this.botFivePanel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }

                for (var j = 0; j <= 16; j++)
                {
                    this.cardHolder[j].Visible = false;
                }

                await this.Finish(1);
            }

            this.intsadded = false;

            if (abc < 6 && abc > 1 && this.rounds >= this.end)
            {
                await this.Finish(2);
            }
        }

        // Ot tuk
        private async Task Finish(int n)
        {
            if (n == 2)
            {
                this.FixWinners();
            }

            this.playerPanel.Visible = false;
            this.botOnePanel.Visible = false;
            this.botTwoPanel.Visible = false;
            this.botThreePanel.Visible = false;
            this.botFourPanel.Visible = false;
            this.botFivePanel.Visible = false;
            this.call = this.bigBlind;
            this.raise = 0;
            this.foldedPlayers = 5;
            this.type = 0;
            this.rounds = 0;
            this.botOnePower = 0;
            this.botTwoPower = 0;
            this.botThreePower = 0;
            this.botFourPower = 0;
            this.botFivePower = 0;
            this.playerPower = 0;
            this.pType = -1;
            this.raise = 0;
            this.botOneType = -1;
            this.botTwoType = -1;
            this.botThreeType = -1;
            this.botFourType = -1;
            this.botFiveType = -1;
            this.botOneTurn = false;
            this.botTwoTurn = false;
            this.botThreeTurn = false;
            this.botFourTurn = false;
            this.botFiveTurn = false;
            this.botOneFinishedTurn = false;
            this.botTwoFinishedTurn = false;
            this.botThreeFinishedTurn = false;
            this.botFourFinishedTurn = false;
            this.botFiveFinishedTurn = false;
            this.playerFolded = false;
            this.botOneFolded = false;
            this.botTwoFolded = false;
            this.botThreeFolded = false;
            this.botFourFolded = false;
            this.botFiveFolded = false;
            this.playerFinishedHisTurn = false;
            this.playerTurn = true;
            this.restart = false;
            this.raising = false;
            this.playerCall = 0;
            this.botOneCall = 0;
            this.botTwoCall = 0;
            this.botThreeCall = 0;
            this.botFourCall = 0;
            this.botFiveCall = 0;
            this.playerRaise = 0;
            this.botOneRaise = 0;
            this.botTwoRaise = 0;
            this.botThreeRaise = 0;
            this.botFourRaise = 0;
            this.botFiveRaise = 0;
            this.height = 0;
            this.width = 0;
            this.winners = 0;
            this.flop = 1;
            this.turn = 2;
            this.river = 3;
            this.end = 4;
            this.maxLeft = 6;
            this.last = 123;
            this.raisedTurn = 1;
            this.bools.Clear();
            this.checkWinners.Clear();
            this.ints.Clear();
            this.win.Clear();
            this.sorted.CurrentHand = 0;
            this.sorted.Power = 0;
            this.tablePot.Text = "0";
            this.t = 60;
            this.up = 10000000;
            this.turnCount = 0;
            this.playerStatus.Text = string.Empty;
            this.botOneStatus.Text = string.Empty;
            this.botTwoStatus.Text = string.Empty;
            this.botThreeStatus.Text = string.Empty;
            this.botFourStatus.Text = string.Empty;
            this.botFiveStatus.Text = string.Empty;
            if (this.playerChips <= 0)
            {
                var f2 = new AddChips();
                f2.ShowDialog();
                if (f2.a != 0)
                {
                    this.playerChips = f2.a;
                    this.botOneChips += f2.a;
                    this.botTwoChips += f2.a;
                    this.botThreeChips += f2.a;
                    this.botFourChips += f2.a;
                    this.botFiveChips += f2.a;
                    this.playerFinishedHisTurn = false;
                    this.playerTurn = true;
                    this.buttonRaise.Enabled = true;
                    this.buttonFold.Enabled = true;
                    this.buttonCheck.Enabled = true;
                    this.buttonRaise.Text = "Raise";
                }
            }

            this.imageLocations = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (var os = 0; os < 17; os++)
            {
                this.cardHolder[os].Image = null;
                this.cardHolder[os].Invalidate();
                this.cardHolder[os].Visible = false;
            }

            await this.Shuffle();

            // await Turns();
        }

        private void FixWinners()
        {
            this.win.Clear();
            this.sorted.CurrentHand = 0;
            this.sorted.Power = 0;
            var fixedLast = "qwerty";

            if (!this.playerStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                this.Rules(0, 1, "Player", ref this.pType, ref this.playerPower, this.playerFinishedHisTurn);
            }

            if (!this.botOneStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                this.Rules(2, 3, "Bot 1", ref this.botOneType, ref this.botOnePower, this.botOneFinishedTurn);
            }

            if (!this.botTwoStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                this.Rules(4, 5, "Bot 2", ref this.botTwoType, ref this.botTwoPower, this.botTwoFinishedTurn);
            }

            if (!this.botThreeStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                this.Rules(6, 7, "Bot 3", ref this.botThreeType, ref this.botThreePower, this.botThreeFinishedTurn);
            }

            if (!this.botFourStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                this.Rules(8, 9, "Bot 4", ref this.botFourType, ref this.botFourPower, this.botFourFinishedTurn);
            }

            if (!this.botFiveStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                this.Rules(10, 11, "Bot 5", ref this.botFiveType, ref this.botFivePower, this.botFiveFinishedTurn);
            }

            this.Winner(this.pType, this.playerPower, "Player", this.playerChips, fixedLast);
            this.Winner(this.botOneType, this.botOnePower, "Bot 1", this.botOneChips, fixedLast);
            this.Winner(this.botTwoType, this.botTwoPower, "Bot 2", this.botTwoChips, fixedLast);
            this.Winner(this.botThreeType, this.botThreePower, "Bot 3", this.botThreeChips, fixedLast);
            this.Winner(this.botFourType, this.botFourPower, "Bot 4", this.botFourChips, fixedLast);
            this.Winner(this.botFiveType, this.botFivePower, "Bot 5", this.botFiveChips, fixedLast);
        }

        // TODO Add switch
        private void Ai(
            int firstCardIndex, 
            int secondCardIndex, 
            ref int botChips, 
            ref bool botTurn, 
            ref bool botFinishedTurn, 
            Label botStatus, 
            int name, 
            double botPower, 
            double botCurrent)
        {
            if (!botFinishedTurn)
            {
                if (Math.Abs(botCurrent - (-1)) < FloatingPointsTolerance)
                {
                    this.HighCard(ref botChips, ref botTurn, ref botFinishedTurn, botStatus, botPower);
                }

                if (Math.Abs(botCurrent) < FloatingPointsTolerance)
                {
                    this.PairTable(ref botChips, ref botTurn, ref botFinishedTurn, botStatus, botPower);
                }

                if (Math.Abs(botCurrent - 1) < FloatingPointsTolerance)
                {
                    this.PairHand(ref botChips, ref botTurn, ref botFinishedTurn, botStatus, botPower);
                }

                if (Math.Abs(botCurrent - 2) < FloatingPointsTolerance)
                {
                    this.TwoPair(ref botChips, ref botTurn, ref botFinishedTurn, botStatus, botPower);
                }

                if (Math.Abs(botCurrent - 3) < FloatingPointsTolerance)
                {
                    this.ThreeOfAKind(ref botChips, ref botTurn, ref botFinishedTurn, botStatus, name, botPower);
                }

                if (Math.Abs(botCurrent - 4) < FloatingPointsTolerance)
                {
                    this.Straight(ref botChips, ref botTurn, ref botFinishedTurn, botStatus, name, botPower);
                }

                if (Math.Abs(botCurrent - 5) < FloatingPointsTolerance
                    || Math.Abs(botCurrent - 5.5) < FloatingPointsTolerance)
                {
                    this.Flush(ref botChips, ref botTurn, ref botFinishedTurn, botStatus, name, botPower);
                }

                if (Math.Abs(botCurrent - 6) < FloatingPointsTolerance)
                {
                    this.FullHouse(ref botChips, ref botTurn, ref botFinishedTurn, botStatus, name, botPower);
                }

                if (Math.Abs(botCurrent - 7) < FloatingPointsTolerance)
                {
                    this.FourOfAKind(ref botChips, ref botTurn, ref botFinishedTurn, botStatus, name, botPower);
                }

                if (Math.Abs(botCurrent - 8) < FloatingPointsTolerance
                    || Math.Abs(botCurrent - 9) < FloatingPointsTolerance)
                {
                    this.StraightFlush(ref botChips, ref botTurn, ref botFinishedTurn, botStatus, name, botPower);
                }
            }

            if (!botFinishedTurn)
            {
                return;
            }

            this.cardHolder[firstCardIndex].Visible = false;
            this.cardHolder[secondCardIndex].Visible = false;
        }

        private void HighCard(ref int botChips, ref bool botTurn, ref bool botFinishedTurn, Label sStatus, double botPower)
        {
            this.HP(ref botChips, ref botTurn, ref botFinishedTurn, sStatus, botPower, 20, 25);
        }

        private void PairTable(ref int botChips, ref bool botTurn, ref bool botFinishedTurn, Label sStatus, double botPower)
        {
            this.HP(ref botChips, ref botTurn, ref botFinishedTurn, sStatus, botPower, 16, 25);
        }

        private void PairHand(ref int botChips, ref bool botTurn, ref bool botFinishedTurn, Label sStatus, double botPower)
        {
            var randomPair = new Random();
            var randomCall = randomPair.Next(10, 16);
            var randomRaise = randomPair.Next(10, 13);
            if (botPower <= 199 && botPower >= 140)
            {
                this.PH(ref botChips, ref botTurn, ref botFinishedTurn, sStatus, randomCall, 6, randomRaise);
            }

            if (botPower <= 139 && botPower >= 128)
            {
                this.PH(ref botChips, ref botTurn, ref botFinishedTurn, sStatus, randomCall, 7, randomRaise);
            }

            if (botPower < 128 && botPower >= 101)
            {
                this.PH(ref botChips, ref botTurn, ref botFinishedTurn, sStatus, randomCall, 9, randomRaise);
            }
        }

        private void TwoPair(ref int botChips, ref bool botTurn, ref bool botFinishedTurn, Label sStatus, double botPower)
        {
            var randPair = new Random();
            var randomCall = randPair.Next(6, 11);
            var randomRaise = randPair.Next(6, 11);
            if (botPower <= 290 && botPower >= 246)
            {
                this.PH(ref botChips, ref botTurn, ref botFinishedTurn, sStatus, randomCall, 3, randomRaise);
            }

            if (botPower <= 244 && botPower >= 234)
            {
                this.PH(ref botChips, ref botTurn, ref botFinishedTurn, sStatus, randomCall, 4, randomRaise);
            }

            if (botPower < 234 && botPower >= 201)
            {
                this.PH(ref botChips, ref botTurn, ref botFinishedTurn, sStatus, randomCall, 4, randomRaise);
            }
        }

        private void ThreeOfAKind(
            ref int botChips, 
            ref bool botTurn, 
            ref bool botFinishedTurn, 
            Label sStatus, 
            int name, 
            double botPower)
        {
            var randomThreeOfAKind = new Random();
            var threeOfAKindCall = randomThreeOfAKind.Next(3, 7);
            var threeOfAKindRaise = randomThreeOfAKind.Next(4, 8);
            if (botPower <= 390 && botPower >= 330)
            {
                this.Smooth(ref botChips, ref botTurn, ref botFinishedTurn, sStatus, name, threeOfAKindCall, threeOfAKindRaise);
            }

            if (botPower <= 327 && botPower >= 321)
            {
                // 10  8
                this.Smooth(ref botChips, ref botTurn, ref botFinishedTurn, sStatus, name, threeOfAKindCall, threeOfAKindRaise);
            }

            if (botPower < 321 && botPower >= 303)
            {
                // 7 2
                this.Smooth(ref botChips, ref botTurn, ref botFinishedTurn, sStatus, name, threeOfAKindCall, threeOfAKindRaise);
            }
        }

        private void Straight(ref int botChips, ref bool botTurn, ref bool botFinishedTurn, Label sStatus, int name, double botPower)
        {
            var randomStraight = new Random();
            var straightCall = randomStraight.Next(3, 6);
            var straightRaise = randomStraight.Next(3, 8);
            if (botPower <= 480 && botPower >= 410)
            {
                this.Smooth(ref botChips, ref botTurn, ref botFinishedTurn, sStatus, name, straightCall, straightRaise);
            }

            if (botPower <= 409 && botPower >= 407)
            {
                // 10  8
                this.Smooth(ref botChips, ref botTurn, ref botFinishedTurn, sStatus, name, straightCall, straightRaise);
            }

            if (botPower < 407 && botPower >= 404)
            {
                this.Smooth(ref botChips, ref botTurn, ref botFinishedTurn, sStatus, name, straightCall, straightRaise);
            }
        }

        private void Flush(ref int botChips, ref bool botTurn, ref bool botFinishedTurn, Label sStatus, int name, double botPower)
        {
            var randomFlush = new Random();
            var flushCall = randomFlush.Next(2, 6);
            var flushRaise = randomFlush.Next(3, 7);
            this.Smooth(ref botChips, ref botTurn, ref botFinishedTurn, sStatus, name, flushCall, flushRaise);
        }

        private void FullHouse(
            ref int botChips, 
            ref bool botTurn, 
            ref bool botFinishedTurn, 
            Label sStatus, 
            int name, 
            double botPower)
        {
            var randomFullHouse = new Random();
            var fullHouseCall = randomFullHouse.Next(1, 5);
            var fullHouseRaise = randomFullHouse.Next(2, 6);
            if (botPower <= 626 && botPower >= 620)
            {
                this.Smooth(ref botChips, ref botTurn, ref botFinishedTurn, sStatus, name, fullHouseCall, fullHouseRaise);
            }

            if (botPower < 620 && botPower >= 602)
            {
                this.Smooth(ref botChips, ref botTurn, ref botFinishedTurn, sStatus, name, fullHouseCall, fullHouseRaise);
            }
        }

        private void FourOfAKind(
            ref int botChips, 
            ref bool botTurn, 
            ref bool botFinishedTurn, 
            Label sStatus, 
            int name, 
            double botPower)
        {
            var randomFourOfAKind = new Random();
            var fourOfAKindCall = randomFourOfAKind.Next(1, 4);
            var fourOfAKindRaise = randomFourOfAKind.Next(2, 5);
            if (botPower <= 752 && botPower >= 704)
            {
                this.Smooth(ref botChips, ref botTurn, ref botFinishedTurn, sStatus, name, fourOfAKindCall, fourOfAKindRaise);
            }
        }

        private void StraightFlush(
            ref int botChips, 
            ref bool botTurn, 
            ref bool botFinishedTurn, 
            Label sStatus, 
            int name, 
            double botPower)
        {
            var randomStraightFlush = new Random();
            var straightFlushCall = randomStraightFlush.Next(1, 3);
            var straightFlushRaise = randomStraightFlush.Next(1, 3);
            if (botPower <= 913 && botPower >= 804)
            {
                this.Smooth(ref botChips, ref botTurn, ref botFinishedTurn, sStatus, name, straightFlushCall, straightFlushRaise);
            }
        }

        private void Fold(ref bool botChips, ref bool botFinishedTurn, Control sStatus)
        {
            this.raising = false;
            sStatus.Text = "Fold";
            botChips = false;
            botFinishedTurn = true;
        }

        private void Check(ref bool currentTurn, Control currentStatus)
        {
            currentStatus.Text = "Check";
            currentTurn = false;
            this.raising = false;
        }

        private void Call(ref int botChips, ref bool botTurn, Control sStatus)
        {
            this.raising = false;
            botTurn = false;
            botChips -= this.call;
            sStatus.Text = "Call " + this.call;
            this.tablePot.Text = (int.Parse(this.tablePot.Text) + this.call).ToString();
        }

        private void Raised(ref int botChips, ref bool botTurn, Label sStatus)
        {
            botChips -= Convert.ToInt32(this.raise);
            sStatus.Text = "Raise " + this.raise;
            this.tablePot.Text = (int.Parse(this.tablePot.Text) + Convert.ToInt32(this.raise)).ToString();
            this.call = Convert.ToInt32(this.raise);
            this.raising = true;
            botTurn = false;
        }

        private void HP(ref int botChips, ref bool botTurn, ref bool botFinishedTurn, Label sStatus, double botPower, int n, int n1)
        {
            var rand = new Random();
            var rnd = rand.Next(1, 4);
            if (this.call <= 0)
            {
                this.Check(ref botTurn, sStatus);
            }

            if (this.call > 0)
            {
                if (rnd == 1)
                {
                    if (this.call <= RoundN(botChips, n))
                    {
                        this.Call(ref botChips, ref botTurn, sStatus);
                    }
                    else
                    {
                        this.Fold(ref botTurn, ref botFinishedTurn, sStatus);
                    }
                }

                if (rnd == 2)
                {
                    if (this.call <= RoundN(botChips, n1))
                    {
                        this.Call(ref botChips, ref botTurn, sStatus);
                    }
                    else
                    {
                        this.Fold(ref botTurn, ref botFinishedTurn, sStatus);
                    }
                }
            }

            if (rnd == 3)
            {
                if (Math.Abs(this.raise) < FloatingPointsTolerance)
                {
                    this.raise = this.call * 2;
                    this.Raised(ref botChips, ref botTurn, sStatus);
                }
                else
                {
                    if (this.raise <= RoundN(botChips, n))
                    {
                        this.raise = this.call * 2;
                        this.Raised(ref botChips, ref botTurn, sStatus);
                    }
                    else
                    {
                        this.Fold(ref botTurn, ref botFinishedTurn, sStatus);
                    }
                }
            }

            if (botChips <= 0)
            {
                botFinishedTurn = true;
            }
        }

        private void PH(ref int botChips, ref bool botTurn, ref bool botFinishedTurn, Label sStatus, int n, int n1, int r)
        {
            var rand = new Random();
            var rnd = rand.Next(1, 3);
            if (this.rounds < 2)
            {
                if (this.call <= 0)
                {
                    this.Check(ref botTurn, sStatus);
                }

                if (this.call > 0)
                {
                    if (this.call >= RoundN(botChips, n1))
                    {
                        this.Fold(ref botTurn, ref botFinishedTurn, sStatus);
                    }

                    if (this.raise > RoundN(botChips, n))
                    {
                        this.Fold(ref botTurn, ref botFinishedTurn, sStatus);
                    }

                    if (!botFinishedTurn)
                    {
                        if (this.call >= RoundN(botChips, n) && this.call <= RoundN(botChips, n1))
                        {
                            this.Call(ref botChips, ref botTurn, sStatus);
                        }

                        if (this.raise <= RoundN(botChips, n) && this.raise >= RoundN(botChips, n) / 2)
                        {
                            this.Call(ref botChips, ref botTurn, sStatus);
                        }

                        if (this.raise <= RoundN(botChips, n) / 2)
                        {
                            if (this.raise > 0)
                            {
                                this.raise = RoundN(botChips, n);
                                this.Raised(ref botChips, ref botTurn, sStatus);
                            }
                            else
                            {
                                this.raise = this.call * 2;
                                this.Raised(ref botChips, ref botTurn, sStatus);
                            }
                        }
                    }
                }
            }

            if (this.rounds >= 2)
            {
                if (this.call > 0)
                {
                    if (this.call >= RoundN(botChips, n1 - rnd))
                    {
                        this.Fold(ref botTurn, ref botFinishedTurn, sStatus);
                    }

                    if (this.raise > RoundN(botChips, n - rnd))
                    {
                        this.Fold(ref botTurn, ref botFinishedTurn, sStatus);
                    }

                    if (!botFinishedTurn)
                    {
                        if (this.call >= RoundN(botChips, n - rnd) && this.call <= RoundN(botChips, n1 - rnd))
                        {
                            this.Call(ref botChips, ref botTurn, sStatus);
                        }

                        if (this.raise <= RoundN(botChips, n - rnd) && this.raise >= RoundN(botChips, n - rnd) / 2)
                        {
                            this.Call(ref botChips, ref botTurn, sStatus);
                        }

                        if (this.raise <= RoundN(botChips, n - rnd) / 2)
                        {
                            if (this.raise > 0)
                            {
                                this.raise = RoundN(botChips, n - rnd);
                                this.Raised(ref botChips, ref botTurn, sStatus);
                            }
                            else
                            {
                                this.raise = this.call * 2;
                                this.Raised(ref botChips, ref botTurn, sStatus);
                            }
                        }
                    }
                }

                if (this.call <= 0)
                {
                    this.raise = RoundN(botChips, r - rnd);
                    this.Raised(ref botChips, ref botTurn, sStatus);
                }
            }

            if (botChips <= 0)
            {
                botFinishedTurn = true;
            }
        }

        // Do tuk
        private void Smooth(
            ref int botChips, 
            ref bool botTurn, 
            ref bool botFTurn, 
            Label botStatus, 
            int name, 
            int n, 
            int r)
        {
            var rand = new Random();
            var rnd = rand.Next(1, 3);
            if (this.call <= 0)
            {
                this.Check(ref botTurn, botStatus);
            }
            else
            {
                if (this.call >= RoundN(botChips, n))
                {
                    if (botChips > this.call)
                    {
                        this.Call(ref botChips, ref botTurn, botStatus);
                    }
                    else if (botChips <= this.call)
                    {
                        this.raising = false;
                        botTurn = false;
                        botChips = 0;
                        botStatus.Text = "Call " + botChips;
                        this.tablePot.Text = (int.Parse(this.tablePot.Text) + botChips).ToString();
                    }
                }
                else
                {
                    if (this.raise > 0)
                    {
                        if (botChips >= this.raise * 2)
                        {
                            this.raise *= 2;
                            this.Raised(ref botChips, ref botTurn, botStatus);
                        }
                        else
                        {
                            this.Call(ref botChips, ref botTurn, botStatus);
                        }
                    }
                    else
                    {
                        this.raise = this.call * 2;
                        this.Raised(ref botChips, ref botTurn, botStatus);
                    }
                }
            }

            if (botChips <= 0)
            {
                botFTurn = true;
            }
        }

        #region Variables

        private readonly Panel playerPanel = new Panel();

        private readonly Panel botOnePanel = new Panel();

        private readonly Panel botTwoPanel = new Panel();

        private readonly Panel botThreePanel = new Panel();

        private readonly Panel botFourPanel = new Panel();

        private readonly Panel botFivePanel = new Panel();

        private readonly int[] reserve = new int[17];

        private readonly Image[] deck = new Image[52];

        private readonly PictureBox[] cardHolder = new PictureBox[52];

        private readonly Timer timer = new Timer();

        private readonly Timer updates = new Timer();

        private readonly List<bool?> bools = new List<bool?>();

        private readonly List<Type> win = new List<Type>();

        private readonly List<string> checkWinners = new List<string>();

        private readonly List<int> ints = new List<int>();

        // string[] ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
        private string[] imageLocations =
            {
                "Assets\\Cards\\33.png", "Assets\\Cards\\22.png", "Assets\\Cards\\29.png", 
                "Assets\\Cards\\21.png", "Assets\\Cards\\36.png", "Assets\\Cards\\17.png", 
                "Assets\\Cards\\40.png", "Assets\\Cards\\16.png", "Assets\\Cards\\5.png", 
                "Assets\\Cards\\47.png", "Assets\\Cards\\37.png", "Assets\\Cards\\13.png", 
                "Assets\\Cards\\12.png", "Assets\\Cards\\8.png", "Assets\\Cards\\18.png", 
                "Assets\\Cards\\15.png", "Assets\\Cards\\27.png"
            };

        private int call = 500;

        private ProgressBar progressBar = new ProgressBar();

        private int foldedPlayers = 5;

        private int playerChips = 10000;

        private int botOneChips = 10000;

        private int botTwoChips = 10000;

        private int botThreeChips = 10000;

        private int botFourChips = 10000;

        private int botFiveChips = 10000;

        private double type;

        private double rounds = 0;

        private double botOnePower;

        private double botTwoPower;

        private double botThreePower;

        private double botFourPower;

        private double botFivePower;

        private double playerPower = 0;

        private double pType = -1;

        private double raise = 0;

        private double botOneType = -1;

        private double botTwoType = -1;

        private double botThreeType = -1;

        private double botFourType = -1;

        private double botFiveType = -1;

        private bool botOneTurn = false;

        private bool botTwoTurn = false;

        private bool botThreeTurn = false;

        private bool botFourTurn = false;

        private bool botFiveTurn = false;

        private bool botOneFinishedTurn = false;

        private bool botTwoFinishedTurn = false;

        private bool botThreeFinishedTurn = false;

        private bool botFourFinishedTurn = false;

        private bool botFiveFinishedTurn = false;

        private bool playerFolded;

        private bool botOneFolded;

        private bool botTwoFolded;

        private bool botThreeFolded;

        private bool botFourFolded;

        private bool botFiveFolded;

        private bool intsadded;

        private bool changed;

        private int playerCall = 0;

        private int botOneCall = 0;

        private int botTwoCall = 0;

        private int botThreeCall = 0;

        private int botFourCall = 0;

        private int botFiveCall = 0;

        private int playerRaise = 0;

        private int botOneRaise = 0;

        private int botTwoRaise = 0;

        private int botThreeRaise = 0;

        private int botFourRaise = 0;

        private int botFiveRaise = 0;

        private int height;

        private int width;

        private int winners = 0;

        private int flop = 1;

        private int turn = 2;

        private int river = 3;

        private int end = 4;

        private int maxLeft = 6;

        private int last = 123;

        private int raisedTurn = 1;

        private bool playerFinishedHisTurn = false;

        private bool playerTurn = true;

        private bool restart = false;

        private bool raising = false;

        private Type sorted;

        private int t = 60;

        private int i;

        private int bigBlind = 500;

        private int smallBlind = 250;

        private int up = 10000000;

        private int turnCount = 0;

        #endregion

        #region UI

        private async void timer_Tick(object sender, object e)
        {
            if (this.PlayerTurnTimer.Value <= 0)
            {
                this.playerFinishedHisTurn = true;
                await this.Turns();
            }

            if (this.t > 0)
            {
                this.t--;
                this.PlayerTurnTimer.Value = (this.t / 6) * 100;
            }
        }

        // TODO Magic strings ! Replace with constants
        private void Update_Tick(object sender, object e)
        {
            if (this.playerChips <= 0)
            {
                this.tableChips.Text = "Chips : 0";
            }

            if (this.botOneChips <= 0)
            {
                this.tableChipsBot1.Text = "Chips : 0";
            }

            if (this.botTwoChips <= 0)
            {
                this.tableChipsBot2.Text = "Chips : 0";
            }

            if (this.botThreeChips <= 0)
            {
                this.tableChipsBot3.Text = "Chips : 0";
            }

            if (this.botFourChips <= 0)
            {
                this.tableChipsBot4.Text = "Chips : 0";
            }

            if (this.botFiveChips <= 0)
            {
                this.tableChipsBot5.Text = "Chips : 0";
            }

            this.tableChips.Text = "Chips : " + this.playerChips;
            this.tableChipsBot1.Text = "Chips : " + this.botOneChips;
            this.tableChipsBot2.Text = "Chips : " + this.botTwoChips;
            this.tableChipsBot3.Text = "Chips : " + this.botThreeChips;
            this.tableChipsBot4.Text = "Chips : " + this.botFourChips;
            this.tableChipsBot5.Text = "Chips : " + this.botFiveChips;
            if (this.playerChips <= 0)
            {
                this.playerTurn = false;
                this.playerFinishedHisTurn = true;
                this.buttonCall.Enabled = false;
                this.buttonRaise.Enabled = false;
                this.buttonFold.Enabled = false;
                this.buttonCheck.Enabled = false;
            }

            if (this.up > 0)
            {
                this.up--;
            }

            if (this.playerChips >= this.call)
            {
                this.buttonCall.Text = "Call " + this.call;
            }
            else
            {
                this.buttonCall.Text = "All in";
                this.buttonRaise.Enabled = false;
            }

            if (this.call > 0)
            {
                this.buttonCheck.Enabled = false;
            }

            if (this.call <= 0)
            {
                this.buttonCheck.Enabled = true;
                this.buttonCall.Text = "Call";
                this.buttonCall.Enabled = false;
            }

            if (this.playerChips <= 0)
            {
                this.buttonRaise.Enabled = false;
            }

            int parsedValue;

            if (this.tableRaise.Text != string.Empty && int.TryParse(this.tableRaise.Text, out parsedValue))
            {
                if (this.playerChips <= int.Parse(this.tableRaise.Text))
                {
                    this.buttonRaise.Text = "All in";
                }
                else
                {
                    this.buttonRaise.Text = "Raise";
                }
            }

            if (this.playerChips < this.call)
            {
                this.buttonRaise.Enabled = false;
            }
        }

        private async void BFold_Click(object sender, EventArgs e)
        {
            this.playerStatus.Text = "Fold";
            this.playerTurn = false;
            this.playerFinishedHisTurn = true;
            await this.Turns();
        }

        private async void BCheck_Click(object sender, EventArgs e)
        {
            if (this.call <= 0)
            {
                this.playerTurn = false;
                this.playerStatus.Text = "Check";
            }
            else
            {
                // playerStatus.Text = "All in " + Chips;
                this.buttonCheck.Enabled = false;
            }

            await this.Turns();
        }

        private async void BCall_Click(object sender, EventArgs e)
        {
            this.Rules(0, 1, "Player", ref this.pType, ref this.playerPower, this.playerFinishedHisTurn);
            if (this.playerChips >= this.call)
            {
                this.playerChips -= this.call;
                this.tableChips.Text = "Chips : " + this.playerChips;

                // TODO Split conditions
                this.tablePot.Text = this.tablePot.Text != string.Empty
                                      ? (int.Parse(this.tablePot.Text) + this.call).ToString()
                                      : this.call.ToString();

                this.playerTurn = false;
                this.playerStatus.Text = "Call " + this.call;
                this.playerCall = this.call;
            }
            else if (this.playerChips <= this.call && this.call > 0)
            {
                this.tablePot.Text = (int.Parse(this.tablePot.Text) + this.playerChips).ToString();
                this.playerStatus.Text = "All in " + this.playerChips;
                this.playerChips = 0;
                this.tableChips.Text = "Chips : " + this.playerChips;
                this.playerTurn = false;
                this.buttonFold.Enabled = false;
                this.playerCall = this.playerChips;
            }

            await this.Turns();
        }

        // TODO Magic strings ! Replace with constants
        // TODO Code repetition ! Make a method and replace
        private async void BRaise_Click(object sender, EventArgs e)
        {
            this.Rules(0, 1, "Player", ref this.pType, ref this.playerPower, this.playerFinishedHisTurn);
            int parsedValue;
            if (this.tableRaise.Text != string.Empty && int.TryParse(this.tableRaise.Text, out parsedValue))
            {
                if (this.playerChips > this.call)
                {
                    if (this.raise * 2 > int.Parse(this.tableRaise.Text))
                    {
                        this.tableRaise.Text = (this.raise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }

                    if (this.playerChips >= int.Parse(this.tableRaise.Text))
                    {
                        this.call = int.Parse(this.tableRaise.Text);
                        this.raise = int.Parse(this.tableRaise.Text);
                        this.playerStatus.Text = "Raise " + this.call;
                        this.tablePot.Text = (int.Parse(this.tablePot.Text) + this.call).ToString();
                        this.buttonCall.Text = "Call";
                        this.playerChips -= int.Parse(this.tableRaise.Text);
                        this.raising = true;
                        this.last = 0;
                        this.playerRaise = Convert.ToInt32(this.raise);
                    }
                    else
                    {
                        this.call = this.playerChips;
                        this.raise = this.playerChips;
                        this.tablePot.Text = (int.Parse(this.tablePot.Text) + this.playerChips).ToString();
                        this.playerStatus.Text = "Raise " + this.call;
                        this.playerChips = 0;
                        this.raising = true;
                        this.last = 0;
                        this.playerRaise = Convert.ToInt32(this.raise);
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }

            this.playerTurn = false;
            await this.Turns();
        }

        private void BAdd_Click(object sender, EventArgs e)
        {
            if (this.tableAdd.Text != string.Empty)
            {
                this.playerChips += int.Parse(this.tableAdd.Text);
                this.botOneChips += int.Parse(this.tableAdd.Text);
                this.botTwoChips += int.Parse(this.tableAdd.Text);
                this.botThreeChips += int.Parse(this.tableAdd.Text);
                this.botFourChips += int.Parse(this.tableAdd.Text);
                this.botFiveChips += int.Parse(this.tableAdd.Text);
            }

            this.tableChips.Text = "Chips : " + this.playerChips;
        }

        private void BOptions_Click(object sender, EventArgs e)
        {
            this.tableBigBlind.Text = this.bigBlind.ToString();
            this.tableSmallBlind.Text = this.smallBlind.ToString();
            if (this.tableBigBlind.Visible == false)
            {
                this.tableBigBlind.Visible = true;
                this.tableSmallBlind.Visible = true;
                this.buttonBigBlind.Visible = true;
                this.buttonSmallBlind.Visible = true;
            }
            else
            {
                this.tableBigBlind.Visible = false;
                this.tableSmallBlind.Visible = false;
                this.buttonBigBlind.Visible = false;
                this.buttonSmallBlind.Visible = false;
            }
        }

        // TODO Code repetition !
        private void BSB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.tableSmallBlind.Text.Contains(",") || this.tableSmallBlind.Text.Contains("."))
            {
                MessageBox.Show("The Small Blind can be only round number !");
                this.tableSmallBlind.Text = this.smallBlind.ToString();
                return;
            }

            if (!int.TryParse(this.tableSmallBlind.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.tableSmallBlind.Text = this.smallBlind.ToString();
                return;
            }

            if (int.Parse(this.tableSmallBlind.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                this.tableSmallBlind.Text = this.smallBlind.ToString();
            }

            if (int.Parse(this.tableSmallBlind.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }

            if (int.Parse(this.tableSmallBlind.Text) >= 250 && int.Parse(this.tableSmallBlind.Text) <= 100000)
            {
                this.smallBlind = int.Parse(this.tableSmallBlind.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        // TODO Code repetition !
        private void BBB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.tableBigBlind.Text.Contains(",") || this.tableBigBlind.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                this.tableBigBlind.Text = this.bigBlind.ToString();
                return;
            }

            if (!int.TryParse(this.tableSmallBlind.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.tableSmallBlind.Text = this.bigBlind.ToString();
                return;
            }

            if (int.Parse(this.tableBigBlind.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                this.tableBigBlind.Text = this.bigBlind.ToString();
            }

            if (int.Parse(this.tableBigBlind.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }

            if (int.Parse(this.tableBigBlind.Text) < 500 || int.Parse(this.tableBigBlind.Text) > 200000)
            {
                return;
            }

            this.bigBlind = int.Parse(this.tableBigBlind.Text);
            MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
        }

        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            this.width = this.Width;
            this.height = this.Height;
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tbSB_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbBotChips2_TextChanged(object sender, EventArgs e)
        {

        }

        private void botTwoStatus_Click(object sender, EventArgs e)
        {

        }

        private void tbRaise_TextChanged(object sender, EventArgs e)
        {

        }

        private void PlayerButtonTimer_Click(object sender, EventArgs e)
        {

        }
    }
}