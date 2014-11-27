using System;

namespace ConceptualBlending.Tools.Medusa.Components
{
	public class MonsterRenderer
	{
		public ConnectionPointRegister ConnectionPointRegister { get; private set; }

		public MonsterRenderer (ConnectionPointRegister connectionPointRegister)
		{
			ConnectionPointRegister = connectionPointRegister;
		}

		public void ReadStatement(Statement statement) 
		{
			if (Configuration.IsActivated (Configuration.FLAG_VERBOSE)) {
				Print (statement);
			}
		}

		void Print (Statement statement)
		{
			var msg = string.Format ("Connect texture {0}#{1} (x={2}, y={3}) with texture {4}#{5} (x={6},y={7})",
						  statement.First.Shape.Identifier, 
						  statement.First.Shape.Texture.GetHashCode(),
				          ConnectionPointRegister [statement.First.Shape.Identifier] [statement.FirstPoint].X,
				          ConnectionPointRegister [statement.First.Shape.Identifier] [statement.FirstPoint].Y, 
						  statement.Second.Shape.Identifier,
						  statement.Second.Shape.Texture.GetHashCode(),
				          ConnectionPointRegister [statement.Second.Shape.Identifier] [statement.SecondPoint].X, 
				          ConnectionPointRegister [statement.Second.Shape.Identifier] [statement.SecondPoint].Y
			          );
			System.Console.WriteLine (msg);
		}
	}
}

