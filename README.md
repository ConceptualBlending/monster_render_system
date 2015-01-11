Medusa Rendering Ontologies Utility
=====================

Scientific team projects for master students of the Faculty of Computer Science (FIN), ,(c) Marcus Pinnecke, (marcus.pinnecke@st.ovgu.de). This project is part of conceptual blending project, see https://github.com/ConceptualBlending.



Running Medusa on OS X
----------------------

```
$cd Medusa/Binaries/Release/Medusa2.app/Contents/MacOS/
$./Medusa2 REP_FILE INPUT_FILE [OUTPUT_FILE]
```

General usage and command line options
--------------------------------------

Usage:
```
medusa [options] repositroy-file markup-file [output-file]
```

Options:

| Short        | Long           | Description  |
--- | --- | ---
| -h, | --help |  Show this help
| -w, | --window | Show a window containg the rendered image
| -v, | --verbose |  Display more information during runtim
| -p, | --points | Display connection points
| -n, | --no-output | Disable output file creation

Arguments:

| Name       | Required | Description  |
--- | ---
| repositroy-file | Yes | Path to a .json repository file
| markup-file | Yes | Path to a .json input file containing the markup
| output-file | Optional, if -w flag | Path to a not existing file for output

Notes:
The option -n cannot stand alone without the -w option whereas it is possible to generate an output file and display it the same time. If -n is not set you have to set the [output-file] argument.
