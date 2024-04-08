using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontendConnector : MonoBehaviour
{
    public static bool isReveivingInformation = false;
    private void Start()
    {
        #if UNITY_EDITOR 
        
        keke();

        #endif
    }

    private void keke()
    {
        StartRecievingData();
        
        // string j = "{\"type\":\"point message\",\"point\":\"[-6.396451982923883e-07, 0.0296957291794697, 6.0352566615339555e-05]\",\"label\":\"0\"}";
        string[] js = new string[] {
            "{\"type\":\"point message\",\"point\":\"[-6.396451982923883e-07, 0.0296957291794697, 6.0352566615339555e-05]\",\"label\":\"0\"}",
            "{\"type\":\"point message\",\"point\":\"[6.396451982923883e-07, 0.0296957291794697, -6.0352566615339555e-05]\",\"label\":\"0\"}",
            "{\"type\":\"point message\",\"point\":\"[-6.396451982923883, 2.96957291794697, 6.0352566615339555]\",\"label\":\"0\"}",
            "{\"type\":\"point message\",\"point\":\"[6.396451982923883, 0.0296957291794697, 6.0352566615339555]\",\"label\":\"0\"}",
        };
        foreach (string dotJSON in js)
        {
            RecieveDotData(dotJSON);
        }
        StopRecievingData();

        StartRecievingData();
        js = new string[] {
            "{\"type\":\"point message\",\"point\":\"[0.396451982923883e-07, 0.0296957291794697, 6.0352566615339555e-05]\",\"label\":\"0\"}",
            "{\"type\":\"point message\",\"point\":\"[0.396451982923883e-07, 0.0296957291794697, -6.0352566615339555e-05]\",\"label\":\"0\"}",
            "{\"type\":\"point message\",\"point\":\"[0.396451982923883, 2.96957291794697, 6.0352566615339555]\",\"label\":\"0\"}",
            "{\"type\":\"point message\",\"point\":\"[0.396451982923883, 0.0296957291794697, 6.0352566615339555]\",\"label\":\"0\"}",
        };
        foreach (string dotJSON in js)
        {
            RecieveDotData(dotJSON);
        }
        StopRecievingData();
    }
    public void StartRecievingData()
    {
        isReveivingInformation = true;
        EventManager.OnStartReceivingData?.Invoke();
    }
    public void RecieveDotData(string dotJSON)
    {
        if (dotJSON == null)
        {
            Notifier.instance.CreateNotificaton("No data passed");
        }

        DotInitialInfo newDot = Utils.GetDotsDataFromJSON(dotJSON);

        EventManager.OnDotDataReceived?.Invoke(newDot);
    }
    public void StopRecievingData()
    {
        isReveivingInformation = false;
        EventManager.OnStopReceivingData?.Invoke();
    }
}
