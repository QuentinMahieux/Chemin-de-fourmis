using System;
using UnityEngine;

public class ChangeLayer : MonoBehaviour
{
    public KeyCode inputKey =  KeyCode.Space;
    
    [Header("Background")]
    public GameObject backgroundGround;
    public GameObject backgroundUnderGround;

    private void Start()
    {
        backgroundGround.SetActive(true);
        backgroundUnderGround.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputKey))
        {
            backgroundGround.SetActive(!backgroundGround.activeSelf);
            backgroundUnderGround.SetActive(!backgroundUnderGround.activeSelf);
        }
    }
}
