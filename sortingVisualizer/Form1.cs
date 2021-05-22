using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sortingVisualizer
{
    public partial class Form1 : Form
    {
        int[] theArray;
        Graphics g;
        BackgroundWorker bgw = null;
        bool Paused = false;

        public Form1()
        {
            InitializeComponent();
            PopulateDropdown();
        }

        private void PopulateDropdown()
        {
            List<string> ClassList = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(ISortEngine).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.Name).ToList();
            ClassList.Sort();
            foreach(string entry in ClassList)
            {
                comboBox1.Items.Add(entry);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnSort_Click(object sender, EventArgs e)
        {
            if (theArray == null) BtnReset_Click(null, null);
            bgw = new BackgroundWorker();
            bgw.WorkerSupportsCancellation = true;
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.RunWorkerAsync(argument: comboBox1.SelectedItem);
        }
        private void BtnPause_Click(object sender, EventArgs e)
        {
            if (!Paused)
            {
                bgw.CancelAsync();
                Paused = true;
            }
            else
            {
                if (bgw.IsBusy) return;
                int NumEntries = panel1.Width;
                int MaxVal = panel1.Height;
                Paused = false;
                for(int i = 0; i < NumEntries; i++)
                {
                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), i,0,1, MaxVal);
                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i, MaxVal - theArray[i], 1, MaxVal);
                }
                bgw.RunWorkerAsync(argument: comboBox1.SelectedItem);
            }
        }
        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            g = panel1.CreateGraphics();
            int NumEntries = panel1.Width;
            int MaxVal = panel1.Height;
            theArray = new int[NumEntries];
            g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), 0, 0, NumEntries, MaxVal);
            Random rand = new Random();
            for(int i = 0; i < NumEntries; i++)
            {
                theArray[i] = rand.Next(0, MaxVal);
            }
            for(int i = 0; i < NumEntries; i++)
            {
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i, MaxVal - theArray[i], 1, MaxVal);

            }
        }

        #region BackGroundStuff

        public void bgw_DoWork(object sender,System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            string SortEngineName = (string)e.Argument;
            Type type = Type.GetType("sortingVisualizer." + SortEngineName);
            var ctors = type.GetConstructors();
            try
            {
                ISortEngine se = (ISortEngine)ctors[0].Invoke(new object[] { theArray, g, panel1.Height });
                while(!se.IsSorted() && (!bgw.CancellationPending))
                {
                    se.NextStep();
                }
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #endregion

      
    }
}
