using System.Collections;
using System.Collections.Generic;
using Accord.Statistics.Kernels;
using UnityEngine;


// public enum DotGroup
// {
//     Red,
//     Green,
//     Blue
// }
public class DotInfo
{
    public readonly Vector3 position;
    public readonly DotsGroup group;
    public readonly Material material;


    public DotInfo(Vector3 position, DotsGroup group)
    {
        this.position = position;
        this.group = group;
        this.material = group.groupMaterial;
    }

    // public DotInfo(Vector3 position, string group)
    // {
    //     this.position = position;
    //     this.group = group;
    // }

    // public DotInfo(Vector3 position, string group, Material material)
    // {
    //     this.position = position;
    //     this.group = group;
    //     this.material = material;
    // }
}

