using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public UnityEvent<Color> ColorPickerEvent = new UnityEvent<Color>();
    [SerializeField] Button colorPickButton;
    [SerializeField] Texture2D colorChart;
    [SerializeField] RectTransform cursor;
    [SerializeField] Image button;
    [SerializeField] Image cursorColor;

    private void Start()
    {
        colorPickButton = GetComponent<Button>();
    }
    public void DisableInteraction()
    {
        // TODO: alco need to disable child gameobjects
        colorPickButton.interactable = false;
    }
    public void EnableInteraction()
    {
        colorPickButton.interactable = true;
    }
    public void EndDrag(BaseEventData data)
    {
        Color color = PickColor(data);
        ColorPickerEvent.Invoke(color);
    }

    public void StartDrag(BaseEventData data)
    {
        PickColor(data);
    }

    public void Drag(BaseEventData data)
    {
        PickColor(data);
    }

    private Color PickColor(BaseEventData data)
    {
        PointerEventData pointer = data as PointerEventData;
        cursor.position = pointer.position;
        Color pickedColor = colorChart.GetPixel((int)((cursor.localPosition.x) * (colorChart.width / transform.GetChild(0).GetComponent<RectTransform>().rect.width)), (int)(cursor.localPosition.y * ((colorChart.height) / transform.GetChild(0).GetComponent<RectTransform>().rect.height)));
        button.color = pickedColor;
        cursorColor.color = pickedColor;
        return pickedColor;
    }

    public void SetButtonColor(Color color)
    {
        button.color = color;
    }




}
