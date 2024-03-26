using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor.PackageManager;
using UnityEngine.Networking;

[Serializable]
public class CommentData
{
    public RedditComment redditComment;
    public DownloadError error;

    public CommentData()
    {
        redditComment = new RedditComment();
        error = new DownloadError("", false);
    }

}
[Serializable]
public class RedditComment
{
    public readonly int id;
    public readonly string text;
}
[Serializable]
public class DownloadError
{
    public string errorText;
    public bool isError;

    public DownloadError(string errorText, bool isError)
    {
        this.errorText = errorText;
        this.isError = isError;
    }
}

public enum RequestType
{
    Download,
    DownloadAll,
    CheckCardsCount
}

[RequireComponent(typeof(CoroutinesQueue))]
public class CommentsDownloader : MonoBehaviour
{
    [SerializeField] private string targetURL;
    [SerializeField] private List<RedditComment> downloadedComments;
    private CoroutinesQueue coroutines;
    private int desiredCommentCount;

    private void Awake()
    {
        coroutines = GetComponent<CoroutinesQueue>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        DownloadAllComments();
    }

    public static CommentData GetCardData(string data)
    {
        try
        {
            return JsonUtility.FromJson<CommentData>(data);
        }
        catch (Exception)
        {
            throw new Exception("Invalid JSON file");
        }
    }

    public string SetCardData(CommentData comment)
    {
        return JsonUtility.ToJson(comment);
    }

    private void HandleDownload(RedditComment comment)
    {
        // EventManager.CardLoaded(downloadedCardStats);
        if (!downloadedComments.Contains(comment))
        {
            downloadedComments.Add(comment);
        }

        if (downloadedComments.Count == desiredCommentCount)
        {
            // EventManager.AllCardsLoaded(LoadedCards);
        }
    }

    public void CheckCardsCount()
    {
        // StopAllCoroutines();
        WWWForm form = new WWWForm();
        form.AddField("request-type", RequestType.CheckCardsCount.ToString());
        coroutines.Enqueue(SendRequest(form, RequestType.CheckCardsCount));
    }

    public void DownloadAllComments()
    {
        WWWForm form = new WWWForm();
        form.AddField("request-type", RequestType.DownloadAll.ToString());
        coroutines.Enqueue(SendRequest(form, RequestType.DownloadAll));
    }

    public void EnqueueDownloads()
    {
        for (int i = 1; i <= desiredCommentCount; i++)
        {
            DownloadComment(i);
        }
    }

    public void DownloadComment(int id)
    {
        WWWForm form = new WWWForm();
        form.AddField("request-type", RequestType.Download.ToString());
        form.AddField("id", id);
        coroutines.Enqueue(SendRequest(form, RequestType.Download));
    }
    

    IEnumerator SendRequest(WWWForm form, RequestType requestType)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(targetURL, form))
        {
            request.SetRequestHeader("Access-Control-Allow-Origin" , "*");
            
            yield return request.SendWebRequest();

            string[] pages = targetURL.Split('/');
            int page = pages.Length - 1;

            // Check if request was succesfull
            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + request.error);
                    // go.text = pages[page] + ": Error: " + request.error;
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + request.error);
                    // go.text = pages[page] + ": HTTP Error: " + request.error;
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + request.downloadHandler.text);
                    // go.text = pages[page] + ":\nReceived: " + webRequest.downloadHandler.text;
                    HandleRequest(request, requestType);
                    break;
            }
        }
    }

    private void HandleRequest(UnityWebRequest request, RequestType requestType)
    {
        switch (requestType)
        {
            case (RequestType.CheckCardsCount):
                desiredCommentCount = Int32.Parse(request.downloadHandler.text);
                break;
            case (RequestType.DownloadAll):
                desiredCommentCount = Int32.Parse(request.downloadHandler.text);
                EnqueueDownloads();
                break;
            case (RequestType.Download):
                CommentData temp = GetCardData(request.downloadHandler.text);
                if (temp.error.isError)
                {
                    Debug.Log(temp.error.errorText);
                    break;
                }
                RedditComment comment = temp.redditComment;
                HandleDownload(comment);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null);
        }
    }
}
