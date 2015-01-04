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
	public class SceneBlue : AbstractScene
	{
		public SceneBlue (Game game, GraphicsDevice graphicsDevice) : base (game, graphicsDevice, "SceneBlue", 800, 480)
		{
		}

		#region implemented abstract members of AbstractScene

		public override void LoadContent ()
		{
		}

		public override void RenderScene (GameTime gameTime, GraphicsDevice graphicsDevice, RenderTarget2D renderTargetScene)
		{
			graphicsDevice.SetRenderTarget (renderTargetScene);
			graphicsDevice.Clear (Color.DarkBlue);
			graphicsDevice.SetRenderTarget (null);
		}

		#region implemented abstract members of AbstractScene

		public override void Update (GameTime gameTime, Dictionary<Player, VirtualGamePad> playerGamePads)
		{
			if (playerGamePads[Player.One].State.Buttons.A == ButtonState.Pressed) {
				//Console.WriteLine ("Next...");
				SceneService.Instance.RunNextScene (new SceneRed (this.game, base.GraphicsDevice));
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

