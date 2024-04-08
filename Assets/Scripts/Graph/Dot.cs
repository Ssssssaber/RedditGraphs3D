using System;
using UnityEngine;


[Serializable]
public class DotInfo
{
    public readonly Graph3D graphReference;
    public readonly Vector3 position;
    public readonly DotGroup group;
    public readonly Material material;
    

    public DotInfo(Graph3D graphReference, Vector3 position, DotGroup group)
    {
        this.graphReference = graphReference;
        this.position = position;
        this.group = group;
        this.material = group.groupMaterial;
    }

    public DotInfo(Graph3D graphReference, Vector3 position, string groupName)
    {
        this.graphReference = graphReference;
        this.position = position;
        DotGroup group = graphReference.CreateNewDotGroup(groupName);
        this.group = group;
        this.material = group.groupMaterial;
    }
}

