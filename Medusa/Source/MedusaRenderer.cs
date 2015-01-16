using Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Core;
using System.IO;
using System;
using System.Collections.Generic;
using Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Utils;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa
{
	public class MedusaRenderer 
	{
		Universe universe;
		Repository repository;
		string outputFileName;
		MonsterMarkup markup;

		public MedusaRenderer (Repository repository, MonsterMarkup markup, string outputFileName)
		{
			this.repository = repository;
			this.outputFileName = outputFileName;
			this.markup = markup;
		}

		public Bitmap Run() 
		{
			buildUniverse ();
			return SaveAsPng ();
		}

		void buildUniverse() {
			universe = new Universe ();

			foreach (var relation in markup.Relations) {
				ImportIntoUniverse (relation.Individual1);
				ImportIntoUniverse (relation.Individual2);
				universe.Connect (relation.Individual1, relation.Point1, relation.Individual2, relation.Point2);
			}
		}

//		public bool firstRun = true;

		public Bitmap SaveAsPng() 
		{
//			Vector2 minLeft = universe.GetDrawableObjectMinPosition ();
//			Vector2 correction = new Vector2 (minLeft.X < 0 ? -minLeft.X : minLeft.X, minLeft.Y < 0 ? -minLeft.Y : minLeft.Y);
//
//			int width = (int) (universe.GetMaxWidth() + universe.GetMaximumX() + correction.X);
//			int height = (int) (universe.GetMaxHeight () + universe.GetMaximumY() + correction.Y);
//
//			Console.WriteLine ("Storing..." + universe.ToString());

			return universe.Draw ();

			/*if (Config.ShowWindow) {
				GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.White);

				spriteBatch.Begin();
				universe.Draw (spriteBatch);
				spriteBatch.End();

			} 

			if (Config.StoreOutputImage && firstRun) {
				RenderTarget2D render = new RenderTarget2D(GraphicsDevice, width, height, false, 
						SurfaceFormat.Color, DepthFormat.Depth24);

				GraphicsDevice.SetRenderTarget(render);
				GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.White);

				spriteBatch.Begin();
				universe.Draw (spriteBatch);
				spriteBatch.End();

				GraphicsDevice.SetRenderTarget(null);

				FileStream fs = new FileStream(this.outputFileName, FileMode.OpenOrCreate);
				SaveAsImage(render, fs, width, height, ImageFormat.Png);
				fs.Flush();

				firstRun = false;
			}*/
		}

		// This method is copied from windows source of monogame because current monogame linux port
		// does not offer this method.
		private void SaveAsImage(Image source, Stream stream, int width, int height, ImageFormat format)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream", "'stream' cannot be null (Nothing in Visual Basic)");
			}
			if (width <= 0)
			{
				throw new ArgumentOutOfRangeException("width", width, "'width' cannot be less than or equal to zero");
			}
			if (height <= 0)
			{
				throw new ArgumentOutOfRangeException("height", height, "'height' cannot be less than or equal to zero");
			}
			if (format == null)
			{
				throw new ArgumentNullException("format", "'format' cannot be null (Nothing in Visual Basic)");
			}

		/*	byte[] data = null;
			GCHandle? handle = null;
			Bitmap bitmap = null;
			try 
			{
				data = new byte[width * height * 4];
				handle = GCHandle.Alloc(data, GCHandleType.Pinned);
				source.GetData(data);

				// internal structure is BGR while bitmap expects RGB
				for(int i = 0; i < data.Length; i += 4)
				{
					byte temp = data[i + 0];
					data[i + 0] = data[i + 2];
					data[i + 2] = temp;
				}

				bitmap = new Bitmap(width, height, width * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, handle.Value.AddrOfPinnedObject());

				bitmap.Save(stream, format);
			} 
			finally 
			{
				if (bitmap != null)
				{
					bitmap.Dispose();
				}
				if (handle.HasValue)
				{
					handle.Value.Free();
				}
				if (data != null)
				{
					data = null;
				}
			}*/
		}

		public Bitmap GetTexture (string fileName)
		{
			return (Bitmap) Bitmap.FromFile (fileName);
		}

		void ImportIntoUniverse(string individualName) {
			string textureFileName = null;
			var referencePoints = new List<ReferencePoint> ();

			foreach (var def in markup.Definitions) {
				if (def.Identifier == individualName) {
					foreach (var type in repository.RepositoryContent) {
						if (type.Type == def.Type) {
							textureFileName = type.ImageFile;

							foreach (var point in type.Points)
								referencePoints.Add (new ReferencePoint (point.Key, new Vector2 (point.Value.x, point.Value.y), ReferencePoint.PointType.A));
							break;
						}
					}
					break;
				}
			}

			var texture = GetTexture (repository.RootDirectory + textureFileName);
			if (!universe.Contains(individualName))
				universe.Add (new ReferencePointContainer (individualName, texture, referencePoints.ToArray ()));
		}
	}
}
