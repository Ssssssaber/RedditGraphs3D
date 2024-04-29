using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWithToggle : MonoBehaviour
{
    [SerializeField] private Button toggleButton;
    [SerializeField] private GameObject objectToToggle;
    // Start is called before the first frame update
    void Start()
    {
        toggleButton.onClick.AddListener(ToggleGameObject);
    }

    private void ToggleGameObject()
    {
        objectToToggle.SetActive(!objectToToggle.activeSelf);
    }
}
