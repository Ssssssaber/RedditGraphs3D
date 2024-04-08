using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph3D : MonoBehaviour
{
    [SerializeField] private GameObject circle;
    [SerializeField] private List<Material> materials;
    // [SerializeField] private List<Transform> groups;
    [SerializeField] private Dictionary<string, DotGroup> dotGroups = new Dictionary<string, DotGroup>();
    [SerializeField] private Transform panelForGroups;
    [SerializeField] private GroupPanel groupPanelPrefab;
    private Transform graphOrigin;
    private Transform AxisX;
    private Transform AxisY;
    private Transform AxisZ;

    private Vector3 GraphDimensions;
    private List<DotInfo> globalDots = new List<DotInfo>();

    private void Awake()
    {
        graphOrigin = transform.Find("GraphOrigin").GetComponent<Transform>();
        AxisX = transform.Find("AxisX").GetComponent<Transform>();
        AxisY = transform.Find("AxisY").GetComponent<Transform>();
        AxisZ = transform.Find("AxisZ").GetComponent<Transform>();
        // TODO: create appropriate monob script for graph axes

        GraphDimensions.x = AxisX.position.x;
        GraphDimensions.y = AxisX.position.y;
        GraphDimensions.z = AxisX.position.z;

        
    }

    public void ImitateDotsLoad()
    {
        
        StartCoroutine(GenerateDotBatchesWithDelay(
            new Vector3(-10, -10, - 10), 
            new Vector3(10, 10, 10), 
            200, 
            20,
            "1", 
            0.5f
        ));
    }

    public IEnumerator GenerateDotsWithDelay(Vector3 minCoords, Vector3 maxCoords, int dotsCount, string groupName, float delayInSeconds)
    {
        
        int dotsGenerated = 0;
        DotGroup group = CreateNewDotGroup(groupName);
        group.BeginAddingNewDots();
        while (dotsGenerated < dotsCount)
        {
            DrawDot(new DotInfo(this, Utils.RandomPointBetweenTwoVectors(minCoords, maxCoords), group));
            dotsGenerated++;
            yield return new WaitForSeconds(delayInSeconds);
        }
        group.EndAddingNewDots();
    }

    public IEnumerator GenerateDotBatchesWithDelay(Vector3 minCoords, Vector3 maxCoords, int dotsCount, int batchLength, string groupName, float delayInSeconds)
    {
        int dotsGenerated = 0;
        DotGroup group = CreateNewDotGroup(groupName);
        group.BeginAddingNewDots();
        while (dotsGenerated <= dotsCount)
        {
            DotInfo[] dotBatch = new DotInfo[batchLength];
            for (int i = 0; i < batchLength; i++)
            {
                dotBatch[i] = new DotInfo(this, Utils.RandomPointBetweenTwoVectors(minCoords, maxCoords), group);
            }
            Plot(dotBatch, false);
            dotsGenerated += batchLength;
            yield return new WaitForSeconds(delayInSeconds);
        }
        group.EndAddingNewDots();
    }
    
    public void GenerateData()
    {
        globalDots.Clear();

        GenerateDotsGlobal(10, new Vector3(0, 0, 0), new Vector3(10, 10, 10), "Group 1");
        GenerateDotsGlobal(15, new Vector3(20, 20, 20), new Vector3(40, 40, 40), "Group 2");
        GenerateDotsGlobal(15, new Vector3(10, 10, 10), new Vector3(20, 20, 0), "Group 3");
        
        // ShowGraph(globalDots);
    }

    private void GenerateDotsGlobal(int count, Vector3 min, Vector3 max, string groupName, bool replace = true)
    {
        Vector3[] dots = new Vector3[count];
        for (int i = 0; i < count; i++)
        {
            dots[i] = Utils.RandomPointBetweenTwoVectors(min, max);
            
            
        }   
        Plot(dots, groupName);
        dotGroups[groupName].EndAddingNewDots();
    }

    public DotGroup CreateNewDotGroup(string groupName, bool recievingFromFrontend = false)
    {   
        GameObject newGroupOrigin = Instantiate(new GameObject($"{groupName} origin"), graphOrigin.transform.position, Quaternion.identity, graphOrigin.transform);
        DotGroup group = newGroupOrigin.AddComponent<DotGroup>();
        Material dotMaterial = new Material(Shader.Find("Standard"))
        {
            color = RandomColor()
        };
        group.SetParams(groupName, dotMaterial);
        dotGroups.Add(groupName, group);

        GroupPanel panel = Instantiate(groupPanelPrefab, panelForGroups);
        group.AttachPanel(panel);
        
        if (recievingFromFrontend) group.SubscribeToStopRecieveingData();

        return group;
    }

    private Color32 RandomColor(float min = 0f, float max = 1f)
    {
        return new Color(
            Random.Range(min, max), 
            Random.Range(min, max), 
            Random.Range(min, max)
        );
    }

    public void Plot(Vector3[] dots, string groupName, bool replace = true)
    {
        if (!dotGroups.ContainsKey(groupName))
        {
            CreateNewDotGroup(groupName);
        }
        
        if (replace)
        {
            foreach (Transform child in dotGroups[groupName].transform)
            {
                Destroy(child.gameObject);
            }
        }

        foreach(Vector3 coords in dots)
        {
            DrawDot(new DotInfo(this, coords, dotGroups[groupName]));
        }
    }

    public void Plot(DotInfo[] dots, bool replace = true)
    {
        string groupName = dots[0].group.groupName;

        if (!dotGroups.ContainsKey(groupName))
        {
            CreateNewDotGroup(groupName);
        }
        
        if (replace)
        {
            foreach (Transform child in dotGroups[groupName].transform)
            {
                Destroy(child.gameObject);
            }
        }

        foreach(DotInfo dot in dots)
        {
            DrawDot(dot);
        }
    }

    public void Plot(Vector3 dot, string groupName, bool replace = true, bool recievingFromFrontend = false)
    {
        if (!dotGroups.ContainsKey(groupName))
        {
            CreateNewDotGroup(groupName, recievingFromFrontend);
        }
        
        if (replace)
        {
            foreach (Transform child in dotGroups[groupName].transform)
            {
                Destroy(child.gameObject);
            }
        }
        DrawDot(new DotInfo(this, dot, dotGroups[groupName]));
    }
    
    /* TODO: 
        - automatically create groups
        - automatically create materials
        - automatically change size of axes
        - save shown data: create scrollable list in ui with an ability to turn on and off groups
        - strategy design pattern for different text analysis techniques
        - add labels to axes
    */

    private void ShowGraph(List<DotInfo> values)
    {
        foreach(var dot in values)
        {
            DrawDot(dot);
        }
    }


    private void DrawDot(DotInfo info)
    {
        GameObject dot = Instantiate(circle);
        dot.transform.SetParent(dotGroups[info.group.groupName].transform);
        dot.transform.position = info.position;
        dot.GetComponent<Renderer>().material = info.group.groupMaterial; 
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
