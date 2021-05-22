using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortingVisualizer
{
    interface ISortEngine
    {
        // void DoWork(int[] theArray, Graphics G, int MaxVal);
        void NextStep();
        bool IsSorted();

        void ReDraw();
    }
}
