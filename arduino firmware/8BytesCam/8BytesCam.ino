/*
 * 
 * 
 * Made by Alex Zaslvskis 2021 
 */


#define MAX_0 350 // analogRead constant for 0 
#define MAX_1 360 // analogRead constant for 1 

void setup() {
  // put your setup code here, to run once:

  Serial.begin(9600); // setup serial connection

}

void loop() {

  int frame[8]; // array of frame
  for (int a = 0; a < 8; a++) { // for that will save data as buffer
    int val = map( analogRead(a + 6), 350, 360, 0, 1); // using map convert vals. analogRead(A+6) on Arduino nano the A0 is 14 port. is dirty hack that works idealy.
    if (val < 0 ) { // if value is bit smaller that 0 is still zero . We are using bin system.
      val = 0; // set value to zero 
    }
    if (val > 1) {   // if value is bit bigger  that 1 is still 1 . We are using bin system.
      val = 1; // set value to one 
    }
    frame[a] = val; // write value to frame buffer array.

  }
  Serial.println(""); // print emtry string
  for (int b = 0; b < 8; b++) { // create a loop
    Serial.print(frame[b]); // print the values
    Serial.print(",");// add delimiter
  }
  Serial.println(""); // print emtry line
}
