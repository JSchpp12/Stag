using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stag
{
    class EmbedTracker
    {
        public int currX, currY, nxtColorVal, nextBit;
        private int numBitsToUse; 
        private int maxX;
        private int maxY; 

        //nextColorVal: 
        // 0 - R
        // 1 - G
        // 2 - B
        public EmbedTracker(int startX, int startY, int inMaxX, int inMaxY, int inNumToUse)
        {
            currX = startX;
            currY = startY;
            maxX = inMaxX - 1;
            maxY = inMaxY - 1;
            nextBit = 0; 
            nxtColorVal = 0;
            numBitsToUse = inNumToUse; 
        }
        public void incrementNextTarget()
        {
            if (nxtColorVal < 2)
            {
                nxtColorVal++;
            }
            else
            {
                if (currX < maxX)
                {
                    currX++;
                }
                else
                {
                    currX = 0;
                    currY++;
                }
                nxtColorVal = 0; //reset to R
            }
        }

        public void incrementCoordinate()
        {
            if (currX < maxX)
            {
                currX++;
            }
            else
            {
                currY++;
                currX = 0; 
            }
        }
    }
}
