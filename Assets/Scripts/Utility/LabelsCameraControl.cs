using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LabelsCameraControl : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    private Transform labelsContainer; 
    private void Awake()
    {
        if (mainCamera == null) mainCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
        labelsContainer = transform.Find("AxisLabels");
        // labelsContainer = transform;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < labelsContainer.childCount; i++)
        {
            // labelsContainer.GetChild(i).LookAt(mainCamera.transform.position);
            LookAtCamera(labelsContainer.GetChild(i));
        }  
    }


    private void LookAtCamera(Transform label)
    {
        // label.LookAt(mainCamera.transform.position)
//         label.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
// mainCamera.transform.rotation * Vector3.up);;
        label.rotation = Quaternion.RotateTowards(mainCamera.rotation, label.rotation, 0);
        // label.rotation = mainCamera.rotation;

        // var currentScale = label.localScale;
        // label.localScale = new Vector3(-1 * currentScale.x, currentScale.y, currentScale.z);
    }
}
