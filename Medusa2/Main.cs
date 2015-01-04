
using System;
using System.Collections.Generic;
using System.Linq;

using MonoMac.AppKit;
using MonoMac.Foundation;
using Medusa2;
using System.IO;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main (string[] args)
		{
			// TODO: Read from command line
			string repositoryFile = "/Users/marcus/Desktop/MonsterRenderer/Repository/Repository.json";
			string repositoryRoot = Path.GetDirectoryName (repositoryFile);
			string markupFile = "/Users/marcus/Desktop/MonsterRenderer/MonsterMarkup/markup1.json";
			string outputFileName = "/Users/marcus/Desktop/MonsterRenderer/OutputFiles/"+Path.GetRandomFileName()+".png";

			var repository = Repository.Deserialize (repositoryRoot, string.Join("\n", File.ReadAllLines(repositoryFile)));
			var monsterMarkup = MonsterMarkup.Deserialize (string.Join("\n", File.ReadAllLines(markupFile)));

			var validator = new Validator (repository, monsterMarkup);
			validator.validate ();

			if (!(Config.ShowWindow || Config.StoreOutputImage))
				throw new Exception ("Illegal configuration combination. If you don't show the application window you have to enable the output storing flag.");


			NSApplication.Init ();
			
			using (var p = new NSAutoreleasePool ()) {
				NSApplication.SharedApplication.Delegate = new AppDelegate (repository, monsterMarkup, outputFileName);
				NSApplication.Main (args);
			}


		}
	}

	class AppDelegate : NSApplicationDelegate
	{
		MedusaRenderer game;
		Repository repository;
		MonsterMarkup markup;
		string outputFileName;

		public AppDelegate(Repository repository, MonsterMarkup markup, string outputFileName) {
			this.repository = repository;
			this.markup = markup;
			this.outputFileName = outputFileName;
		}

		public override void FinishedLaunching (MonoMac.Foundation.NSObject notification)
		{
			game = new MedusaRenderer (repository, markup, outputFileName);
			game.Run ();
		}

		public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender)
		{
			return true;
		}
	}
}


