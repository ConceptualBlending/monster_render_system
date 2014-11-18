using System;

namespace Hoax.Framework.Components.Graphics2D
{
	public class ContentState
	{
		public ContentState ()
		{
			LocalContentIsLoaded = false;
			EntireContentIsLoaded = false;
			LocalContentIsUnloaded = false;
			EntireContentIsUnloaded = false;
		}

		public bool LocalContentIsLoaded { get; internal set; }

		public bool LocalContentIsUnloaded { get; internal set; }

		public bool EntireContentIsLoaded { get; internal set; }

		public bool EntireContentIsUnloaded { get; internal set; }

	}
}

