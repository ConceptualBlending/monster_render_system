using System;
using System.Collections.Generic;

namespace Hoax.Platforming
{
	public class Map
	{
		//
		// Properties
		//
		public CollisonFlags[, ] CollisionMap 
		{
			get;
			private set;
		}

		public ushort MapWidth
		{
			get;
			private set;
		}

		public ushort MapHeight
		{
			get;
			private set;
		}

		/*public ICollection<Background> BackgroundLayer {
			get;
			private set;
		}*/

		// 
		// Flags
		//
		[Flags]
		public enum CollisonFlags : byte
		{
			LeftSide	= 1 << 0,
			TopSide		= 1 << 1,
			RightSide	= 1 << 2,
			BottomSide	= 1 << 3,
			Ladder		= 1 << 4,
		}

		//
		// Constructor
		//
		public Map(ushort width, ushort height)
		{
			this.MapWidth = width;
			this.MapHeight = height;
			this.CollisionMap = new CollisonFlags[width, height];
			for (ushort y = 0; y < height; y++)
				for (ushort x = 0; x < width; x++)
					this [x, y] = 0;

		}

		public CollisonFlags this[ushort x, ushort y] 
		{
			get {
				return this.CollisionMap [x, y];
			}

			set {
				this.CollisionMap [x, y] = value;
			}
		}



		/**
		 * 	TODO: Object layer


		public Palette PaletteLayerMG {
			get;
			private set;
		}

		public Palette PaletteLayerBG0 {
			get;
			private set;
		}

		public Palette PaletteLayerBG1 {
			get;
			private set;
		}

		public Palette PaletteLayerBG2 {
			get;
			private set;
		}

		public Palette PaletteLayerFG0 {
			get;
			private set;
		}

		public Palette PaletteLayerFG1 {
			get;
			private set;
		}

		public Palette PaletteLayerFG2 {
			get;
			private set;
		}

		public byte	Zoomlevel {
			get;
			private set;
		}

		public string Name {
			get;
			private set;
		}

		public string Author {
			get;
			private set;
		}

		public Overlay OverlayLayerMG {
			get;
			private set;
		}

		public Overlay OverlayLayerBG1 {
			get;
			private set;
		}

		public Overlay OverlayLayerBG2 {
			get;
			private set;
		}

		public Overlay OverlayLayerBG3 {
			get;
			private set;
		}
		 
		public Overlay OverlayLayerFG1 {
			get;
			private set;
		}

		public Overlay OverlayLayerFG2 {
			get;
			private set;
		}

		public Overlay OverlayLayerFG3 {
			get;
			private set;
		}


		public byte LayerBG1ParallaxDeph {
			get;
			private set;
		}

		public byte LayerBG2ParallaxDeph {
			get;
			private set;
		}

		public byte LayerBG3ParallaxDeph {
			get;
			private set;
		}

		public byte LayerFG1ParallaxDeph {
			get;
			private set;
		}

		public byte LayerFG2ParallaxDeph {
			get;
			private set;
		}

		public byte LayerFG3ParallaxDeph {
			get;
			private set;
		}
			
		//
		// Enumerations
		//
		public enum Palette
		{
			Forest
		}

		public enum BackgroundType
		{
			Forest,
			Dessert
		}

		//
		// Structs
		//
		public struct Overlay
		{
			byte			R;
			byte			G;
			byte			B;
			byte			A;
		}

		public struct Background
		{
			BackgroundType	BackgroundType;
			byte			ZoomLevel;
			ushort			XOffset;
			ushort			YOffset;
			ushort			ZOffset;
		}

		public struct Tile 
		{
			TileFlags LayerMGFlags;
			byte LayerMGTile;
			byte LayerFG0Tile;
			byte LayerFG1Tile;
			byte LayerFG2Tile;
		}
			
		[Flags]
		public enum TileFlags : byte
		{
			Region00Mark,
			Region01Mark,
			Region02Mark,
			Region03Mark,
			Region04Mark,
			Region05Mark,
			Region06Mark,
			Region07Mark,
			Region08Mark,
			Region09Mark,
			Region10Mark,
			Region11Mark,
			Region12Mark,
			Region13Mark,
			Region14Mark,
			Region15Mark,
			Region16Mark,
			Region17Mark,
			Region18Mark,
			Region19Mark,
			Region20Mark,
			Region21Mark,
			Region22Mark,
			Region23Mark,
			Region24Mark,
			Region25Mark,
			Region26Mark,
			Region27Mark,
			Region28Mark,
			Region29Mark,
			Region30Mark,
			Region31Mark,

			//
			// Collision types
			//
			CollisionLadder,
			CollisionSemiLeft,
			CollisionSemiUp,
			CollisionSemiRight,
			CollisionSemiDown
		} */ 

	}
}

