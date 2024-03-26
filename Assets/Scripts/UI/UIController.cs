using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance {private set; get; }
    [SerializeField] private Button buttonBOW;
    [SerializeField] private Button buttonTFIDF;
    private Dictionary<string, Button> buttons = new Dictionary<string, Button>();
    
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
    }

    private void Start()
    {
        if (buttonBOW == null)
        {
            buttonBOW = transform.Find("Panel").Find("ButtonBOW").GetComponent<Button>();
        }
        if (buttonTFIDF == null)
        {
            buttonTFIDF = transform.Find("Panel").Find("ButtonTFIDF").GetComponent<Button>();
        }
        buttons.Add("BOW", buttonBOW);
        buttons.Add("TFIDF", buttonTFIDF);
    }

    public void SetActiveButton(string buttonName, bool active)
    {
        buttons[buttonName].interactable = active;
    }

}
