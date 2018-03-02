
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq.Expressions;
using System;

public class TriggerTest : MonoBehaviour
{

    void OnTriggerEnter(Collider collider)
    {
        print("Enter");
    }
    void OnTriggerExit(Collider collider)
    {
        print("out");
    }
}
