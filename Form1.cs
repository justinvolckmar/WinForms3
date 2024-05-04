
namespace WinForms3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int last_id = 0;
        private int count = 0;
        private int solved = 0;

        private async void handle_reveal(object? sender, EventArgs e)
        {
            PictureBox pressed = sender as PictureBox;
            int id = Array.IndexOf(pictures, pressed);

            //if clicking on a revealed image, cancel click
            if (flipped[id] == false)
            {
                //handle reveals when count 1
                if (count == 1)
                {
                    //successful reveal
                    if (images[id] == images[last_id] && id != last_id)
                    {
                        pictures[id].Image = Image.FromFile(images[id]);
                        pictures[id].BackColor = Color.White;
                        flipped[id] = true;
                        solved++;
                        //use time in seconds to create a score
                        int timeSeconds = startTime.Elapsed.Seconds + startTime.Elapsed.Minutes * 60;
                        int scoreGained = 300 / timeSeconds + 700 - timeSeconds;
                        //sum the score for all matches
                        score += scoreGained;
                        textBox1.Text = score.ToString();
                    }
                    else
                    {
                        //reveal the clicked image
                        pictures[id].Image = Image.FromFile(images[id]);
                        pictures[id].BackColor = Color.White;
                        pictures[id].SizeMode = PictureBoxSizeMode.Zoom;
                        flipped[id] = true;

                        //wait .2s
                        await Task.Delay(200);

                        //hide both images
                        pictures[id].Image = Image.FromFile("../../../match.png");
                        pictures[id].BackColor = Color.CornflowerBlue;
                        flipped[id] = false;

                        pictures[last_id].Image = Image.FromFile("../../../match.png");
                        pictures[last_id].BackColor = Color.CornflowerBlue;
                        flipped[last_id] = false;
                    }
                    count = 0;
                }
                else //count is 0 so reveal the card
                {
                    pictures[id].Image = Image.FromFile(images[id]);
                    pictures[id].BackColor = Color.White;
                    count++;
                    last_id = id;
                    flipped[id] = true;
                }
            }

            if (solved >= 8)
            {
                int timeSeconds = startTime.Elapsed.Seconds + startTime.Elapsed.Minutes * 60;
                //display win
                MessageBox.Show("You earned " + score + " points in " + timeSeconds + " seconds!", "You win!", MessageBoxButtons.OK);
                
                //reset
                int numPics = 16;
                int i;

                String[] paths = ["../../../apple.jpg", "../../../bananas.png", "../../../grapes.jpg", "../../../kiwi.jpg",
                              "../../../orange.jpg", "../../../pear.jpg", "../../../strawberry.jpg", "../../../watermelon.jpg"];
                int[] pathCount = [0, 0, 0, 0, 0, 0, 0, 0];
                Random rnd = new Random();

                images = new List<String>();
                flipped = new List<bool>();

                for (i = 0; i < numPics; i++)
                {
                    //random 0-7
                    int path = Convert.ToInt32(rnd.NextDouble() * 7.0);
                    if (pathCount[path] > 1)//reroll duplicates
                    {
                        i--;
                    }
                    else
                    {
                        pathCount[path]++;
                        images.Add(paths[path]);
                        flipped.Add(false);
                    }
                    //reset board pieces
                    pictures[i].Image = Image.FromFile("../../../match.png");
                    pictures[i].BackColor = Color.CornflowerBlue;
                }

                //reset count, timer & score
                solved = 0;
                score = 0;
                startTime.Reset();
                textBox1.Text = score.ToString();
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
                textBox2.Text = mins + ":" + secs;
                startTime.Start();
                matchTimer();
            }

        }
    }
}
