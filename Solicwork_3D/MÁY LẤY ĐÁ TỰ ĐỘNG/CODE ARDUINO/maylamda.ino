#include <Servo.h>

Servo myServo;
const int optoPin = 2; // chân tín hiệu opto
bool actionTriggered = false;

void setup() {
  pinMode(optoPin, INPUT);
  myServo.attach(9);     // servo gắn vào chân D9
  myServo.write(90);     // dừng servo (giá trị trung bình)
}

void loop() {
  int optoState = digitalRead(optoPin);

  if (optoState == HIGH && !actionTriggered) {
    actionTriggered = true;

    delay(1000); // đợi 1 giây trước khi bắt đầu quay

    // Kiểm tra xem tín hiệu có bị mất trong lúc chờ không
    if (digitalRead(optoPin) == LOW) {
      myServo.write(90); // dừng nếu mất tín hiệu
      actionTriggered = false;
      return;
    }

    myServo.write(180); // quay thuận chiều kim đồng hồ
    // ==== CHỖ CHỈNH THỜI GIAN QUAY THUẬN ====
    // Thời gian quay thuận = 20 lần * 100ms = 2000ms = 2 giây
    for (int i = 0; i < 5; i++) {
      if (digitalRead(optoPin) == LOW) {
        myServo.write(90);
        actionTriggered = false;
        return;
      }
      delay(100);
    }

    myServo.write(0); // quay ngược chiều kim đồng hồ
    // ==== CHỖ CHỈNH THỜI GIAN QUAY NGƯỢC ====
    // Thời gian quay ngược = 10 lần * 100ms = 1000ms = 1 giây
    for (int i = 0; i < 20; i++) {
      if (digitalRead(optoPin) == LOW) {
        myServo.write(90);
        actionTriggered = false;
        return;
      }
      delay(100);
    }

    myServo.write(180); // quay thuận chiều kim đồng hồ
    // ==== CHỖ CHỈNH THỜI GIAN QUAY THUẬN ====
    // Thời gian quay thuận = 20 lần * 100ms = 2000ms = 2 giây
    for (int i = 0; i < 5; i++) {
      if (digitalRead(optoPin) == LOW) {
        myServo.write(90);
        actionTriggered = false;
        return;
      }
      delay(100);
    }

    myServo.write(90); // dừng sau khi quay xong
  }

  if (optoState == LOW) {
    myServo.write(90);       // dừng servo ngay
    actionTriggered = false; // cho phép kích lại khi HIGH lần sau
  }
}
