using System;
using Microsoft.Xna.Framework;

namespace Hoax.Engine.Graphics2D
{
	public class EmptyNode : Node
	{
		public EmptyNode (string identifier) : base(identifier) 
		{
		}
	

		#region implemented abstract members of Entity
		protected override void OnTransformationChanged (Matrix transformation2D)
		{
		}
		#endregion
	}
}

