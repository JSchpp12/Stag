﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stag
{
    class stag_Bitmap
    {
        public int MaxMsgSize = 0;
        public string Message = string.Empty;
        public Bitmap newBitmap;
        private Bitmap orgBitmap;
        private int numPixelsNeededPerChar = 0;
        private int numBitsUse;
        private const string msgStart = "!*";
        private const string msgEnd = "*!";

        public stag_Bitmap(Bitmap image, int inNumBitsUse)
        {
            orgBitmap = image;
            numBitsUse = inNumBitsUse;
            calculateSizes();
        }

        public stag_Bitmap(Bitmap image)
        {
            orgBitmap = image;
        }

        private void calculateSizes()
        {
            int totalNumPixels = orgBitmap.Width * orgBitmap.Height;
            int bitsPerPixel = numBitsUse * 3;
            int numPixelsNeeded = (int)Math.Ceiling(Decimal.Divide(8, bitsPerPixel));
            decimal decNumPixelsNeeded = Decimal.Divide(8, bitsPerPixel);
            decimal msgSize = Decimal.Divide(totalNumPixels, decNumPixelsNeeded);
            //float msgSize = totalNumPixels / numPixelsNeeded; 
            decimal floored = Math.Floor(msgSize);
            MaxMsgSize = (int)floored - 5;
        }

        //read the message out of the target file 
        public async Task decodeMessageAsync()
        {
            Debug.WriteLine("Beginning Decode");
            int numBitsUsedToEncode;
            int numBitsUsed = 0;
            bool testComplete = false;

            //need to figure out how many bits are used....
            for (int i = 1; i <= 8; i++)
            {
                EmbedTracker testTracker = new EmbedTracker(0, 0, orgBitmap.Width, orgBitmap.Height, i);
                for (int j = 0; j < msgStart.Length; j++)
                {
                    var currentChar = Decode(i, testTracker);
                    if (currentChar == msgStart[j] && j == msgStart.Length - 1)
                    {
                        testComplete = true;
                        numBitsUsed = i;
                    }
                }
                if (testComplete)
                {
                    break;
                }
            }
            if (!testComplete)
            {
                Exception ex = new Exception("Unable to decode message from image");
                throw ex;
            }
        }

        public async Task embedMessageAsync()
        {
            BitArray charBits;
            EmbedTracker embedTracker = new EmbedTracker(0, 0, orgBitmap.Width, orgBitmap.Height, numBitsUse);
            newBitmap = orgBitmap;

            Debug.WriteLine("Embedding beginning of message values");
            //encode start of message flag 
            for (int i = 0; i < msgStart.Length; i++)
            {
                char current = msgStart.ElementAt(i);
                charBits = charToBitArray(current);
                encode(charBits, embedTracker, 8);
            }

            //encode the number of bits used per pixel for encoding -- this will be used for decoding (will only take up 4 bits no matter what as 8 is max)
            //string sBits = Convert.ToString(numBitsUse, 2);
            //int[] bits = sBits.PadLeft(4, '0')
            //                  .Select(x => int.Parse(x.ToString()))
            //                  .ToArray();
            //BitArray arrayNumUsed = new BitArray(4);
            //encode(arrayNumUsed, embedTracker, 4);

            Debug.WriteLine("Embedding message");
            //encode actual message 
            try
            {
                var messageBytes = stringToBytes(Message);
                for (int k = 0; k < messageBytes.Length; k++)
                {
                    //char current = Message.ElementAt(k);
                    //BitArray charArray = charToBitArray(current);
                    BitArray charArray = new BitArray(messageBytes[k]);
                    encode(charArray, embedTracker, 8);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


            Debug.WriteLine("Embedding end of message values");
            //encode end of message flag 
            for (int m = 0; m < msgEnd.Length; m++)
            {
                char current = msgEnd.ElementAt(m);
                charBits = charToBitArray(current);
                encode(charBits, embedTracker, 8);
            }
        }

        private void encode(BitArray encodeSource, EmbedTracker currentTracker, int size)
        {
            int numEncoded = 0;
            Byte r, g, b;
            try
            {
                while (numEncoded < size)
                {
                    int processingX = currentTracker.currX;
                    int processingY = currentTracker.currY;
                    var currPixel = orgBitmap.GetPixel(processingX, processingY);
                    BitArray currentByte = new BitArray(8);
                    switch (currentTracker.nxtColorVal)
                    {
                        case 0:
                            //set the red pixel
                            currentByte = new BitArray(new byte[] { currPixel.R });
                            store(currentByte, encodeSource, currentTracker, numEncoded);
                            r = ConvertToByte(currentByte);
                            currPixel = Color.FromArgb(r, currPixel.G, currPixel.B);
                            newBitmap.SetPixel(processingX, processingY, currPixel);
                            break;
                        case 1:
                            //set the green pixel
                            currentByte = new BitArray(new byte[] { currPixel.G });
                            store(currentByte, encodeSource, currentTracker, numEncoded);
                            g = ConvertToByte(currentByte);
                            currPixel = Color.FromArgb(currPixel.R, g, currPixel.B);
                            newBitmap.SetPixel(processingX, processingY, currPixel);
                            break;
                        case 2:
                            //set the blue pixel
                            currentByte = new BitArray(new byte[] { currPixel.B });
                            store(currentByte, encodeSource, currentTracker, numEncoded);
                            b = ConvertToByte(currentByte);
                            currPixel = Color.FromArgb(currPixel.R, currPixel.G, b);
                            newBitmap.SetPixel(processingX, processingY, currPixel);
                            break;
                    }
                    numEncoded += numBitsUse;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        //store the information in the source array into the target array
        private void store(BitArray targetArray, BitArray sourceArray, EmbedTracker tracker, int numUsed)
        {
            //if they select 3 bits to use
            //on the last pass -- would only need to use 2 bits ( 3, 6, 9)
            int numToUse = numBitsUse;
            try
            {
                if ((numUsed + numToUse) > 8)
                {
                    numToUse = ((numToUse + numUsed) % 8) + 1;
                }
                for (int j = 0; j < numToUse; j++)
                {
                    targetArray[j] = sourceArray[numUsed];
                    tracker.incrementNextTarget();
                    numUsed++;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        //test the number passed into this method against the image. Returns true if the number of bits tested returns a valid message header
        private char Decode(int numBitsUse, EmbedTracker tracker)
        {
            Byte r, g, b;
            int numRead = 0;
            BitArray dataByte = new BitArray(8);

            try
            {
                //try and read for the first character of the start of message which should be at the beginning of the image
                while (numRead < 8)
                {
                    int processingX = tracker.currX;
                    int processingY = tracker.currY;
                    var currPixel = orgBitmap.GetPixel(processingX, processingY);
                    BitArray currentByte = new BitArray(8);

                    switch (tracker.nxtColorVal)
                    {
                        case 0:
                            currentByte = new BitArray(new byte[] { currPixel.R });
                            break;
                        case 1:
                            currentByte = new BitArray(new byte[] { currPixel.G });
                            break;
                        case 2:
                            currentByte = new BitArray(new byte[] { currPixel.B });
                            break;
                    }
                    //this is not correct might be an encoding issue
                    for (int i = 0; i < numBitsUse; i++)
                    {
                        dataByte[numRead] = currentByte[i];
                        numRead++;
                    }
                    tracker.incrementNextTarget();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


            return (char)ConvertToByte(dataByte);
        }

        private byte ConvertToByte(BitArray bits)
        {
            if (bits.Count != 8)
            {
                throw new ArgumentException("bits");
            }
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }

        private BitArray charToBitArray(char source)
        {
            Byte charByte = Convert.ToByte(source);
            BitArray charBits = new BitArray(new Byte[] { charByte });
            return charBits;
        }

        private Byte[] stringToBytes(string sourceString)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(sourceString);
        }
    }
}
