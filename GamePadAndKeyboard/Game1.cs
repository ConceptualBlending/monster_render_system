#region File Description
//-----------------------------------------------------------------------------
// GamePadAndKeyboardGame.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
using Hoax.Engine.Input;


#endregion

#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

#endregion

namespace Hoax.Examples.GamePadAndKeyboard
{
	/// <summary>
	/// Default Project Template
	/// </summary>
	public class Game1 : Game
	{

		#region Fields

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Texture2D logoTexture;
		IGamePad virtualGamePad;

		#endregion

		#region Initialization

		public Game1 ()
		{

			graphics = new GraphicsDeviceManager (this);
			
			Content.RootDirectory = "Assets";

			graphics.IsFullScreen = false;

			setupGamePads ();
		}

		void setupGamePads ()
		{
			GamePadButtonsKeyBinding gamePadButtonsKeyBinding = new GamePadButtonsKeyBinding ()
			{
				keyA = Keys.A,
				keyB = Keys.B,
				keyBack = Keys.C,
				keyBigButton = Keys.D,
				keyLeftShoulder = Keys.E,
				keyLeftStick = Keys.F,
				keyRightShoulder = Keys.G,
				keyRightStick = Keys.H,
				keyStart = Keys.I,
				keyX = Keys.X,
				keyY = Keys.Y
			};
			DirectionKeyBinding dPadKeyBinding = new DirectionKeyBinding()
			{
				keyDown = Keys.Down,
				keyLeft = Keys.Left,
				keyUp = Keys.Up,
				keyRight = Keys.Right,
			};
			DirectionKeyBinding thumbStickLeftBinding = new DirectionKeyBinding ()
			{
				keyDown = Keys.D0,
				keyLeft = Keys.D1,
				keyUp = Keys.D2,
				keyRight = Keys.D3
			};
			DirectionKeyBinding thumbStickRightBinding = new DirectionKeyBinding ()
			{
				keyDown = Keys.D4,
				keyLeft = Keys.D5,
				keyUp = Keys.D6,
				keyRight = Keys.D7
			};
			GamePadTriggerKeyBinding gamePadTriggerKeyBinding = new GamePadTriggerKeyBinding () 
			{
				keyLeft = Keys.D8,
				keyRight = Keys.D9,
			};

			virtualGamePad = new KeyboardGamePad (gamePadButtonsKeyBinding, dPadKeyBinding, thumbStickLeftBinding, thumbStickRightBinding, gamePadTriggerKeyBinding);
		}

		/// <summary>
		/// Overridden from the base Game.Initialize. Once the GraphicsDevice is setup,
		/// we'll use the viewport to initialize some values.
		/// </summary>
		protected override void Initialize ()
		{
			base.Initialize ();
		}


		/// <summary>
		/// Load your graphics content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be use to draw textures.
			spriteBatch = new SpriteBatch (graphics.GraphicsDevice);
			
			// TODO: use this.Content to load your game content here eg.
			logoTexture = Content.Load<Texture2D> ("logo");
		}

		#endregion

		#region Update and Draw

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// TODO: Add your update logic here			
            		
			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself. 
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			// Clear the backbuffer

			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);

			spriteBatch.Begin ();

			// draw the logo
			spriteBatch.Draw (logoTexture, new Vector2 (130, 200), Color.White);

			string pressed = "pressed: ";

			if (virtualGamePad.GetState ().Buttons.A)
				pressed += "A ";
			if (virtualGamePad.GetState ().Buttons.B)
				pressed += "B ";
			if (virtualGamePad.GetState ().Buttons.Back)
				pressed += "Back ";
			if (virtualGamePad.GetState ().Buttons.BigButton)
				pressed += "BigButton ";
			if (virtualGamePad.GetState ().Buttons.LeftShoulder)
				pressed += "LeftShoulder ";
			if (virtualGamePad.GetState ().Buttons.LeftStick)
				pressed += "LeftStick ";
			if (virtualGamePad.GetState ().Buttons.RightShoulder)
				pressed += "RightShoulder ";
			if (virtualGamePad.GetState ().Buttons.RightStick)
				pressed += "RightStick ";
			if (virtualGamePad.GetState ().Buttons.Start)
				pressed += "Start ";
			if (virtualGamePad.GetState ().Buttons.X)
				pressed += "X ";
			if (virtualGamePad.GetState ().Buttons.Y)
				pressed += "Y ";

			if (virtualGamePad.GetState ().DPad.Down)
				pressed += "DPad_Down ";
			if (virtualGamePad.GetState ().DPad.Left)
				pressed += "DPad_Left ";
			if (virtualGamePad.GetState ().DPad.Right)
				pressed += "DPad_Right ";
			if (virtualGamePad.GetState ().DPad.Up)
				pressed += "DPad_Up ";

			pressed += "Trigger_Left " + virtualGamePad.GetState ().ThumbSticks.Left.X + ", " + virtualGamePad.GetState ().ThumbSticks.Left.Y;
			pressed += "Trigger_Right " + virtualGamePad.GetState ().ThumbSticks.Right.X + ", " + virtualGamePad.GetState ().ThumbSticks.Right.Y;
				
			pressed += "Trigger_Left " + virtualGamePad.GetState ().Triggers.Left;
			pressed += "Trigger_Right " + virtualGamePad.GetState ().Triggers.Right;

			System.Console.Out.WriteLine (pressed);

			spriteBatch.End ();

			//TODO: Add your drawing code here
			base.Draw (gameTime);
		}

		#endregion
	}
}
