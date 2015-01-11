
using System;
using System.Collections.Generic;
using System.Linq;

using MonoMac.AppKit;
using MonoMac.Foundation;
using System.IO;
using Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.IO;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main (string[] args)
		{
			string repositoryFile, markupFile, outputFileName;
			ParseResult result = parseArguments (args, out repositoryFile, out markupFile, out outputFileName);

			if (result == ParseResult.Ok) {

				if (Config.VerboseMode) {
					Console.WriteLine ("[Modes: show connection points={0}, show window={1}, store image={2}m verbose mode={3}]", Config.ShowConnectionsPoints, Config.ShowWindow, Config.StoreOutputImage, Config.VerboseMode);
					Console.WriteLine ("[IO: repository file={0}, markup file={1}, output file={2}]", repositoryFile, markupFile, outputFileName);
				}
			
				string repositoryRoot = Path.GetDirectoryName (repositoryFile);

				var repository = Repository.Deserialize (repositoryRoot, string.Join ("\n", File.ReadAllLines (repositoryFile)));
				var monsterMarkup = MonsterMarkup.Deserialize (string.Join ("\n", File.ReadAllLines (markupFile)));

				var validator = new Validator (repository, monsterMarkup);
				validator.validate ();

				if (!(Config.ShowWindow || Config.StoreOutputImage))
					throw new Exception ("Illegal configuration combination. If you don't show the application window you have to enable the output storing flag.");


				NSApplication.Init ();

				using (var p = new NSAutoreleasePool ()) {
					NSApplication.SharedApplication.Delegate = new AppDelegate (repository, monsterMarkup, outputFileName);
					NSApplication.Main (args);
				}

			} else {

			}

		}

		public class ArgComparer : IEqualityComparer<string>
		{
			public bool Equals (string x, string y)
			{
				return  x.Trim ().ToLower () == y.Trim ().ToLower ();
			}
			public int GetHashCode (string obj)
			{
				return obj.GetHashCode ();
			}
		}

		private static readonly ArgComparer argComparer = new ArgComparer ();

		public enum ParseResult {
			Ok,

			RepositoryFileNotExists,

			MarkupFileNotExists,

			OutputFileExists,

 			ShowHelp
		}

		static ParseResult parseArguments (string[] args, out string repositoryFile, out string markupFile, out string outputFile)
		{
			repositoryFile = markupFile = outputFile = "";

			string helpLong = "--help";
			string helpShort = "-h";

			string windowLong = "--window";
			string windowShort = "-w";

			string verboseLong = "--verbose";
			string verboseShort = "-v";

			string pointsLong = "--points";
			string pointsShort = "-p";

			string noOutputLong = "--no-output";
			string noOutputShort = "-n";

			Config.ShowWindow = (args.Contains (windowLong, argComparer) || args.Contains (windowShort, argComparer));
			Config.ShowConnectionsPoints = (args.Contains (pointsLong, argComparer) || args.Contains (pointsShort, argComparer));
			Config.StoreOutputImage = !args.Contains (noOutputLong, argComparer) && !args.Contains (noOutputShort, argComparer);
			Config.VerboseMode = (args.Contains (verboseLong, argComparer) || args.Contains (verboseShort, argComparer));

			int noffset = Config.StoreOutputImage ? 1 : 0;

			if (args.Length == 0 || args.Length < 2 + noffset || args.Contains (helpLong, argComparer) || args.Contains (helpShort, argComparer))  {
				Console.WriteLine ("Medusa Rendering Ontologies Utility, \n(c) Marcus Pinnecke, marcus.pinnecke@st.ovgu.de\nThis program is part of conceptual blending project, \nsee https://github.com/ConceptualBlending");
				Console.WriteLine ("\nUsage:");
				Console.WriteLine ("    medusa [options] repositroy-file markup-file [output-file]");
				Console.WriteLine ("\nOptions:");
				Console.WriteLine (string.Format("    {0}\t{1}", "-h, --help".PadRight(20), "Show this help"));
				Console.WriteLine (string.Format("    {0}\t{1}", "-w, --window".PadRight(20), "Show a window containg the rendered image"));
				Console.WriteLine (string.Format("    {0}\t{1}", "-v, --verbose".PadRight(20), "Display more information during runtime"));
				Console.WriteLine (string.Format("    {0}\t{1}", "-p, --points".PadRight(20), "Display connection points"));
				Console.WriteLine (string.Format("    {0}\t{1}", "-n, --no-output".PadRight(20), "Disable output file creation."));
				Console.WriteLine ("\nArguments:");
				Console.WriteLine (string.Format("    {0}\t{1}", "repositroy-file (required)".PadRight(20), "Path to a .json repository file"));
				Console.WriteLine (string.Format("    {0}\t{1}", "markup-file (required)".PadRight(20), "Path to a .json input file containing the markup"));
				Console.WriteLine (string.Format("    {0}\t{1}", "output-file (optional)".PadRight(20), "Path to a not existing file for output"));
				Console.WriteLine ("\nNotes:");
				Console.WriteLine ("    The option -n cannot stand alone without the -w option whereas it is ");
				Console.WriteLine ("    possible to generate an output file and display it the same time. ");
				Console.WriteLine ("    If -n is not set you have to set the [output-file] argument.");
				Console.WriteLine ("\nExamples:");
				printExample ();
				Console.WriteLine ("\n\nIf you found any bugs, please report them to marcus.pinnecke@st.ovgu.de");

				return ParseResult.ShowHelp;
			} 

			repositoryFile = args [args.Length - 2 - noffset];
			markupFile = args [args.Length - 1 - noffset];
			outputFile = Config.StoreOutputImage? args [args.Length - 1] : "";

			if (Config.StoreOutputImage && File.Exists (outputFile)) {
				Console.WriteLine (string.Format ("Output file \"{0}\" does already exists.", outputFile));
				return ParseResult.OutputFileExists;
			} 

			if (!File.Exists (markupFile)) {
				Console.WriteLine (string.Format ("Markup file \"{0}\" does not exist.", markupFile));
				return ParseResult.MarkupFileNotExists;
			}

			if (!File.Exists (repositoryFile)) {
				Console.WriteLine (string.Format ("Repository file \"{0}\" does not exist.", repositoryFile));
				return ParseResult.RepositoryFileNotExists;
			}

			return ParseResult.Ok;
		}

		public static void printExample()
		{
			Console.WriteLine ("    The following example shows how to create a simple repository at ");
			Console.WriteLine ("    ~/repository and an input file ~/input/input.json. ");
			Console.WriteLine ("    The result of rendering will be stored in ~/output/myfile.png. ");
			Console.WriteLine ("    The repository will contain 4 images and the repository index file.");
			Console.WriteLine ("    Inside the index file three of those four images were indexed,");
			Console.WriteLine ("    each then providing some connection points.");
			Console.WriteLine ("\nRepository content:");
			Console.WriteLine ("    The repository is a directory containing images indexed by a repository ");
			Console.WriteLine ("    index file. Assume the following");
			Console.WriteLine ("    directory content:\n");
			Console.WriteLine (string.Format ("      {0}\t{1}", "~/repository".PadRight (20), "Repository root directory"));
			Console.WriteLine (string.Format ("      |-- {0}\t{1}", "myrep.json".PadRight (20), "Repository index file"));
			Console.WriteLine (string.Format ("      |-- {0}\t{1}", "A".PadRight (20), "Sub directory"));
			Console.WriteLine (string.Format ("      |   |-- {0}\t{1}", "a.png".PadRight (20), "Image 1"));
			Console.WriteLine (string.Format ("      |   |-- {0}\t{1}", "b.png".PadRight (20), "Image 2"));
			Console.WriteLine (string.Format ("      |-- {0}\t{1}", "B".PadRight (20), "Sub directory"));
			Console.WriteLine (string.Format ("      |   |-- {0}\t{1}", "b.png".PadRight (20), "Image 3"));
			Console.WriteLine (string.Format ("      |   |-- {0}\t{1}", "c.png".PadRight (20), "Image 4"));
			Console.WriteLine ("\nRepository index file:");
			Console.WriteLine ("    The repository index file \"myrep.json\" contains (relative) paths to images");
			Console.WriteLine ("    inside the repository as well as a unique name for each image and a list of ");
			Console.WriteLine ("    defined so called connection points. Not each file inside the repository ");
			Console.WriteLine ("    index file directory should be named here. However, if an is listed in-");
			Console.WriteLine ("    side the repository index file, make sure the file exists. Assume creating");
			Console.WriteLine ("    four \"types\" based on A/a.png, A/b.png, B/b.png. Please note that a type");
			Console.WriteLine ("    is only identified by it's name. As you will see we take one image file ");
			Console.WriteLine ("    twice (A/a.png) and have to equal images (A/b.png, B/b.png). However, ");
			Console.WriteLine ("    the following file creates four different \"types\". Please ignore the");
			Console.WriteLine ("    (required) fields MedusaFormatToken and Version for now and use them as ");
			Console.WriteLine ("    below. You should personalize your repository with the following");
			Console.WriteLine ("    properties: RepositoryName, RepositoryDescription and a list");
			Console.WriteLine ("    of autors.\n");
			Console.WriteLine ("    The index file content is:\n");
			Console.WriteLine ("\t" + repository_demo);
			Console.WriteLine ("\n\nMarkup file:");
			Console.WriteLine ("    This file contains individual definitions (based on a type inside the ");
			Console.WriteLine ("    repository) and relation definitions which conntects and move individuals.");
			Console.WriteLine ("     Not each type in the repository is required to use. In the following we ");
			Console.WriteLine ("    want to create 4 individuals. Two of them are from type \"Type2\" whereas");
			Console.WriteLine ("    one is from type \"Type3\" and the other is \"Typpe4\". Inside the ");
			Console.WriteLine ("    definitions part we connect some connections points of those defined");
			Console.WriteLine ("    individuals.");
			Console.WriteLine ("    \nThe markup file content is:\n");
			Console.WriteLine ("\t" + markup_demo);
			Console.WriteLine ("\nTo render the markup file relative to the repository, call:");
			Console.WriteLine ("    medusa ~/repository/myrep.json ~/input/input.json ~/output/myfile.png");
							
		}

		public static readonly string repository_demo = "{\n  \"MedusaFormatToken\": \"REP_TYPE_FORMAT_1\",\n  \"RepositoryContent\": [\n    {\n      \"Type\": \"Type1\",\n      \"ImageFile\": \"A/a.png\",\n      \"Points\": {\n        \"Point1\": {\n          \"x\": 340,\n          \"y\": 100\n        }\n      }\n    },\n    {\n      \"Type\": \"Type2\",\n      \"ImageFile\": \"A/a.png\",\n      \"Points\": {\n        \"Point1\": {\n          \"x\": 140,\n          \"y\": 200\n        },\n        \"Point2\": {\n          \"x\": 110,\n          \"y\": 110\n        }\n      }\n    },\n    {\n      \"Type\": \"Type3\",\n      \"ImageFile\": \"A/b.png\",\n      \"Points\": {\n        \"Point1\": {\n          \"x\": 340,\n          \"y\": 100\n        },\n        \"Point2\": {\n          \"x\": 100,\n          \"y\": 410\n        }\n      }\n    },\n    {\n      \"Type\": \"Type4\",\n      \"ImageFile\": \"B/b.png\",\n      \"Points\": {\n        \"Point1\": {\n          \"x\": 340,\n          \"y\": 100\n        },\n        \"Point2\": {\n          \"x\": 100,\n          \"y\": 410\n        }\n      }\n    }\n  ],\n  \"RepositoryName\": \"DemoRepository\",\n  \"RepositoryDescription\": \"Constains art for demo\",\n  \"Version\": 1,\n  \"Autors\": [\n    {\n      \"Name\": \"Marcus Pinnecke\",\n      \"EMail\": \"marcus.pinnecke@st.ovgu.de\"\n    }\n  ]\n}".Replace("\n", "\n\t");
		public static readonly string markup_demo = "{\n  \"Definitions\": [\n    {\n      \"Identifier\": \"i1\",\n      \"Type\": \"Type2\"\n    },\n    {\n      \"Identifier\": \"i2\",\n      \"Type\": \"Type2\"\n    },\n    {\n      \"Identifier\": \"i3\",\n      \"Type\": \"Type3\"\n    },\n    {\n      \"Identifier\": \"i4\",\n      \"Type\": \"Type4\"\n    }\n  ],\n  \"Relations\": [\n    {\n      \"Individual1\": \"i1\",\n      \"Point1\": \"Point1\",\n      \"Individual2\": \"i3\",\n      \"Point2\": \"Point1\"\n    },\n    {\n      \"Individual1\": \"i2\",\n      \"Point1\": \"Point2\",\n      \"Individual2\": \"i3\",\n      \"Point2\": \"Point2\"\n    },\n    {\n      \"Individual1\": \"i3\",\n      \"Point1\": \"Point1\",\n      \"Individual2\": \"i1\",\n      \"Point2\": \"Point2\"\n    },\n    {\n      \"Individual1\": \"i4\",\n      \"Point1\": \"Point1\",\n      \"Individual2\": \"i2\",\n      \"Point2\": \"Point1\"\n    }\n  ]\n}".Replace("\n", "\n\t");


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


