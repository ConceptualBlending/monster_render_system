using System;

namespace Hoax.Engine.Input
{
	public struct GamePadState
	{
		//
		// Properties
		//
		public IGamePadButtons Buttons 
		{ 
			get; 
			set; 
		}

		public IGamePadDPad DPad 
		{ 
			get; 
			set;  
		}

		public bool IsConnected 
		{ 
			get; 
			set;  
		}

		public IGamePadThumbSticks ThumbSticks 
		{ 
			get; 
			set;  
		}

		public IGamePadTriggers Triggers 
		{ 
			get; 
			set;  
		}

		//
		// Constructors
		//
		public GamePadState(IGamePadButtons buttons, IGamePadDPad dPad, bool isConnected, IGamePadThumbSticks thumbSticks, IGamePadTriggers triggers) 
		{
			this = default(GamePadState);
			this.Buttons = buttons;
			this.DPad = dPad;
			this.IsConnected = isConnected;
			this.ThumbSticks = thumbSticks;
			this.Triggers = triggers;
		}

		//
		// Methods
		//
		public override bool Equals (object obj)
		{
			return obj is GamePadState && this == (GamePadState)obj;
		}

		public override int GetHashCode ()
		{
			return this.Buttons.GetHashCode() * 2 + this.DPad.GetHashCode() * 3 + this.IsConnected.GetHashCode() * 5 + 
				this.ThumbSticks.GetHashCode() * 7 + this.Triggers.GetHashCode() * 11;
		}

		//
		// Operators
		//
		public static bool operator == (GamePadState left, GamePadState right) {
			return left.Buttons == right.Buttons && left.DPad == right.DPad && left.IsConnected == right.IsConnected &&
				left.ThumbSticks == right.ThumbSticks && left.Triggers == right.Triggers;
		}

		public static bool operator != (GamePadState left, GamePadState right) {
			return !(left == right);
		}
	}
}

