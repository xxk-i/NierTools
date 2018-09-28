import os
import sys
import struct

if len(sys.argv) < 2:
    print("Usage: python unpack_name_blacklist.py word_xx.bin")
    sys.exit()

banList = open(sys.argv[1], "rb")
outFile = open(sys.argv[1] + ".txt", "w+")

banListSize = os.path.getsize(sys.argv[1])
wordCount = struct.unpack("<L", banList.read(4))[0]
print("Word Count: " + str(wordCount))

while banList.tell() != banListSize:
    wordBytesLength = struct.unpack("<L", banList.read(4))[0]
    wordBytes = banList.read(wordBytesLength * 2)
    word = []
    # byte - subtrahend
    for i in range(0, len(wordBytes), 2):
        if (wordBytes[i] - wordBytes[i + 1] > 0):
            letter = wordBytes[i] - wordBytes[i + 1]
            word.append(letter)

        else:
            letter = 255 - wordBytes[i + 1]
            print(chr(255 - wordBytes[i] + 1))
    
    for letter in word:
        outFile.write(chr(letter))
    
    if not (banList.tell() == banListSize):
        outFile.write("\n")