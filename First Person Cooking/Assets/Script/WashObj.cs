using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashObj : MonoBehaviour
{
    public float washTime;
    private float x;
    
    public bool isWashing;
    
    private DishWasher dishWasher;
    
    public void WashStart(GameObject obj)
    {
        isWashing = true;
        x = washTime;
        dishWasher = obj.GetComponent<DishWasher>();
    }

    private void Update()
    {
        if (!isWashing) return;
        x -= Time.deltaTime;

        if (!(x <= 0)) return;
        if(TryGetComponent(out Tabak tabak)) tabak.state = Tabak.State.Clear;
        if (TryGetComponent(out MeshRenderer meshRenderer)) meshRenderer.material.color = new Color(1, 1, 1);
        isWashing = false;
    }

    public void WashEnd()
    {
        try
        {
            isWashing = false;
            dishWasher.washObjects.Remove(gameObject);
        }
        catch (Exception e)
        {
            print("A");
        }
    }
}
