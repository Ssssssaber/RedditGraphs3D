using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using Accord;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    [SerializeField] private MySqlConnector databaseConnector;
    [SerializeField] private Graph3D graph;
    private string[] comments;
    private double[][] resultBOW;
    private double[][] resultTFIDF;

    // Start is called before the first frame update
    void Start()
    {
        LoadComments();
    }

    private void LoadComments()
    {
        databaseConnector.OpenConneciton();
        comments = databaseConnector.ReadComments().ToArray();
    }

    private void PerformTFIDF()
    {
        resultTFIDF = TextAnalysis.TextTFIDF(comments);

        // graph.plot
    }

    private void PerformBOW()
    {
        TextAnalysis.TextTFIDF(comments);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
