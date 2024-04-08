using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    public static Vector3 RandomPointBetweenTwoVectors(Vector3 minCoords, Vector3 maxCoords)
    {
        return new Vector3(
            Random.Range(minCoords.x, maxCoords.x),
            Random.Range(minCoords.y, maxCoords.y),
            Random.Range(minCoords.z, maxCoords.z)
        );
    }

    public static Vector3[] GenerateDotsForGraph(Vector3 minCoords, Vector3 maxCoords, int dotsCount)
    {
        Vector3[] dots = new Vector3[dotsCount];
        for (int i = 0; i < dotsCount; i++)
        {
            dots[i] = RandomPointBetweenTwoVectors(minCoords, maxCoords);            
        }  
        return dots;
    }


    public static IEnumerator GenerateDotsWithDelay(Vector3 minCoords, Vector3 maxCoords, int dotsCount, float delayInSeconds)
    {
        int dotsGenerated = 0;
        while (dotsGenerated < dotsCount)
        {
            // EventManager.OnDotDataReceived?.Invoke(RandomPointBetweenTwoVectors(minCoords, maxCoords));
            dotsGenerated++;
        }
        yield return new WaitForSeconds(delayInSeconds);
    }

    public static DotInitialInfo GetDotsDataFromJSON(string json)
    {
        DotInitialInfo dotInfo = new DotInitialInfo();
        try 
        {
            string groupString = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(json)["label"];
            dotInfo.groupName = groupString;
            string batchString = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(json)["point"];
            float[] point = Newtonsoft.Json.JsonConvert.DeserializeObject<float[]>(batchString);
            dotInfo.coords = new Vector3(point[0], point[1], point[2]);
            Debug.Log(dotInfo.ToString());
            
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        return dotInfo;
    }

    
}
