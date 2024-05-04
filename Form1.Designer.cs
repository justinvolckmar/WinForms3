using System.Diagnostics;

namespace WinForms3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            int numPics = 16; //4x4
            pictures = new PictureBox[numPics];

            int i, j = 0, k = 0;

            for (i = 0; i < numPics; i++)
            {
                pictures[i] = new PictureBox();
                pictures[i].BorderStyle = BorderStyle.FixedSingle;
                pictures[i].Location = new Point(105 * j + 5, 105 * k + 5);
                pictures[i].Name = "picture" + i;
                pictures[i].Size = new Size(100, 100);
                pictures[i].TabIndex = i;
                pictures[i].TabStop = false;
                pictures[i].BackColor = Color.CornflowerBlue;
                pictures[i].Click += handle_reveal;
                pictures[i].Image = Image.FromFile("../../../match.png");
                pictures[i].SizeMode = PictureBoxSizeMode.Zoom;

                //grid
                j++;
                if (j >= 4)
                {
                    j = 0;
                    k++;
                }

                Controls.Add(pictures[i]);
            }

            label1 = new Label();
            label1.Location = new Point(430, 5);
            label1.Size = new Size(100, 30);
            label1.Font = new Font(FontFamily.GenericSansSerif, 12);
            label1.TabIndex = 17;
            label1.TabStop = false;
            label1.Text = "Score";

            textBox1 = new TextBox();
            textBox1.Location = new Point(430, 40);
            textBox1.Size = new Size(100, 30);
            textBox1.Enabled = false;
            textBox1.TabIndex = 17;
            textBox1.TabStop = false;
            textBox1.BackColor = Color.White;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Font = new Font(FontFamily.GenericSansSerif, 10);
            textBox1.TextAlign = HorizontalAlignment.Center;

            label2 = new Label();
            label2.Location = new Point(430, 85);
            label2.Size = new Size(100, 30);
            label2.Font = new Font(FontFamily.GenericSansSerif, 12);
            label2.TabIndex = 17;
            label2.TabStop = false;
            label2.Text = "Time";

            textBox2 = new TextBox();
            textBox2.Location = new Point(430, 120);
            textBox2.Size = new Size(100, 30);
            textBox2.Enabled = false;
            textBox2.TabIndex = 17;
            textBox2.TabStop = false;
            textBox2.BackColor = Color.White;
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Font = new Font(FontFamily.GenericSansSerif, 10);
            textBox2.TextAlign = HorizontalAlignment.Center;

            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(textBox2);

            String[] paths = ["../../../apple.jpg", "../../../bananas.png", "../../../grapes.jpg", "../../../kiwi.jpg",
                              "../../../orange.jpg", "../../../pear.jpg", "../../../strawberry.jpg", "../../../watermelon.jpg"];
            int[] pathCount = [0, 0, 0, 0, 0, 0, 0, 0];
            Random rnd = new Random();

            images = new List<String>();
            flipped = new List<bool>();

            for (i = 0; i < numPics; i++)
            {
                int path = Convert.ToInt32(rnd.NextDouble() * 7.0);
                if (pathCount[path] > 1)
                {
                    i--;
                }
                else
                {
                    pathCount[path]++;
                    images.Add(paths[path]);
                    flipped.Add(false);
                }
            }

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(540, 430);

            Name = "Form1";
            Text = "Match Game";

            score = 0;
            textBox1.Text = score.ToString();
            startTime = Stopwatch.StartNew();
            matchTimer();
        }

        private async void matchTimer()
        {
            //loop timer until matches are solved
            while (solved <= 7)
            {
                //get the time and format for 00:00
                TimeSpan elapsed = startTime.Elapsed;
                String mins = elapsed.Minutes.ToString();
                if (mins.Length < 2)
                {
                    mins = "0" + mins;
                }
                String secs = elapsed.Seconds.ToString();
                if (secs.Length < 2)
                {
                    secs = "0" + secs;
                }
                //output formatted time and wait 1s to repeat
                textBox2.Text = mins + ":" + secs;
                await Task.Delay(1000);
            }
        }

        private Stopwatch startTime;
        private int score;

        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;
        private PictureBox[] pictures;
        private List<String> images;
        private List<bool> flipped; //handles the flip state to stop double clicking
    }
}