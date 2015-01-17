<h3 align="center">
  <img src="Assets/medusa_logo.png" alt="medusa logo" />
</h3>

-------

Medusa
============

###### Medusa let you display a visual version of a given ontology's individual. It's supports you with a easy to use description format (JSON) for *concepts* and a well workflow support for web services. You can use it in console-only linux environments as well as in GUI mode where it can optional shows the output directly to the screen. If you want you can transfer you *concept* definition as a file or directly through std-in to it and store the output to a place in you file systems. This leads to an easy integration in a web like workflow where you can represent your ontology's individual as constructed by the user.

Get in contact with the developer on Twitter: [@marcus_pinnecke](https://twitter.com/marcus_pinnecke)

-------
<p align="center">
    <a href="#installation">Installation</a> &bull; 
    <a href="#quick-start">Quick Start</a> &bull; 
    <a href="#customise">Customise</a> &bull; 
    <a href="#extensions">Development</a> &bull; 
</p>
-------

# Installation
Medusa is very easy to use and doesn't not need a dedicated installation script itself. However, because it runs on .NET it is required that your machine has the .NET runtime or compatible runtimes installed. The Medusa repository comes with a runable copy inside the repository. Lets start with copy the repository to your local file system. After this you can install a .NET runtime if don't have already. Finally you can navigate to Medusa's binary and run it.

## Step 1: Make a local copy of the repository
To get a personal copy of Medusa clone this by typing the following inside a terminal

```
$git clone https://github.com/ConceptualBlending/monster_render_system.git
```

Please make sure that your machine runs a .NET runtime because Medusa requires it. If you don't run Windows take a look at [Mono](http://www.mono-project.com/) which is a free port of .NET for Linux and OS X and others. 

## Step 2: Install the .NET/Mono runtime
If you are on a modern Windows machine or you have Mono already installed, you can continue with *Step 3*. 
### OS X
If you want to run Medusa on OS X just download the [Mono Package for OS X](http://www.mono-project.com/download/#download-mac) and install it. For further instructions see [Introduction to Mono on OS X](http://www.mono-project.com/docs/about-mono/supported-platforms/osx/).
### Linux
If you are on a linux machine type the following inside a terminal

```
$ sudo apt-get install mono-complete
```

This will automatically set up you environment in order to run .NET/Mono applications.

## Step 3: Test that everything works
Navigate to the **Medusa/Binaries/Release/** inside your local copy and run
```
$ mono medusa.exe --window --no-output ../../Examples/Repository/Repository.json ../../Examples/MonsterMarkup/markup1.json
```
This will open medusa in window mode which directly displays an individual defined inside the examples directory. The result won't be stored and get lost after you close that window. Please note that the **mono** is only required if you are on a non-Windows machine.

# Quick Start
# Customise Input and Repository
# Development

Medusa is written in pure C# using the standard components .NET framework. 

Medusa Rendering Ontologies Utility
=====================
Scientific team projects for master students of the Faculty of Computer Science (FIN), ,(c) Marcus Pinnecke, (marcus.pinnecke@st.ovgu.de). This project is part of conceptual blending project, see https://github.com/ConceptualBlending.

Installation Instructions
=====================

Medusa is written in C# using MonoGame. If you want to run this tool you need the [Mono](http://www.mono-project.com/) platform and [MonoGame](http://www.monogame.net/) installed on your machine. Although the target platforms are Linux and Mac OS you should be able to compile and run in on Windows using .NET and XNA. 

Linux
-----

```
sudo apt-get install mono-complete
```

tetsed on Ubuntu.

Mac OS
------

Download and install [Mono Runtime Environment Package](http://www.mono-project.com/download/) if don't have installed it already. For further instructions see [Introduction to Mono on OS X](http://www.mono-project.com/docs/about-mono/supported-platforms/osx/).


First steps
-----------

Navigate to **Medusa/Binaries/Release** and try in your terminal

```
less ../../Examples/MonsterMarkup/stdin.txt | mono medusa.exe -w --use-stdin -n ../../Examples/Repository/Repository.json
```

Development Instructions
=====================

Dependencies
------------
[Json.NET](http://james.newtonking.com/json) tested with .NET 3.5 version, which should be referenced when compiling the source. 

* TODO: Use *unsafe* option.

Linux
-----
Using [MonoDevelop](http://www.monodevelop.com/) as development environment with MonoGame Plugin is recommended. If you don't have this installed already type the following into your terminal.
```
sudo apt-get install libmonogame-cil-dev monodevelop-monogame
```

Mac OS
------
Using [MonoDevelop/Xamarin Studio](http://xamarin.com/) as development environment with [MonoGame Plugin](http://www.monogame.net/downloads/) is recommended.

Windows
-----
This project was not tested on Windows but should be buildable with Visual C# Express and XNA GameStudio. 

Usage Instructions
=====================
It is recommanded to run medusa via terminal. Because plattform differences, the process is a bit different for linux and OS X. In general, running medusa without any additional command line will open the help information which is the same as
```
$./Medusa2 --help
```
```
mono medusa.exe [options] repositroy-file [markup-file] [output-file]
```
Use *--help* for more information. If set *repositroy-file*, *markup-file* and *output-file* must be **absolute paths**.

Running Medusa on OS X
----------------------

```
$cd Medusa/Binaries/Release/Medusa2.app/Contents/MacOS/
$./Medusa2 REP_FILE INPUT_FILE [OUTPUT_FILE]
```

Running Medusa on Windows
----------------------
The engine underlying medusa is *Mono/MonoGame* which is compatible to *.NET/XNA*. Within Visual Studio you should be able to open the project solution file and compile it with XNA GameStudio AddIn but this was not tested.

General usage and command line options
--------------------------------------

Usage:
```
medusa [options] repositroy-file markup-file [output-file]
```

Options:

| Short        | Long           | Description  |
| ---        | ---           | ---  |
| -h, | --help |  Show this help
| -w, | --window | Show a window containg the rendered image
| -v, | --verbose |  Display more information during runtime
| -p, | --points | Display connection points
| -n, | --no-output | Disable output file creation

Arguments:

| Name       | Required | Description  |
| ---       | --- | ---  |
| repositroy-file | Yes | Path to a .json repository file
| markup-file | Yes | Path to a .json input file containing the markup
| output-file | Optional, if -w flag | Path to a not existing file for output

**Notes**: The option *-n* cannot stand alone without the *-w* option whereas it is possible to generate an output file and display it the same time. If *-n* is not set you have to set the *[output-file]* argument.

Example and File Formats
========================

The following example shows how to create a simple repository at *~/repository* and an input file *~/input/input.json*. The result of rendering will be stored in *~/output/myfile.png.* The repository will contain 4 images and the repository index file. Inside the index file three of those four images were indexed, each then providing some connection points. 

Repository content
-------------------
The repository is a directory containing images indexed by a repository index file. Assume the following directory content:
```
~/repository # Repository root directory
      |-- myrep.json" # Repository index file
      |-- A # Sub directory
      |   |-- a.png # Image 1
      |   |-- b.png # Image 2
      |-- B # Sub directory
      |   |-- b.png # Image 3
      |   |-- c.png # Image 4
```

Repository index file
-------------------
The repository index file *myrep.jso* contains (relative) paths to images inside the repository as well as a unique name for each image and a list of defined so called connection points. Not each file inside the repository index file directory should be named here. However, if an is listed inside the repository index file, make sure the file exists. Assume creating four *types* based on *A/a.png*, *A/b.png*, *B/b.png*. Please note that a type is only identified by it's name. As you will see we take one image file twice (*A/a.png*) and have to equal images (*A/b.png*, *B/b.png*). However the following file creates four different *types*. Please ignore the (required) fields *MedusaFormatToken* and *Version* for now and use them as below. You should personalize your repository with the following properties: *RepositoryName*, *RepositoryDescription* and a list of *authors*. 

The index file content is
```
{
  "MedusaFormatToken": "REP_TYPE_FORMAT_1",
  "RepositoryContent": [
    {
      "Type": "Type1",
      "ImageFile": "A/a.png",
      "Points": {
        "Point1": {
          "x": 340,
          "y": 100
        }
      }
    },
    {
      "Type": "Type2",
      "ImageFile": "A/a.png",
      "Points": {
        "Point1": {
          "x": 140,
          "y": 200
        },
        "Point2": {
          "x": 110,
          "y": 110
        }
      }
    },
    {
      "Type": "Type3",
      "ImageFile": "A/b.png",
      "Points": {
        "Point1": {
          "x": 340,
          "y": 100
        },
        "Point2": {
          "x": 100,
          "y": 410
        }
      }
    },
    {
      "Type": "Type4",
      "ImageFile": "B/b.png",
      "Points": {
        "Point1": {
          "x": 340,
          "y": 100
        },
        "Point2": {
          "x": 100,
          "y": 410
        }
      }
    }
  ],
  "RepositoryName": "DemoRepository",
  "RepositoryDescription": "Constains art for demo",
  "Version": 1,
  "Autors": [
    {
      "Name": "Marcus Pinnecke",
      "EMail": "marcus.pinnecke@st.ovgu.de"
    }
  ]
}
```
**Important note**: Pleace take for all assets, files, references or external resources case-sensivity into account.

Markup files
-------------------
This file contains individual definitions (based on a type inside the repository) and relation definitions which conntects and move individuals. Not each type in the repository is required to use. In the following we want to create 4 individuals. Two of them are from type *Type2* whereas one is from type *Type3* and the other is *Typpe4*. Inside the definitions part we connect some connections points of those defined individuals.

The markup file content is:
```
{
  "Definitions": [
    {
      "Identifier": "i1",
      "Type": "Type2"
    },
    {
      "Identifier": "i2",
      "Type": "Type2"
    },
    {
      "Identifier": "i3",
      "Type": "Type3"
    },
    {
      "Identifier": "i4",
      "Type": "Type4"
    }
  ],
  "Relations": [
    {
      "Individual1": "i1",
      "Point1": "Point1",
      "Individual2": "i3",
      "Point2": "Point1"
    },
    {
      "Individual1": "i2",
      "Point1": "Point2",
      "Individual2": "i3",
      "Point2": "Point2"
    },
    {
      "Individual1": "i3",
      "Point1": "Point1",
      "Individual2": "i1",
      "Point2": "Point2"
    },
    {
      "Individual1": "i4",
      "Point1": "Point1",
      "Individual2": "i2",
      "Point2": "Point1"
    }
  ]
}
```
To render the markup file relative to the repository, call:
```
medusa /home/me/repository/myrep.json /home/me/input/input.json /home/me/output/myfile.png
```
