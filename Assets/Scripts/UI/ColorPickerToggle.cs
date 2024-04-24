using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorPickerToggle : MonoBehaviour, IGameUI
{
    private Button colorPickButton;
    private DotGroup dotGroup; 
    private Image buttonBackground;

    private UnityAction<Color> colorChangeAction;

    private void Awake()
    {
        colorPickButton = GetComponent<Button>();
        buttonBackground = GetComponent<Image>();
        colorPickButton.onClick.AddListener(ColorPickerToggleAction);
    }

    public void SetupColorPickerToggle(UnityAction<Color> colorChangeAction, Color initialColor)
    {
        this.colorChangeAction = colorChangeAction;
        buttonBackground.color = initialColor;
    }

    public void SetDotGroup(DotGroup dotGroup)
    {
        this.dotGroup = dotGroup;
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

    public void SetNewGroupColor(Color color)
    {
        buttonBackground.color = color;
        colorChangeAction?.Invoke(color);
    }

    private void ColorPickerToggleAction()
    {
        ColorPickerSingletone.Instance.EnableColorPicker(this);
    }
}
