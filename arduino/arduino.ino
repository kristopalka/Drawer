#include <SPI.h>
#include <Adafruit_GFX.h>
#include <Adafruit_PCD8544.h>

Adafruit_PCD8544 display = Adafruit_PCD8544(7, 6, 5, 4, 3);
int size = 2;
int collor = 1;

void drawCircles(byte x1, byte y1, byte x2, byte y2)
{
  if(abs(x1-x2) > size/2 || abs(y1-y2) > size/2)
  {
    byte x3 = (x1+x2)/2, y3 = (y1+y2)/2;
    display.fillCircle(x3, y3, size / 2, collor);
    drawCircles(x1, y1, x3, y3);
    drawCircles(x3, y3, x2, y2);
  }
}




void setup()
{
  display.begin();
  display.setContrast(40);
  display.clearDisplay();
  display.display();
  Serial.begin(9600);
}

void loop()
{
  if (Serial.available())
  {
    String d;
    d = Serial.readStringUntil('~');
    byte x1, y1, x2, y2;

    

    if (d[0] == '#')
    {
      switch (d[1])
      {
        case 'R': //RESUME
          display.clearDisplay();
          break;

        case 'S': //CHANGE SIZE
          size = (int)d[2];
          break;

        case 'C': //CHANGE COLLOR
          collor = (int)d[2];
          break;

        case 'D': //DRAW
          for (int i = 2; i < d.length(); i += 2)
          {
            x1 = (byte)d[i];
            y1 = (byte)d[i + 1];
            if (i == 2)
            {
              x2 = x1;
              y2 = y1;
            }



            if(size > 1)
            {
              display.fillCircle(x1, y1, size / 2, collor);
              drawCircles(x1, y1, x2, y2);
            }
            else 
            {
              display.drawLine(x1, y1, x2, y2, collor);
            }

            
            x2 = x1;
            y2 = y1;
          }
          break;
      }
    }
    display.display();
  }
}
