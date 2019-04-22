using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POI : MonoBehaviour
{
    public Vector3 position;

    void Start()
    {
        position = transform.position;
    }
}
