void setup() {
  // put your setup code here, to run once:

  Serial.begin(9600);
  Serial.println(A0);
}

void loop() {

  int frame[8]; // array of frame
  for (int a = 0; a < 8; a++) {
    int val = map( analogRead(a+6), 350, 360, 0, 1);
    if (val < 0 ) {
      val = 0;
    }
    if (val > 1) {
      val = 1;
    }
    frame[a] = val;

  }
  Serial.println("");
  for (int b = 0; b < 8; b++) {
    Serial.print(frame[b]);
     Serial.print(",");
  }
  Serial.println("");
}
