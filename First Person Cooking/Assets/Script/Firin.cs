using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Pisirilebilir pisirilebilir))
            pisirilebilir.PisirmeStart();
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Pisirilebilir pisirilebilir))
            pisirilebilir.PisirmeEnd();
    }
}
