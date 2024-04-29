using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SetScalePanel : MonoBehaviour
{
    public static SetScalePanel instance {private set; get;}
    
    [SerializeField] private Scrollbar scaleScrollbar;
    [SerializeField] private TMP_InputField currentScaleInput;
    [SerializeField] private TMP_InputField minScaleInput;
    [SerializeField] private TMP_InputField maxScaleInput;
    [SerializeField] private Button closeButton;
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
    private float _maxScale = 3f;
    private float persentage;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            closeButton.onClick.AddListener(DisableScalePanel);
            DisableScalePanel();
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

    private void ResetPanel()
    {
        minScaleInput.text = "";
        maxScaleInput.text = "";
        currentScaleInput.text = "";
        scaleScrollbar.value = 0;
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

    public void EnableScalePanel(UnityAction<float> onScaleChanged)
    {
        if (onCurrentScaleChanged != null || ColorPickerSingletone.Instance.gameObject.activeSelf)
        {
            Notifier.instance.CreateNotificaton("You did not complete previous action");
        }
        onCurrentScaleChanged = onScaleChanged;
        gameObject.SetActive(true);
    }
    
    public void DisableScalePanel()
    {
        onCurrentScaleChanged = null;
        ResetPanel();
        gameObject.SetActive(false);
    }
}
