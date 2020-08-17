# TurtleTest
Turtle traversing a minefield.

Following example as much as possible but since and x and y where sometimes reversed in the example I am using the coordinate system as follows for clarity sake: 

\
 \x= 0   1   2   3   4  N=5
y=\_____________________
   |   |   |   |   |   |
 0 |   |   |   |   |   |
   |___|___|___|___|___|
   |   |   |   |   |   |
 1 | ^ | m |   | m |   |
   |___|___|___|___|___|
   |   |   |   |   |   |
 2 |   |   |   |   | e |
   |___|___|___|___|___|
   |   |   |   |   |   |
 3 |   |   |   | m |   |
   |___|___|___|___|___|
M=4

    N
    ^
    |
 W<--->E
    |
    \/
    S
		

Expected Input Format:
N M			| Grid Size
x0,y0 x1,y1 x2,y2 	| Mine location list
X Y			| exit  location
X Y Dir			| initial turtle state.
R L M M M		| sequence of movement instructions
...			| program will read all sequences till end of file.

Please make sure TurtleInstructions.txt is in the same directory as the TurtleTest.exe binary. 
Entry point is TurtleTest>Program.cs
Main Functionality is in TurtleLibrary: TurtleState.cs, MineField.cs
Using NUnit for unit tests in TurtleLibrary.UnitTests
