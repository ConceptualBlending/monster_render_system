using System;
using HoaxFramework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Hoax.Engine.Input;
using System.Diagnostics;

namespace Hoax.Engine.Services
{
	public sealed class SceneService
	{
		#region Thread-Safe Singleton
		private static volatile SceneService instance;
		private static object syncRoot = new Object();

		private SceneService() {
			this.sceneStack = new Stack<SceneObject> ();
		}

		public static SceneService Instance
		{
			get 
			{
				if (instance == null) 
				{
					lock (syncRoot) 
					{
						if (instance == null) 
							instance = new SceneService();
					}
				}

				return instance;
			}
		}
		#endregion

		public enum SceneType { Regular, Intermediate }

		public class Fader 
		{
			public struct FadeStruct {
				public AbstractScene From;
				public AbstractScene To;
				public TransitionBase Transition;

				public FadeStruct(AbstractScene from, AbstractScene to, TransitionBase transition) {
					this.From = from;
					this.To = to;
					this.Transition = transition;
				}
			}

			Queue<FadeStruct> Fadings = new Queue<FadeStruct>();

			public void Fade(AbstractScene from, AbstractScene to, TransitionBase transition)
			{
				Fadings.Enqueue (new FadeStruct(from, to, transition));
			}

			public FadeStruct GetCurrent() 
			{
				return Fadings.Peek ();
			}

			public void CurrentDone()
			{
				Fadings.Dequeue ();
			}

			public bool Fading()
			{
				return Fadings.Count > 0;
			}
		}

		public struct SceneObject  
		{
			public AbstractScene Scene 
			{
				get;
				private set;
			}

			public SceneType Type
			{
				get;
				private set;
			}

			public SceneObject(AbstractScene scene, SceneType type) {
				this = default(SceneObject);
				this.Scene = scene;
				this.Type = type;
			}
		}

		Stack<SceneObject> sceneStack;
		Fader fader = new Fader ();

		SpriteBatch spriteBatch;
		GraphicsDevice graphicsDevice;
		Color[] colorBuffer;
		Texture2D renderedScene;
		RenderTarget2D renderTarget;

		int width;
		int height;

		Game game;

		public void Initialize (Game game, GraphicsDevice graphicsDevice, int width, int height)
		{
			this.game = game;
			this.graphicsDevice = graphicsDevice;
			this.spriteBatch = new SpriteBatch (graphicsDevice);
			this.renderedScene = new Texture2D (graphicsDevice, width, height);
			this.width = width;
			this.height = height;
			this.colorBuffer = new Color[width * height];
			this.renderTarget = new RenderTarget2D (graphicsDevice, width, height);
			this.SystemErrorMode = false;
		}

		public enum SceneSwitchResponse
		{
			Accepted, Rejected
		}

		public bool SystemErrorMode { 
			get;
			private set;
		}

		public SceneSwitchResponse RunNextScene (AbstractScene scene, SceneType sceneType = SceneType.Regular)
		{

				if (this.sceneStack.Count == 0) {
				this.sceneStack.Push (new SceneObject (new EmptyScene (this.game, this.graphicsDevice, 800, 480),  // TODO: Warum hier fix resolution?
						sceneType));
				}

			if (this.fader.Fading () && !(scene is SystemErrorScene)) {
				return SceneSwitchResponse.Rejected;
			} else{
				Trace.WriteLine ("Run next scene \"" + scene.Name + "\"");

				if (this.SystemErrorMode |= scene is SystemErrorScene && this.SystemErrorMode) {
					this.sceneStack.Clear ();
					this.sceneStack.Push (new SceneObject(scene, SceneType.Regular));
					return SceneSwitchResponse.Accepted;
				}

				//Console.WriteLine ("Run next...");

				AbstractScene currentScene = this.sceneStack.Peek ().Scene;
				//TransitionBase transition = new DissolveTransition (graphicsDevice, scene.Width, scene.Height, DissolveTransition.LinearTransitionMedium);
				TransitionBase transition = new OutboxTransition (graphicsDevice, scene.Width, scene.Height);
				this.fader.Fade (currentScene, scene, transition);

				currentScene.OnLeaving ();
				scene.LoadContent ();
				scene.OnEntering ();

				transition.TransitionEnded += (sender, e) => {
					currentScene.OnLeaved ();
					scene.OnEntered ();

					this.sceneStack.Pop ();
					this.sceneStack.Push (new SceneObject (scene, SceneType.Regular)); // TODO

					this.fader.CurrentDone ();
				};



				/*switch (sceneType) {
			case SceneType.Regular:
				if (this.sceneStack.Count > 0) {
					SceneObject last = this.sceneStack.Pop ();
					last.Scene.OnLeave ();
				}
				break;
			case SceneType.Intermediate:
				if (this.sceneStack.Count == 0)
					throw new Exception ("Itermediate scenes could not be activated without any parent");
				CurrentScene.Scene.OnPause ();

				break;
			default:
				throw new Exception ("Unkown scene type");
			}

			this.sceneStack.Push (new SceneObject(scene, sceneType));
			scene.LoadContent ();
			scene.OnEnter ();*/
				return SceneSwitchResponse.Accepted;
			}
		}

