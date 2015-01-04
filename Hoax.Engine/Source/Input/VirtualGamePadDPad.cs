using System;
using Microsoft.Xna.Framework.Input;

namespace Hoax.Engine.Input
{
	public struct VirtualGamePadDPad
	{
		//
		// Properties
		//
		public ButtonState Left 
		{ 
			get {
				if (funcLeft == null)
					throw new NotImplementedException ();
				return funcLeft.Invoke () ? ButtonState.Pressed : ButtonState.Released;
			}
		}

		public ButtonState Up 
		{ 
			get {
				if (funcUp == null)
					throw new NotImplementedException ();
				return funcUp.Invoke () ? ButtonState.Pressed : ButtonState.Released;
			} 
		}

		public ButtonState Right 
		{ 
			get {
				if (funcRight == null)
					throw new NotImplementedException ();
				return funcRight.Invoke () ? ButtonState.Pressed : ButtonState.Released;
			} 
		}

		public ButtonState Down 
		{ 
			get {
				if (funcDown == null)
					throw new NotImplementedException ();
				return funcDown.Invoke () ? ButtonState.Pressed : ButtonState.Released;
			}
		}

		//
		// Fields
		//
		private Func<bool> funcLeft;
		private Func<bool> funcUp;
		private Func<bool> funcRight;
		private Func<bool> funcDown;

		//
		// Constructor
		//
		public VirtualGamePadDPad(Func<bool> funcLeft, Func<bool> funcUp, Func<bool> funcRight, Func<bool> funcDown)
		{
			this = default(VirtualGamePadDPad);
			this.funcLeft = funcLeft;
			this.funcUp = funcUp;
			this.funcLeft = funcLeft;
			this.funcDown = funcDown;
		}
	}
}

