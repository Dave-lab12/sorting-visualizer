using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortingVisualizer
{
    class SortEngineMoveToBack : ISortEngine
    {
        private int[] theArray;
        private Graphics g;
        private int MaxVal;
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        private int CurrentListPointer = 0;
        public SortEngineMoveToBack(int[] theArray_In, Graphics g_In, int MaxVal_In)
        {
            theArray = theArray_In;
            g = g_In;
            MaxVal = MaxVal_In;
        }

        public void NextStep()
        {
            if (CurrentListPointer >= theArray.Count() - 1) CurrentListPointer = 0;
            if(theArray[CurrentListPointer] > theArray[CurrentListPointer + 1])
            {
                Rotate(CurrentListPointer);
            }
            CurrentListPointer++;
           

        }

        private void Rotate(int currentListPointer)
        {
            int temp = theArray[CurrentListPointer];
            int EndPoint = theArray.Count() - 1;

            for(int i = CurrentListPointer; i < EndPoint; i++)
            {
                theArray[i] = theArray[i + 1];
                DrawBar(i, theArray[i]);
            }

            theArray[EndPoint] = temp;
            DrawBar(EndPoint, theArray[EndPoint]);
        }

        private void DrawBar(int position, int height)
        {
            g.FillRectangle(BlackBrush, position, 0, 1, MaxVal);
            g.FillRectangle(WhiteBrush, position, MaxVal - theArray[position], 1, MaxVal);

        }

        public bool IsSorted()
        {
            for (int i = 0; i < theArray.Count() - 1; i++)
            {
                if (theArray[i] > theArray[i + 1]) return false;

            }
            return true;
        }
        public void ReDraw()
        {
            for (int i = 0; i < (theArray.Count() - 1); i++)
            {
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i, MaxVal - theArray[i], 1, MaxVal);
            }
        }
    }
}