		public void ResumePreviousScene () {
	/*		if (CurrentScene.Type != SceneType.Intermediate)
				throw new Exception ("Current scene is not intermediate.");
			else {
				this.sceneStack.Pop ().Scene.OnLeaving();
				this.sceneStack.Peek ().Scene.OnResume ();
			}*/
		}

		public void Update(GameTime gameTime, Dictionary<Player, VirtualGamePad> playerGamePads) 
		{
			try {
				if (!GameService.Instance.Initialized) {
					#if DEBUG
					Console.WriteLine("Init game service...");
					#endif
					GameService.Instance.Initialize();
				}

				if (this.fader.Fading () && !this.SystemErrorMode) {
					this.fader.GetCurrent ().From.Update (gameTime, playerGamePads);
					this.fader.GetCurrent ().To.Update (gameTime, playerGamePads);
					this.fader.GetCurrent ().Transition.Update (gameTime);
				} else if (this.sceneStack.Count > 0) {
					this.sceneStack.Peek ().Scene.Update (gameTime, playerGamePads);
				}
			} catch (Exception e) {
				Trace.WriteLine("FATAL: unhandled exception during update occurred!" + e.Message);

				this.SystemErrorMode = true;
				this.RunNextScene (new SystemErrorScene (this.game, this.graphicsDevice));
			}
		}

		public RenderTarget2D Render(GameTime gameTime, GraphicsDevice graphicsDevice) 
		{
			try {
				RenderTarget2D renderedScene = null;

				if (this.fader.Fading () && !this.SystemErrorMode) {
					RenderTarget2D texFrom = this.fader.GetCurrent ().From.Render (gameTime, graphicsDevice);
					RenderTarget2D texNext = this.fader.GetCurrent ().To.Render (gameTime, graphicsDevice);
					this.fader.GetCurrent ().Transition.Render (graphicsDevice, texFrom, texNext);
					renderedScene = this.fader.GetCurrent ().Transition.Result;
				} else {
					renderedScene = this.sceneStack.Peek ().Scene.Render(gameTime, graphicsDevice);
				}

				graphicsDevice.pushRenderTarget (this.renderTarget);
				graphicsDevice.Clear (Color.Black);
//
				if (renderedScene != null) {
					this.spriteBatch.Begin ();
					this.spriteBatch.Draw (renderedScene, Vector2.Zero);
					this.spriteBatch.End ();
				}

				graphicsDevice.popRenderTarget ();

//				this.renderTarget.GetData (this.colorBuffer);

				return this.renderTarget;
			} catch (Exception e) {
				this.SystemErrorMode = true;
				this.spriteBatch.End ();

				Trace.WriteLine("FATAL: unhandled exception during rendering occurred! " + e.Message);
				Trace.WriteLine(e.StackTrace);

				this.RunNextScene (new SystemErrorScene (this.game, this.graphicsDevice));
				return null;//Render (gameTime, graphicsDevice);
			}
		}

	}
}

