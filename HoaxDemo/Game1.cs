#region File Description
//-----------------------------------------------------------------------------
// Array2DDemoGame.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Diagnostics;
using Hoax.Framework.Components.Graphics2D;
using Hoax.Framework.Components.StateMachine;


#endregion

#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;


#endregion

namespace HoaxDemo
{
	public class Game1 : Game
	{

		#region Fields

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SceneGraph sceneGraph;	
		Sprite[] sprites = new Sprite[5000];


		#endregion

		#region Initialization

		public Game1 ()
		{

			graphics = new GraphicsDeviceManager (this);

			Content.RootDirectory = "Assets";

			graphics.IsFullScreen = false;

			sceneGraph = new SceneGraph (this);
			Sprite sprite1 = new Sprite ("S1", "logo.png");
			Sprite sprite2 = new Sprite ("S2", "logo.png");
			Sprite sprite3 = new Sprite ("S3", "logo.png");
			Sprite sprite4 = new Sprite ("S4", "logo.png");
			Sprite sprite5 = new Sprite ("S5", "logo.png");
			Sprite sprite6 = new Sprite ("S6", "logo.png");
			Sprite sprite7 = new Sprite ("S7", "logo.png");
			Sprite sprite8 = new Sprite ("S8", "logo.png");
			Sprite sprite9 = new Sprite ("S9", "logo.png");
			Sprite sprite10 = new Sprite ("S10", "logo.png");
			Sprite sprite11 = new Sprite ("S11", "logo.png");

			for (int i = 0; i < 200; i++) {
				Sprite a = new Sprite ("X" + i, "logo.png");
				a.Move (new Vector2 (0, i * 2));
				sprite8.AttachChild (a);
				a = new Sprite ("X" + i, "logo.png");
				a.Move (new Vector2 (0, i * 2));
				sprite9.AttachChild (a);
				a = new Sprite ("X" + i, "logo.png");
				a.Move (new Vector2 (0, i * 2));
				sprite10.AttachChild (a);
				a = new Sprite ("X" + i, "logo.png");
				a.Move (new Vector2 (0, i * 2));
				sprite11.AttachChild (a);
				a = new Sprite ("X" + i, "logo.png");
				a.Move (new Vector2 (0, i * 2));
				sprite6.AttachChild (a);
			}

			sprite1.AttachChild (sprite2);
			sprite1.AttachChild (sprite6);
			sprite2.AttachChild (sprite3 );
			sprite2.AttachChild (sprite4 );
			sprite4.AttachChild (sprite5 );
			sprite6.AttachChild (sprite8);
			sprite6.AttachChild (sprite9);
			sprite6.AttachChild (sprite10);
			sprite6.AttachChild (sprite11);

			sceneGraph.RootNode += sprite1;

			sprite2.Move (new Vector2 (50, 50));
			sprite8.Move (new Vector2 (0, 50));
			sprite9.Move (new Vector2 (50, 0));
			sprite10.Move (new Vector2 (0, -50));
			sprite11.Move (new Vector2 (-50, 0));


		}
			
		protected override void Initialize ()
		{
			base.Initialize ();

		}

		String printIt(ISet<uint> set) {
			String result = "[";
			foreach (var e in set)
				result += e + ", ";
			return result += "]";
		}

		void stateMachineDemo ()
		{
			ParallelStateMachine machine = new ParallelStateMachine ();
			uint STATE_PLAY  = machine.GenerateState ();
			uint STATE_PAUSE = machine.GenerateState ();
			uint STATE_STOP  = machine.GenerateState ();
			uint STATE_A  = machine.GenerateState ();
			uint STATE_B  = machine.GenerateState ();
			uint STATE_C  = machine.GenerateState ();

			uint SYMBOL_A = machine.GenerateSymbole ();

			machine.AddStartState (STATE_PAUSE);
			machine.AddFinalState (STATE_STOP);

			machine.StateChanged += (object sender, StateEntryEventArgs e) => Debug.WriteLine (e.Type.ToString() + " " + printIt(e.LastStates) + " " + printIt(e.CurrentStates) + " " + e.Symbol) ;

			machine.RunSequence (sourceNode: STATE_PAUSE, symbol: SYMBOL_A, destNode: STATE_PLAY);
			machine.RunSequence (sourceNode: STATE_STOP, symbol: SYMBOL_A, destNode: STATE_PAUSE);
			machine.RunParallel(STATE_PLAY, STATE_A, STATE_B);
			machine.RunSequence (sourceNode: STATE_A, symbol: SYMBOL_A, destNode: STATE_C);
			machine.Synchronize(STATE_STOP, STATE_B, STATE_C);

			machine.Consume (SYMBOL_A);
			machine.Consume (SYMBOL_A);
			machine.Consume (SYMBOL_A);
			machine.Consume (SYMBOL_A);
			machine.Consume (SYMBOL_A);

			HashSet<uint> aba = new HashSet<uint> ();
		}

