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
