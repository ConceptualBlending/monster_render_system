﻿using Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Core;
using System.IO;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.IO;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa
{
	public class MedusaRenderer : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Universe universe;
		Repository repository;
		string outputFileName;
		MonsterMarkup markup;

		public MedusaRenderer (Repository repository, MonsterMarkup markup, string outputFileName)
		{
			graphics = new GraphicsDeviceManager (this);			
			Content.RootDirectory = "Assets";
			graphics.IsFullScreen = false;
			this.Window.Title = "Medusa";
			this.repository = repository;
			this.outputFileName = outputFileName;
			this.markup = markup;
		}

		void buildUniverse() {
			universe = new Universe (this);

			foreach (var relation in markup.Relations) {
				ImportIntoUniverse (relation.Individual1);
				ImportIntoUniverse (relation.Individual2);
				universe.Connect (relation.Individual1, relation.Point1, relation.Individual2, relation.Point2);
			}
		}

		protected override void Initialize ()
		{
			base.Initialize ();
		}

		protected override void LoadContent ()
		{
			spriteBatch = new SpriteBatch (graphics.GraphicsDevice);

			buildUniverse ();
	
			if (Config.StoreOutputImage && !Config.ShowWindow)
				SaveAsPng ();
		}

		protected override void Update (GameTime gameTime)
		{
			if (!Config.ShowWindow)
				base.Exit ();		
			base.Update (gameTime);
		}

		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);
			SaveAsPng ();
			base.Draw (gameTime);
		}

		public bool firstRun = true;

		public void SaveAsPng() 
		{
			Vector2 minLeft = universe.GetDrawableObjectMinPosition ();
			Vector2 correction = new Vector2 (minLeft.X < 0 ? -minLeft.X : minLeft.X, minLeft.Y < 0 ? -minLeft.Y : minLeft.Y);

			int width = (int) (universe.GetMaxWidth() + universe.GetMaximumX() + correction.X);
			int height = (int) (universe.GetMaxHeight () + universe.GetMaximumY() + correction.Y);

			if (Config.ShowWindow) {
				GraphicsDevice.Clear(Color.White);

				spriteBatch.Begin();
				universe.Draw (spriteBatch);
				spriteBatch.End();

			} 

			if (Config.StoreOutputImage && firstRun) {
				RenderTarget2D render = new RenderTarget2D(GraphicsDevice, width, height, false, 
						SurfaceFormat.Color, DepthFormat.Depth24);

				GraphicsDevice.SetRenderTarget(render);
				GraphicsDevice.Clear(Color.White);

				spriteBatch.Begin();
				universe.Draw (spriteBatch);
				spriteBatch.End();

				GraphicsDevice.SetRenderTarget(null);

				FileStream fs = new FileStream(this.outputFileName, FileMode.OpenOrCreate);
				render.SaveAsPng(fs, width, height);
				fs.Flush();

				firstRun = false;
			}
		}

		public Texture2D GetTexture (string fileName)
		{
			FileStream filestream = new FileStream(fileName, FileMode.Open);
			var result = Texture2D.FromStream(graphics.GraphicsDevice, filestream);
			filestream.Close ();
			return result;
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
