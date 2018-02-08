import csv
import numpy
import matplotlib.pyplot as plt
from PIL import Image

reader = csv.reader(open("output.csv"), delimiter=",")
x = list(reader)
result = numpy.array(x)

img = plt.imshow(result)
