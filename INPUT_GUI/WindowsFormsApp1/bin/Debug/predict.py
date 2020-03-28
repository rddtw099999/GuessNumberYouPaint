import numpy as np
import ctypes
import keras
from keras.models import Sequential
from keras.layers import Dense, Activation, Conv1D, Flatten, Dropout, MaxPooling1D
from keras.regularizers import l1, l2
from keras.optimizers import SGD, Adam
import matplotlib.pyplot as plt
from keras.datasets import mnist
import argparse


def MakeArgParser():
    parser = argparse.ArgumentParser()
    parser.add_argument('--content', type=str,
                        default=None, help='Content to say')
    parser.add_argument('--timestamp', type=str, default=None,
                        help='The File Name of Result')
    return parser

if __name__ == '__main__':
    parser = MakeArgParser()
    args = parser.parse_args()
    # ctypes.windll.user32.MessageBoxW(0, "Test", "Your title", 1)
    # args.timestamp
    # 1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111
    # str(args.content)
    #
    with open(str(args.timestamp) + '.txt', mode='w') as resultFile:
        model = keras.models.load_model('great_model.h5')
        # f = open('modelloaded.txt', 'a')
        # f.write(args.content)
        # ctypes.windll.user32.MessageBoxW(0, "Model loaded", "Your title", 1)
        getcontent = str(args.content)
        picdata = [0] * 28 * 28
        picdata = list(getcontent)
        # res = str(picdata)
        # data = list(getcontent)
        data = np.matrix(picdata, dtype='int')
        data = np.array(data)
        data = data.reshape(1, 28, 28)
        # res=str(data)
        data = data * 255

        # f = open('news.txt', 'a')
        # f.write(str(data))
        res = model.predict(data)
        data = str(res)
        # res = model.predict(data)
        think = 0
        #for n in range(1, 10):
        #	if(res[0][think] <= res[0][n]):
        #		think = n
                # print(think)
        # if(res[0][think] < 0.7):
        # f = open('expected.txt', 'a')
        # f.write(think)

        resultFile.write(data)
        # else:
        # resultFile.write(think)
