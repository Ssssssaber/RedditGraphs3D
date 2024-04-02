using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotsGroup : MonoBehaviour
{
    public string groupName {get; private set;}
    public Material groupMaterial {get; private set;}
    public GroupPanel panelUI {get; private set;}
    private List<Vector3> initialPositions = new List<Vector3>();
    
    private void Start()
    {
        // const Material mat = new Material(); 
        SetInitialPos();
        panelUI.colorPicker.ColorPickerEvent.AddListener(ChangeGroupColor);
    }

    private void ChangeGroupColor(Color color)
    {
        groupMaterial.color = color;
    }

    private void SetInitialPos()
    {
        foreach (Transform child in transform)
        {
            initialPositions.Add(child.transform.position);
        }
    }

    public void AttachPanel(GroupPanel group)
    {
        panelUI = group;
        panelUI.Setup(groupName, ChangeScale, ToggleGroup, groupMaterial.color);
    }

    public void SetParams(string groupName, Material material)
    {
        this.groupName = groupName;
        groupMaterial = material;
    }

    public void ToggleGroup(bool value)
    {
        gameObject.SetActive(value);
        Debug.Log("eheehehe");
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
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.position = initialPositions[i] * newScale;
        }
    }

}
