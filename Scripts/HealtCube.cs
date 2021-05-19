using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtCube : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
