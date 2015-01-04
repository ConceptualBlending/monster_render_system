using System;
using Microsoft.Xna.Framework.Input;

namespace Hoax.Engine.Input
{
	public struct VirtualGamePadButtons
	{
		//
		// Properties
		//
		public ButtonState A 
		{ 
			get {
				if (funcA == null)
					throw new NotImplementedException ();
				return this.funcA.Invoke () ? ButtonState.Pressed : ButtonState.Released;
			}
		}

		public ButtonState B 
		{ 
			get {
				if (funcB == null)
					throw new NotImplementedException ();
				return this.funcB.Invoke () ? ButtonState.Pressed : ButtonState.Released;
			}
		}

		public ButtonState Back 
		{ 
			get {
				if (funcBack == null)
					throw new NotImplementedException ();
				return this.funcBack.Invoke () ? ButtonState.Pressed : ButtonState.Released;
			}
		}

		public ButtonState BigButton 
		{ 
			get {
				if (funcBigButton == null)
					throw new NotImplementedException ();
				return this.funcBigButton.Invoke () ? ButtonState.Pressed : ButtonState.Released;
			}
		}

		public ButtonState LeftBumber 
		{ 
			get {
				if (funcLeftBumber == null)
					throw new NotImplementedException ();
				return this.funcLeftBumber.Invoke () ? ButtonState.Pressed : ButtonState.Released;
			}
		}

		public ButtonState LeftStick 
		{ 
			get {
				if (funcLeftStick == null)
					throw new NotImplementedException ();
				return this.funcLeftStick.Invoke () ? ButtonState.Pressed : ButtonState.Released;
			}
		}

		public ButtonState RightBumber 
		{ 
			get {
				if (funcRightBumber == null)
					throw new NotImplementedException ();
				return this.funcRightBumber.Invoke () ? ButtonState.Pressed : ButtonState.Released;
			}
		}

		public ButtonState RightStick 
		{ 
			get {
				if (funcRightStick == null)
					throw new NotImplementedException ();
				return this.funcRightStick.Invoke () ? ButtonState.Pressed : ButtonState.Released;
			}
		}

		public ButtonState Start 
		{ 
			get {
				if (funcStart == null)
					throw new NotImplementedException ();
				return this.funcStart.Invoke () ? ButtonState.Pressed : ButtonState.Released;
			}
		}

		public ButtonState X 
		{ 
			get {
				if (funcX == null)
					throw new NotImplementedException ();
				return this.funcX.Invoke () ? ButtonState.Pressed : ButtonState.Released;
			}
		}

		public ButtonState Y 
		{ 
			get {
				if (funcY == null)
					throw new NotImplementedException ();
				return this.funcY.Invoke () ? ButtonState.Pressed : ButtonState.Released;
			}
		}

		//
		// Fields
		//
		private Func<bool> funcA;
		private Func<bool> funcB;
		private Func<bool> funcBack;
		private Func<bool> funcBigButton;
		private Func<bool> funcLeftBumber;
		private Func<bool> funcLeftStick;
		private Func<bool> funcRightBumber;
		private Func<bool> funcRightStick;
		private Func<bool> funcStart;
		private Func<bool> funcX; 
		private Func<bool> funcY; 

		// 
		// Constructor
		//

		public VirtualGamePadButtons(Func<bool> funcA, Func<bool> funcB, Func<bool> funcBack, Func<bool> funcBigButton,	Func<bool> funcLeftBumber, Func<bool> funcLeftStick,
			Func<bool> funcRightBumber, Func<bool> funcRightStick, Func<bool> funcStart, Func<bool> funcX, Func<bool> funcY) 
		{
			this.funcA = funcA;
			this.funcB = funcB;
			this.funcBack = funcBack;
			this.funcBigButton = funcBigButton;
			this.funcLeftBumber = funcLeftBumber;
			this.funcLeftStick = funcLeftStick;
			this.funcRightBumber = funcRightBumber;
			this.funcRightStick = funcRightStick;
			this.funcStart = funcStart;
			this.funcX = funcX;
			this.funcY = funcY;
		}
	}
}

