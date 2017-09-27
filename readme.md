# Kata - Minesweeper
## Definition
The U.S. army has planned to use scout drones on a battlefield.
This battlefield is rectangular. Drones must inspect the field to ease the job of minesweepers.

 A drone’s position and location are represented by:
 - x coordinates
 - y coordinates
 - a letter indicating a cardinal compass point. Possible letters are __N__, __E__, __S__ and __W__.

In order to control a drone, a soldier sends a simple string of symbols. The possible symbols are ‘__<__’, ‘__>__’ and ‘__*__’.
- ‘<’ and ‘>’ makes the drone spin 90 degrees left or right respectively, without moving from its current location.
- ‘*’ means move forward one unit, keeping the same heading.
- A drone cannot move out from the battlefield.
- A '*' command that would get a drone off the field has no effect on the drone.

_(0,0)_ are the lower-left coordinates. Assume that the coordinates directly East from _(x, y)_ is _(x+1, y)_.

### Description of the input:
The first line gives the upper-right coordinates of the battlefield. The rest of the input is information pertaining to the drones that have been deployed. Each drone has two lines of input :
- The first line gives the drone’s position
- The second line is a series of instructions telling the drone how to explore the battlefield.
- The position is made up of the x and y co-ordinates and the drone’s orientation. For example __"0 0 E"__ means the drone is in the bottom left corner and facing East.

Each drone will move sequentially, which means that the second drone won’t start to move until the first one has finished moving.

Example of input:

    5 5
    1 1 N
    >********<**********
    1 2 N
    <*<*<*<**
    3 3 E
    **>**>*>>*

### Description of the output:

The output for each drone should be its final co-ordinates and heading.
