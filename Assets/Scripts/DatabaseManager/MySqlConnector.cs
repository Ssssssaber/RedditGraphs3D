using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using System.IO;
using MySql.Data.MySqlClient;
using System.Drawing;
using UnityEditor;
using TMPro;

public class MySqlConnector : MonoBehaviour
{
    string server = "localhost";
    string database = "reddit_parsing";
    string uid = "root";
    string password = "";
    MySqlConnection connection;
    
    public void OpenConneciton()
    {
        try
        {
            string connectionString = $"server={server};uid={uid};" +
    $"pwd={password};database={database}";
            connection = new MySqlConnection(connectionString);
            GameObject go = GameObject.Find("Debug");
            // go.GetComponent<TextMeshProUGUI>().text = "Opening Connection";
            connection.Open();
            
            // go.GetComponent<TextMeshProUGUI>().text = "Connection state: " + connection.State;
            Debug.Log("Connection state: " + connection.State);
        }
        catch (IOException ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    public List<string> ReadComments()
    {
        if (connection == null)
        {
            OpenConneciton();
        }

        List<string> comments = new List<string>(); 

        string query = "SELECT * FROM submission_comment";

        MySqlCommand MS_Command = new MySqlCommand(query, connection);

        MySqlDataReader MS_Reader = MS_Command.ExecuteReader();
        while (MS_Reader.Read() )
        {
            // Debug.Log(MS_Reader.GetString(3));    
            comments.Add(MS_Reader.GetString(3));
        }
        MS_Reader.Close();

        EventManager.OnCommentsLoaded?.Invoke();
        return comments;
    }

    private void OnApplicationQuit()
    {
        Debug.Log("killing con");
        if (connection != null)
        {
            if (connection.State.ToString() != "Closed")
                connection.Close();
            connection.Dispose();
        }
    }
}
