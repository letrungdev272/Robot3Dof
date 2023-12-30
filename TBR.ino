
const int stepPin1 = 2;
const int dirPin1 = 5;
const int enPin1 = 8;

const int stepPin2 = 3;
const int dirPin2 = 6;

const int stepPin3 = 4;
const int dirPin3 = 7;

float x1, y1, z1, x2, y2, z2, x3, y3, z3,d2;
float vtmm1, vtmm2, vtmm3, vt1, vt2, vt3;
int flag;
String buf_s, value;
int val1, val2, timepusle;
String chuoi = "90,90", chuoi1, chuoi2, chuoi3;
byte moc, moc1;
void setup() {

  //   Sets the two pins as Outputs
  Serial.begin(9600);
  flag = 0;
  vt1 = 0;
  vt2 = 0;
  vt3 = 0;

  timepusle = 5000;

  vtmm1 = 0;
  vtmm2 = 0;
  vtmm3 = 0;

  pinMode(stepPin1, OUTPUT);
  pinMode(dirPin1, OUTPUT);
  pinMode(enPin1, OUTPUT);
  digitalWrite(enPin1, LOW);

  pinMode(stepPin2, OUTPUT);
  pinMode(dirPin2, OUTPUT);

  pinMode(stepPin3, OUTPUT);
  pinMode(dirPin3, OUTPUT);

}

//hàm quay thuận động cơ 1
void on_step1() {

  /////////////////////////////////////
  digitalWrite(dirPin1, HIGH); // Enables the motor to move in a particular direction
  // Makes 200 pulses for making one full cycle rotation
  for (int x1 = 0; x1 < vtmm1; x1++) {
    digitalWrite(stepPin1, HIGH);
    delayMicroseconds(timepusle);
    digitalWrite(stepPin1, LOW);
    delayMicroseconds(timepusle);
    vt1++;
  }
  delay(200); // One second delay
  //////////////////////////////////////////////

}

//hàm quay thuận động cơ 2 
void on_step2() {
  digitalWrite(dirPin2, HIGH); // Enables the motor to move in a particular direction
  for (int x2 = 0; x2 < vtmm2; x2++) {
    digitalWrite(stepPin2, HIGH);
    delayMicroseconds(timepusle);
    digitalWrite(stepPin2, LOW);
    delayMicroseconds(timepusle);
    vt2++;

  }
  delay(200); // One second delay

}
//hàm quay thuận động cơ 3
void on_step3() {
  digitalWrite(dirPin3, HIGH); // Enables the motor to move in a particular direction
  for (int x3 = 0; x3 < vtmm3; x3++) {
    digitalWrite(stepPin3, HIGH);
    delayMicroseconds(timepusle);
    digitalWrite(stepPin3, LOW);
    delayMicroseconds(timepusle);
    vt3++;

  }
  delay(200); // One second delay
}
//hàm quay nghịch động cơ 3
void off_step3() {
  digitalWrite(dirPin3, LOW); //Changes the rotations direction
  for (int x3 = 0; x3 < abs(vtmm3); x3++) {
    digitalWrite(stepPin3, HIGH);
    delayMicroseconds(timepusle);
    digitalWrite(stepPin3, LOW);
    delayMicroseconds(timepusle);
    vt3--;
  }
  delay(200);

}
//hàm quay nghịch động cơ 2
void off_step2() {
  digitalWrite(dirPin2, LOW); //Changes the rotations direction
  for (int x2 = 0; x2 < abs(vtmm2); x2++) {
    digitalWrite(stepPin2, HIGH);
    delayMicroseconds(timepusle);
    digitalWrite(stepPin2, LOW);
    delayMicroseconds(timepusle);
    vt2--;
  }
  delay(200);
}
//hàm quay nghịch động cơ 1
void off_step1() {
  digitalWrite(dirPin1, LOW); //Changes the rotations direction
  for (int x1 = 0; x1 < abs(vtmm1); x1++) {
    digitalWrite(stepPin1, HIGH);
    delayMicroseconds(timepusle);
    digitalWrite(stepPin1, LOW);
    delayMicroseconds(timepusle);
    vt1--;
  }
  delay(200);
}
///////////////////////////////////////
void loop() {
  if (Serial.available()) {
    chuoi = Serial.readString(); //Serial đọc chuỗi nhận về từ c#
    for (int i = 0; i < chuoi.length(); i++) {
      if (chuoi.charAt(i) == ' ') {
        moc = i; //Tìm vị trí của dấu " "
      }
    }
    for (int i = 0; i < chuoi.length(); i++) {
      if (chuoi.charAt(i) == ',') {
        moc1 = i; //Tìm vị trí của dấu ","
      }
    }
    chuoi1 = chuoi;//90 90,90
    chuoi2 = chuoi;
    chuoi3 = chuoi;
    chuoi1.remove(moc); //Tách giá trị theta1 ra
    chuoi2.remove(0, moc + 1); //Tách giá trị theta2 ra
    chuoi3.remove(0, moc1 + 1); 
    y1 = chuoi1.toInt(); //Chuyển chuoi1 thành số
    y2 = chuoi2.toInt(); //Chuyển chuoi2 thành số
    y3 = chuoi3.toInt();
    Serial.println(y1);
    Serial.println(y2);
    Serial.println(y3);
    
   
    flag = 0;
    // quy góc ra xung cho động cơ bước
    z1 = y1 * 1.666; // y1 * 3 / 1.8 
    z2 = y2 * 1.666;
    z3 = y3 * 1.666;

    vtmm1 = z1 - vt1; // so sánh với góc nhập trước đó
    vtmm2 = z2 - vt2;
    vtmm3 = z3 - vt3;
  }

  if ((flag == 0) && (vtmm1 >= 0)) 
    on_step1();
  if ((flag == 0) && (vtmm2 >= 0))
    on_step2();
  if ((flag == 0) && (vtmm3 >= 0))
    on_step3();
  if ((flag == 0) && (vtmm1 < 0))
    off_step1();
  if ((flag == 0) && (vtmm2 < 0))
    off_step2();
  if ((flag == 0) && (vtmm3 < 0))
    off_step3();
  flag = 1;

}
