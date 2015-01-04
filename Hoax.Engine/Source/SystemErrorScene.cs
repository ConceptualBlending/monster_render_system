using System;
using Hoax.Engine.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace HoaxFramework
{
	public class SystemErrorScene : AbstractScene
	{
		public SystemErrorScene(Game game, GraphicsDevice graphicsDevice): base(game, graphicsDevice, "Error scene", 800, 480)
		{
			// TODO: System error scene
		}

		public static readonly Color BackgroundColor = new Color(46, 54, 82);
		#region implemented abstract members of AbstractScene
		public override void LoadContent ()
		{

		}
		public override void RenderScene (GameTime gameTime, GraphicsDevice graphicsDevice, RenderTarget2D renderTargetScene)
		{
			graphicsDevice.SetRenderTarget (renderTargetScene);
			graphicsDevice.Clear (BackgroundColor);
			graphicsDevice.SetRenderTarget (null);
		}
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
	}
}

