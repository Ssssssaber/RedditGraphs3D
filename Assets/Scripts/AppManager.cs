using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using Accord;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    public bool UseWebRequest = true;
    public static Graph3D activeGraph {private set; get;}

    [SerializeField]
    private WebRequestDownloader databaseConnector;
    [SerializeField] private Graph3D graph;
    [SerializeField] private RedditComment[] comments;
    [SerializeField] private string[] commentsText;
    [SerializeField] private Vector3[] resultBOW;
    [SerializeField] private Vector3[] resultTFIDF;
    [SerializeField] private TMP_Text notificationText;
    private bool commentsLoaded = false;
    private LoadingPanel loadingPanel;
    private void Awake()
    {
        loadingPanel = LoadingPanel.instance;
        activeGraph = graph;        
        
        EventManager.OnDotDataReceived.AddListener(PlotNewDot);

        EventManager.OnStartReceivingData.AddListener(RecetRecivingNewData);
        EventManager.OnStartReceivingData.AddListener(NotifyReceivingNewData);
        EventManager.OnStopReceivingData.AddListener(NotifyStopReceivingNewData);
    }
    void Start()
    {
        DeactivateBOW();
        DeactivateTFIDF();
    }

    private bool reciveNewData = true;

    public void NotifyReceivingNewData()
    {
        Notifier.instance.CreateNotificaton("Started receiving new data");
    }
    public void NotifyStopReceivingNewData()
    {
        Notifier.instance.CreateNotificaton("Stopped receiving new data");
    }
    public void RecetRecivingNewData()
    {
        reciveNewData = true;
    }

    private void PlotNewDot(DotInitialInfo dotInitialInfo)
    {
        graph.Plot(dotInitialInfo.coords, dotInitialInfo.groupName, reciveNewData, true);
        reciveNewData = false;
    }

    private void ShowLoading()
    {
        loadingPanel.SetActive(true);
    }
    
    private void HideLoading()
    {
        loadingPanel.SetActive(false);
    }

    private void NotifyCommentsLoaded()
    {
        Notifier.instance.CreateNotificaton($"{comments.Length} comments loaded");
    }

    private void ActivateBOW()
    {
        UIController.instance.SetActiveButton("BOW", true);
    }

    private void ActivateTFIDF()
    {
        UIController.instance.SetActiveButton("TFIDF", true);
    }

    private void DeactivateBOW()
    {
        UIController.instance.SetActiveButton("BOW", false);
    }

    private void DeactivateTFIDF()
    {
        UIController.instance.SetActiveButton("TFIDF", false);
    }

    private void GetComments()
    {
        comments = databaseConnector.GetComments().ToArray();
        commentsText = comments.Select(comment => comment.text).ToArray();
        AllowDataAnalysis();
    }

    private void AllowDataAnalysis()
    {
        commentsLoaded = true;
        Debug.Log("Comments loaded");
    }

    public void PerformTFIDF()
    {
        resultTFIDF = AnalyseText(commentsText, 10, TextAnalysis.AnalysisTFIDF, "TFIDF");
    }

    public void PerformBOW()
    {
        resultBOW = AnalyseText(commentsText, 100, TextAnalysis.AnalysisBOW, "BOW");
    }
    
    private Vector3[] AnalyseText(string[] comments, int amount, TextAnalysisMethod method, string operationName)
    {
        if (amount < 0 || !commentsLoaded)
        { 
            Debug.Log("bullshit prikaz");
            return new Vector3[0];
        }
        amount = amount > comments.Length ? comments.Length : amount;
        ArraySegment<string> commentsSlice = new ArraySegment<string>(comments, 0, amount);
        double[][] result = method(commentsSlice.ToArray());
        Vector3[] vectors = Utils.DoubleArrayToVectors(result);
        graph.Plot(vectors, operationName);

        return vectors;
    }
}