		protected override void LoadContent ()
		{
			Debug.WriteLine ("EntireContentIsLoaded " + sceneGraph.RootNode.ContentState.EntireContentIsLoaded);
			Debug.WriteLine ("EntireContentIsLoaded " + sceneGraph.RootNode.ContentState.LocalContentIsLoaded);
			spriteBatch = new SpriteBatch(GraphicsDevice);
			sceneGraph.LoadContent ();
			Debug.WriteLine ("EntireContentIsLoaded " + sceneGraph.RootNode.ContentState.EntireContentIsLoaded);
			Debug.WriteLine ("EntireContentIsLoaded " + sceneGraph.RootNode.ContentState.LocalContentIsLoaded);

			tileRegister = new TileRegister (this, "tilemap.png", new Rectangle (0, 0, 16, 16));
			LayerRenderer layerRenderer = new LayerRenderer ();
			int mapWidth = 20;
			int mapHeight = 20;
			Random random = new Random ();
			int[] flatMap = new int[mapWidth * mapHeight];
			for (int i = 0; i < flatMap.Length; i++)
				flatMap [i] = random.Next (4);

			layer = layerRenderer.Render(this, new LayerDescription(tileRegister, flatMap, mapWidth));

			ParallelAutomaton<int, string> pfa = new ParallelAutomaton<int, string>(new int[] {1}, new int[] {6,4});
			pfa.AddParallelTransition (new List<int>() { 1 }, new List<int>() { 2 });
			pfa.AddParallelTransition (new List<int>() { 1 }, new List<int>() { 5 });
			pfa.AddTransition ( 2 , "c", 3);
			pfa.AddTransition ( 3 , "c", 3);
			pfa.AddTransition ( 3 , "d", 4);
			pfa.AddTransition ( 4 , "c", 3);
			pfa.AddTransition ( 4 , "d", 4);
			pfa.AddTransition ( 5 , "a", 5);
			pfa.AddTransition ( 5 , "b", 6);
			pfa.Consume ("a");
			pfa.Consume ("c");
			pfa.Consume ("c");
			pfa.Consume ("d");
			pfa.Consume ("b");

			Debug.WriteLine (pfa.Accept ());

			stateMachineDemo ();
		}

		Texture2D layer;
		TileRegister tileRegister;


		#endregion

		#region Update and Draw

		Vector2 pos = new Vector2();

		protected override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
			//Debug.WriteLine (TagService.Instance.Get ("Sprite200").Count);
			pos += new Vector2 (1, 0);
			sceneGraph.RootNode ["S1"]["S2"].Move (pos);
			sceneGraph.RootNode.Update (gameTime);
		}

		protected override void Draw (GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin ();
			Entity.Traverse<BreadthFirst> (sceneGraph.RootNode, node => { 
				if (node is Drawable) {
					Sprite sprite = (Sprite) node;
					spriteBatch.Draw(sprite.Texture, sprite.Transformation2D.WorldPosition, sprite.Texture.Bounds, Color.White, sprite.Transformation2D.WorldRotation, sprite.Transformation2D.PivotPoint, sprite.Transformation2D.WorldScale, SpriteEffects.None, 1);
				}} );

			for (int i = 0; i < tileRegister.TileCount (); i++) {
				Texture2D tex = new Texture2D (this.GraphicsDevice, 16, 16);
				tex.SetData<Color> (tileRegister [i]);
				spriteBatch.Draw (tex, new Vector2 (200, 200 + (16*i)), Color.DarkMagenta);
			}

			spriteBatch.Draw (layer, new Vector2(100,100), Color.DarkMagenta);
			spriteBatch.End ();

			base.Draw(gameTime);
		}

		#endregion


	}
}
