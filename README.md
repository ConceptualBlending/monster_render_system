<h3 align="center">
  <img src="Assets/medusa_logo.png" alt="medusa logo" />
</h3>

-------

Medusa
============

###### Medusa let you display a visual version of a given ontology's individual. It's supports you with a easy to use description format (JSON) for *concepts* in a declarative way. You can use it in console-only linux environments as well as in GUI mode where it can optional shows the output directly to the screen. If you want you can transfer you *concept* definition as a file or directly through std-in to it and store the output to a place in you file systems. This leads to an easy integration in a web like workflow where you can represent your ontology's individual as constructed by the user.

This project is part of conceptual blending project, see https://github.com/ConceptualBlending.

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
Medusa is very easy to use and doesn't not need a dedicated installation script itself. However, because it runs on .NET it is required that your machine has the .NET runtime or compatible runtime installed. The Medusa repository comes with a runable copy inside. Lets start with copying the repository to your local file system. After this you can install a .NET runtime if don't have one already. Finally you can navigate to Medusa's binary and run it.

## Step 1: Make a local copy of the repository
To get a personal copy of Medusa clone it's by typing the following inside a terminal

```
$git clone https://github.com/ConceptualBlending/monster_render_system.git
```

On Windows machines you may have to install an git system first. An alternativ way is to download the repository as zip file in the right side of this page.

Please make sure that your machine runs a .NET runtime because Medusa requires it. If you don't run Windows take a look at [Mono](http://www.mono-project.com/) which is a free port of .NET for Linux and OS X and others. 

## Step 2: Install the .NET/Mono runtime
If you are on a modern Windows machine or you have Mono already installed, you can continue with *Step 3*. 
### OS X
If you want to run Medusa on OS X just download the [Mono Package for OS X](http://www.mono-project.com/download/#download-mac) and install it. For further instructions see [Introduction to Mono on OS X](http://www.mono-project.com/docs/about-mono/supported-platforms/osx/).
### Linux
If you are on a Linux machine type the following inside a terminal

```
$ sudo apt-get install mono-complete
```

This will automatically set up you environment in order to run .NET/Mono applications.

## Step 3: Test that everything works
Navigate to the **Medusa/Binaries/Release/** inside your local copy and run
```
$ mono medusa.exe --window --no-output ../../Examples/Repository/Repository.json ../../Examples/MonsterMarkup/markup1.json
```
This will open Medusa in window mode which directly displays an individual defined inside the examples directory. The result won't be stored and get lost after you close that window. Please note that the **mono** prefix is only required if you are on a non-Windows machine running Mono.

# Quick Start
There are only two basic concepts to know if you want to use Medusa. 

A **repository** for Medusa is an index directory containing several images which are associated to *types*. Each type contains a list of points (*connection points*) with which you can express *relations* between typed *individuals*. For a set of individuals and releations between them, Medua will infer the connection points depending the individuals type and will take care to automatically arrange each individual to match your definition. 

How an individual is descripted is done with a so called **markup file**. Basically a (new) individual contains of relations between existing ones (which are also defined inside the markup file). Each of those individuals must be typed with a type inside the repository except the new one. Whereas you have to build the repository at least one time, you can build any combination of typed individuals if you want with less effort. In most use cases you will link the repository index and one markup file to the Medusa binary by typing

```
$ mono medusa.exe <path to repository index file> <path to markup input file> <path to output file>
```

# Customise Input and Repository
## Repositories

A repository is a directory containing images indexed by a repository index file. Assume the following directory content:
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

The repository index file *myrep.jso* contains (relative) paths to images inside the repository as well as a unique name for each image and a list of defined connection points. Not each file inside the repository index file directory should be named here. However, if an is listed inside the repository index file, make sure the file exists. Assume creating four *types* based on *A/a.png*, *A/b.png*, *B/b.png*. Please note that a type is only identified by it's name. As you will see we take one image file twice (*A/a.png*) and have to equal images (*A/b.png*, *B/b.png*). However the following file creates four different *types*. Please ignore the (required) fields *MedusaFormatToken* and *Version* for now and use them as below. You should personalize your repository with the following properties: *RepositoryName*, *RepositoryDescription* and a list of *authors*. 

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

## Markup files
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
## Running Medusa with your files
It is recommanded to run medusa via terminal. Running medusa without any additional argument in the command line will open the help information which is the same as
```
$ mono medusa.exe --help
```
The general usage is as follows
```
$ mono medusa.exe [options] repositroy-file [markup-file] [output-file]
```
Please note if you set *repositroy-file*, *markup-file* and *output-file* they must be *absolute paths* or relative to the current working directory. It is not recommanded to use *~* as shortcut for your home directory. 

## General usage and command line options

The following table shows possible *option* flags.

| Short        | Long           | Description  |
| ---        | ---           | ---  |
|-h | --help | Show this help and quit|
|-e | --show-examples | Show examples and quit|
|-w | --window | Show a window containg the rendered image|
|-o | --overwrite | Allows overwriting existing output files|
| | --use-stdin | Reads the input markup file from stdin|
|-n | --no-output | Disable output file creation|

Combinations of short arguments like *-wo* are **not** supported. Please use *-w -o* instead.

Arguments:

| Name       | Required | Description  |
| ---       | --- | ---  |
| repositroy-file | Yes | Path to a .json repository file
| markup-file | Not required, iff *--use-stdin* is set | Path to a .json input file containing the markup
| output-file | Not required, iff *-w* is set | Path to a not existing file for output

**Notes**: The option -n cannot stand alone without the -w option whereas it is possible to generate an output file and display it the same time.If -n is not set you have to set the [output-file] argument. The [markup-file] is not allowed if and only if *--use-stdin* flag is set.

## Examples
Medusa comes with an example repository (Medusa/Examples/Repository/Repository.json) and some markup files to play with (Examples/MonsterMarkup/*.json). 

### Use of standard input
You can put your markup directly as an input stream into Medusa instead of linking to a file. This is possible with the *--use-stdin* flag. An example call is

```
$ mono medusa.exe --use-stdin --no-output ../../Examples/Repository/Repository.json
Type :done if you finished your input or :cancel to abort.
>
```

After *>* you can type line by line you *markup file* ending with *:done*. Although it is possible to create markups on the fly with this, the standard input flag will mostly used to transfer output from another application directly to Medusa without the need of a temporary file. This is very handy when embedding Medusa inside a workflow. Suppose you have an application called *monsterman* which produces Medusa markup files based on some other input and requirements. You can transfer the output of *monsterman* to Medusa with 
```
less monsterman | mono medusa.exe -w --use-stdin -n ../../Examples/Repository/Repository.json
```
In this example Medusa runs in window mode without any file output. Because medusa uses JSON as format you can consider to use a format converter between the output of *monsterman* and Medusa if *monsterman* produces non-Markup-files. A pseudo call with a converter tool, let's call it *convert*, between *monsterman*'s output and Medusa's input is
```
less monsterman | convert | mono medusa.exe -w --use-stdin -n ../../Examples/Repository/Repository.json
```

# Development

Medusa is written in pure C# using the standard components .NET framework. 

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


Example and File Formats
========================

The following example shows how to create a simple repository at *~/repository* and an input file *~/input/input.json*. The result of rendering will be stored in *~/output/myfile.png.* The repository will contain 4 images and the repository index file. Inside the index file three of those four images were indexed, each then providing some connection points. 

Repository content
-------------------


Markup files
-------------------

```
To render the markup file relative to the repository, call:
```
medusa /home/me/repository/myrep.json /home/me/input/input.json /home/me/output/myfile.png
```
