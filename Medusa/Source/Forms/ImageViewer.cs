using System;
using System.Windows.Forms;
using System.Drawing;

namespace medusa
{
	public class ImageViewer : Form
	{
		PictureBox pictureBox;

		public ImageViewer (Bitmap image)
		{
			pictureBox = new PictureBox () {
				Name = "pictureBox",
				Size = new Size (base.Width, base.Height),
				Location = new Point (0, 0),
				Image = image,
				SizeMode = PictureBoxSizeMode.CenterImage
			};
			this.Controls.Add (pictureBox);
			this.Width = image.Width;
			this.Height = image.Height;
			this.BackColor = Color.White;
			this.SizeChanged += (object sender, EventArgs e) => pictureBox.Size = new Size(base.Width, base.Height);
		}

	}
}

