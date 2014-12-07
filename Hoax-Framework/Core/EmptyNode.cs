using System;
using Microsoft.Xna.Framework;

namespace Hoax.Framework.Components.Graphics2D
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

