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
        
    }

    public void SetActiveButton(string buttonName, bool active)
    {
        buttons[buttonName].interactable = active;
    }

}
