using System;
using Microsoft.Xna.Framework.Graphics;

namespace HoaxFramework
{
	public class SlideTransition : TransitionBase
	{
		public SlideTransition(GraphicsDevice graphicsDevice, int width, int height) : base(graphicsDevice, width, height) 
		{
		}

		#region implemented abstract members of TransitionBase

		public override void Reset (int width, int height)
		{
			throw new NotImplementedException ();
		}

		public override void OnUpdate (Microsoft.Xna.Framework.GameTime gameTime)
		{
			throw new NotImplementedException ();
		}

		protected override Microsoft.Xna.Framework.Color[] PerformTransition (Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice)
		{
			throw new NotImplementedException ();
		}

		public override bool IsTransitionComplete {
			get {
				throw new NotImplementedException ();
			}
		}

		#endregion


	}
}

