using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform target; 
    
    private void Awake()
    {
        // if (mainCamera == null) mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform.position);
    }
}
