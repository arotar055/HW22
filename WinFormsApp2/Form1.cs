namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        int[] buttonValues; 
        Button[] buttons;
        int currentIndex = 0;
        System.Windows.Forms.Timer gameTimer; 
        int gameDuration;
        int elapsedTime; 
        public Form1()
        {
            InitializeComponent();
            InitializeButtons();
            InitializeGameTimer();
        }

        private void InitializeButtons()
        {
            List<Button> buttonList = new List<Button>();

            foreach (Control control in this.Controls)
            {
                if (control is Button button && button.Name != "button17")
                {
                    button.Text = ""; 
                    button.Enabled = false;
                    button.Click += Button_Click; 
                    buttonList.Add(button); 
                }
            }
            buttons = buttonList.ToArray();
        }

        private void InitializeButtonsWithRandomNumbers()
        {
            Random random = new Random();
            foreach (Button button in buttons)
            {
                button.Text = random.Next(0, 100).ToString();
                button.Enabled = true; 
            }

            buttonValues = buttons.Select(b => int.Parse(b.Text)).ToArray();
            Array.Sort(buttonValues);

            currentIndex = 0; 
        }

        private void InitializeGameTimer()
        {
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
        }

        private void StartGame()
        {
            elapsedTime = 0;
            progressBar1.Value = 0; 
            progressBar1.Maximum = gameDuration; 
            gameTimer.Start(); 
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime++;

            progressBar1.Value = Math.Min(elapsedTime, gameDuration);
            if (elapsedTime >= gameDuration)
            {
                gameTimer.Stop();
                MessageBox.Show("Вы проиграли! Время истекло.");
                ResetGame();
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                int currentNumber = int.Parse(button.Text);

                if (currentIndex < buttonValues.Length && currentNumber == buttonValues[currentIndex])
                {
                    button.Enabled = false; 
                    listBox1.Items.Add(currentNumber); 
                    currentIndex++; 

                    if (currentIndex == buttonValues.Length)
                    {
                        gameTimer.Stop();
                        MessageBox.Show("Вы выиграли!");
                        ResetGame();
                    }
                }
                else
                {
                    MessageBox.Show("Вы нажали не по порядку возрастания!");
                }
            }
        }

        private void ResetGame()
        {
            foreach (Button button in buttons)
            {
                button.Text = "";
                button.Enabled = false;
            }

            listBox1.Items.Clear();
            progressBar1.Value = 0; 
        }

        private void button17_Click(object sender, EventArgs e)
        {
            InitializeButtonsWithRandomNumbers();
            listBox1.Items.Clear(); 
            StartGame(); 
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            if (int.TryParse(domainUpDown1.Text, out int duration))
            {
                gameDuration = duration;
            }
            else
            {
                domainUpDown1.Text = "10"; 
                gameDuration = 10;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) 
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
