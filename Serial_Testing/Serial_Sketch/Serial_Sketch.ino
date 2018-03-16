#define MYSERIAL Serial
int i = 0,j=0;

void setup() {
  // put your setup code here, to run once:
  MYSERIAL.begin(9600);

}

void loop() {

  MYSERIAL.write('0' + j);
  MYSERIAL.write('0' + i);
  MYSERIAL.write(' ');
  //delay(2);
  i++;
/*
  if ((i>=10)&&(j>=10)) {
    MYSERIAL.write('\n');
  }
  */

  if(i >= 10){ i = 0; j++;};
  if(j >= 10) j = 0;

}
