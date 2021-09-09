using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    public Material Color;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<MeshRenderer>().material = Color;
        }
    }
}
