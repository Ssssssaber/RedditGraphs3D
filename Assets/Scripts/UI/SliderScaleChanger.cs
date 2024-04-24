using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderScaleChanger : MonoBehaviour, IGameUI
{

    public float currentScale 
    {
        get { return currentScale; }
        set 
        {
            currentText.text = $"New scale: {value}";
            _currentScale = value;
        }
    }
    private float _currentScale = 1f;

    public float minScale 
    {
        get { return _minScale; }
        set 
        {
            minText.text = value.ToString();
            _minScale = value;
        }
    }

    private float _minScale = 0.1f;
    
    
    private float maxScale
   {
        get { return _maxScale; }
        set 
        {
            maxText.text = value.ToString();
            _maxScale = value;
        }
    }

    private float _maxScale = 3f;
    [SerializeField] private TMP_Text currentText;
    [SerializeField] private Scrollbar scaleScrollbar;
    [SerializeField] private TMP_InputField minScaleInput;
    [SerializeField] private TMP_InputField maxScaleInput;
    [SerializeField] private TMP_Text minText;
    [SerializeField] private TMP_Text maxText;

    public void DisableInteraction()
    {
        minScaleInput.interactable = false;
        maxScaleInput.interactable = false;
    }

    public void EnableInteraction()
    {
        minScaleInput.interactable = true;
        maxScaleInput.interactable = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetInputAction(minScaleInput, SetMinScale);
        SetInputAction(maxScaleInput, SetMaxScale);
        scaleScrollbar.onValueChanged.AddListener(SetCurrentScale);
    }

    private void SetInputAction(TMP_InputField inputField, UnityAction<string> action)
    {
        inputField.onValueChanged.AddListener(action);
    }

    private void SetScrollbarAction(UnityAction<float> action)
    {
        
    }

    private void SetCurrentScale(float scale) 
    {
        currentScale = scale;
    }
    

    public void SetMinScale(string inputScale)
    {
        if (float.TryParse(inputScale, out float scale)) 
        {
            minScale = scale;
        }
    }

    public void SetMaxScale(string inputScale)
    {
        if (float.TryParse(inputScale, out float scale)) 
        {
            maxScale = scale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
