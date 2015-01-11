Medusa Rendering Ontologies Utility
=====================
Scientific team projects for master students of the Faculty of Computer Science (FIN), ,(c) Marcus Pinnecke, (marcus.pinnecke@st.ovgu.de). This project is part of conceptual blending project, see https://github.com/ConceptualBlending.

Installation Instructions
=====================

Medusa is written in C# using MonoGame. If you want to run this tool you need the [Mono](http://www.mono-project.com/) and [MonoGame](http://www.monogame.net/) installed on your machine. Although the target platforms are Linux and Mac OS you should be able to compile and run in on Windows using .NET and XNA. 

Linux
-----

```
sudo apt-get install mono-complete
```

tetsed on Ubuntu.


Development Instructions
=====================

Linux
-----
```
sudo apt-get install libmonogame-cil-dev monodevelop-monogame
```


- TODO: Use *unsafe* option.
- 

Usage Instructions
=====================
It is recommanded to run medusa via terminal. Because plattform differences, the process is a bit different for linux and OS X. In general, running medusa without any additional command line will open the help information which is the same as
```
$./Medusa2 --help
```


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
medusa ~/repository/myrep.json ~/input/input.json ~/output/myfile.png
```
