using System;
using Microsoft.Xna.Framework.Input;

namespace Hoax.Engine.Input
{
	public struct GamePadTriggerKeyBinding
	{
		public Keys keyLeft { get; set; }
		public Keys keyRight { get; set; }

		/*public GamePadTriggerKeyBinding(Keys keyLeft, Keys keyRight)
		{
			this.keyLeft = keyLeft;
			this.keyRight = keyRight;
		}*/
	}
}

