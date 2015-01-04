using System;
using Microsoft.Xna.Framework;
using Hoax.Engine.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace HoaxFramework
{
	public sealed class EmptyScene : AbstractScene
	{
		public EmptyScene(Game game, GraphicsDevice graphicsDevice, int width, int height) : base(game, graphicsDevice, "Empty scene", width, height)
		{
		}

		#region implemented abstract members of AbstractScene
		public override void LoadContent ()
		{

		}
		public override void RenderScene (GameTime gameTime, GraphicsDevice graphicsDevice, RenderTarget2D renderTargetScene)
		{
			graphicsDevice.SetRenderTarget (renderTargetScene);
			graphicsDevice.Clear (Color.Black);
			graphicsDevice.SetRenderTarget (null);
		}


		#region implemented abstract members of AbstractScene

		public override void Update (GameTime gameTime, Dictionary<Player, VirtualGamePad> PlayerGamePads)
		{

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

