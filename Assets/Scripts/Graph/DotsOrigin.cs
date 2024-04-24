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

    private List<Vector3> initialDotPositions = new List<Vector3>();
    private float initialDotScale = 3f;
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

    public void SetGroupColor(Color color)
    {
        groupMaterial.color = color;
    }

    private void SetInitialPos()
    {
        initialDotPositions = new List<Vector3>();
        for (int i = 0; i < transform.childCount; i++)
        {
            initialDotPositions.Add(transform.GetChild(i).transform.position);
        }
    }

    public void AttachPanel(GroupPanel group)
    {
        panelUI = group;
        panelUI.Setup(groupName, ChangeDotsSizeScale, ChangeDotsPositionScale, ToggleGroup, SetGroupColor, groupMaterial.color);
        // panelUI.colorPicker.ColorPickerEvent.AddListener(SetGroupColor);
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

    public void ChangeDotsPositionScale(string inputScale)
    {
        if (float.TryParse(inputScale, out float scale)) 
        {
            ChangeDotsPositionScale(scale);
        }
    }

    public void ChangeDotsPositionScale(float newScale)
    {
        var ini = initialDotPositions;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.position = ini[i] * newScale;
        }
    }

    public void ChangeDotsSizeScale(string inputScale)
    {
        if (float.TryParse(inputScale, out float scale)) 
        {
            ChangeDotsSizeScale(scale);
        }
    }

    public void ChangeDotsSizeScale(float newScale)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.localScale = new Vector3(1, 1, 1) * initialDotScale * newScale;
        }
    }

}
