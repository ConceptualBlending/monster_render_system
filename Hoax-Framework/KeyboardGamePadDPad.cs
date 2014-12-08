using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Hoax.Engine.Input
{
	internal sealed class KeyboardGamePadDPad : IGamePadDPad {

		#region IGamePadDPad implementation
		//
		// Properties
		//
		public bool Left {
			get {
				return Keyboard.GetState ().IsKeyDown (keyBinding.keyLeft);
			}
		}
		public bool Up {
			get {
				return Keyboard.GetState ().IsKeyDown (keyBinding.keyUp);
			}
		}
		public bool Right {
			get {
				return Keyboard.GetState ().IsKeyDown (keyBinding.keyRight);
			}
		}
		public bool Down {
			get {
				return Keyboard.GetState ().IsKeyDown (keyBinding.keyDown);
			}
		}
		#endregion

		//
		// Private fields
		//
		private DirectionKeyBinding keyBinding;

		//
		// Constructors
		//
		public KeyboardGamePadDPad(DirectionKeyBinding keyBinding) 
		{
			this.keyBinding = keyBinding;
		}



	}
	
}
