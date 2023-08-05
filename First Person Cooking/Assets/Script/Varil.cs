using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Varil : MonoBehaviour
{
    public GameObject verilcekObje;
    
    public float maxObj;
    private float x;

    public GameObject ObjeyiVer()
    {
        if (maxObj > x)
        {
            x++;
            return Instantiate(verilcekObje, transform.position, Quaternion.identity);
        }
        
        return null;
    }
}
