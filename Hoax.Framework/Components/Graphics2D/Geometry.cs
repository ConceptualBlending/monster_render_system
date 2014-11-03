using System;

namespace Hoax.Framework.Components.Graphics2D
{
	public abstract class Drawable : Node
	{
		CullingMode CullinMode { get; set; }

		public Drawable(string identifier) : base (identifier) {
			CullinMode = CullingMode.Inherit;
		}
	}
}

