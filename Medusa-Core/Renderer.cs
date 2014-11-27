using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Hoax.Framework.Components.Graphics2D;
using HoaxFramework;
using Microsoft.Xna.Framework.Input;
using Hoax.Components.Graphics;
using Hoax.Components.Diagnostics;
using ConceptualBlending.Tools.Medusa.Components;
using System.Collections.Generic;

namespace ConceptualBlending.Tools.Medusa
{
public abstract class Renderer : Game
{

		#region Fields

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		#endregion

		#region Initialization

		public Renderer ()
		{

			graphics = new GraphicsDeviceManager (this);

			Content.RootDirectory = "Assets";


		}

		protected override void Initialize ()
		{
			base.Initialize ();

			//--------------------------------------------------------------------------------------------------------
			//
			//	D E M O
			//
			//	The following example shows the "via hand" definition of individuals, images (shapes) and connections
			//	points and how the system infers the information we need to render the compound image.
			//
			//	Please note: This example shows the state of development and is very verbose and hard coded.
			//			     In the next versions this will be automated with processes and finally the
			//				 renderer can be called via terminal command line where you call
			//					
			//				NAME
			//					medusa - conceptual blending rendering tool
			//
			//				SYNOPSIS
			//					medusa [options] repository file [output file]
			//
			//				DESCRIPTION
			//					The options are the following:
			//
			//					-v
			//						Runs medusa in verbose mode which gives informatin about interpretation, 
			//						alignments and others.
			//
			//					-g	
			//						Instead of saving the resulting image to the [output file] medusa will run
			//						in graphical window mode.
			//			
			//
			//				DIAGNOSTICS
			//					The medusa tool exits 0 on success, and >0 if an error occurs.
			//
			//				EXAMPLE
			//					medusa source.medusa
			//
			//					medusa -g source.txt monster.png
			//					medusa -g --load plugin-parse-owl.dll source.owl monster.png
			//					medusa -v --textures texrep.xml --points prep.xml source.owl monster.png
			//
			//				MEDUSA FILE FORMAT EXAMLPE
			//
			//					#options -v --textures texrep.xml --points prep.xml monster.png
			//
			//					HorseHead h
			//					HorseTorso t
			//					HorseLeg l1
			//					HorseLeg l2
			//
			//					[h, ToBody, t, Head]
			//					[l1, ToBody, t, Leg1]
			//					[l2, ToBody, t, Leg2]
			//
			//--------------------------------------------------------------------------------------------------------

			Configuration.SetFlag (Configuration.FLAG_VERBOSE);

			TextureRepository textureRepository = new TextureRepository ();
			textureRepository.AddSource ("HorseHead", new InternalRessourceTextureProvider (this, "box1"));
			textureRepository.AddSource ("HorseTorso", new InternalRessourceTextureProvider (this, "box2"));
			textureRepository.AddSource ("HorseLeg", new InternalRessourceTextureProvider (this, "box3"));

			ConnectionPointRegister connectionPointRegister = new ConnectionPointRegister ();

			Dictionary<string, Vector2> horseHeadPoints = new Dictionary<string, Vector2> ();
			horseHeadPoints ["ToBody"] = new Vector2 (80, 120);

			Dictionary<string, Vector2> horseLegPoints = new Dictionary<string, Vector2> ();
			horseLegPoints["ToBody"] = new Vector2(30,10);

			Dictionary<string, Vector2> horseTorsoPoints = new Dictionary<string, Vector2> ();
			horseTorsoPoints["Head"] = new Vector2(20,20);
			horseTorsoPoints["Leg1"] = new Vector2(40,140);
			horseTorsoPoints["Leg2"] = new Vector2(260,140);

			connectionPointRegister.AddConnectionPoints ("HorseHead", horseHeadPoints);
			connectionPointRegister.AddConnectionPoints ("HorseLeg", horseLegPoints);
			connectionPointRegister.AddConnectionPoints ("HorseTorso", horseTorsoPoints);


			Individual h = new Individual("h", new Shape ("HorseHead", textureRepository, connectionPointRegister));
			Individual t = new Individual("t", new Shape ("HorseTorso", textureRepository, connectionPointRegister));
			Individual l1 = new Individual("l1", new Shape ("HorseLeg", textureRepository, connectionPointRegister));
			Individual l2 = new Individual("l2", new Shape ("HorseLeg", textureRepository, connectionPointRegister));


			MonsterRenderer monsterRenderer = new MonsterRenderer (connectionPointRegister);
			monsterRenderer.ReadStatement (new Statement (h, "ToBody", t, "Head"));
			monsterRenderer.ReadStatement (new Statement (l1, "ToBody", t, "Leg1"));
			monsterRenderer.ReadStatement (new Statement (l2, "ToBody", t, "Leg2"));

			/*
			 * MonsterRenderer runs in verbose mode. It prints the following to the standard output
			 * 
			 * Connect texture HorseHead#-62101120 (x=80, y=120) with texture HorseTorso#830132224 (x=20,y=20)
			 * Connect texture HorseLeg#1722365568 (x=30, y=10) with texture HorseTorso#830132224 (x=40,y=140)
			 * Connect texture HorseLeg#1363722624 (x=30, y=10) with texture HorseTorso#830132224 (x=260,y=140)
			 * 
			 * Based on the individual names and points the systems infer which shape (image) is meant and
			 * which point from the left individual should be connected to which point of the right.
			 * 
			 * You can see the shape name followed by "#num (x,y)" where #num is the hash code of this object
			 * (different hash codes means different objects in this context, although the content (the image)
			 *  is the same). The point (x,y) is the defined connection point.
			 */ 

		}

		protected override void LoadContent ()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
		}

		float degree = 0.0f;

		#endregion

		#region Update and Draw


		protected override void Update (GameTime gameTime)
		{
			base.Update (gameTime);

		}

		protected override void Draw (GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			base.Draw(gameTime);
		}

		#endregion


	}
}

