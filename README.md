# Drawer
Windows app and Arduino code, communicating by serial port

## Overview
Drawer is a program that allows drawing monochromatic vector graphics objects on a Windows app, send it in real time through a serial port to Arduino board and draw on Nokia 5510 screen.

### Connect app to Arduino
![IMG_20200104_174702.jpg](https://github.com/thekristopl/Drawer/blob/master/gitresources/IMG_20200104_174702.jpg?raw=true)
### Draw something
![IMG_20200104_174649.jpg](https://github.com/thekristopl/Drawer/blob/master/gitresources/IMG_20200104_174649.jpg?raw=true)
### See the same on Arduino screen
![IMG_20200104_174628.jpg](https://github.com/thekristopl/Drawer/blob/master/gitresources/IMG_20200104_174628.jpg?raw=true)

## How to launch the app
Firstly, you have to do hardware. Connect Arduino board to Nokia 5110 screen. Examples How to do it you can see [HERE](https://create.arduino.cc/projecthub/muhammad-aqib/interfacing-nokia-5110-lcd-with-arduino-7bfcdd). If you used other Arduino pins than me, change it in source code!
Then connect Arduino with your PC using USB cable and upload code to board.
Then run [windows app](https://github.com/thekristopl/Drawer/blob/master/gitresources/drawer.exe?raw=true). Choose proper port and have fun with drawing :)

## Usage & License
Project Simulation is free to use and fork under [BEER-WARE LICENSE](https://pl.wikipedia.org/wiki/Beerware).
