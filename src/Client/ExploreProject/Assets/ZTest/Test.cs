using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
    public GameObject Go;
    public GameObject Go2;

    public bool IsTest = false;

    private GameObject go;

    // Use this for initialization
    void Start()
    {
        byte b = 129;
        Debug.LogError(b);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTest) {
            IsTest = false;
            Debug.LogError(go is GameObject);

            Debug.LogError(transform.FindChild("aa"));
        }
    }
}
