using System;
using Hoax.Engine.Services;
using Microsoft.Xna.Framework.Input;
using Hoax.Engine.Diagnostics;
using System.Diagnostics;

namespace HoaxFramework
{
	using System;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using HoaxFramework;

	namespace CaptainCash.Core
	{
		public abstract class GameWindow : Microsoft.Xna.Framework.Game
		{
			#region Fields

			GraphicsDeviceManager graphics;
			SpriteBatch spriteBatch;
			InputManager inputManager;
			Texture2D monitorScanlines;

			Vector2 screenScaleVector;

			#endregion

			public abstract AbstractScene GetStartScene();

			#region Initialization

			public GameWindow ()
			{
				graphics = new GraphicsDeviceManager (this);
				Content.RootDirectory = "Assets";

				#if DEBUG
				graphics.IsFullScreen = false;
				#else
				graphics.IsFullScreen = true;
				#endif

				inputManager = new InputManager();
				GameService.Instance.GameWindow = this;

				graphics.PreferredBackBufferWidth = DefaultWindowsWidth;
				graphics.PreferredBackBufferHeight = DefaultWindowsHeight;

				updateAspectRation ();
			}

			public void SetBackBufferDimension (int width, int height)
			{
				graphics.PreferredBackBufferWidth = width;
				graphics.PreferredBackBufferHeight = height;

				//graphics.ApplyChanges();

				updateAspectRation ();
			}

			private Vector2 screenPosition = Vector2.Zero;

			public static readonly int FullGameSceneResolutionWidth = 800;
			public static readonly int FullGameSceneResolutionHeight = 480;
			public static readonly int DefaultWindowsWidth = 800;
			public static readonly int DefaultWindowsHeight = 480;
			public static readonly int GameResolutionWidth = 800;
			public static readonly int GameResolutionHeight = 480;

			private void updateAspectRation() 
			{
				float scaleWidth = 1.0f;
				float scaleHeight = 1.0f;
				float tds = 1.0f;
				float lrs = 1.0f;

				bool scaleUpW = FullGameSceneResolutionWidth < graphics.PreferredBackBufferWidth;
				bool scaleUpH = FullGameSceneResolutionHeight < graphics.PreferredBackBufferHeight;
				Trace.WriteLine ("Scale canvas " + (scaleUpW ? "W up" : "W down") + (scaleUpH ? "H up" : "H down") );

				tds = graphics.PreferredBackBufferWidth < FullGameSceneResolutionWidth ? graphics.PreferredBackBufferWidth / (float) FullGameSceneResolutionWidth : FullGameSceneResolutionWidth / (float) graphics.PreferredBackBufferWidth;
				lrs = graphics.PreferredBackBufferHeight < FullGameSceneResolutionHeight ? graphics.PreferredBackBufferHeight / (float) FullGameSceneResolutionHeight : FullGameSceneResolutionHeight / (float) graphics.PreferredBackBufferHeight;

				scaleWidth = scaleUpW ? (tds < lrs ? lrs : tds) : (tds < lrs ? lrs : tds);
				scaleHeight = scaleUpH ? (tds < lrs ? lrs : tds) : (tds < lrs ? lrs : tds);

				int offsetTop = 0;// (int) Math.Ceiling(graphics.PreferredBackBufferHeight - scaleWidth * FullGameSceneResolutionHeight);
				int offsetLeft = 0;//(int) Math.Ceiling(graphics.PreferredBackBufferWidth - scaleWidth * FullGameSceneResolutionWidth);

				Console.WriteLine ("Top: " + offsetTop + " / " + "Left: " + offsetLeft);
				screenPosition = new Vector2 (offsetLeft/2, offsetTop/2);

				screenScaleVector = new Vector2 (scaleUpW ? 1/scaleWidth : scaleWidth, scaleUpH ? 1/scaleHeight : scaleHeight);
			}

			protected override void Initialize ()
			{
				base.Initialize ();
			}


			protected override void LoadContent ()
			{
				spriteBatch = new SpriteBatch (graphics.GraphicsDevice);

				monitorScanlines = Content.Load<Texture2D> ("Monitor/Scanlines.png");

				SceneService.Instance.Initialize (this, graphics.GraphicsDevice, GameResolutionWidth, GameResolutionHeight);	// TODO: Game Resolution!
				SceneService.Instance.RunNextScene (GetStartScene ());
			}

			#endregion

			#region Update and Draw

			protected override void Update (GameTime gameTime)
			{
				base.Update (gameTime);
				SceneService.Instance.Update (gameTime, inputManager.PlayerGamePads);

				#if DEBUG
				if (Keyboard.GetState ().IsKeyDown (Keys.LeftAlt)) {
					if (Keyboard.GetState ().IsKeyDown (Keys.Escape))
						Exit ();
				}
				#endif
			}

			FrameCounter fc = new FrameCounter ();

			int oldTime = 0;

			protected override void Draw (GameTime gameTime)
			{
				graphics.GraphicsDevice.Clear (Color.CornflowerBlue);

				spriteBatch.Begin ();

				Texture2D entireSceneTexture = SceneService.Instance.Render (gameTime, graphics.GraphicsDevice);
				if (entireSceneTexture != null) {
					spriteBatch.Draw (entireSceneTexture, screenPosition, entireSceneTexture.Bounds, Color.White, 0.0f, Vector2.Zero, screenScaleVector, SpriteEffects.None, 0f);
					spriteBatch.Draw (monitorScanlines, screenPosition, entireSceneTexture.Bounds, Color.White, 0.0f, Vector2.Zero, screenScaleVector, SpriteEffects.None, 0f);
				}


				if (gameTime.TotalGameTime.Milliseconds - oldTime > 1) {
					var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
					fc.Update (deltaTime);
					var fps = string.Format ("FPS: {0}", fc.AverageFramesPerSecond);
					//Console.WriteLine (fps);
					oldTime = gameTime.TotalGameTime.Seconds;
				}

				spriteBatch.End ();

				base.Draw (gameTime);
			}

			#endregion

		}
	}


}

