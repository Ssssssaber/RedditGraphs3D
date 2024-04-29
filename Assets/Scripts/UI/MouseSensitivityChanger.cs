using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MouseSensitivityChanger : MonoBehaviour
{
    public static MouseSensitivityChanger instance {private set; get;}
    
    [SerializeField] private Scrollbar scaleScrollbar;
    [SerializeField] private TMP_InputField currentScaleInput;
    [SerializeField] private TMP_InputField minScaleInput;
    [SerializeField] private TMP_InputField maxScaleInput;
    private float currentScale 
    {
        get { return currentScale; }
        set 
        {
            _currentScale = value;
            currentScaleInput.text = value.ToString();
            onCurrentScaleChanged?.Invoke(value);
        }
    }
    private UnityAction<float> onCurrentScaleChanged;
    private float _currentScale = 1f;

    private float minScale 
    {
        get { return _minScale; }
        set 
        {
            _minScale = value;
            UpdateScalePersentage();
        }
    }

    private float _minScale = 0.1f;
    private float maxScale
    {
        get { return _maxScale; }
        set 
        {
            _maxScale = value;
            UpdateScalePersentage();
        }
    }
    private float _maxScale = 2f;
    private float persentage;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // DisableScalePanel();
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        minScaleInput.onValueChanged.AddListener(SetMinScale);
        maxScaleInput.onValueChanged.AddListener(SetMaxScale);
        currentScaleInput.onValueChanged.AddListener(InputCurrentScaleAction);
        scaleScrollbar.onValueChanged.AddListener(CalculateCurrentScale);
        UpdateScalePersentage();
    }


    private void CalculateCurrentScale(float inputScale)
    {
        currentScale = minScale + persentage * inputScale * 100;
        // Debug.Log($"currentScale = {minScale} + {persentage} * {inputScale} = {currentScale};");
    }

    private void UpdateScalePersentage()
    {
        persentage = (maxScale - minScale) / 100;
        // Debug.Log($"{persentage} : {minScale + persentage * 100}");
    }

    private void SetMinScale(string inputScale)
    {
        if (float.TryParse(inputScale, out float scale)) 
        {
            minScale = scale;
        }
    }

    private void SetMaxScale(string inputScale)
    {
        if (float.TryParse(inputScale, out float scale)) 
        {
            maxScale = scale;
        }
    }

    private void InputCurrentScaleAction(string inputScale)
    {
        if (float.TryParse(inputScale, out float scale)) 
        {
            onCurrentScaleChanged?.Invoke(scale);
        }
    }

    public void SubscribeToValueChange(UnityAction<float> action)
    {
        onCurrentScaleChanged += action;
    }
    public void UnSubscribeToValueChange(UnityAction<float> action)
    {
        onCurrentScaleChanged -= action;
    }
}
