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
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    public partial class UI : Form
    {
        [DllImport(@"Aho_Corasick.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr creat_Automaton([In, Out] string[] array, int arraySize);

        [DllImport(@"Aho_Corasick.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr creat_search(IntPtr automaton);

        [DllImport(@"Aho_Corasick.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int do_searchWords(IntPtr search, [In, Out] string[] array, int arraySize, string text);


        public const int SILICON_MAX_SIZE = 2500;
        public const int SILICON_MIN_SIZE = 10;//NOTE: shoud be bigger just for simulating
        public UI()
        {
            InitializeComponent();
            // Empty the display table 
            tblErr.Rows.Clear();
            


            try {
                //reading and validating the string
                string[] lines = System.IO.File.ReadAllLines(@"silicons.txt");
            
                if (validateSilicon(lines)) { 
                    
                    string silicon = lines[0];//first line is the silicon

                    Error err = new Error();

                    //getting errors from db
                    string[] error = err.getErrors();
                    
                    //using dll functions
                    var automaton = creat_Automaton(error, error.Length);
                    var search = creat_search(automaton);
                    int a = do_searchWords(search, error, error.Length, silicon);


                    string[,] TwoDArr = err.getExistingErrorsAndHandler(error);
                    addRow(TwoDArr, TwoDArr.GetLength(0));
                }
            }
            catch (Exception e)
            {
                displayError(e.Message);
            }
        }

        // Adds rows to the table in the form
        private void addRow(string[,] arr, int rows)
        {
            lbl.Text = rows + " errors found";
            if (rows != 0)
            {
                int RowCount;
                for (int i = 0; i < rows; i++)
                {
                    RowCount = tblErr.RowCount;
                    tblErr.Rows.Add();
                    tblErr[0, RowCount].Value = i + 1; // Num of error
                    tblErr[1, RowCount].Value = arr[i, 0]; // Error
                    tblErr[2, RowCount].Value = arr[i, 1]; // Handler
                }
            }
            else
                tblErr.Hide();
        }

        private bool validateSilicon(string [] lines)
        {
     
            if (lines.Length == 0)
            {
                displayError("no silicons strings found");
                return false;
            }


            if (lines[0].Length > SILICON_MAX_SIZE || lines[0].Length < SILICON_MIN_SIZE)
            {
                displayError("invalid silicon");
                return false;
            }
            for (int i = 0; i < lines[0].Length; i++)
            {
                if (lines[0][i] != '0' && lines[0][i] != '1')
                {
                    displayError("invalid silicon - containing non binary chares");
                    return false;
                }
            }
            return true;
          

        }
        private void displayError(string msg)
        {
            lbl.Text = msg;
            tblErr.Hide();
        }

       }
    }

