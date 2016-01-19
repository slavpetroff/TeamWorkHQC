namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;

    public partial class Form1 : Form
    {
        #region Variables
        ProgressBar progressBar = new ProgressBar();

        Panel playerPanel = new Panel();
        Panel botOnePanel = new Panel();
        Panel botTwoPanel = new Panel();
        Panel botThreePanel = new Panel();
        Panel botFourPanel = new Panel();
        Panel botFivePanel = new Panel();

        int call = 500;
        int foldedPlayers = 5;

        public int playerChips = 10000;
        public int botOneChips = 10000;
        public int botTwoChips = 10000;
        public int botThreeChips = 10000;
        public int botFourChips = 10000;
        public int botFiveChips = 10000;

        double type;
        double rounds = 0;

        double botOnePower;
        double botTwoPower;
        double botThreePower;
        double botFourPower;
        double botFivePower;
        double playerPower = 0;

        double pType = -1;
        double Raise = 0;

        double botOneType = -1;
        double botTwoType = -1;
        double botThreeType = -1;
        double botFourType = -1;
        double botFiveType = -1;

        bool botOneTurn = false;
        bool botTwoTurn = false;
        bool botThreeTurn = false;
        bool botFourTurn = false;
        bool botFiveTurn = false;

        bool botOneFinishedTurn = false;
        bool botTwoFinishedTurn = false;
        bool botThreeFinishedTurn = false;
        bool botFourFinishedTurn = false;
        bool botFiveFinishedTurn = false;

        bool playerFolded;
        bool botOneFolded;
        bool botTwoFolded;
        bool botThreeFolded;
        bool botFourFolded;
        bool botFiveFolded;

        bool intsadded;
        bool changed;

        int playerCall = 0;
        int botOneCall = 0;
        int botTwoCall = 0;
        int botThreeCall = 0;
        int botFourCall = 0;
        int botFiveCall = 0;

        int playerRaise = 0;
        int botOneRaise = 0;
        int botTwoRaise = 0;
        int botThreeRaise = 0;
        int botFourRaise = 0;
        int botFiveRaise = 0;

        int height;
        int width;

        int winners = 0;
        int Flop = 1;
        int Turn = 2;
        int River = 3;
        int End = 4;
        int maxLeft = 6;
        int last = 123;
        int raisedTurn = 1;

        List<bool?> bools = new List<bool?>();
        List<Type> Win = new List<Type>();
        List<string> CheckWinners = new List<string>();
        List<int> ints = new List<int>();

        bool playerFinishedHisTurn = false;
        bool playerTurn = true;
        bool restart = false;
        bool raising = false;

        Poker.Type sorted;
        //string[] ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
        string[] ImgLocation ={
                   "Assets\\Cards\\33.png",
                    "Assets\\Cards\\22.png",
                    "Assets\\Cards\\29.png","Assets\\Cards\\21.png",
                    "Assets\\Cards\\36.png","Assets\\Cards\\17.png",
                    "Assets\\Cards\\40.png","Assets\\Cards\\16.png",
                    "Assets\\Cards\\5.png","Assets\\Cards\\47.png",
                    "Assets\\Cards\\37.png",
                    "Assets\\Cards\\13.png",
                    "Assets\\Cards\\12.png",
                    "Assets\\Cards\\8.png","Assets\\Cards\\18.png",
                    "Assets\\Cards\\15.png","Assets\\Cards\\27.png"};
        int[] reserve = new int[17];
        Image[] deck = new Image[52];
        PictureBox[] cardHolder = new PictureBox[52];
        Timer timer = new Timer();
        Timer updates = new Timer();

        int t = 60;
        int i;
        int bigBlind = 500;
        int smallBlind = 250;
        int up = 10000000;
        int turnCount = 0;

        #endregion
        public Form1()
        {
            bools.Add(playerFinishedHisTurn); bools.Add(botOneFinishedTurn); bools.Add(botTwoFinishedTurn); bools.Add(botThreeFinishedTurn); bools.Add(botFourFinishedTurn); bools.Add(botFiveFinishedTurn);
            call = bigBlind;
            MaximizeBox = false;
            MinimizeBox = false;
            updates.Start();
            InitializeComponent();
            width = this.Width;
            height = this.Height;
            Shuffle();
            tbPot.Enabled = false;
            tbChips.Enabled = false;
            tbBotChips1.Enabled = false;
            tbBotChips2.Enabled = false;
            tbBotChips3.Enabled = false;
            tbBotChips4.Enabled = false;
            tbBotChips5.Enabled = false;
            tbChips.Text = "Chips : " + playerChips.ToString();
            tbBotChips1.Text = "Chips : " + botOneChips.ToString();
            tbBotChips2.Text = "Chips : " + botTwoChips.ToString();
            tbBotChips3.Text = "Chips : " + botThreeChips.ToString();
            tbBotChips4.Text = "Chips : " + botFourChips.ToString();
            tbBotChips5.Text = "Chips : " + botFiveChips.ToString();
            timer.Interval = (1 * 1 * 1000);
            timer.Tick += timer_Tick;
            updates.Interval = (1 * 1 * 100);
            updates.Tick += Update_Tick;
            tbBB.Visible = true;
            tbSB.Visible = true;
            bBB.Visible = true;
            bSB.Visible = true;
            tbBB.Visible = true;
            tbSB.Visible = true;
            bBB.Visible = true;
            bSB.Visible = true;
            tbBB.Visible = false;
            tbSB.Visible = false;
            bBB.Visible = false;
            bSB.Visible = false;
            tbRaise.Text = (bigBlind * 2).ToString();
        }
        async Task Shuffle()
        {
            bools.Add(playerFinishedHisTurn); bools.Add(botOneFinishedTurn); bools.Add(botTwoFinishedTurn); bools.Add(botThreeFinishedTurn); bools.Add(botFourFinishedTurn); bools.Add(botFiveFinishedTurn);
            bCall.Enabled = false;
            bRaise.Enabled = false;
            bFold.Enabled = false;
            bCheck.Enabled = false;
            MaximizeBox = false;
            MinimizeBox = false;
            bool check = false;
            Bitmap backImage = new Bitmap("Assets\\Back\\Back.png");
            int horizontal = 580, vertical = 480;
            Random r = new Random();
            for (i = ImgLocation.Length; i > 0; i--)
            {
                int j = r.Next(i);
                var k = ImgLocation[j];
                ImgLocation[j] = ImgLocation[i - 1];
                ImgLocation[i - 1] = k;
            }
            for (i = 0; i < 17; i++)
            {

                deck[i] = Image.FromFile(ImgLocation[i]);
                var charsToRemove = new string[] { "Assets\\Cards\\", ".png" };
                foreach (var c in charsToRemove)
                {
                    ImgLocation[i] = ImgLocation[i].Replace(c, string.Empty);
                }
                reserve[i] = int.Parse(ImgLocation[i]) - 1;
                cardHolder[i] = new PictureBox();
                cardHolder[i].SizeMode = PictureBoxSizeMode.StretchImage;
                cardHolder[i].Height = 130;
                cardHolder[i].Width = 80;
                this.Controls.Add(cardHolder[i]);
                cardHolder[i].Name = "pb" + i.ToString();
                await Task.Delay(200);
                #region Throwing Cards
                if (i < 2)
                {
                    if (cardHolder[0].Tag != null)
                    {
                        cardHolder[1].Tag = reserve[1];
                    }
                    cardHolder[0].Tag = reserve[0];
                    cardHolder[i].Image = deck[i];
                    cardHolder[i].Anchor = (AnchorStyles.Bottom);
                    //Holder[i].Dock = DockStyle.Top;
                    cardHolder[i].Location = new Point(horizontal, vertical);
                    horizontal += cardHolder[i].Width;
                    this.Controls.Add(playerPanel);
                    playerPanel.Location = new Point(cardHolder[0].Left - 10, cardHolder[0].Top - 10);
                    playerPanel.BackColor = Color.DarkBlue;
                    playerPanel.Height = 150;
                    playerPanel.Width = 180;
                    playerPanel.Visible = false;
                }
                if (botOneChips > 0)
                {
                    foldedPlayers--;
                    if (i >= 2 && i < 4)
                    {
                        if (cardHolder[2].Tag != null)
                        {
                            cardHolder[3].Tag = reserve[3];
                        }
                        cardHolder[2].Tag = reserve[2];
                        if (!check)
                        {
                            horizontal = 15;
                            vertical = 420;
                        }
                        check = true;
                        cardHolder[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                        cardHolder[i].Image = backImage;
                        cardHolder[i].Image = deck[i];
                        cardHolder[i].Location = new Point(horizontal, vertical);
                        horizontal += cardHolder[i].Width;
                        cardHolder[i].Visible = true;
                        this.Controls.Add(botOnePanel);
                        botOnePanel.Location = new Point(cardHolder[2].Left - 10, cardHolder[2].Top - 10);
                        botOnePanel.BackColor = Color.DarkBlue;
                        botOnePanel.Height = 150;
                        botOnePanel.Width = 180;
                        botOnePanel.Visible = false;
                        if (i == 3)
                        {
                            check = false;
                        }
                    }
                }
                if (botTwoChips > 0)
                {
                    foldedPlayers--;
                    if (i >= 4 && i < 6)
                    {
                        if (cardHolder[4].Tag != null)
                        {
                            cardHolder[5].Tag = reserve[5];
                        }
                        cardHolder[4].Tag = reserve[4];
                        if (!check)
                        {
                            horizontal = 75;
                            vertical = 65;
                        }
                        check = true;
                        cardHolder[i].Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                        cardHolder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        cardHolder[i].Location = new Point(horizontal, vertical);
                        horizontal += cardHolder[i].Width;
                        cardHolder[i].Visible = true;
                        this.Controls.Add(botTwoPanel);
                        botTwoPanel.Location = new Point(cardHolder[4].Left - 10, cardHolder[4].Top - 10);
                        botTwoPanel.BackColor = Color.DarkBlue;
                        botTwoPanel.Height = 150;
                        botTwoPanel.Width = 180;
                        botTwoPanel.Visible = false;
                        if (i == 5)
                        {
                            check = false;
                        }
                    }
                }
                if (botThreeChips > 0)
                {
                    foldedPlayers--;
                    if (i >= 6 && i < 8)
                    {
                        if (cardHolder[6].Tag != null)
                        {
                            cardHolder[7].Tag = reserve[7];
                        }
                        cardHolder[6].Tag = reserve[6];
                        if (!check)
                        {
                            horizontal = 590;
                            vertical = 25;
                        }
                        check = true;
                        cardHolder[i].Anchor = (AnchorStyles.Top);
                        cardHolder[i].Image = backImage;
                        cardHolder[i].Image = deck[i];
                        cardHolder[i].Location = new Point(horizontal, vertical);
                        horizontal += cardHolder[i].Width;
                        cardHolder[i].Visible = true;
                        this.Controls.Add(botThreePanel);
                        botThreePanel.Location = new Point(cardHolder[6].Left - 10, cardHolder[6].Top - 10);
                        botThreePanel.BackColor = Color.DarkBlue;
                        botThreePanel.Height = 150;
                        botThreePanel.Width = 180;
                        botThreePanel.Visible = false;
                        if (i == 7)
                        {
                            check = false;
                        }
                    }
                }
                if (botFourChips > 0)
                {
                    foldedPlayers--;
                    if (i >= 8 && i < 10)
                    {
                        if (cardHolder[8].Tag != null)
                        {
                            cardHolder[9].Tag = reserve[9];
                        }
                        cardHolder[8].Tag = reserve[8];
                        if (!check)
                        {
                            horizontal = 1115;
                            vertical = 65;
                        }
                        check = true;
                        cardHolder[i].Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                        cardHolder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        cardHolder[i].Location = new Point(horizontal, vertical);
                        horizontal += cardHolder[i].Width;
                        cardHolder[i].Visible = true;
                        this.Controls.Add(botFourPanel);
                        botFourPanel.Location = new Point(cardHolder[8].Left - 10, cardHolder[8].Top - 10);
                        botFourPanel.BackColor = Color.DarkBlue;
                        botFourPanel.Height = 150;
                        botFourPanel.Width = 180;
                        botFourPanel.Visible = false;
                        if (i == 9)
                        {
                            check = false;
                        }
                    }
                }
                if (botFiveChips > 0)
                {
                    foldedPlayers--;
                    if (i >= 10 && i < 12)
                    {
                        if (cardHolder[10].Tag != null)
                        {
                            cardHolder[11].Tag = reserve[11];
                        }
                        cardHolder[10].Tag = reserve[10];
                        if (!check)
                        {
                            horizontal = 1160;
                            vertical = 420;
                        }
                        check = true;
                        cardHolder[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
                        cardHolder[i].Image = backImage;
                        cardHolder[i].Image = deck[i];
                        cardHolder[i].Location = new Point(horizontal, vertical);
                        horizontal += cardHolder[i].Width;
                        cardHolder[i].Visible = true;
                        this.Controls.Add(botFivePanel);
                        botFivePanel.Location = new Point(cardHolder[10].Left - 10, cardHolder[10].Top - 10);
                        botFivePanel.BackColor = Color.DarkBlue;
                        botFivePanel.Height = 150;
                        botFivePanel.Width = 180;
                        botFivePanel.Visible = false;
                        if (i == 11)
                        {
                            check = false;
                        }
                    }
                }
                if (i >= 12)
                {
                    cardHolder[12].Tag = reserve[12];
                    if (i > 12) cardHolder[13].Tag = reserve[13];
                    if (i > 13) cardHolder[14].Tag = reserve[14];
                    if (i > 14) cardHolder[15].Tag = reserve[15];
                    if (i > 15)
                    {
                        cardHolder[16].Tag = reserve[16];

                    }
                    if (!check)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }
                    check = true;
                    if (cardHolder[i] != null)
                    {
                        cardHolder[i].Anchor = AnchorStyles.None;
                        cardHolder[i].Image = backImage;
                        cardHolder[i].Image = deck[i];
                        cardHolder[i].Location = new Point(horizontal, vertical);
                        horizontal += 110;
                    }
                }
                #endregion
                if (botOneChips <= 0)
                {
                    botOneFinishedTurn = true;
                    cardHolder[2].Visible = false;
                    cardHolder[3].Visible = false;
                }
                else
                {
                    botOneFinishedTurn = false;
                    if (i == 3)
                    {
                        if (cardHolder[3] != null)
                        {
                            cardHolder[2].Visible = true;
                            cardHolder[3].Visible = true;
                        }
                    }
                }
                if (botTwoChips <= 0)
                {
                    botTwoFinishedTurn = true;
                    cardHolder[4].Visible = false;
                    cardHolder[5].Visible = false;
                }
                else
                {
                    botTwoFinishedTurn = false;
                    if (i == 5)
                    {
                        if (cardHolder[5] != null)
                        {
                            cardHolder[4].Visible = true;
                            cardHolder[5].Visible = true;
                        }
                    }
                }
                if (botThreeChips <= 0)
                {
                    botThreeFinishedTurn = true;
                    cardHolder[6].Visible = false;
                    cardHolder[7].Visible = false;
                }
                else
                {
                    botThreeFinishedTurn = false;
                    if (i == 7)
                    {
                        if (cardHolder[7] != null)
                        {
                            cardHolder[6].Visible = true;
                            cardHolder[7].Visible = true;
                        }
                    }
                }
                if (botFourChips <= 0)
                {
                    botFourFinishedTurn = true;
                    cardHolder[8].Visible = false;
                    cardHolder[9].Visible = false;
                }
                else
                {
                    botFourFinishedTurn = false;
                    if (i == 9)
                    {
                        if (cardHolder[9] != null)
                        {
                            cardHolder[8].Visible = true;
                            cardHolder[9].Visible = true;
                        }
                    }
                }
                if (botFiveChips <= 0)
                {
                    botFiveFinishedTurn = true;
                    cardHolder[10].Visible = false;
                    cardHolder[11].Visible = false;
                }
                else
                {
                    botFiveFinishedTurn = false;
                    if (i == 11)
                    {
                        if (cardHolder[11] != null)
                        {
                            cardHolder[10].Visible = true;
                            cardHolder[11].Visible = true;
                        }
                    }
                }
                if (i == 16)
                {
                    if (!restart)
                    {
                        MaximizeBox = true;
                        MinimizeBox = true;
                    }
                    timer.Start();
                }
            }
            if (foldedPlayers == 5)
            {
                DialogResult dialogResult = MessageBox.Show("Would You Like To Play Again ?", "You Won , Congratulations ! ", MessageBoxButtons.YesNo);
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
                foldedPlayers = 5;
            }
            if (i == 17)
            {
                bRaise.Enabled = true;
                bCall.Enabled = true;
                bRaise.Enabled = true;
                bRaise.Enabled = true;
                bFold.Enabled = true;
            }
        }
        async Task Turns()
        {
            #region Rotating
            if (!playerFinishedHisTurn)
            {
                if (playerTurn)
                {
                    FixCall(pStatus, ref playerCall, ref playerRaise, 1);
                    //MessageBox.Show("Player's Turn");
                    pbTimer.Visible = true;
                    pbTimer.Value = 1000;
                    t = 60;
                    up = 10000000;
                    timer.Start();
                    bRaise.Enabled = true;
                    bCall.Enabled = true;
                    bRaise.Enabled = true;
                    bRaise.Enabled = true;
                    bFold.Enabled = true;
                    turnCount++;
                    FixCall(pStatus, ref playerCall, ref playerRaise, 2);
                }
            }
            if (playerFinishedHisTurn || !playerTurn)
            {
                await AllIn();
                if (playerFinishedHisTurn && !playerFolded)
                {
                    if (bCall.Text.Contains("All in") == false || bRaise.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        maxLeft--;
                        playerFolded = true;
                    }
                }
                await CheckRaise(0, 0);
                pbTimer.Visible = false;
                bRaise.Enabled = false;
                bCall.Enabled = false;
                bRaise.Enabled = false;
                bRaise.Enabled = false;
                bFold.Enabled = false;
                timer.Stop();
                botOneTurn = true;
                if (!botOneFinishedTurn)
                {
                    if (botOneTurn)
                    {
                        FixCall(b1Status, ref botOneCall, ref botOneRaise, 1);
                        FixCall(b1Status, ref botOneCall, ref botOneRaise, 2);
                        Rules(2, 3, "Bot 1", ref botOneType, ref botOnePower, botOneFinishedTurn);
                        MessageBox.Show("Bot 1's Turn");
                        Ai(2, 3, ref botOneChips, ref botOneTurn, ref botOneFinishedTurn, b1Status, 0, botOnePower, botOneType);
                        turnCount++;
                        last = 1;
                        botOneTurn = false;
                        botTwoTurn = true;
                    }
                }
                if (botOneFinishedTurn && !botOneFolded)
                {
                    bools.RemoveAt(1);
                    bools.Insert(1, null);
                    maxLeft--;
                    botOneFolded = true;
                }
                if (botOneFinishedTurn || !botOneTurn)
                {
                    await CheckRaise(1, 1);
                    botTwoTurn = true;
                }
                if (!botTwoFinishedTurn)
                {
                    if (botTwoTurn)
                    {
                        FixCall(b2Status, ref botTwoCall, ref botTwoRaise, 1);
                        FixCall(b2Status, ref botTwoCall, ref botTwoRaise, 2);
                        Rules(4, 5, "Bot 2", ref botTwoType, ref botTwoPower, botTwoFinishedTurn);
                        MessageBox.Show("Bot 2's Turn");
                        Ai(4, 5, ref botTwoChips, ref botTwoTurn, ref botTwoFinishedTurn, b2Status, 1, botTwoPower, botTwoType);
                        turnCount++;
                        last = 2;
                        botTwoTurn = false;
                        botThreeTurn = true;
                    }
                }
                if (botTwoFinishedTurn && !botTwoFolded)
                {
                    bools.RemoveAt(2);
                    bools.Insert(2, null);
                    maxLeft--;
                    botTwoFolded = true;
                }
                if (botTwoFinishedTurn || !botTwoTurn)
                {
                    await CheckRaise(2, 2);
                    botThreeTurn = true;
                }
                if (!botThreeFinishedTurn)
                {
                    if (botThreeTurn)
                    {
                        FixCall(b3Status, ref botThreeCall, ref botThreeRaise, 1);
                        FixCall(b3Status, ref botThreeCall, ref botThreeRaise, 2);
                        Rules(6, 7, "Bot 3", ref botThreeType, ref botThreePower, botThreeFinishedTurn);
                        MessageBox.Show("Bot 3's Turn");
                        Ai(6, 7, ref botThreeChips, ref botThreeTurn, ref botThreeFinishedTurn, b3Status, 2, botThreePower, botThreeType);
                        turnCount++;
                        last = 3;
                        botThreeTurn = false;
                        botFourTurn = true;
                    }
                }
                if (botThreeFinishedTurn && !botThreeFolded)
                {
                    bools.RemoveAt(3);
                    bools.Insert(3, null);
                    maxLeft--;
                    botThreeFolded = true;
                }
                if (botThreeFinishedTurn || !botThreeTurn)
                {
                    await CheckRaise(3, 3);
                    botFourTurn = true;
                }
                if (!botFourFinishedTurn)
                {
                    if (botFourTurn)
                    {
                        FixCall(b4Status, ref botFourCall, ref botFourRaise, 1);
                        FixCall(b4Status, ref botFourCall, ref botFourRaise, 2);
                        Rules(8, 9, "Bot 4", ref botFourType, ref botFourPower, botFourFinishedTurn);
                        MessageBox.Show("Bot 4's Turn");
                        Ai(8, 9, ref botFourChips, ref botFourTurn, ref botFourFinishedTurn, b4Status, 3, botFourPower, botFourType);
                        turnCount++;
                        last = 4;
                        botFourTurn = false;
                        botFiveTurn = true;
                    }
                }
                if (botFourFinishedTurn && !botFourFolded)
                {
                    bools.RemoveAt(4);
                    bools.Insert(4, null);
                    maxLeft--;
                    botFourFolded = true;
                }
                if (botFourFinishedTurn || !botFourTurn)
                {
                    await CheckRaise(4, 4);
                    botFiveTurn = true;
                }
                if (!botFiveFinishedTurn)
                {
                    if (botFiveTurn)
                    {
                        FixCall(b5Status, ref botFiveCall, ref botFiveRaise, 1);
                        FixCall(b5Status, ref botFiveCall, ref botFiveRaise, 2);
                        Rules(10, 11, "Bot 5", ref botFiveType, ref botFivePower, botFiveFinishedTurn);
                        MessageBox.Show("Bot 5's Turn");
                        Ai(10, 11, ref botFiveChips, ref botFiveTurn, ref botFiveFinishedTurn, b5Status, 4, botFivePower, botFiveType);
                        turnCount++;
                        last = 5;
                        botFiveTurn = false;
                    }
                }
                if (botFiveFinishedTurn && !botFiveFolded)
                {
                    bools.RemoveAt(5);
                    bools.Insert(5, null);
                    maxLeft--;
                    botFiveFolded = true;
                }
                if (botFiveFinishedTurn || !botFiveTurn)
                {
                    await CheckRaise(5, 5);
                    playerTurn = true;
                }
                if (playerFinishedHisTurn && !playerFolded)
                {
                    if (bCall.Text.Contains("All in") == false || bRaise.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        maxLeft--;
                        playerFolded = true;
                    }
                }
                #endregion
                await AllIn();
                if (!restart)
                {
                    await Turns();
                }
                restart = false;
            }
        }

        void Rules(int c1, int c2, string currentText, ref double current, ref double Power, bool foldedTurn)
        {
            if (c1 == 0 && c2 == 1)
            {
            }
            if (!foldedTurn || c1 == 0 && c2 == 1 && pStatus.Text.Contains("Fold") == false)
            {
                #region Variables
                bool done = false, vf = false;
                int[] Straight1 = new int[5];
                int[] Straight = new int[7];
                Straight[0] = reserve[c1];
                Straight[1] = reserve[c2];
                Straight1[0] = Straight[2] = reserve[12];
                Straight1[1] = Straight[3] = reserve[13];
                Straight1[2] = Straight[4] = reserve[14];
                Straight1[3] = Straight[5] = reserve[15];
                Straight1[4] = Straight[6] = reserve[16];
                var a = Straight.Where(o => o % 4 == 0).ToArray();
                var b = Straight.Where(o => o % 4 == 1).ToArray();
                var c = Straight.Where(o => o % 4 == 2).ToArray();
                var d = Straight.Where(o => o % 4 == 3).ToArray();
                var st1 = a.Select(o => o / 4).Distinct().ToArray();
                var st2 = b.Select(o => o / 4).Distinct().ToArray();
                var st3 = c.Select(o => o / 4).Distinct().ToArray();
                var st4 = d.Select(o => o / 4).Distinct().ToArray();
                Array.Sort(Straight); Array.Sort(st1); Array.Sort(st2); Array.Sort(st3); Array.Sort(st4);
                #endregion
                for (i = 0; i < 16; i++)
                {
                    if (reserve[i] == int.Parse(cardHolder[c1].Tag.ToString()) && reserve[i + 1] == int.Parse(cardHolder[c2].Tag.ToString()))
                    {
                        //Pair from Hand current = 1

                        rPairFromHand(ref current, ref Power);

                        #region Pair or Two Pair from Table current = 2 || 0
                        rPairTwoPair(ref current, ref Power);
                        #endregion

                        #region Two Pair current = 2
                        rTwoPair(ref current, ref Power);
                        #endregion

                        #region Three of a kind current = 3
                        rThreeOfAKind(ref current, ref Power, Straight);
                        #endregion

                        #region Straight current = 4
                        rStraight(ref current, ref Power, Straight);
                        #endregion

                        #region Flush current = 5 || 5.5
                        rFlush(ref current, ref Power, ref vf, Straight1);
                        #endregion

                        #region Full House current = 6
                        rFullHouse(ref current, ref Power, ref done, Straight);
                        #endregion

                        #region Four of a Kind current = 7
                        rFourOfAKind(ref current, ref Power, Straight);
                        #endregion

                        #region Straight Flush current = 8 || 9
                        rStraightFlush(ref current, ref Power, st1, st2, st3, st4);
                        #endregion

                        #region High Card current = -1
                        rHighCard(ref current, ref Power);
                        #endregion
                    }
                }
            }
        }
        private void rStraightFlush(ref double current, ref double Power, int[] st1, int[] st2, int[] st3, int[] st4)
        {
            if (current >= -1)
            {
                if (st1.Length >= 5)
                {
                    if (st1[0] + 4 == st1[4])
                    {
                        current = 8;
                        Power = (st1.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st1[0] == 0 && st1[1] == 9 && st1[2] == 10 && st1[3] == 11 && st1[0] + 12 == st1[4])
                    {
                        current = 9;
                        Power = (st1.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st2.Length >= 5)
                {
                    if (st2[0] + 4 == st2[4])
                    {
                        current = 8;
                        Power = (st2.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st2[0] == 0 && st2[1] == 9 && st2[2] == 10 && st2[3] == 11 && st2[0] + 12 == st2[4])
                    {
                        current = 9;
                        Power = (st2.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st3.Length >= 5)
                {
                    if (st3[0] + 4 == st3[4])
                    {
                        current = 8;
                        Power = (st3.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st3[0] == 0 && st3[1] == 9 && st3[2] == 10 && st3[3] == 11 && st3[0] + 12 == st3[4])
                    {
                        current = 9;
                        Power = (st3.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st4.Length >= 5)
                {
                    if (st4[0] + 4 == st4[4])
                    {
                        current = 8;
                        Power = (st4.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st4[0] == 0 && st4[1] == 9 && st4[2] == 10 && st4[3] == 11 && st4[0] + 12 == st4[4])
                    {
                        current = 9;
                        Power = (st4.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }
        private void rFourOfAKind(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (Straight[j] / 4 == Straight[j + 1] / 4 && Straight[j] / 4 == Straight[j + 2] / 4 &&
                        Straight[j] / 4 == Straight[j + 3] / 4)
                    {
                        current = 7;
                        Power = (Straight[j] / 4) * 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 7 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Straight[j] / 4 == 0 && Straight[j + 1] / 4 == 0 && Straight[j + 2] / 4 == 0 && Straight[j + 3] / 4 == 0)
                    {
                        current = 7;
                        Power = 13 * 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 7 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }
        private void rFullHouse(ref double current, ref double Power, ref bool done, int[] Straight)
        {
            if (current >= -1)
            {
                type = Power;
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3 || done)
                    {
                        if (fh.Length == 2)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                current = 6;
                                Power = 13 * 2 + current * 100;
                                Win.Add(new Type() { Power = Power, Current = 6 });
                                sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                            if (fh.Max() / 4 > 0)
                            {
                                current = 6;
                                Power = fh.Max() / 4 * 2 + current * 100;
                                Win.Add(new Type() { Power = Power, Current = 6 });
                                sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                        }
                        if (!done)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                Power = 13;
                                done = true;
                                j = -1;
                            }
                            else
                            {
                                Power = fh.Max() / 4;
                                done = true;
                                j = -1;
                            }
                        }
                    }
                }
                if (current != 6)
                {
                    Power = type;
                }
            }
        }
        private void rFlush(ref double current, ref double Power, ref bool vf, int[] Straight1)
        {
            if (current >= -1)
            {
                var f1 = Straight1.Where(o => o % 4 == 0).ToArray();
                var f2 = Straight1.Where(o => o % 4 == 1).ToArray();
                var f3 = Straight1.Where(o => o % 4 == 2).ToArray();
                var f4 = Straight1.Where(o => o % 4 == 3).ToArray();
                if (f1.Length == 3 || f1.Length == 4)
                {
                    if (reserve[i] % 4 == reserve[i + 1] % 4 && reserve[i] % 4 == f1[0] % 4)
                    {
                        if (reserve[i] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = reserve[i] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (reserve[i + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = reserve[i + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (reserve[i] / 4 < f1.Max() / 4 && reserve[i + 1] / 4 < f1.Max() / 4)
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f1.Length == 4)//different cards in hand
                {
                    if (reserve[i] % 4 != reserve[i + 1] % 4 && reserve[i] % 4 == f1[0] % 4)
                    {
                        if (reserve[i] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = reserve[i] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (reserve[i + 1] % 4 != reserve[i] % 4 && reserve[i + 1] % 4 == f1[0] % 4)
                    {
                        if (reserve[i + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = reserve[i + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f1.Length == 5)
                {
                    if (reserve[i] % 4 == f1[0] % 4 && reserve[i] / 4 > f1.Min() / 4)
                    {
                        current = 5;
                        Power = reserve[i] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (reserve[i + 1] % 4 == f1[0] % 4 && reserve[i + 1] / 4 > f1.Min() / 4)
                    {
                        current = 5;
                        Power = reserve[i + 1] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (reserve[i] / 4 < f1.Min() / 4 && reserve[i + 1] / 4 < f1.Min())
                    {
                        current = 5;
                        Power = f1.Max() + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f2.Length == 3 || f2.Length == 4)
                {
                    if (reserve[i] % 4 == reserve[i + 1] % 4 && reserve[i] % 4 == f2[0] % 4)
                    {
                        if (reserve[i] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = reserve[i] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (reserve[i + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = reserve[i + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (reserve[i] / 4 < f2.Max() / 4 && reserve[i + 1] / 4 < f2.Max() / 4)
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f2.Length == 4)//different cards in hand
                {
                    if (reserve[i] % 4 != reserve[i + 1] % 4 && reserve[i] % 4 == f2[0] % 4)
                    {
                        if (reserve[i] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = reserve[i] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (reserve[i + 1] % 4 != reserve[i] % 4 && reserve[i + 1] % 4 == f2[0] % 4)
                    {
                        if (reserve[i + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = reserve[i + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f2.Length == 5)
                {
                    if (reserve[i] % 4 == f2[0] % 4 && reserve[i] / 4 > f2.Min() / 4)
                    {
                        current = 5;
                        Power = reserve[i] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (reserve[i + 1] % 4 == f2[0] % 4 && reserve[i + 1] / 4 > f2.Min() / 4)
                    {
                        current = 5;
                        Power = reserve[i + 1] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (reserve[i] / 4 < f2.Min() / 4 && reserve[i + 1] / 4 < f2.Min())
                    {
                        current = 5;
                        Power = f2.Max() + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f3.Length == 3 || f3.Length == 4)
                {
                    if (reserve[i] % 4 == reserve[i + 1] % 4 && reserve[i] % 4 == f3[0] % 4)
                    {
                        if (reserve[i] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = reserve[i] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (reserve[i + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = reserve[i + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (reserve[i] / 4 < f3.Max() / 4 && reserve[i + 1] / 4 < f3.Max() / 4)
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f3.Length == 4)//different cards in hand
                {
                    if (reserve[i] % 4 != reserve[i + 1] % 4 && reserve[i] % 4 == f3[0] % 4)
                    {
                        if (reserve[i] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = reserve[i] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (reserve[i + 1] % 4 != reserve[i] % 4 && reserve[i + 1] % 4 == f3[0] % 4)
                    {
                        if (reserve[i + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = reserve[i + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f3.Length == 5)
                {
                    if (reserve[i] % 4 == f3[0] % 4 && reserve[i] / 4 > f3.Min() / 4)
                    {
                        current = 5;
                        Power = reserve[i] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (reserve[i + 1] % 4 == f3[0] % 4 && reserve[i + 1] / 4 > f3.Min() / 4)
                    {
                        current = 5;
                        Power = reserve[i + 1] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (reserve[i] / 4 < f3.Min() / 4 && reserve[i + 1] / 4 < f3.Min())
                    {
                        current = 5;
                        Power = f3.Max() + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f4.Length == 3 || f4.Length == 4)
                {
                    if (reserve[i] % 4 == reserve[i + 1] % 4 && reserve[i] % 4 == f4[0] % 4)
                    {
                        if (reserve[i] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = reserve[i] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (reserve[i + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = reserve[i + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (reserve[i] / 4 < f4.Max() / 4 && reserve[i + 1] / 4 < f4.Max() / 4)
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f4.Length == 4)//different cards in hand
                {
                    if (reserve[i] % 4 != reserve[i + 1] % 4 && reserve[i] % 4 == f4[0] % 4)
                    {
                        if (reserve[i] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = reserve[i] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (reserve[i + 1] % 4 != reserve[i] % 4 && reserve[i + 1] % 4 == f4[0] % 4)
                    {
                        if (reserve[i + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = reserve[i + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f4.Length == 5)
                {
                    if (reserve[i] % 4 == f4[0] % 4 && reserve[i] / 4 > f4.Min() / 4)
                    {
                        current = 5;
                        Power = reserve[i] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (reserve[i + 1] % 4 == f4[0] % 4 && reserve[i + 1] / 4 > f4.Min() / 4)
                    {
                        current = 5;
                        Power = reserve[i + 1] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (reserve[i] / 4 < f4.Min() / 4 && reserve[i + 1] / 4 < f4.Min())
                    {
                        current = 5;
                        Power = f4.Max() + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }
                //ace
                if (f1.Length > 0)
                {
                    if (reserve[i] / 4 == 0 && reserve[i] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (reserve[i + 1] / 4 == 0 && reserve[i + 1] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f2.Length > 0)
                {
                    if (reserve[i] / 4 == 0 && reserve[i] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (reserve[i + 1] / 4 == 0 && reserve[i + 1] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f3.Length > 0)
                {
                    if (reserve[i] / 4 == 0 && reserve[i] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (reserve[i + 1] / 4 == 0 && reserve[i + 1] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f4.Length > 0)
                {
                    if (reserve[i] / 4 == 0 && reserve[i] % 4 == f4[0] % 4 && vf && f4.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (reserve[i + 1] / 4 == 0 && reserve[i + 1] % 4 == f4[0] % 4 && vf)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }
        private void rStraight(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                var op = Straight.Select(o => o / 4).Distinct().ToArray();
                for (int j = 0; j < op.Length - 4; j++)
                {
                    if (op[j] + 4 == op[j + 4])
                    {
                        if (op.Max() - 4 == op[j])
                        {
                            current = 4;
                            Power = op.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 4 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                        else
                        {
                            current = 4;
                            Power = op[j + 4] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 4 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                    }
                    if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
                    {
                        current = 4;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 4 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }
        private void rThreeOfAKind(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3)
                    {
                        if (fh.Max() / 4 == 0)
                        {
                            current = 3;
                            Power = 13 * 3 + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 3 });
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 3;
                            Power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 3 });
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                }
            }
        }
        private void rTwoPair(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    if (reserve[i] / 4 != reserve[i + 1] / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (tc - k < 12)
                            {
                                max--;
                            }
                            if (tc - k >= 12)
                            {
                                if (reserve[i] / 4 == reserve[tc] / 4 && reserve[i + 1] / 4 == reserve[tc - k] / 4 ||
                                    reserve[i + 1] / 4 == reserve[tc] / 4 && reserve[i] / 4 == reserve[tc - k] / 4)
                                {
                                    if (!msgbox)
                                    {
                                        if (reserve[i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (reserve[i + 1] / 4) * 2 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (reserve[i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (reserve[i] / 4) * 2 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (reserve[i + 1] / 4 != 0 && reserve[i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (reserve[i] / 4) * 2 + (reserve[i + 1] / 4) * 2 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                    }
                                    msgbox = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void rPairTwoPair(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                bool msgbox1 = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    for (int k = 1; k <= max; k++)
                    {
                        if (tc - k < 12)
                        {
                            max--;
                        }
                        if (tc - k >= 12)
                        {
                            if (reserve[tc] / 4 == reserve[tc - k] / 4)
                            {
                                if (reserve[tc] / 4 != reserve[i] / 4 && reserve[tc] / 4 != reserve[i + 1] / 4 && current == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (reserve[i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (reserve[i] / 4) * 2 + 13 * 4 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (reserve[i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (reserve[i + 1] / 4) * 2 + 13 * 4 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (reserve[i + 1] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (reserve[tc] / 4) * 2 + (reserve[i + 1] / 4) * 2 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (reserve[i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (reserve[tc] / 4) * 2 + (reserve[i] / 4) * 2 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                    }
                                    msgbox = true;
                                }
                                if (current == -1)
                                {
                                    if (!msgbox1)
                                    {
                                        if (reserve[i] / 4 > reserve[i + 1] / 4)
                                        {
                                            if (reserve[tc] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + reserve[i] / 4 + current * 100;
                                                Win.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = reserve[tc] / 4 + reserve[i] / 4 + current * 100;
                                                Win.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                        else
                                        {
                                            if (reserve[tc] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + reserve[i + 1] + current * 100;
                                                Win.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = reserve[tc] / 4 + reserve[i + 1] / 4 + current * 100;
                                                Win.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
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
        }
        private void rPairFromHand(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                if (reserve[i] / 4 == reserve[i + 1] / 4)
                {
                    if (!msgbox)
                    {
                        if (reserve[i] / 4 == 0)
                        {
                            current = 1;
                            Power = 13 * 4 + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 1 });
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 1;
                            Power = (reserve[i + 1] / 4) * 4 + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 1 });
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                    msgbox = true;
                }
                for (int tc = 16; tc >= 12; tc--)
                {
                    if (reserve[i + 1] / 4 == reserve[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (reserve[i + 1] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + reserve[i] / 4 + current * 100;
                                Win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (reserve[i + 1] / 4) * 4 + reserve[i] / 4 + current * 100;
                                Win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                    if (reserve[i] / 4 == reserve[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (reserve[i] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + reserve[i + 1] / 4 + current * 100;
                                Win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (reserve[tc] / 4) * 4 + reserve[i + 1] / 4 + current * 100;
                                Win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                }
            }
        }
        private void rHighCard(ref double current, ref double Power)
        {
            if (current == -1)
            {
                if (reserve[i] / 4 > reserve[i + 1] / 4)
                {
                    current = -1;
                    Power = reserve[i] / 4;
                    Win.Add(new Type() { Power = Power, Current = -1 });
                    sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                else
                {
                    current = -1;
                    Power = reserve[i + 1] / 4;
                    Win.Add(new Type() { Power = Power, Current = -1 });
                    sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                if (reserve[i] / 4 == 0 || reserve[i + 1] / 4 == 0)
                {
                    current = -1;
                    Power = 13;
                    Win.Add(new Type() { Power = Power, Current = -1 });
                    sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
            }
        }

        void Winner(double current, double Power, string currentText, int chips, string lastly)
        {
            if (lastly == " ")
            {
                lastly = "Bot 5";
            }
            for (int j = 0; j <= 16; j++)
            {
                //await Task.Delay(5);
                if (cardHolder[j].Visible)
                    cardHolder[j].Image = deck[j];
            }
            if (current == sorted.Current)
            {
                if (Power == sorted.Power)
                {
                    winners++;
                    CheckWinners.Add(currentText);
                    if (current == -1)
                    {
                        MessageBox.Show(currentText + " High Card ");
                    }
                    if (current == 1 || current == 0)
                    {
                        MessageBox.Show(currentText + " Pair ");
                    }
                    if (current == 2)
                    {
                        MessageBox.Show(currentText + " Two Pair ");
                    }
                    if (current == 3)
                    {
                        MessageBox.Show(currentText + " Three of a Kind ");
                    }
                    if (current == 4)
                    {
                        MessageBox.Show(currentText + " Straight ");
                    }
                    if (current == 5 || current == 5.5)
                    {
                        MessageBox.Show(currentText + " Flush ");
                    }
                    if (current == 6)
                    {
                        MessageBox.Show(currentText + " Full House ");
                    }
                    if (current == 7)
                    {
                        MessageBox.Show(currentText + " Four of a Kind ");
                    }
                    if (current == 8)
                    {
                        MessageBox.Show(currentText + " Straight Flush ");
                    }
                    if (current == 9)
                    {
                        MessageBox.Show(currentText + " Royal Flush ! ");
                    }
                }
            }
            if (currentText == lastly)//lastfixed
            {
                if (winners > 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        playerChips += int.Parse(tbPot.Text) / winners;
                        tbChips.Text = playerChips.ToString();
                        //pPanel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        botOneChips += int.Parse(tbPot.Text) / winners;
                        tbBotChips1.Text = botOneChips.ToString();
                        //b1Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        botTwoChips += int.Parse(tbPot.Text) / winners;
                        tbBotChips2.Text = botTwoChips.ToString();
                        //b2Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        botThreeChips += int.Parse(tbPot.Text) / winners;
                        tbBotChips3.Text = botThreeChips.ToString();
                        //b3Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        botFourChips += int.Parse(tbPot.Text) / winners;
                        tbBotChips4.Text = botFourChips.ToString();
                        //b4Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        botFiveChips += int.Parse(tbPot.Text) / winners;
                        tbBotChips5.Text = botFiveChips.ToString();
                        //b5Panel.Visible = true;
                    }
                    //await Finish(1);
                }
                if (winners == 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        playerChips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //pPanel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        botOneChips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //b1Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        botTwoChips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //b2Panel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        botThreeChips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //b3Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        botFourChips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //b4Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        botFiveChips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //b5Panel.Visible = true;
                    }
                }
            }
        }
        async Task CheckRaise(int currentTurn, int raiseTurn)
        {
            if (raising)
            {
                turnCount = 0;
                raising = false;
                raisedTurn = currentTurn;
                changed = true;
            }
            else
            {
                if (turnCount >= maxLeft - 1 || !changed && turnCount == maxLeft)
                {
                    if (currentTurn == raisedTurn - 1 || !changed && turnCount == maxLeft || raisedTurn == 0 && currentTurn == 5)
                    {
                        changed = false;
                        turnCount = 0;
                        Raise = 0;
                        call = 0;
                        raisedTurn = 123;
                        rounds++;
                        if (!playerFinishedHisTurn)
                            pStatus.Text = "";
                        if (!botOneFinishedTurn)
                            b1Status.Text = "";
                        if (!botTwoFinishedTurn)
                            b2Status.Text = "";
                        if (!botThreeFinishedTurn)
                            b3Status.Text = "";
                        if (!botFourFinishedTurn)
                            b4Status.Text = "";
                        if (!botFiveFinishedTurn)
                            b5Status.Text = "";
                    }
                }
            }
            if (rounds == Flop)
            {
                for (int j = 12; j <= 14; j++)
                {
                    if (cardHolder[j].Image != deck[j])
                    {
                        cardHolder[j].Image = deck[j];
                        playerCall = 0; playerRaise = 0;
                        botOneCall = 0; botOneRaise = 0;
                        botTwoCall = 0; botTwoRaise = 0;
                        botThreeCall = 0; botThreeRaise = 0;
                        botFourCall = 0; botFourRaise = 0;
                        botFiveCall = 0; botFiveRaise = 0;
                    }
                }
            }
            if (rounds == Turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    if (cardHolder[j].Image != deck[j])
                    {
                        cardHolder[j].Image = deck[j];
                        playerCall = 0; playerRaise = 0;
                        botOneCall = 0; botOneRaise = 0;
                        botTwoCall = 0; botTwoRaise = 0;
                        botThreeCall = 0; botThreeRaise = 0;
                        botFourCall = 0; botFourRaise = 0;
                        botFiveCall = 0; botFiveRaise = 0;
                    }
                }
            }
            if (rounds == River)
            {
                for (int j = 15; j <= 16; j++)
                {
                    if (cardHolder[j].Image != deck[j])
                    {
                        cardHolder[j].Image = deck[j];
                        playerCall = 0; playerRaise = 0;
                        botOneCall = 0; botOneRaise = 0;
                        botTwoCall = 0; botTwoRaise = 0;
                        botThreeCall = 0; botThreeRaise = 0;
                        botFourCall = 0; botFourRaise = 0;
                        botFiveCall = 0; botFiveRaise = 0;
                    }
                }
            }
            if (rounds == End && maxLeft == 6)
            {
                string fixedLast = "qwerty";
                if (!pStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    Rules(0, 1, "Player", ref pType, ref playerPower, playerFinishedHisTurn);
                }
                if (!b1Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                    Rules(2, 3, "Bot 1", ref botOneType, ref botOnePower, botOneFinishedTurn);
                }
                if (!b2Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    Rules(4, 5, "Bot 2", ref botTwoType, ref botTwoPower, botTwoFinishedTurn);
                }
                if (!b3Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    Rules(6, 7, "Bot 3", ref botThreeType, ref botThreePower, botThreeFinishedTurn);
                }
                if (!b4Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    Rules(8, 9, "Bot 4", ref botFourType, ref botFourPower, botFourFinishedTurn);
                }
                if (!b5Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    Rules(10, 11, "Bot 5", ref botFiveType, ref botFivePower, botFiveFinishedTurn);
                }
                Winner(pType, playerPower, "Player", playerChips, fixedLast);
                Winner(botOneType, botOnePower, "Bot 1", botOneChips, fixedLast);
                Winner(botTwoType, botTwoPower, "Bot 2", botTwoChips, fixedLast);
                Winner(botThreeType, botThreePower, "Bot 3", botThreeChips, fixedLast);
                Winner(botFourType, botFourPower, "Bot 4", botFourChips, fixedLast);
                Winner(botFiveType, botFivePower, "Bot 5", botFiveChips, fixedLast);
                restart = true;
                playerTurn = true;
                playerFinishedHisTurn = false;
                botOneFinishedTurn = false;
                botTwoFinishedTurn = false;
                botThreeFinishedTurn = false;
                botFourFinishedTurn = false;
                botFiveFinishedTurn = false;
                if (playerChips <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        playerChips = f2.a;
                        botOneChips += f2.a;
                        botTwoChips += f2.a;
                        botThreeChips += f2.a;
                        botFourChips += f2.a;
                        botFiveChips += f2.a;
                        playerFinishedHisTurn = false;
                        playerTurn = true;
                        bRaise.Enabled = true;
                        bFold.Enabled = true;
                        bCheck.Enabled = true;
                        bRaise.Text = "Raise";
                    }
                }
                playerPanel.Visible = false; botOnePanel.Visible = false; botTwoPanel.Visible = false; botThreePanel.Visible = false; botFourPanel.Visible = false; botFivePanel.Visible = false;
                playerCall = 0; playerRaise = 0;
                botOneCall = 0; botOneRaise = 0;
                botTwoCall = 0; botTwoRaise = 0;
                botThreeCall = 0; botThreeRaise = 0;
                botFourCall = 0; botFourRaise = 0;
                botFiveCall = 0; botFiveRaise = 0;
                last = 0;
                call = bigBlind;
                Raise = 0;
                ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                bools.Clear();
                rounds = 0;
                playerPower = 0; pType = -1;
                type = 0; botOnePower = 0; botTwoPower = 0; botThreePower = 0; botFourPower = 0; botFivePower = 0;
                botOneType = -1; botTwoType = -1; botThreeType = -1; botFourType = -1; botFiveType = -1;
                ints.Clear();
                CheckWinners.Clear();
                winners = 0;
                Win.Clear();
                sorted.Current = 0;
                sorted.Power = 0;
                for (int os = 0; os < 17; os++)
                {
                    cardHolder[os].Image = null;
                    cardHolder[os].Invalidate();
                    cardHolder[os].Visible = false;
                }
                tbPot.Text = "0";
                pStatus.Text = "";
                await Shuffle();
                await Turns();
            }
        }
        void FixCall(Label status, ref int cCall, ref int cRaise, int options)
        {
            if (rounds != 4)
            {
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
                if (options == 2)
                {
                    if (cRaise != Raise && cRaise <= Raise)
                    {
                        call = Convert.ToInt32(Raise) - cRaise;
                    }
                    if (cCall != call || cCall <= call)
                    {
                        call = call - cCall;
                    }
                    if (cRaise == Raise && Raise > 0)
                    {
                        call = 0;
                        bCall.Enabled = false;
                        bCall.Text = "Callisfuckedup";
                    }
                }
            }
        }
        async Task AllIn()
        {
            #region All in
            if (playerChips <= 0 && !intsadded)
            {
                if (pStatus.Text.Contains("Raise"))
                {
                    ints.Add(playerChips);
                    intsadded = true;
                }
                if (pStatus.Text.Contains("Call"))
                {
                    ints.Add(playerChips);
                    intsadded = true;
                }
            }
            intsadded = false;
            if (botOneChips <= 0 && !botOneFinishedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(botOneChips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (botTwoChips <= 0 && !botTwoFinishedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(botTwoChips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (botThreeChips <= 0 && !botThreeFinishedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(botThreeChips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (botFourChips <= 0 && !botFourFinishedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(botFourChips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (botFiveChips <= 0 && !botFiveFinishedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(botFiveChips);
                    intsadded = true;
                }
            }
            if (ints.ToArray().Length == maxLeft)
            {
                await Finish(2);
            }
            else
            {
                ints.Clear();
            }
            #endregion

            var abc = bools.Count(x => x == false);

            #region LastManStanding
            if (abc == 1)
            {
                int index = bools.IndexOf(false);
                if (index == 0)
                {
                    playerChips += int.Parse(tbPot.Text);
                    tbChips.Text = playerChips.ToString();
                    playerPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }
                if (index == 1)
                {
                    botOneChips += int.Parse(tbPot.Text);
                    tbChips.Text = botOneChips.ToString();
                    botOnePanel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }
                if (index == 2)
                {
                    botTwoChips += int.Parse(tbPot.Text);
                    tbChips.Text = botTwoChips.ToString();
                    botTwoPanel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }
                if (index == 3)
                {
                    botThreeChips += int.Parse(tbPot.Text);
                    tbChips.Text = botThreeChips.ToString();
                    botThreePanel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }
                if (index == 4)
                {
                    botFourChips += int.Parse(tbPot.Text);
                    tbChips.Text = botFourChips.ToString();
                    botFourPanel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }
                if (index == 5)
                {
                    botFiveChips += int.Parse(tbPot.Text);
                    tbChips.Text = botFiveChips.ToString();
                    botFivePanel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }
                for (int j = 0; j <= 16; j++)
                {
                    cardHolder[j].Visible = false;
                }
                await Finish(1);
            }
            intsadded = false;
            #endregion

            #region FiveOrLessLeft
            if (abc < 6 && abc > 1 && rounds >= End)
            {
                await Finish(2);
            }
            #endregion


        }
        // Ot tuk
        async Task Finish(int n)
        {
            if (n == 2)
            {
                FixWinners();
            }
            playerPanel.Visible = false;
            botOnePanel.Visible = false;
            botTwoPanel.Visible = false;
            botThreePanel.Visible = false;
            botFourPanel.Visible = false;
            botFivePanel.Visible = false;
            call = bigBlind; Raise = 0;
            foldedPlayers = 5;
            type = 0; rounds = 0;
            botOnePower = 0;
            botTwoPower = 0;
            botThreePower = 0;
            botFourPower = 0;
            botFivePower = 0;
            playerPower = 0;
            pType = -1;
            Raise = 0;
            botOneType = -1;
            botTwoType = -1;
            botThreeType = -1;
            botFourType = -1;
            botFiveType = -1;
            botOneTurn = false;
            botTwoTurn = false;
            botThreeTurn = false;
            botFourTurn = false;
            botFiveTurn = false;
            botOneFinishedTurn = false;
            botTwoFinishedTurn = false;
            botThreeFinishedTurn = false;
            botFourFinishedTurn = false;
            botFiveFinishedTurn = false;
            playerFolded = false;
            botOneFolded = false;
            botTwoFolded = false;
            botThreeFolded = false;
            botFourFolded = false;
            botFiveFolded = false;
            playerFinishedHisTurn = false;
            playerTurn = true;
            restart = false;
            raising = false;
            playerCall = 0;
            botOneCall = 0;
            botTwoCall = 0;
            botThreeCall = 0;
            botFourCall = 0;
            botFiveCall = 0;
            playerRaise = 0;
            botOneRaise = 0;
            botTwoRaise = 0;
            botThreeRaise = 0;
            botFourRaise = 0;
            botFiveRaise = 0;
            height = 0;
            width = 0;
            winners = 0;
            Flop = 1;
            Turn = 2;
            River = 3;
            End = 4;
            maxLeft = 6;
            last = 123;
            raisedTurn = 1;
            bools.Clear();
            CheckWinners.Clear();
            ints.Clear();
            Win.Clear();
            sorted.Current = 0;
            sorted.Power = 0;
            tbPot.Text = "0";
            t = 60;
            up = 10000000;
            turnCount = 0;
            pStatus.Text = "";
            b1Status.Text = "";
            b2Status.Text = "";
            b3Status.Text = "";
            b4Status.Text = "";
            b5Status.Text = "";
            if (playerChips <= 0)
            {
                AddChips f2 = new AddChips();
                f2.ShowDialog();
                if (f2.a != 0)
                {
                    playerChips = f2.a;
                    botOneChips += f2.a;
                    botTwoChips += f2.a;
                    botThreeChips += f2.a;
                    botFourChips += f2.a;
                    botFiveChips += f2.a;
                    playerFinishedHisTurn = false;
                    playerTurn = true;
                    bRaise.Enabled = true;
                    bFold.Enabled = true;
                    bCheck.Enabled = true;
                    bRaise.Text = "Raise";
                }
            }

            ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (int os = 0; os < 17; os++)
            {
                cardHolder[os].Image = null;
                cardHolder[os].Invalidate();
                cardHolder[os].Visible = false;
            }

            await Shuffle();
            //await Turns();
        }

        void FixWinners()
        {
            Win.Clear();
            sorted.Current = 0;
            sorted.Power = 0;
            string fixedLast = "qwerty";
            if (!pStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                Rules(0, 1, "Player", ref pType, ref playerPower, playerFinishedHisTurn);
            }

            if (!b1Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                Rules(2, 3, "Bot 1", ref botOneType, ref botOnePower, botOneFinishedTurn);
            }

            if (!b2Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                Rules(4, 5, "Bot 2", ref botTwoType, ref botTwoPower, botTwoFinishedTurn);
            }

            if (!b3Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                Rules(6, 7, "Bot 3", ref botThreeType, ref botThreePower, botThreeFinishedTurn);
            }

            if (!b4Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                Rules(8, 9, "Bot 4", ref botFourType, ref botFourPower, botFourFinishedTurn);
            }

            if (!b5Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                Rules(10, 11, "Bot 5", ref botFiveType, ref botFivePower, botFiveFinishedTurn);
            }

            Winner(pType, playerPower, "Player", playerChips, fixedLast);
            Winner(botOneType, botOnePower, "Bot 1", botOneChips, fixedLast);
            Winner(botTwoType, botTwoPower, "Bot 2", botTwoChips, fixedLast);
            Winner(botThreeType, botThreePower, "Bot 3", botThreeChips, fixedLast);
            Winner(botFourType, botFourPower, "Bot 4", botFourChips, fixedLast);
            Winner(botFiveType, botFivePower, "Bot 5", botFiveChips, fixedLast);
        }

        private void Ai(int c1, int c2, ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, double botCurrent)
        {
            if (!sFTurn)
            {
                if (botCurrent == -1)
                {
                    HighCard(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 0)
                {
                    PairTable(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 1)
                {
                    PairHand(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 2)
                {
                    TwoPair(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 3)
                {
                    ThreeOfAKind(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 4)
                {
                    Straight(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 5 || botCurrent == 5.5)
                {
                    Flush(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 6)
                {
                    FullHouse(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 7)
                {
                    FourOfAKind(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 8 || botCurrent == 9)
                {
                    StraightFlush(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
            }
            if (sFTurn)
            {
                cardHolder[c1].Visible = false;
                cardHolder[c2].Visible = false;
            }
        }
        private void HighCard(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            HP(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, 20, 25);
        }

        private void PairTable(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            HP(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, 16, 25);
        }

        private void PairHand(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            Random randomPair = new Random();
            int randomCall = randomPair.Next(10, 16);
            int randomRaise = randomPair.Next(10, 13);
            if (botPower <= 199 && botPower >= 140)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, randomCall, 6, randomRaise);
            }

            if (botPower <= 139 && botPower >= 128)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, randomCall, 7, randomRaise);
            }

            if (botPower < 128 && botPower >= 101)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, randomCall, 9, randomRaise);
            }
        }

        private void TwoPair(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            Random randPair = new Random();
            int randomCall = randPair.Next(6, 11);
            int randomRaise = randPair.Next(6, 11);
            if (botPower <= 290 && botPower >= 246)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, randomCall, 3, randomRaise);
            }

            if (botPower <= 244 && botPower >= 234)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, randomCall, 4, randomRaise);
            }

            if (botPower < 234 && botPower >= 201)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, randomCall, 4, randomRaise);
            }
        }

        private void ThreeOfAKind(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random randomThreeOfAKind = new Random();
            int threeOfAKindCall = randomThreeOfAKind.Next(3, 7);
            int threeOfAKindRaise = randomThreeOfAKind.Next(4, 8);
            if (botPower <= 390 && botPower >= 330)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, threeOfAKindCall, threeOfAKindRaise);
            }

            if (botPower <= 327 && botPower >= 321)//10  8
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, threeOfAKindCall, threeOfAKindRaise);
            }

            if (botPower < 321 && botPower >= 303)//7 2
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, threeOfAKindCall, threeOfAKindRaise);
            }
        }

        private void Straight(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random randomStraight = new Random();
            int straightCall = randomStraight.Next(3, 6);
            int straightRaise = randomStraight.Next(3, 8);
            if (botPower <= 480 && botPower >= 410)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, straightCall, straightRaise);
            }

            if (botPower <= 409 && botPower >= 407)//10  8
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, straightCall, straightRaise);
            }

            if (botPower < 407 && botPower >= 404)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, straightCall, straightRaise);
            }
        }

        private void Flush(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random randomFlush = new Random();
            int flushCall = randomFlush.Next(2, 6);
            int flushRaise = randomFlush.Next(3, 7);
            Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, flushCall, flushRaise);
        }

        private void FullHouse(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random randomFullHouse = new Random();
            int fullHouseCall = randomFullHouse.Next(1, 5);
            int fullHouseRaise = randomFullHouse.Next(2, 6);
            if (botPower <= 626 && botPower >= 620)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fullHouseCall, fullHouseRaise);
            }

            if (botPower < 620 && botPower >= 602)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fullHouseCall, fullHouseRaise);
            }
        }

        private void FourOfAKind(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random randomFourOfAKind = new Random();
            int fourOfAKindCall = randomFourOfAKind.Next(1, 4);
            int fourOfAKindRaise = randomFourOfAKind.Next(2, 5);
            if (botPower <= 752 && botPower >= 704)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fourOfAKindCall, fourOfAKindRaise);
            }
        }

        private void StraightFlush(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random randomStraightFlush = new Random();
            int straightFlushCall = randomStraightFlush.Next(1, 3);
            int straightFlushRaise = randomStraightFlush.Next(1, 3);
            if (botPower <= 913 && botPower >= 804)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, straightFlushCall, straightFlushRaise);
            }
        }

        private void Fold(ref bool sTurn, ref bool sFTurn, Label sStatus)
        {
            raising = false;
            sStatus.Text = "Fold";
            sTurn = false;
            sFTurn = true;
        }

        private void Check(ref bool cTurn, Label cStatus)
        {
            cStatus.Text = "Check";
            cTurn = false;
            raising = false;
        }

        private void Call(ref int sChips, ref bool sTurn, Label sStatus)
        {
            raising = false;
            sTurn = false;
            sChips -= call;
            sStatus.Text = "Call " + call;
            tbPot.Text = (int.Parse(tbPot.Text) + call).ToString();
        }

        private void Raised(ref int sChips, ref bool sTurn, Label sStatus)
        {
            sChips -= Convert.ToInt32(Raise);
            sStatus.Text = "Raise " + Raise;
            tbPot.Text = (int.Parse(tbPot.Text) + Convert.ToInt32(Raise)).ToString();
            call = Convert.ToInt32(Raise);
            raising = true;
            sTurn = false;
        }

        private static double RoundN(int sChips, int n)
        {
            double a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }

        private void HP(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, int n, int n1)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 4);
            if (call <= 0)
            {
                Check(ref sTurn, sStatus);
            }

            if (call > 0)
            {
                if (rnd == 1)
                {
                    if (call <= RoundN(sChips, n))
                    {
                        Call(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
                if (rnd == 2)
                {
                    if (call <= RoundN(sChips, n1))
                    {
                        Call(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
            }

            if (rnd == 3)
            {
                if (Raise == 0)
                {
                    Raise = call * 2;
                    Raised(ref sChips, ref sTurn, sStatus);
                }
                else
                {
                    if (Raise <= RoundN(sChips, n))
                    {
                        Raise = call * 2;
                        Raised(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
            }

            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }

        private void PH(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int n, int n1, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (rounds < 2)
            {
                if (call <= 0)
                {
                    Check(ref sTurn, sStatus);
                }
                if (call > 0)
                {
                    if (call >= RoundN(sChips, n1))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (Raise > RoundN(sChips, n))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (!sFTurn)
                    {
                        if (call >= RoundN(sChips, n) && call <= RoundN(sChips, n1))
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (Raise <= RoundN(sChips, n) && Raise >= (RoundN(sChips, n)) / 2)
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (Raise <= (RoundN(sChips, n)) / 2)
                        {
                            if (Raise > 0)
                            {
                                Raise = RoundN(sChips, n);
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                Raise = call * 2;
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }

                    }
                }
            }
            if (rounds >= 2)
            {
                if (call > 0)
                {
                    if (call >= RoundN(sChips, n1 - rnd))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (Raise > RoundN(sChips, n - rnd))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (!sFTurn)
                    {
                        if (call >= RoundN(sChips, n - rnd) && call <= RoundN(sChips, n1 - rnd))
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (Raise <= RoundN(sChips, n - rnd) && Raise >= (RoundN(sChips, n - rnd)) / 2)
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (Raise <= (RoundN(sChips, n - rnd)) / 2)
                        {
                            if (Raise > 0)
                            {
                                Raise = RoundN(sChips, n - rnd);
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                Raise = call * 2;
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }
                    }
                }
                if (call <= 0)
                {
                    Raise = RoundN(sChips, r - rnd);
                    Raised(ref sChips, ref sTurn, sStatus);
                }
            }
            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }

        // Do tuk
        void Smooth(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, int name, int n, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (call <= 0)
            {
                Check(ref botTurn, botStatus);
            }
            else
            {
                if (call >= RoundN(botChips, n))
                {
                    if (botChips > call)
                    {
                        Call(ref botChips, ref botTurn, botStatus);
                    }
                    else if (botChips <= call)
                    {
                        raising = false;
                        botTurn = false;
                        botChips = 0;
                        botStatus.Text = "Call " + botChips;
                        tbPot.Text = (int.Parse(tbPot.Text) + botChips).ToString();
                    }
                }
                else
                {
                    if (Raise > 0)
                    {
                        if (botChips >= Raise * 2)
                        {
                            Raise *= 2;
                            Raised(ref botChips, ref botTurn, botStatus);
                        }
                        else
                        {
                            Call(ref botChips, ref botTurn, botStatus);
                        }
                    }
                    else
                    {
                        Raise = call * 2;
                        Raised(ref botChips, ref botTurn, botStatus);
                    }
                }
            }
            if (botChips <= 0)
            {
                botFTurn = true;
            }
        }

        #region UI
        private async void timer_Tick(object sender, object e)
        {
            if (pbTimer.Value <= 0)
            {
                playerFinishedHisTurn = true;
                await Turns();
            }
            if (t > 0)
            {
                t--;
                pbTimer.Value = (t / 6) * 100;
            }
        }
        private void Update_Tick(object sender, object e)
        {
            if (playerChips <= 0)
            {
                tbChips.Text = "Chips : 0";
            }
            if (botOneChips <= 0)
            {
                tbBotChips1.Text = "Chips : 0";
            }
            if (botTwoChips <= 0)
            {
                tbBotChips2.Text = "Chips : 0";
            }
            if (botThreeChips <= 0)
            {
                tbBotChips3.Text = "Chips : 0";
            }
            if (botFourChips <= 0)
            {
                tbBotChips4.Text = "Chips : 0";
            }
            if (botFiveChips <= 0)
            {
                tbBotChips5.Text = "Chips : 0";
            }
            tbChips.Text = "Chips : " + playerChips.ToString();
            tbBotChips1.Text = "Chips : " + botOneChips.ToString();
            tbBotChips2.Text = "Chips : " + botTwoChips.ToString();
            tbBotChips3.Text = "Chips : " + botThreeChips.ToString();
            tbBotChips4.Text = "Chips : " + botFourChips.ToString();
            tbBotChips5.Text = "Chips : " + botFiveChips.ToString();
            if (playerChips <= 0)
            {
                playerTurn = false;
                playerFinishedHisTurn = true;
                bCall.Enabled = false;
                bRaise.Enabled = false;
                bFold.Enabled = false;
                bCheck.Enabled = false;
            }
            if (up > 0)
            {
                up--;
            }
            if (playerChips >= call)
            {
                bCall.Text = "Call " + call.ToString();
            }
            else
            {
                bCall.Text = "All in";
                bRaise.Enabled = false;
            }
            if (call > 0)
            {
                bCheck.Enabled = false;
            }
            if (call <= 0)
            {
                bCheck.Enabled = true;
                bCall.Text = "Call";
                bCall.Enabled = false;
            }
            if (playerChips <= 0)
            {
                bRaise.Enabled = false;
            }
            int parsedValue;

            if (tbRaise.Text != "" && int.TryParse(tbRaise.Text, out parsedValue))
            {
                if (playerChips <= int.Parse(tbRaise.Text))
                {
                    bRaise.Text = "All in";
                }
                else
                {
                    bRaise.Text = "Raise";
                }
            }
            if (playerChips < call)
            {
                bRaise.Enabled = false;
            }
        }
        private async void bFold_Click(object sender, EventArgs e)
        {
            pStatus.Text = "Fold";
            playerTurn = false;
            playerFinishedHisTurn = true;
            await Turns();
        }
        private async void bCheck_Click(object sender, EventArgs e)
        {
            if (call <= 0)
            {
                playerTurn = false;
                pStatus.Text = "Check";
            }
            else
            {
                //pStatus.Text = "All in " + Chips;

                bCheck.Enabled = false;
            }
            await Turns();
        }
        private async void bCall_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", ref pType, ref playerPower, playerFinishedHisTurn);
            if (playerChips >= call)
            {
                playerChips -= call;
                tbChips.Text = "Chips : " + playerChips.ToString();
                if (tbPot.Text != "")
                {
                    tbPot.Text = (int.Parse(tbPot.Text) + call).ToString();
                }
                else
                {
                    tbPot.Text = call.ToString();
                }
                playerTurn = false;
                pStatus.Text = "Call " + call;
                playerCall = call;
            }
            else if (playerChips <= call && call > 0)
            {
                tbPot.Text = (int.Parse(tbPot.Text) + playerChips).ToString();
                pStatus.Text = "All in " + playerChips;
                playerChips = 0;
                tbChips.Text = "Chips : " + playerChips.ToString();
                playerTurn = false;
                bFold.Enabled = false;
                playerCall = playerChips;
            }
            await Turns();
        }
        private async void bRaise_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", ref pType, ref playerPower, playerFinishedHisTurn);
            int parsedValue;
            if (tbRaise.Text != "" && int.TryParse(tbRaise.Text, out parsedValue))
            {
                if (playerChips > call)
                {
                    if (Raise * 2 > int.Parse(tbRaise.Text))
                    {
                        tbRaise.Text = (Raise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    else
                    {
                        if (playerChips >= int.Parse(tbRaise.Text))
                        {
                            call = int.Parse(tbRaise.Text);
                            Raise = int.Parse(tbRaise.Text);
                            pStatus.Text = "Raise " + call.ToString();
                            tbPot.Text = (int.Parse(tbPot.Text) + call).ToString();
                            bCall.Text = "Call";
                            playerChips -= int.Parse(tbRaise.Text);
                            raising = true;
                            last = 0;
                            playerRaise = Convert.ToInt32(Raise);
                        }
                        else
                        {
                            call = playerChips;
                            Raise = playerChips;
                            tbPot.Text = (int.Parse(tbPot.Text) + playerChips).ToString();
                            pStatus.Text = "Raise " + call.ToString();
                            playerChips = 0;
                            raising = true;
                            last = 0;
                            playerRaise = Convert.ToInt32(Raise);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            playerTurn = false;
            await Turns();
        }
        private void bAdd_Click(object sender, EventArgs e)
        {
            if (tbAdd.Text == "") { }
            else
            {
                playerChips += int.Parse(tbAdd.Text);
                botOneChips += int.Parse(tbAdd.Text);
                botTwoChips += int.Parse(tbAdd.Text);
                botThreeChips += int.Parse(tbAdd.Text);
                botFourChips += int.Parse(tbAdd.Text);
                botFiveChips += int.Parse(tbAdd.Text);
            }
            tbChips.Text = "Chips : " + playerChips.ToString();
        }
        private void bOptions_Click(object sender, EventArgs e)
        {
            tbBB.Text = bigBlind.ToString();
            tbSB.Text = smallBlind.ToString();
            if (tbBB.Visible == false)
            {
                tbBB.Visible = true;
                tbSB.Visible = true;
                bBB.Visible = true;
                bSB.Visible = true;
            }
            else
            {
                tbBB.Visible = false;
                tbSB.Visible = false;
                bBB.Visible = false;
                bSB.Visible = false;
            }
        }
        private void bSB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (tbSB.Text.Contains(",") || tbSB.Text.Contains("."))
            {
                MessageBox.Show("The Small Blind can be only round number !");
                tbSB.Text = smallBlind.ToString();
                return;
            }
            if (!int.TryParse(tbSB.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                tbSB.Text = smallBlind.ToString();
                return;
            }
            if (int.Parse(tbSB.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                tbSB.Text = smallBlind.ToString();
            }
            if (int.Parse(tbSB.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }
            if (int.Parse(tbSB.Text) >= 250 && int.Parse(tbSB.Text) <= 100000)
            {
                smallBlind = int.Parse(tbSB.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }
        private void bBB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (tbBB.Text.Contains(",") || tbBB.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                tbBB.Text = bigBlind.ToString();
                return;
            }
            if (!int.TryParse(tbSB.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                tbSB.Text = bigBlind.ToString();
                return;
            }
            if (int.Parse(tbBB.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                tbBB.Text = bigBlind.ToString();
            }
            if (int.Parse(tbBB.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }
            if (int.Parse(tbBB.Text) >= 500 && int.Parse(tbBB.Text) <= 200000)
            {
                bigBlind = int.Parse(tbBB.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }
        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            width = this.Width;
            height = this.Height;
        }
        #endregion
    }
}