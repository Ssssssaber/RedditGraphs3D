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
// add master panel that will change scale of all circles and set global scale
// make graph length scalable 
// double check if recieve data works correct
public class AppManager : MonoBehaviour
{
    public bool UseWebRequest = true;
    public static Graph3D activeGraph {private set; get;}

    [SerializeField]
    private WebRequestDownloader databaseConnector;
    [SerializeField] private Graph3D graph;
    [SerializeField] private RedditComment[] comments;
    [SerializeField] private string[] commentsText;
    [SerializeField] private TMP_Text notificationText;
    private bool commentsLoaded = false;
    private LoadingPanel loadingPanel;
    private void Awake()
    {
        loadingPanel = LoadingPanel.instance;
        activeGraph = graph;        
        
        EventManager.OnDotDataReceived.AddListener(PlotNewDot);
        EventManager.OnStartReceivingData.AddListener(ResetRecivingNewData);

        EventManager.OnStartReceivingData.AddListener(NotifyReceivingNewData);
        EventManager.OnStopReceivingData.AddListener(NotifyStopReceivingNewData);
    }
    void Start()
    {
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
    public void ResetRecivingNewData()
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
}

