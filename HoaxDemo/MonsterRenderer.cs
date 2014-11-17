#region File Description
//-----------------------------------------------------------------------------
// Array2DDemoGame.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Diagnostics;
using Hoax.Framework.Components.Graphics2D;
using Hoax.Framework.Components.StateMachine;


#endregion

#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;


#endregion

namespace MonsterRenderer
{
	public class MonsterRenderer : Game
	{

		#region Fields

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SceneGraph sceneGraph;	

		Sprite sprite1, sprite2, sprite3, sprite4;

		#endregion

		#region Initialization

		public MonsterRenderer ()
		{

			graphics = new GraphicsDeviceManager (this);

			Content.RootDirectory = "Assets";

			graphics.IsFullScreen = false;

			Window.Title = "Medusa";

			sceneGraph = new SceneGraph (this);
			sprite1 = new Sprite ("S1", "logo.png");
			sprite2 = new Sprite ("S2", "logo.png");
			sprite3 = new Sprite ("S3", "logo.png");
			sprite4 = new Sprite ("S4", "logo.png");

			sprite1.Move (new Vector2 (500, 300));
			sprite2.Move (new Vector2 (-60, 60));
			sprite3.Move (new Vector2 (60, -60));
			sprite4.Move (new Vector2 (600, 300));

			sprite1.AttachChild (sprite2);
			sprite1.AttachChild (sprite3);

			sceneGraph.RootNode += sprite1;
			sceneGraph.RootNode += sprite4;
		}
			
		protected override void Initialize ()
		{
			base.Initialize ();

		}

		protected override void LoadContent ()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			sceneGraph.LoadContent ();
		}

		float degree = 0.0f;

		#endregion

		#region Update and Draw


		protected override void Update (GameTime gameTime)
		{
			base.Update (gameTime);

			// TODO: This should force a world rotation for all attached childs
			sprite1.Rotate (degree += 0.02f);
	
			float mx = (float) Math.Sin (gameTime.TotalGameTime.TotalMilliseconds / 400) * 100;
			sprite1.Move (new Vector2(200 + mx,300));
			sprite4.Move (new Vector2(200,300 - mx));

			sceneGraph.RootNode.Update (gameTime);
		}

		protected override void Draw (GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin ();
			Entity.Traverse<BreadthFirst> (sceneGraph.RootNode, node => { 
				if (node is Drawable) {
					Sprite sprite = (Sprite) node;
					spriteBatch.Draw(sprite.Texture, sprite.Transformation2D.WorldPosition, sprite.Texture.Bounds, Color.White, sprite.Transformation2D.WorldRotation, sprite.Transformation2D.PivotPoint, sprite.Transformation2D.WorldScale, SpriteEffects.None, 1);
				}} );
					
			spriteBatch.End ();

			base.Draw(gameTime);
		}

		#endregion


	}
}
