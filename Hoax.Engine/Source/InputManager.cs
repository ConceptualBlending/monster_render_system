using System;
using Hoax.Engine.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HoaxFramework
{
	public class InputManager
	{
		public Dictionary<Player, VirtualGamePad> PlayerGamePads 
		{
			get;
			private set;
		}

		public InputManager() 
		{
			PlayerGamePads = new Dictionary<Player, VirtualGamePad>();
			SetController (Player.One, new DefaultVirtalGamePad());
		}

		public void SetController(Player player, VirtualGamePad gamePad) 
		{
			this.PlayerGamePads [player] = gamePad;
		}

	}
}

