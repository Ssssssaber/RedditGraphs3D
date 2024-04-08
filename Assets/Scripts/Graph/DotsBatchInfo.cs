using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DotInitialInfo {
    public string groupName;
    public Vector3 coords;

    public override string ToString()
    {
        return $"{groupName} - {coords}";
    }
}