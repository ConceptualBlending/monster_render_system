using System;
using Microsoft.Xna.Framework;

namespace Hoax.Framework.Components.Graphics2D
{
	public class SceneGraph
	{
		private Game Game { get; set; }
		private Entity _rootNode;
		public Entity RootNode { 
			get {
				return _rootNode;
			}
			set  {
				_rootNode = value;
				_rootNode.Game = Game;
			}
		}

		public SceneGraph (Game game)
		{
			Game = game;
			RootNode = new EmptyNode ("RootNode");

		}

		public SceneGraph Update(GameTime gameTime) {
			RootNode.Update (gameTime);
			return this;
		}

		public void LoadContent ()
		{
			RootNode.LoadContent ();
		}
	}
}

