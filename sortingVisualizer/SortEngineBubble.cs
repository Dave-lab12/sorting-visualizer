using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace sortingVisualizer
{
    class SortEngineBubble : ISortEngine
    {
       
        private int[] theArray;
        private Graphics g;
        private int MaxVal;
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        public SortEngineBubble(int[] theArray_In, Graphics g_In, int MaxVal_In)
        {
            theArray = theArray_In;
            g = g_In;
            MaxVal = MaxVal_In;
        }

        public void NextStep()
        {
           
            for(int i = 0; i < theArray.Count() - 1; i++)
            {
                if (theArray[i] > theArray[i + 1])
                {
                    Swap(i, i + 1);
                }
            }
            
        }

        private void Swap(int i, int p)
        {
            int temp = theArray[i];
            theArray[i] = theArray[i + 1];
            theArray[i + 1] = temp;

            DrawBar(i, theArray[i]);
            DrawBar(p, theArray[p]);
        }

        private void DrawBar(int position ,int height)
        {
            g.FillRectangle(BlackBrush, position, 0, 1, MaxVal);
            g.FillRectangle(WhiteBrush, position, MaxVal - theArray[position],1,MaxVal);

        }

        public bool IsSorted()
        {
            for(int i = 0;i< theArray.Count() - 1; i++)
            {
                if (theArray[i] > theArray[i + 1]) return false;

            }
            return true;
        }
        public void ReDraw()
        {
            for(int i = 0; i < (theArray.Count() - 1); i++)
            {
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i, MaxVal - theArray[i], 1, MaxVal);
            }
        }
    }
}
