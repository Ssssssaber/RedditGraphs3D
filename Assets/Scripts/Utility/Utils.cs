using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3[] DoubleArrayToVectors(double[][] doubles)
    {
        Vector3[] vectors = new Vector3[doubles.Length];
        for (int i = 0; i < doubles.Length; i++)
        {
            vectors[i] = new Vector3((float)doubles[i][0], (float)doubles[i][1], (float)doubles[i][2]);
        }
        return vectors;
    }
}
