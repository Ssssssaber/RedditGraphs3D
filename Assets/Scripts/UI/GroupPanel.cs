using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GroupPanel : MonoBehaviour, IGameUI
{
    // TODO: 
    //add changing group name : event manager
    [SerializeField] private TMP_Text groupNameLabel;
    [SerializeField] private TMP_InputField sizeScaleInput;
    [SerializeField] private TMP_InputField positionScaleInput;
    [SerializeField] private Toggle visibilityToggle;
    [SerializeField] public ColorPickerToggle colorPickerToggle;
    
    public Action OnAllGroupDotsLoaded;
    public void Setup(string groupName, UnityAction<string> sizeScaleInputAction, 
        UnityAction<string> positionScaleInputAction, UnityAction<bool> toggleAction,
        UnityAction<Color> colorChangeAction, Color color)
    {
        SetGroupName(groupName);
        SetSizeScaleInputAction(sizeScaleInputAction);
        SetPositionScaleInputAction(positionScaleInputAction);
        SetToggleAction(toggleAction);
        colorPickerToggle.SetupColorPickerToggle(colorChangeAction, color);
        // colorPickerToggle.SetButtonColor(color);
        
    }

    public void DisableInteraction()
    {
        sizeScaleInput.interactable = false;
        positionScaleInput.interactable = false;
        visibilityToggle.interactable = false;
        colorPickerToggle.DisableInteraction();
    }

    public void EnableInteraction()
    {
        sizeScaleInput.interactable = true;
        positionScaleInput.interactable = true;
        visibilityToggle.interactable = true;
        colorPickerToggle.EnableInteraction();
    }


    public void SetGroupName(string groupName)
    {
        groupNameLabel.text = groupName;
    }

    public void SetSizeScaleInputAction(UnityAction<string> action)
    {
        sizeScaleInput.onValueChanged.AddListener(action);
    }

    public void SetPositionScaleInputAction(UnityAction<string> action)
    {
        positionScaleInput.onValueChanged.AddListener(action);
    }

    public void SetToggleAction(UnityAction<bool> action)
    {        
        visibilityToggle.onValueChanged.AddListener(action);        
    }
}
