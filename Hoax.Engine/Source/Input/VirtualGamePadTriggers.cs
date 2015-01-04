using System;

namespace Hoax.Engine.Input
{
	public struct VirtualGamePadTriggers
	{
		//
		// Properties
		//
		public float Left 
		{ 
			get {
				if (funLeft == null)
					throw new NotImplementedException ();
				return funLeft.Invoke ();
			}
		}

		public float Right 
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
		private Func<float> funLeft;
		private Func<float> funRight;

		// 
		// Constructor
		//
		public VirtualGamePadTriggers(Func<float> funLeft, Func<float> funRight)
		{
			this.funLeft = funLeft;
			this.funRight = funRight;
		}
	}
}

