using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LoadingPanel : MonoBehaviour
{
    public static LoadingPanel instance {private set; get;}
   [SerializeField] private TMP_Text loadingComment;
   [SerializeField] private RectTransform mask;
    float originalSize;
    
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

        originalSize = mask.rect.width;
        
    }

    private void Start()
    {
        EventManager.OnCommentsDownloadStart.AddListener(StartLoadingBarUpdate);
        EventManager.OnCommentDownloadEnd.AddListener(StopLoadingBarUpdate);
        

    }

    private void StartLoadingBarUpdate()
    {
        this.SetActive(true);
        StartCoroutine("UpdateLoadingBar");
    }

    private void StopLoadingBarUpdate()
    {
        StopCoroutine("UpdateLoadingBar");
        this.SetActive(false);
    }

    private IEnumerator UpdateLoadingBar()
    {
        while (true)
        {
            SetLoadingScale(CountLoadingScale());
            yield return new WaitForSeconds(.2f);
        }
    }

    private float CountLoadingScale()
    {
        float up = WebRequestDownloader.instance.downloadedCommentsCount; 
        float down = WebRequestDownloader.instance.desiredCommentCount;
        return up / down;
    }

    public void SetLoadingComment(string text)
    {
        loadingComment.text = text;
    }

    public void SetLoadingScale(float value)
    {
        mask.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

    public void SetActive(bool visible)
    {
        gameObject.SetActive(visible);
    }
}
