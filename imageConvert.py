import csv
import numpy
import matplotlib.pyplot as plt
from PIL import Image

result = numpy.genfromtxt("output.csv", delimiter=",")
result = result.astype(numpy.float)
result = result*255/65535
# img = plt.imshow(result)

# Creates a random image 100*100 pixels
# mat = numpy.random.random((100,100))

# Creates PIL image
img = Image.fromarray(result, 'L')
img.save('randomTest.bmp')
