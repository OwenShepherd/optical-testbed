with open('TeensyTest.txt') as f:
    data = f.read()

firstDigit = "1"
secondDigit = "2"
prevDigit = 12
readFirst = False
readSecond = False
flags = 0
counter = -100

for c in data:

    prevDigit = int(firstDigit + secondDigit)

    if (readFirst):
        firstDigit = c;
        readFirst = False
        readSecond = True
    elif (readSecond):
        secondDigit = c;
        readSecond = False
    elif (c==" " and counter = -100):
        readFirst = true
