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

public class Error
{
    public Error()
    {
    }

    // Returns array of all errors from DB
    public string[] getErrors()
    {

        string sql = @"select e.errors
                       from Errors e";

        // Calling the function that returns Reader by query
        SqlDataReader rdr = ExecuteReader(sql);

        // Count =  length of array
        int count = 0;
        while (rdr.Read())
        {
            count++;
        }
        string[] errorsArray = new string[count];

        // Calling the function that returns Reader by query
        rdr = ExecuteReader(sql);

        count = 0;

        // Passes the query result and puts errors in the array
        while (rdr.Read())
        {
            errorsArray[count] = rdr[0].ToString();
            count++;
        }
        return errorsArray;
    }

    // The UI class call to this function
    public string[,] getExistingErrorsAndHandler(string[] arrExistingErrors)
    {
        string sql = @"select e.errors, e.handler
                       from Errors e";

        // Calling the function that returns Reader by query
        SqlDataReader rdr = ExecuteReader(sql);

        // Dictionary of errors (key) + handler (value)
        IDictionary<string, string> dict = new Dictionary<string, string>();

        // Passes the query result and puts errors and handler in the dictionary
        while (rdr.Read())
        {
            dict.Add(new KeyValuePair<string, string>(rdr[0].ToString(), rdr[1].ToString()));
        }

        bool flag; // Flag = existing error
        string key;
        
        /*
          Pass on the dictionary and delete any errors that don't exist in the current silicon.
          At the end of the transition,
          only the errors that exist in the current silicon will be left in the dictionary and their handler.
        */
         for (int j = 0; j < dict.Count; j++)
         {
            key = dict.Keys.ElementAt(j);
            flag = false;
            for (int i = 0; i < arrExistingErrors.Length; i++)
            {
                if (Equals(key, arrExistingErrors[i]))
                {
                    flag = true;
                    break;
                }
            }
            // This key (error) is not exist in the current silicon
            if (!flag)
            {
                dict.Remove(key); // removes the key which is not in arrExistingErrors 
                j--;
            }
         }
         string[,] arrExistingErrorsAndHandler = new string[dict.Count, 2];
        
        // Copy the dictionary to a 2D array
         for (int j = 0; j < dict.Count; j++)
         {
            key = dict.Keys.ElementAt(j);
            string value = dict[key];
            arrExistingErrorsAndHandler[j, 0] = key; // Key (error)
            arrExistingErrorsAndHandler[j, 1] = value; // Value (handler)
         }

         return arrExistingErrorsAndHandler;
    }

    // Returns Reader by query
    public SqlDataReader ExecuteReader(string sqlString)
    {
        SqlCommand com = CreateCommand(sqlString);
        SqlDataReader rdr = com.ExecuteReader();
        return rdr;
    }

    private SqlCommand CreateCommand(string sqlString)
    {
        string conString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\project4\WindowsFormsApp1\errorsDB.mdf; Integrated Security = True; Connect Timeout = 30";

        // Connects to DB and opens the connection
        SqlConnection con = new SqlConnection(conString);
        con.Open();
        // Send the query and the connection 
        SqlCommand com = new SqlCommand(sqlString, con);
        return com;
    }

}
