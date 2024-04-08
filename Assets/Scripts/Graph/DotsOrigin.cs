using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotGroup : MonoBehaviour
{
    // TODO: ALLOW DYNAMICALLY CHANGE SCALE
    public string groupName {get; private set;}
    public Material groupMaterial {get; private set;}
    private GroupPanel panelUI;

    private List<Vector3> initialPositions = new List<Vector3>();
    private void Awake()
    {
    }

    public void SubscribeToStopRecieveingData()
    {
        EventManager.OnStopReceivingData.AddListener(EndAddingNewDots);
    }

    public void BeginAddingNewDots()
    {
        panelUI.DisableInteraction();
    }
    public void EndAddingNewDots()
    {
        panelUI.EnableInteraction();
        SetInitialPos();
    }

    private void ChangeGroupColor(Color color)
    {
        groupMaterial.color = color;
    }

    private void SetInitialPos()
    {
        initialPositions = new List<Vector3>();
        for (int i = 0; i < transform.childCount; i++)
        {
            initialPositions.Add(transform.GetChild(i).transform.position);
        }
    }

    public void AttachPanel(GroupPanel group)
    {
        panelUI = group;
        panelUI.Setup(groupName, ChangeScale, ToggleGroup, groupMaterial.color);
        panelUI.colorPicker.ColorPickerEvent.AddListener(ChangeGroupColor);
        BeginAddingNewDots();
    }

    public void SetParams(string groupName, Material material)
    {
        this.groupName = groupName;
        groupMaterial = material;
    }

    public void ToggleGroup(bool value)
    {
        gameObject.SetActive(value);
    }

    public void ChangeScale(string inputScale)
    {
        if (float.TryParse(inputScale, out float scale)) 
        {
            ChangeScale(scale);
        }
    }

    public void ChangeScale(float newScale)
    {
        var ini = initialPositions;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.position = ini[i] * newScale;
        }
    }

}
