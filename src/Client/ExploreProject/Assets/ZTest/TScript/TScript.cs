using api;
using GameCore;
using System.Collections.Generic;
using UnityEngine;

public class TScript : MonoBehaviour
{
    public GameObject go;

	void Start ()
    {
        gameObject.SetChild(go);
    }

    private void Test1(CreatePlayerGC aa)
    {

    }
}

