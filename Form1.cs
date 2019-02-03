using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DotGrapher
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // private members
        private List<double> xset = new List<double>();
        private List<double> yset = new List<double>();
        private double x;
        private double y;
        private string fileName;

        // public members

        // addYButton should actually be named addXYButton but UI changes made it difficult to do so
        private void addYButton_Click(object sender, EventArgs e)
        {
            // generate history

            x = Convert.ToDouble(xTextBox.Text);
            y = Convert.ToDouble(yTextBox.Text);
            xset.Add(x);
            yset.Add(y);

            xLabel3.Text = xTextBox.Text;
            yLabel3.Text = yTextBox.Text;

            // generate graph
            this.chart.Series["series"].Points.AddXY(x, y);

            // generate min/max of yset
            var min = yset.Min();
            var max = yset.Max();
            minLabel2.Text = Convert.ToString(min);
            maxLabel2.Text = Convert.ToString(max);

        }

        // see full history
        private void fullHistoryButton_Click(object sender, EventArgs e)
        {
            string xvalues = string.Join(Environment.NewLine, xset);
            string yvalues = string.Join(Environment.NewLine, yset);
            MessageBox.Show("x value: \n" + xvalues + "\n ------------------- \n" + "y values: \n" + yvalues);
        }

        private void saveAsButton_Click(object sender, EventArgs e)
        {
            fileName = saveAsTextBox.Text;
            fileName += ".txt";

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                // write xset to file
                for (int i = 0; i < xset.Count; i++)
                {
                    sw.WriteLine(xset[i]);
                }

                // insert delimiter
                sw.WriteLine("eoxs"); // eoxs = end of x set

                // write yset to file
                for (int j = 0; j < yset.Count; j++)
                {
                    sw.WriteLine(yset[j]);
                }
            }

            MessageBox.Show("Successfully Saved!");
        }
    }
}
