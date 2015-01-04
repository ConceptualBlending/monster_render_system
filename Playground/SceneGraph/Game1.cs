#region File Description
//-----------------------------------------------------------------------------
// SceneGraphGame.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
using Hoax.Engine.Common;
using Hoax.Engine.Graphics2D;
using Hoax.Engine.Diagnostics;
using Hoax.Engine.Physics2D;

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

namespace Hoax.Examples.Scenes
{
	public class Game1 : Game
	{
		int max_sprites = 10;
		int max_canvases = 150;
		SpriteBatch spriteBatch;	

		SceneGraph sceneGraph;	
		Sprite sprite1, sprite2, sprite3, sprite4;
		GraphicsDeviceManager graphics;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 1024;
			graphics.PreferredBackBufferHeight = 768;
			graphics.PreferMultiSampling = false;
			graphics.ApplyChanges(); 

			setupSceneGraph ();

			Content.RootDirectory = "Assets";
		}

		void setupSceneGraph ()
		{
			graphics.IsFullScreen = false;

			sceneGraph = new SceneGraph (this);
			sprite1 = new Sprite ("S1", "logo.png");
			sprite2 = new Sprite ("S2", "logo.png");
			sprite3 = new Sprite ("S3", "logo.png");
			sprite4 = new Sprite ("S4", "logo.png");

			sprite1.Move (new Vector2 (500, 300));
			sprite2.Move (new Vector2 (-60, 60));
			sprite3.Move (new Vector2 (60, -60));
			sprite4.Move (new Vector2 (600, 300));

			Random r = new Random ();

			for (int i = 0; i < max_sprites; i++) {
				sprite1.AttachChild (new Sprite ("SX_" + i, "logo.png"));
				sprite1["SX_"+i].Move(new Vector2(-100 + r.Next() % 600,  r.Next() % 600));
			}

			for (int i = 0; i < max_canvases; i++) {
				sprite1.AttachChild (new MyCanvas ("C"+i, this, false));
				sprite1["C"+i].Move(new Vector2(-100 + r.Next() % 600,  -300 + r.Next() % 600));
			}

			sprite1.AttachChild (sprite2);
			sprite1.AttachChild (sprite3);

			sceneGraph.RootNode += new MyCanvas ("CD", this, true);

			sceneGraph.RootNode += sprite1;
			sceneGraph.RootNode += sprite4;
		}

		protected override void Initialize()
		{
			spriteBatch = new SpriteBatch (GraphicsDevice);
			base.Initialize();
		}


		protected override void LoadContent()
		{
			sceneGraph.LoadContent ();
		}


		protected override void UnloadContent()
		{

		}

		float up = -0.05f;

		protected override void Update(GameTime gameTime)
		{
			MethodTimeTracker.Instance.Start ("Renderer: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);

			float mx = (float) Math.Sin (gameTime.TotalGameTime.TotalMilliseconds / 400) * 100;
			sprite1.Move (new Vector2(200 + mx,300));
			sceneGraph.RootNode ["CD"].Move (new Vector2 (sceneGraph.RootNode ["CD"].Transformation2D.LocalPosition.X + 0.06f, sceneGraph.RootNode ["CD"].Transformation2D.LocalPosition.Y + 0.06f));

			up -= 1.00f;

			if (up > 300)
				up = 0;

			MethodTimeTracker.Instance.Start ("Renderer: Update Scene Graph");
			sceneGraph.RootNode.Update (gameTime);
			sceneGraph.RootNode.Scale (new Vector2(sx,sy));
			MethodTimeTracker.Instance.Stop ("Renderer: Update Scene Graph");

			CollisionDetector.Instance.Update ();

			sx -= 0.0001f;
			sy -= 0.0001f;

			base.Update(gameTime);
			MethodTimeTracker.Instance.Stop ("Renderer: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		float sx = 1;
		float sy = 1;
		protected override void Draw(GameTime gameTime)
		{	
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);

			GraphicsDevice.Clear(Color.DarkOliveGreen);

			spriteBatch.Begin ();

			MethodTimeTracker.Instance.Start ("Render Tree");
			Entity.Traverse<BreadthFirst> (sceneGraph.RootNode, node => { 
				if (node is Drawable) {
					try {
						Sprite sprite = (Sprite) node;
						spriteBatch.Draw(sprite.Texture, sprite.Transformation2D.WorldPosition, sprite.Texture.Bounds, Color.White, sprite.Transformation2D.WorldRotation, sprite.Transformation2D.PivotPoint, sprite.Transformation2D.WorldScale, SpriteEffects.None, 1);
					} catch (Exception e ) {}

					try {
						Canvas canvas = (Canvas) node;
						spriteBatch.Draw(canvas, node.Transformation2D);
					} catch (Exception e ) {}
				}} );
			MethodTimeTracker.Instance.Start ("Render Tree");


			spriteBatch.End();

			base.Draw(gameTime);

			MethodTimeTracker.Instance.Stop (System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}
	}

	class MyCanvas : Canvas
	{
		Polygon2D poly;
		SolidLinePolygon2D solidpoly;
		bool ShowHit;

		public MyCanvas(string identifier, Game game, bool theOne) : base(identifier, game, 140, 140, Color.Transparent) {
			poly = new Polygon2D (new Vector2[] { new Vector2 (10,10), new Vector2 (50,100), new Vector2 (80,120) });
			solidpoly = new SolidLinePolygon2D (game, poly, Color.DarkRed);

			if(theOne)
				CollisionDetector.Instance.AddActive(poly);
			else
				CollisionDetector.Instance.AddPassive(poly);

			CollisionDetector.Instance.OnCollisionDectetedHandler += (object sender, CollisionDetectorEventArgs e) => {
				if (e.PolygonA == poly || e.PolygonB == poly)
					ShowHit = true;
			};

			solidpoly.ShowBoundingBox = true;
			base.ShowBorder = false;
		}

		#region implemented abstract members of Entity
		protected override void OnTransformationChanged (Matrix worldMatrix)
		{

			poly.SetWorldMatrix (worldMatrix);
		}
		#endregion


		public override void Update(GameTime gameTime) 
		{
			base.Update (gameTime);
		}

		public override void OnPaint (SpriteBatch spriteBatch)
		{
			solidpoly.BoundingBox.LineColor = ShowHit ? Color.Yellow : Color.Gray;
			solidpoly.Draw (spriteBatch);
			ShowHit = false;

		}
	}
}
