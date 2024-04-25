using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetScalePanel : MonoBehaviour
{
    public static SetScalePanel instance {private set; get;}
    
    [SerializeField] private Scrollbar scaleScrollbar;
    [SerializeField] private TMP_InputField currentScaleInput;
    [SerializeField] private TMP_InputField minScaleInput;
    [SerializeField] private TMP_InputField maxScaleInput;
    public float currentScale 
    {
        get { return currentScale; }
        set 
        {
            _currentScale = value;
            currentScaleInput.text = value.ToString();
        }
    }
    private float _currentScale = 1f;

    public float minScale 
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
    private ColorPickerToggle currentGroupToggle;
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
        minScaleInput.onValueChanged.AddListener(SetMinScale);
        maxScaleInput.onValueChanged.AddListener(SetMaxScale);
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


    public void EnableScalePanel(ColorPickerToggle groupToggle)
    {
        if (currentGroupToggle != null || ColorPickerSingletone.Instance.gameObject.activeSelf)
        {
            Notifier.instance.CreateNotificaton("You did not complete previous action");
        }
        currentGroupToggle = groupToggle;
        gameObject.SetActive(true);
    }

    
    public void DisableScalePanel()
    {
        currentGroupToggle = null;
        gameObject.SetActive(false);
    }
}
