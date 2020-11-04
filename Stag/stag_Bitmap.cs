using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

        public async Task EmbedMessageAsync()
        {
            //call the embedding process from here
            if (Message != string.Empty)
                await Task.Run(embedMessageAsync); 
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

        private async Task embedMessageAsync()
        {
            BitArray charBits; 
            EmbedTracker embedTracker = new EmbedTracker(0, 0, orgBitmap.Width, orgBitmap.Height, numBitsUse);
            newBitmap = orgBitmap; 
            var numEncoded = 0;

            //encode start of message flag 
            for (int i = 0; i < msgStart.Length; i++)
            {
                char current = msgStart.ElementAt(i);
                charBits = charToBitArray(current); 
                encode(charBits, embedTracker, 8); 
            }

            //encode the number of bits used per pixel for encoding -- this will be used for decoding (will only take up 4 bits no matter what as 8 is max)
            string sBits = Convert.ToString(numBitsUse, 2);
            int[] bits = sBits.PadLeft(4, '0')
                              .Select(x => int.Parse(x.ToString()))
                              .ToArray();
            BitArray arrayNumUsed = new BitArray(4);
            encode(arrayNumUsed, embedTracker, 4); 

            //encode actual message 
            for (int k = 0; k < Message.Length; k++)
            {
                char current = Message.ElementAt(k);
                BitArray charArray = charToBitArray(current); 
                encode(charArray, embedTracker, 8); 
            }

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

            //Byte charByte = Convert.ToByte(targetChar);  //get the byte version of the char
            //BitArray charBits = new BitArray(new Byte[] { charByte });  //convert the byte char into a binary array
            int numEncoded = 0;
            Byte r, g, b; 
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

        private void store(BitArray targetArray, BitArray sourceArray, EmbedTracker tracker, int numUsed)
        {
            //if they select 3 bits to use
            //on the last pass -- would only need to use 2 bits ( 3, 6, 9)
            int numToUse = numBitsUse;
            if ((numUsed + numToUse) > 8)
            {
                numToUse = (numToUse % 8) - 1; 
            }
            for (int j = 0; j < numToUse; j++)
            {
                targetArray[j] = sourceArray[j];
                tracker.incrementNextTarget(); 
                numUsed++; 
            }
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
    }
}
