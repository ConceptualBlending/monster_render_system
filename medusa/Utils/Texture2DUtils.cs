using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Utils
{
	public static class Texture2DUtils
	{
		// Source: http://stackoverflow.com/questions/19187737/converting-a-bgr-bitmap-to-rgb
		// just for 4 bytes
	/*	public static Bitmap RGBAtoBGRA(Bitmap bmp)
		{
			BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
				ImageLockMode.ReadWrite, bmp.PixelFormat);

			int length = Math.Abs(data.Stride) * bmp.Height;

			unsafe
			{
				byte* rgbValues = (byte*)data.Scan0.ToPointer();

				for (int i = 0; i < length; i += 4)
				{
					byte dummy = rgbValues[i];
					rgbValues[i] = rgbValues[i + 2];
					rgbValues[i + 2] = dummy;
				}
			}

			bmp.UnlockBits(data);
			return bmp;
		}

		public static Image CreateFromStream(FileStream stream)
		{
			Bitmap image = (Bitmap)Bitmap.FromStream (stream);
			try
			{
				// Fix up the Image to match the expected format
				image = RGBAtoBGRA(image);

				var data = new byte[image.Width * image.Height * 4];

				BitmapData bitmapData = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
					ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
				if (bitmapData.Stride != image.Width * 4) 
					throw new NotImplementedException();
				Marshal.Copy(bitmapData.Scan0, data, 0, data.Length);
				image.UnlockBits(bitmapData);

				Texture2D texture = null;
				texture = new Texture2D(gd, image.Width, image.Height);
				texture.SetData(data);

				return texture;
			}
			finally
			{
				image.Dispose();
			}
		}*/
	}
}

