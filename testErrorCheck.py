

with open('TeensyTest.txt') as f:
    data = f.read()

firstRead = True
firstDigit = "1"
secondDigit = "2"
prevDigit = 12
readFirst = False
readSecond = False
flags = 0
counter = 0

for c in data:

    if (firstRead):
        if (c==' '):
            readFirst = True
            continue
        elif (readFirst):
            firstDigit = c
            readFirst = False
            readSecond = True
            continue
        elif (readSeond):
            secondDigit = c
            readSecond = False
            firstRead = False
            prevDigit = int(firstDigit+secondDigit)
            counter = prevDigit

    else:
        if (readFirst):
            firstDigit = c
            readFirst = False
            readSecond = True
            continue
        elif (readSecond):
            secondDigit = c
            readSecond = False
            prevDigit = int(firstDigit+secondDigit)
            counter = counter+1
            if (counter != prevDigit):
                flags = flags + 1
        else:
            readFirst = True


    if (counter==99):
        counter = 0
