import os
import sys
import struct

if len(sys.argv) < 2:
    print("Usage: python repack_name_blacklsit.py word_list.txt")
    sys.exit()

fileName = sys.argv[1]
wordTxt= open(fileName, "r")
outBin = open(fileName[:-4] + ".bin", "wb+")
spacer = 19

wordLines = wordTxt.readlines()
wordCount = len(wordLines)
outBin.write(struct.pack('<L', wordCount))

print("Word count: " + str(wordCount))

for line in wordLines:
    strippedLine = line.rstrip('\n')
    outBin.write(struct.pack('<L', len(strippedLine)))
    for i in range(len(strippedLine)):
        letterInt = int.from_bytes(strippedLine[i].encode('ansi'), byteorder='little')
        encryptedLetter = letterInt + spacer
        outBin.write(bytes([encryptedLetter]))
        outBin.write(bytes([spacer]))

print("Done")