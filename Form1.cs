
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

            if (count == 1)
            {
                if (images[id] == images[last_id] && id != last_id)
                {
                    pictures[id].Image = Image.FromFile(images[id]);
                    pictures[id].BackColor = Color.White;
                    solved++;
                }
                else
                {

                    pictures[id].Image = Image.FromFile(images[id]);
                    pictures[id].BackColor = Color.White;
                    pictures[id].SizeMode = PictureBoxSizeMode.Zoom;

                    await Task.Delay(200);

                    pictures[id].Image = Image.FromFile("../../../match.png");
                    pictures[id].BackColor = Color.CornflowerBlue;

                    pictures[last_id].Image = Image.FromFile("../../../match.png");
                    pictures[last_id].BackColor = Color.CornflowerBlue;
                }
                count = 0;
            }
            else
            {
                pictures[id].Image = Image.FromFile(images[id]);
                pictures[id].BackColor = Color.White;
                count++;
                last_id = id;
            }

            if (solved >= 8)
            {
                MessageBox.Show("You win!", "Congratulations!");
            }

        }
    }
}
