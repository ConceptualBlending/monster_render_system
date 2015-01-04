using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Hoax.Engine.Input
{						
	internal sealed class KeyboardThumbSticks : IGamePadThumbSticks {

		#region IGamePadThumbSticks implementation
		//
		// Properties
		//
		public Vector2 Left {
			get {
				return Vector2.Zero;
			}
		}
		public Vector2 Right {
			get {
				return Vector2.Zero;
			}
		}
		#endregion

		//
		// Private fields
		//
		DirectionKeyBinding left;

		DirectionKeyBinding right;

		//
		// Constructors
		//
		public KeyboardThumbSticks(DirectionKeyBinding left, DirectionKeyBinding right)
		{
			this.left = left;
			this.right = right;
		}
	}	
}
