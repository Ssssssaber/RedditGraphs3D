using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class RequestTest : MonoBehaviour
{
    TextMeshProUGUI go;
    void Start()
    {
        go = GameObject.Find("Debug").GetComponent<TextMeshProUGUI>();
        // A correct website page.
        StartCoroutine(GetRequest("http://localhost/phpscript/upload_download.php"));

        // // A non-existing page.
        // StartCoroutine(GetRequest("https://error.html"));
    }

    private void Keke()
    {
        // Dictionary<string, string> headers = new Dictionary<string,string>();
        // headers.Add("header-name", "header content");
        // WWW www = new WWW("https://example.com", null, headers);
        // yield return www;
        // Debug.Log (www.text);'
        // UnityWebRequest request = new UnityWebRequest("http://localhost/phpscript/upload_download.php", "POST", );
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
        //     webRequest.SetRequestHeader("Access-Control-Allow-Credentials" , "true");
        //     webRequest.SetRequestHeader("Access-Control-Allow-Headers" , "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
        //     webRequest.SetRequestHeader("Access-Control-Allow-Methods" , "GET, POST, OPTIONS");
            webRequest.SetRequestHeader("Access-Control-Allow-Origin" , "*");
            /*
            "Access-Control-Allow-Credentials": "true",
            "Access-Control-Allow-Headers": "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time",
            "Access-Control-Allow-Methods": "GET, POST, OPTIONS",
            "Access-Control-Allow-Origin": "*",
            */
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    go.text = pages[page] + ": Error: " + webRequest.error;
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    go.text = pages[page] + ": HTTP Error: " + webRequest.error;
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    go.text = pages[page] + ":\nReceived: " + webRequest.downloadHandler.text;
                    break;
            }
        }
    }
}
