using System;
using Hoax.Engine.Graphics2D;
using Microsoft.Xna.Framework;
using Hoax.Engine.Services;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Hoax.Engine.Graphics2D;

namespace ConceptualBlending.Tools.Medusa.Components
{
	public class MonsterRenderer 
	{
		public ConnectionPointRegister ConnectionPointRegister { get; private set; }
		private SceneGraph sceneGraph;
		private SpriteBatch spriteBatch;
		private Game context;

		public MonsterRenderer (Game context, SpriteBatch batch, ConnectionPointRegister connectionPointRegister)
		{
			if (context == null || connectionPointRegister == null || batch == null)
				throw new NullReferenceException ();

			ConnectionPointRegister = connectionPointRegister;
			sceneGraph = new SceneGraph (context);
			this.spriteBatch = batch;
			this.context = context;
		}
			
		public void ReadStatement(Statement statement) 
		{
			if (Configuration.IsActivated (Configuration.FLAG_VERBOSE)) {
				Print (statement);
			}

			string connectionId1 = statement.First.Identifier + "#" + statement.FirstPoint;
			string connectionId2 = statement.Second.Identifier + "#" + statement.SecondPoint;

			if (!TagService.Instance.Contains (connectionId1))
				registerIndividual (statement.First);
			if (!TagService.Instance.Contains (connectionId2))
				registerIndividual (statement.Second);

			Entity connectionPointLhs = TagService.Instance.Get (connectionId1).First();
			Entity connectionPointRhs = TagService.Instance.Get (connectionId2).First();

			Update (null);

			// Assumption: connectionPointLhs < connectionPointRhs in order to move Lhs to Rhs
			float dx = connectionPointRhs.Transformation2D.WorldPosition.X - connectionPointLhs.Transformation2D.WorldPosition.X;
			float dy = connectionPointRhs.Transformation2D.WorldPosition.Y - connectionPointLhs.Transformation2D.WorldPosition.Y;

			// Correction
			if (connectionPointLhs.Transformation2D.WorldPosition.X < connectionPointRhs.Transformation2D.WorldPosition.X) 
				dx *= -1;
			if (connectionPointLhs.Transformation2D.WorldPosition.Y < connectionPointRhs.Transformation2D.WorldPosition.Y) 
				dy *= -1;

			// Move entire group
			Entity mostTop = connectionPointLhs;
			while (mostTop.Parent != sceneGraph.RootNode)
				mostTop = mostTop.Parent;
			mostTop.Move (new Vector2(dx,dy));

			connectionPointLhs.Parent.AttachChild (connectionPointRhs.Parent);


			/*
			if (connectionPointLhs.Transformation2D.WorldPosition.X < connectionPointRhs.Transformation2D.WorldPosition.X) {
				//

			} else {
				dx *= -1;
			}
			if (connectionPointLhs.Transformation2D.WorldPosition.Y < connectionPointRhs.Transformation2D.WorldPosition.Y) {
				//

			} else {
				dy *= -1;
			}


			Entity mostTop = toIndividual;
			while (mostTop.Parent != sceneGraph.RootNode)
				mostTop = mostTop.Parent;
			mostTop.Move (new Vector2(dx,dy));
			toIndividual.AttachChild (fromIndividual);*/


			// TODO: Use compound drawable shape

		}

		void registerIndividual (Individual individual)
		{
			if (!TagService.Instance.Contains (individual.Identifier)) {
				// Enable drawing the actual shape
				Entity entity = new MedusaCore.Shape.DrawableShape (individual.Identifier, individual.Shape.Texture);
				TagService.Instance.Add (entity, individual.Identifier);
				sceneGraph.RootNode.AttachChild (entity);
			

				foreach (var cp in ConnectionPointRegister[individual.Shape.Identifier]) {
					// Store shape specific connection point (because it's not not here)
					string connectionPointName = individual.Identifier + "#" + cp.Key;
					Entity connectionPoint = new MedusaCore.Shape.DrawableConnectionPoint (context, connectionPointName);
					connectionPoint.Move (cp.Value);
					TagService.Instance.Add (connectionPoint, connectionPointName);
					// Enable drawing the connection point
					entity.AttachChild (connectionPoint);
				}

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

		public void Update (GameTime gameTime) {
			sceneGraph.RootNode.Update (gameTime);
		}

		public void Draw (GameTime gameTime)
		{
			System.Console.Out.WriteLine ("DrawCall!");

			Entity.Traverse<BreadthFirst> (sceneGraph.RootNode, node => { 
				System.Console.Out.WriteLine (node.GetHashCode());
					try {
						MedusaCore.Shape.DrawableShape shape = (MedusaCore.Shape.DrawableShape)node;
			
					spriteBatch.Draw (shape.Texture, shape.Transformation2D.WorldPosition+ new Vector2(200,200), shape.Texture.Bounds, 
							Color.White, shape.Transformation2D.WorldRotation, shape.Transformation2D.PivotPoint, 
							shape.Transformation2D.WorldScale, SpriteEffects.None, 1);
					} catch (Exception e) {
					System.Console.Out.WriteLine (e.Message);
					}

				try {
					MedusaCore.Shape.DrawableConnectionPoint shape = (MedusaCore.Shape.DrawableConnectionPoint)node;

					spriteBatch.Draw (shape.Texture, shape.Transformation2D.WorldPosition - new Vector2(16,16) + new Vector2(200,200), shape.Texture.Bounds, 
						Color.White, shape.Transformation2D.WorldRotation, shape.Transformation2D.PivotPoint, 
						shape.Transformation2D.WorldScale, SpriteEffects.None, 1);
				} catch (Exception e) {
					System.Console.Out.WriteLine (e.Message);
				}
			
			}
			);
		}
	}
}

