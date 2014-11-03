using System;
using Microsoft.Xna.Framework;

namespace Hoax.Framework.Exception
{
	public class WorldMatrixDecomposeException : ApplicationException
	{
		public WorldMatrixDecomposeException (Matrix matrix) : base(matrix.ToString()) {}
	}
}

