using System;
using HoaxFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Hoax.Engine.Input;
using System.Collections.Generic;
using Hoax.Engine.Services;
using Microsoft.Xna.Framework.Input;

namespace CaptainCash.Core
{
	public class SceneRed : AbstractScene
	{
		Texture2D logo;
		SpriteBatch spriteBatch;

		public SceneRed (Game game, GraphicsDevice graphicsDevice) : base (game, graphicsDevice, "SceneRed", 800, 480)
		{
			spriteBatch = new SpriteBatch (graphicsDevice);
		}

		#region implemented abstract members of AbstractScene
		public override void LoadContent ()
		{
			logo = ContentLoader.Load<Texture2D> ("logo.png"); //base.game.Content.Load<Texture2D> ("logo.png");
		}
		public override void RenderScene (GameTime gameTime, GraphicsDevice graphicsDevice, RenderTarget2D renderTargetScene)
		{
			graphicsDevice.SetRenderTarget (renderTargetScene);
			graphicsDevice.Clear (Color.Red);
			spriteBatch.Begin ();
			spriteBatch.Draw (logo, Vector2.Zero, Color.White);
			spriteBatch.End ();
			graphicsDevice.SetRenderTarget (null);
		}
		#region implemented abstract members of AbstractScene

		public override void Update (GameTime gameTime, Dictionary<Player, VirtualGamePad> PlayerGamePads)
		{
			if (PlayerGamePads [Player.One].State.Buttons.A == ButtonState.Pressed) {
				//Console.WriteLine ("Next...");
				SceneService.Instance.RunNextScene (new SceneBlue (this.game, base.GraphicsDevice));
			} else if (PlayerGamePads [Player.One].State.Buttons.Back == ButtonState.Pressed) {
				int j = 0;
				int i = 1 / j;
			}
		}

		public override void OnEntered ()
		{

		}

		public override void OnEntering ()
		{

		}

		public override void OnTransitionOutStarted ()
		{

		}

		public override void OnTransitionInEnded ()
		{

		}

		public override void OnLeaved ()
		{

		}

		public override void OnLeaving ()
		{

		}

		public override void OnPause ()
		{

		}

		public override void OnResume ()
		{

		}
			
		#endregion
		#endregion
	}
}

