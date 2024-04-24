using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notifier : MonoBehaviour
{
    public static Notifier instance {private set; get;}
    [SerializeField] private RectTransform notificationList;
    [SerializeField] private GameObject notificationGameObject;
    private GameObject notificationsList;
    
    private void Awake()
    {
        notificationsList = transform.Find("NotificationPanel").gameObject;
        if (instance == null)
        {
            instance = this;
            notificationsList.SetActive(false);
        }
        else 
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CreateNotificaton(string notificationText)
    {
        GameObject notification = Instantiate(notificationGameObject, notificationList);
        TMP_Text text = notification.GetComponentInChildren<TMP_Text>();
        text.text = $"{System.DateTime.Now.ToString("[HH:mm:ss]")} Notification : {notificationText}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
