using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Hoax.Engine.Input
{
	public class DefaultVirtalGamePad : VirtualGamePad
	{
		private static Vector2 VectorUp = new Vector2(0, 1);
		private static Vector2 VectorRight = new Vector2(1, 0);
		private static Vector2 VectorDown = new Vector2(0, -1);
		private static Vector2 VectorLeft = new Vector2(-1, 0);

		private static Func<float, float, bool> vibrationFunction = (left, right) => true;
		private static VirtualGamePadState state = new VirtualGamePadState(
			connectedFunction : () => true,
			buttons : new VirtualGamePadButtons(
				funcA : () => Keyboard.GetState().IsKeyDown(Keys.Space),
				funcB : () => Keyboard.GetState().IsKeyDown(Keys.LeftControl),
				funcBack : () => Keyboard.GetState().IsKeyDown(Keys.Escape),
				funcBigButton : () => Keyboard.GetState().IsKeyDown(Keys.A),
				funcLeftBumber : () => Keyboard.GetState().IsKeyDown(Keys.Q),
				funcLeftStick : () => Keyboard.GetState().IsKeyDown(Keys.A),
				funcRightBumber : () => Mouse.GetState().MiddleButton == ButtonState.Pressed,
				funcRightStick : () => Keyboard.GetState().IsKeyDown(Keys.A),
				funcStart : () => Keyboard.GetState().IsKeyDown(Keys.Enter),
				funcX : () => Keyboard.GetState().IsKeyDown(Keys.R),
				funcY : () => Keyboard.GetState().IsKeyDown(Keys.LeftAlt)
			),
			dPad : new VirtualGamePadDPad(
				funcLeft : () => Keyboard.GetState().IsKeyDown(Keys.Left),
				funcUp : () => Keyboard.GetState().IsKeyDown(Keys.Up),
				funcRight : () => Keyboard.GetState().IsKeyDown(Keys.Right),
				funcDown : () => Keyboard.GetState().IsKeyDown(Keys.Down)
			),
			thumbSticks : new VirtualGamePadThumbSticks( 
				funLeft : () => (Keyboard.GetState().IsKeyDown(Keys.W) ? VectorUp    : Vector2.Zero) +
								(Keyboard.GetState().IsKeyDown(Keys.A) ? VectorLeft  : Vector2.Zero) +
								(Keyboard.GetState().IsKeyDown(Keys.S) ? VectorDown  : Vector2.Zero) +
								(Keyboard.GetState().IsKeyDown(Keys.D) ? VectorRight : Vector2.Zero),
				funRight : null // TODO: Emulate thumbstick via mouse movement
			),
			triggers : new VirtualGamePadTriggers ( 
				funLeft : () => Mouse.GetState().RightButton == ButtonState.Pressed ? 1.0f : 0.0f,
				funRight : () => Mouse.GetState().LeftButton == ButtonState.Pressed ? 1.0f : 0.0f
			)
		);

		public DefaultVirtalGamePad() : base(vibrationFunction, state)  {	}
	}
}

