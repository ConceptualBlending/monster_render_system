using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Threading;
using C5;
using Hoax.Engine.Common;

namespace Hoax.Engine.Physics2D
{
	public sealed class CollisionDetector
	{
		private static volatile CollisionDetector instance;
		private static object syncRoot = new Object();

		private CollisionDetector() {

		}


		public static CollisionDetector Instance
		{
			get 
			{
				if (instance == null) 
				{
					lock (syncRoot) 
					{
						if (instance == null) 
							instance = new CollisionDetector();
					}
				}

				return instance;
			}
		}

		private ArrayList<Polygon2D> PassivePolygons = new ArrayList<Polygon2D>();
		private ArrayList<Polygon2D> ActivePolygons = new ArrayList<Polygon2D>();

		public void AddActive(Polygon2D polygon)
		{
			ActivePolygons.Add (polygon);
		}

		public void AddPassive (Polygon2D polygon)
		{
			PassivePolygons.Add (polygon);
		}
	
		public delegate void CollisionDecteted (object sender, CollisionDetectorEventArgs e);
		public event CollisionDecteted OnCollisionDectetedHandler;

		protected void OnCollisionDecteted (CollisionDetectorEventArgs e)
		{
			if (OnCollisionDectetedHandler != null)
				OnCollisionDectetedHandler (this, e);
		}

		public void Update()
		{
			for (int a = 0; a < ActivePolygons.Count; a++) {
				for (int b = 0; b < PassivePolygons.Count; b++) {
					if (a != b) {
						if (Polygon2D.Intersecs (ActivePolygons [a], PassivePolygons [b])) {
							OnCollisionDecteted (new CollisionDetectorEventArgs (ActivePolygons [a], PassivePolygons [b]));
						}
					}
				}
			}
		}
	}
}

