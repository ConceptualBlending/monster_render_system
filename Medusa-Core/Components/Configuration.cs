using System;

namespace ConceptualBlending.Tools.Medusa.Components
{
	public static class Configuration
	{
		private static ushort Flags = 0;

		public static readonly ushort FLAG_VERBOSE = 1;

		public static bool IsActivated(ushort flag)
		{
			return (Flags & flag) == flag;
		}

		public static void SetFlag(ushort flag)
		{
			Flags |= flag;
		}
	}
}

