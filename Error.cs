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

    // The search class call to this function
    public string[] getErrors()
    {

        string sql = @"select e.errors
                       from Errors e";

        // Calling the function that returns Reader by query
        SqlDataReader rdr = ExecuteReader(sql);

        int count = 0;
        
        // Count num of rows
        while (rdr.Read())
        {
            count++;
        }
        
        sql = @"select e.errors
                from Errors e";

        // Calling the function that returns Reader by query
        rdr = ExecuteReader(sql);

        string[] array = new string[count];
        count = 0;

        // Passes the query result and puts it in the array
        while (rdr.Read())
        {
            array[count] = rdr[0].ToString();
            count++;
        }
        return array;
    }

    // The UI class call to this function
    public string[,] getExistingErrorsAndHandler(string[] arrExistingErrors)
    { 
        string sql = @"select e.errors, e.handler
                       from Errors e";

        // Calling the function that returns Reader by query
        SqlDataReader rdr = ExecuteReader(sql);

        // Dictionary of errors (key) + handler(value)
        IDictionary<string, string> dict = new Dictionary<string, string>();

        while (rdr.Read())
        {
            dict.Add(new KeyValuePair<string, string>(rdr[0].ToString(), rdr[1].ToString()));
        }

        bool flag; // Flag = existing error
        string key;

        // If no known errors exist in the current silicon
        if (arrExistingErrors.Length == 0)
        {
            for (int j = 0; j < dict.Count; j++)
            {
                key = dict.Keys.ElementAt(j);
                dict.Remove(key);
            }
            string[,] arr = new string[0,0];
            return arr;
        }
		/*
          Pass on the dictionary and delete any errors that don't exist in the current silicon.
          At the end of the transition,
          only the errors that exist in the current silicon will be left in the dictionary and their handler.
        */
        else
        { // arrExistingErrors.Length != 0
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
                    dict.Remove(key); // removes the key which is not in arrExistingErrors 
            }
        }
        string[,] arrExistingErrorsAndHandler = new string[dict.Count,2];
        int count = 0;
        
        // Copy the dictionary to a 2D array
        for (int j = 0; j < dict.Count; j++)
        {
            key = dict.Keys.ElementAt(j);
            string value = dict[key];
            arrExistingErrorsAndHandler[count,0] = key; // Key (error)
            arrExistingErrorsAndHandler[count, 1] = String.Copy(value); // Value (handler)
            count++;
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
        string conString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = H:\שנה ד\הנדסת תוכנה\project4\WindowsFormsApp1\errorsDB.mdf; Integrated Security = True; Connect Timeout = 30";

        SqlConnection con = new SqlConnection(conString);
        con.Open();

        SqlCommand com = new SqlCommand(sqlString, con);
        return com;
    }



}
