using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using Unity.VisualScripting;
using System.Linq;

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
    public int id;
    public string text;

    public RedditComment()
    {

    }
    public RedditComment(int id, string text)
    {
        this.id = id;
        this.text = text;
    }
    public RedditComment(string id, string text)
    {
        this.id = Int32.Parse(id);
        this.text = text;
    }
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
public class WebRequestDownloader : MonoBehaviour
{
    public static WebRequestDownloader instance {private set; get;}
    [SerializeField] private string targetURL;
    [SerializeField] private List<RedditComment> downloadedComments;
    private CoroutinesQueue coroutines;
    public int desiredCommentCount;
    public int downloadedCommentsCount => downloadedComments.Count;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        coroutines = GetComponent<CoroutinesQueue>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // DownloadAllComments();
    }

    public List<RedditComment> GetComments()
    {
        return downloadedComments;
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
            EventManager.OnCommentDownloadEnd?.Invoke();
            StopCoroutine("UpdateLoadingBar");
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
        EventManager.OnCommentsDownloadStart?.Invoke();
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
                    // Debug.Log(pages[page] + ":\nReceived: " + request.downloadHandler.text);
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
                        // StartCoroutine("UpdateLoadingBar");
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


                // float up = WebRequestDownloader.instance.downloadedCommentsCount; 
                // float down = WebRequestDownloader.instance.desiredCommentCount;
                // float res = up / down;
                // LoadingPanel.instance.SetLoadingScale(res);
                HandleDownload(comment);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null);
        }
    }
}
