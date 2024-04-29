using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float slowSpeed, normalSpeed, sprintSpeed;
    [SerializeField] private float initialSensitivity = 40f;
    [SerializeField] private float currentSensitivity = 40f;
    private float currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        MouseSensitivityChanger.instance.SubscribeToValueChange(ChangeSensitivity);
    }

    private void ChangeSensitivity(float scale)
    {
        currentSensitivity = initialSensitivity * scale;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(1))
        {
            #if !UNITY_EDITOR && UNITY_WEBGL
                WebGLInput.captureAllKeyboardInput = true;
            #endif
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Rotation();
            Movement();
        }
        else
        {   
            #if !UNITY_EDITOR && UNITY_WEBGL
                WebGLInput.captureAllKeyboardInput = false;
            #endif
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Rotation()
    {
        Vector3 mouseInput = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        transform.Rotate(mouseInput * currentSensitivity * Time.deltaTime * 50);
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
    }

    private void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftAlt))
        {
            currentSpeed = slowSpeed;
        }
        else 
        {
            currentSpeed = normalSpeed;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            input.y = currentSpeed;
        }

        transform.Translate(input * currentSpeed * Time.deltaTime);
    }
}
