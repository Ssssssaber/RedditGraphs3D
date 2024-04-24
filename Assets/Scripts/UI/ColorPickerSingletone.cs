using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ColorPickerSingletone : MonoBehaviour
{
    public static ColorPickerSingletone Instance {get; private set;}
    [SerializeField] private RectTransform cursor;
    [SerializeField] private Image cursorColor;
    [SerializeField] private Texture2D colorChart;
    private ColorPickerToggle currentGroupToggle;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DisableColorPicker();
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public void StartDrag(BaseEventData data)
    {
        if (currentGroupToggle == null) 
        {
            Debug.Log("KEKE");
            return;
        }
        PickColor(data);
    }

    public void Drag(BaseEventData data)
    {
        PickColor(data);
    }

    public void EndDrag(BaseEventData data)
    {
        DisableColorPicker();
    }

    private Color PickColor(BaseEventData data)
    {
        PointerEventData pointer = data as PointerEventData;
        cursor.position = pointer.position;

        Color pickedColor = colorChart.GetPixel((int)(cursor.localPosition.x * (colorChart.width / transform.GetChild(0).GetComponent<RectTransform>().rect.width)), (int)(cursor.localPosition.y * (colorChart.height / transform.GetChild(0).GetComponent<RectTransform>().rect.height)));
        currentGroupToggle.SetNewGroupColor(pickedColor);
        cursorColor.color = pickedColor;
        // Debug.Log("picking color");
        return pickedColor;
    }

    public void EnableColorPicker(ColorPickerToggle groupToggle)
    {
        if (currentGroupToggle != null)
        {
            Notifier.instance.CreateNotificaton("You did not chose color for previous group");
        }
        currentGroupToggle = groupToggle;
        gameObject.SetActive(true);
    }

    
    public void DisableColorPicker()
    {
        currentGroupToggle = null;
        gameObject.SetActive(false);
    }
}
