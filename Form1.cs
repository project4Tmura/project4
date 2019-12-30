using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Empty the display table 
            tblErr.Rows.Clear();
            string[] a = {"1000000000000001", "11110101010101" };
            Error er = new Error();
            string [,] TwoDArr = er.getExistingErrorsAndHandler(a);
            addRow(TwoDArr, TwoDArr.GetLength(0));
        }

        private void addRow(string[,] arr, int rows)
        {
            lbl.Text = rows + " errors found";
            int RowCount;
            for (int i = 0; i < rows; i++)
            {
                RowCount = tblErr.RowCount;
                tblErr.Rows.Add();
                tblErr[0, RowCount].Value = i+1; // Num of error
                tblErr[1, RowCount].Value = arr[i,0]; // Error
                tblErr[2, RowCount].Value = arr[i,1]; // Handler
            }
           
        }
    }
}
