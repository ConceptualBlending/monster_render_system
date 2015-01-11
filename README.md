Medusa Rendering Ontologies Utility
=====================

Scientific team projects for master students of the Faculty of Computer Science (FIN), ,(c) Marcus Pinnecke, (marcus.pinnecke@st.ovgu.de). This project is part of conceptual blending project, see https://github.com/ConceptualBlending.

Running Medusa
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
ABC
```
Markup files
-------------------
This file contains individual definitions (based on a type inside the repository) and relation definitions which conntects and move individuals. Not each type in the repository is required to use. In the following we want to create 4 individuals. Two of them are from type *Type2* whereas one is from type *Type3* and the other is *Typpe4*. Inside the definitions part we connect some connections points of those defined individuals.

The markup file content is:
```
ABC
```
To render the markup file relative to the repository, call:
```
medusa ~/repository/myrep.json ~/input/input.json ~/output/myfile.png
```
