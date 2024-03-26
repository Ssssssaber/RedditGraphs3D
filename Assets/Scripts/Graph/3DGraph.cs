using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph3D : MonoBehaviour
{
    [SerializeField] private GameObject circle;
    [SerializeField] private List<Material> materials;
    // [SerializeField] private List<Transform> groups;
    [SerializeField] private Dictionary<string, DotsGroup> dotGroups = new Dictionary<string, DotsGroup>();
    // [SerializeField] private Dictionary<string, Material
    [SerializeField] private DotsGroup dotsGroupPrefab;
    private Transform graphOrigin;
    private Transform AxisX;
    private Transform AxisY;
    private Transform AxisZ;

    private Vector3 GraphDimensions;
    private List<DotInfo> globalDots = new List<DotInfo>();

    private void Start()
    {
        graphOrigin = transform.Find("GraphOrigin").GetComponent<Transform>();
        AxisX = transform.Find("AxisX").GetComponent<Transform>();
        AxisY = transform.Find("AxisY").GetComponent<Transform>();
        AxisZ = transform.Find("AxisZ").GetComponent<Transform>();
        GraphDimensions.x = AxisX.position.x;
        GraphDimensions.y = AxisX.position.y;
        GraphDimensions.z = AxisX.position.z;

        
    }

    public void ToggleGroup(string groupName)
    {
        // DotGroup group = (DotGroup) DotGroup.Parse(typeof(DotGroup), name);
        // Debug.Log(group.ToString() + groups[(int)group].gameObject.activeSelf.ToString());
        dotGroups[groupName].gameObject.SetActive(!dotGroups[groupName].gameObject.activeSelf);
    }

    private void DestroyChildren()
    {
        foreach (var group in dotGroups.Values)
        {
            foreach (Transform child in group.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
    }

    private void SetLabels(List<DotInfo> dots)
    {
        
    }

    public void GenerateData()
    {
        globalDots.Clear();
        DestroyChildren();

        GenerateDotsGlobal(10, new Vector3(0, 0, 0), new Vector3(10, 10, 10), "Red");
        GenerateDotsGlobal(15, new Vector3(20, 20, 20), new Vector3(40, 40, 40), "Green");
        GenerateDotsGlobal(15, new Vector3(10, 10, 10), new Vector3(20, 20, 0), "Blue");
        
        ShowGraph(globalDots);
    }

    private void GenerateDotsGlobal(int count, Vector3 min, Vector3 max, string groupName)
    {
        if (!dotGroups.ContainsKey(groupName))
        {
            CreateNewDotGroup(groupName);
        }
        for (int i = 0; i < count; i++)
        {
            Vector3 dotPosition = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
            
            globalDots.Add(new DotInfo(dotPosition, dotGroups[groupName]));
        }   
    }

    private void CreateNewDotGroup(string groupName)
    {
        GameObject newGroupOrigin = Instantiate(new GameObject($"{groupName} origin"), graphOrigin.transform.position, Quaternion.identity, graphOrigin.transform);
        DotsGroup group = newGroupOrigin.AddComponent<DotsGroup>();
        Material dotMaterial = new Material(Shader.Find("Standard"))
        {
            color = RandomColor()
        };
        group.SetParams(groupName, dotMaterial);
        
        dotGroups.Add(groupName, group);
    }

    private Color32 RandomColor(float min = 0f, float max = 1f)
    {
        return new Color(
            Random.Range(min, max), 
            Random.Range(min, max), 
            Random.Range(min, max)
        );
    }

    public void Plot(Vector3[] dots, string groupName)
    {
        if (!dotGroups.ContainsKey(groupName))
        {
            CreateNewDotGroup(groupName);
        }
        else
        {
            foreach(Vector3 coords in dots)
            {
                DrawDot(new DotInfo(coords, dotGroups[groupName]));
            }
        }
    }

    private void ShowGraph(double[][] values)
    {
        foreach(var coords in values)
        {
            
        }
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
