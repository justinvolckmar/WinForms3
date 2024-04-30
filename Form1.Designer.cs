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

                j++;
                if (j >= 4)
                {
                    j = 0;
                    k++;
                }

                Controls.Add(pictures[i]);
            }

            String[] paths = ["../../../apple.jpg", "../../../bananas.png", "../../../grapes.jpg", "../../../kiwi.jpg",
                              "../../../orange.jpg", "../../../pear.jpg", "../../../strawberry.jpg", "../../../watermelon.jpg"];
            int[] pathCount = [0, 0, 0, 0, 0, 0, 0, 0];
            Random rnd = new Random();

            images = new List<String>();

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
                }
            }

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(430, 430);

            Name = "Form1";
            Text = "Match Game";
        }

        private PictureBox[] pictures;
        private List<String> images;
    }
}