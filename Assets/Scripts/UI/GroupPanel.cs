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
    [SerializeField] private TMP_Text sizeScale;
    [SerializeField] private TMP_Text positionScale;
    [SerializeField] private Button changeSizeScaleButton;
    [SerializeField] private Button changePositionScaleButton;
    [SerializeField] private Toggle visibilityToggle;
    [SerializeField] private ColorPickerToggle colorPickerToggle;
    
    private UnityAction<float> onSizeScaleChanged;
    private UnityAction<float> onPositionScaleChanged;

    public Action OnAllGroupDotsLoaded;

    private void Awake()
    {
        changeSizeScaleButton.onClick.AddListener(SizeScaleButton);
        changePositionScaleButton.onClick.AddListener(PositionScaleButton);
        onSizeScaleChanged += UpdateSizeText;
        onPositionScaleChanged += UpdatePositionText;
    }

    public void Setup(string groupName, UnityAction<float> onSizeScaleChanged, 
        UnityAction<float> onPositionScaleChanged, UnityAction<bool> toggleAction,
        UnityAction<Color> colorChangeAction, Color color)
    {
        SetGroupName(groupName);
        SetToggleAction(toggleAction);
        this.onSizeScaleChanged += onSizeScaleChanged;
        this.onPositionScaleChanged += onPositionScaleChanged;
        colorPickerToggle.SetupColorPickerToggle(colorChangeAction, color);        
    }

    public void DisableInteraction()
    {
        changePositionScaleButton.interactable = false;
        changeSizeScaleButton.interactable = false;
        visibilityToggle.interactable = false;
        colorPickerToggle.DisableInteraction();
    }

    public void EnableInteraction()
    {
        changePositionScaleButton.interactable = true;
        changeSizeScaleButton.interactable = true;
        visibilityToggle.interactable = true;
        colorPickerToggle.EnableInteraction();
    }


    private void SetGroupName(string groupName)
    {
        groupNameLabel.text = groupName;
    }

    private void UpdateSizeText(float scale)
    {
        sizeScale.text = $"Current: {Math.Round(scale, 2)}";
    }

    private void UpdatePositionText(float scale)
    {
        positionScale.text = $"Current: {Math.Round(scale, 2)}";
    }

    private void SizeScaleButton()
    {
        SetScalePanel.instance.EnableScalePanel(onSizeScaleChanged);
    }

    private void PositionScaleButton()
    {
        
        SetScalePanel.instance.EnableScalePanel(onPositionScaleChanged);
    }

    public void SetToggleAction(UnityAction<bool> action)
    {        
        visibilityToggle.onValueChanged.AddListener(action);        
    }
}
