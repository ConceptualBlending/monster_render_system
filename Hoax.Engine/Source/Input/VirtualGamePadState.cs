using System;
using C5;

namespace Hoax.Engine.Input
{
	public struct VirtualGamePadState
	{
		//
		// Properties
		//
		public VirtualGamePadButtons Buttons { 
			get; 
			set; 
		}

		public VirtualGamePadDPad DPad { 
			get; 
			set;  
		}

		public bool IsConnected { 
			get {
				return connectedFunction.Invoke ();
			}
		}

		public VirtualGamePadThumbSticks ThumbSticks { 
			get; 
			set;  
		}

		public VirtualGamePadTriggers Triggers { 
			get; 
			set;  
		}

		//
		// Fields
		//
		private Fun<bool> connectedFunction;

		//
		// Constructors
		//
		public VirtualGamePadState (Fun<bool> connectedFunction, VirtualGamePadButtons buttons, VirtualGamePadDPad dPad, VirtualGamePadThumbSticks thumbSticks, VirtualGamePadTriggers triggers)
		{
			this = default(VirtualGamePadState);
			this.connectedFunction = connectedFunction;
			this.Buttons = buttons;
			this.DPad = dPad;
			this.ThumbSticks = thumbSticks;
			this.Triggers = triggers;
		}
	}
}

