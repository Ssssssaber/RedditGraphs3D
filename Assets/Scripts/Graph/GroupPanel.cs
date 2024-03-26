using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GroupPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text groupNameLabel;
    [SerializeField] private TMP_InputField scaleInputField;
    [SerializeField] private Toggle visibilityToggle;
    [SerializeField] public ColorPicker colorPicker;

    public void Setup(string groupName, UnityAction<string> inputFieldAction, UnityAction<bool> toggleAction, Color color)
    {
        SetGroupName(groupName);
        SetInputFieldAction(inputFieldAction);
        SetToggleAction(toggleAction);
        colorPicker.SetButtonColor(color);
        
    }


    public void SetGroupName(string groupName)
    {
        groupNameLabel.text = groupName;
    }
    public void SetInputFieldAction(UnityAction<string> action)
    {
        scaleInputField.onValueChanged.AddListener(action);
    }

    public void SetToggleAction(UnityAction<bool> action)
    {
        visibilityToggle.onValueChanged.AddListener(action);        
    }
}
