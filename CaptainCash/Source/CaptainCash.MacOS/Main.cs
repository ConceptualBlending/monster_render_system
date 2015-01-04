
using System;
using System.Collections.Generic;
using System.Linq;

using MonoMac.AppKit;
using MonoMac.Foundation;
using CaptainCash.Core;
using System.IO;

namespace CaptainCash.MacOS
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main (string[] args)
		{
			NSApplication.Init ();
			
			using (var p = new NSAutoreleasePool ()) {
				NSApplication.SharedApplication.Delegate = new AppDelegate ();
				NSApplication.Main (args);
			}


		}
	}

	class AppDelegate : NSApplicationDelegate
	{
		CaptainCashLauncher launcher;

		public override void FinishedLaunching (MonoMac.Foundation.NSObject notification)
		{
			launcher = new CaptainCashLauncher ();
			launcher.Window.AllowUserResizing = true;
			launcher.Window.ClientSizeChanged += new EventHandler<EventArgs>( (sender, e) => launcher.SetBackBufferDimension(launcher.Window.ClientBounds.Width, launcher.Window.ClientBounds.Height));

			launcher.Run ();
		}

		public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender)
		{
			return true;
		}
	}
}


