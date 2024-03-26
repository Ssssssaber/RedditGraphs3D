using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotsGroup : MonoBehaviour
{
    public string groupName;
    public Material groupMaterial;

    public void SetParams(string groupName, Material material)
    {
        this.groupName = groupName;
        groupMaterial = material;
    }

}
