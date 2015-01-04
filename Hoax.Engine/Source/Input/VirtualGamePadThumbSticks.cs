using System;
using Microsoft.Xna.Framework;

namespace Hoax.Engine.Input
{
	public struct VirtualGamePadThumbSticks
	{
		//
		// Properties
		//
		public Vector2 Left 
		{ 
			get {
				if (funLeft == null)
					throw new NotImplementedException ();
				return funLeft.Invoke ();
			}
		}

		public Vector2 Right 
		{ 
			get {
				if (funRight == null)
					throw new NotImplementedException ();
				return funRight.Invoke ();
			}
		}
	
		//
		// Fields
		//
		private Func<Vector2> funLeft;
		private Func<Vector2> funRight;

		// 
		// Constructor
		//
		public VirtualGamePadThumbSticks(Func<Vector2> funLeft, Func<Vector2> funRight)
		{
			this.funLeft = funLeft;
			this.funRight = funRight;
		}
	}
}

