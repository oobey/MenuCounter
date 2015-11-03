MenuCounter README
Updated: 3 Nov 2015

Building
--------
This project was created entirely in Visual Studio 2015, including automated tests. To build this project, load MenuCounter.sln in Visual Studio 2015, then press F6.

Installation/Deployment
-----------------------
This application is a self-contained console application, and as such requires no special installation or deployment. Once built, the exe can be copied to and run from any directory.

Instructions
------------
To use MenuCounter, start a Command Prompt by pressing Win+R, typing "cmd", and hitting enter. Navigate to the directory containing the executable, then run it with "MenuCounter.exe <filepath>".

MenuCounter expects and requires a single command line argument, consisting of the path (relative or absolute) to the file to be processed. The program will process the file, output the result, and terminate.

Testing
-------
Once the solution has been loaded in Visual Studio (see above), simply press Ctrl+R, A (or go to Test>Run>All Tests) to run all tests.
