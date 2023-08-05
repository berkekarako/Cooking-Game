using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Serialization;

public class Tabak : MonoBehaviour
{
    
    [Serializable] public enum State
    {
        Clear,
        Dirty
    }
    
    public State state = State.Clear;
    
    public Transform foodPosition;
    private bool _placeIsFull;
    private GameObject _carryObj;

    public void PlaceObject(GameObject obj)
    {
        if (obj.GetComponent<İnteractable>().tabakaKoyulabilir)
        {
            if (!_placeIsFull && state == State.Clear)
            {
                _placeIsFull = true;
            
                obj.gameObject.GetComponent<Collider>().enabled = true;
                obj.gameObject.GetComponent<Collider>().isTrigger = true;
                obj.GetComponent<Rigidbody>().isKinematic = true;
            
                obj.GetComponent<İnteractable>().TabakinIcinde = true;
                _carryObj = obj;
                obj.transform.rotation = transform.rotation;
                obj.transform.position = foodPosition.position;
                obj.transform.parent = transform;
            }

            else
            {
                obj.gameObject.GetComponent<Collider>().enabled = false;
                obj.gameObject.GetComponent<Collider>().isTrigger = true;
                obj.GetComponent<Rigidbody>().isKinematic = true;
                GameObject.FindWithTag("Player").GetComponent<InteractiveObject>().ObjeBrrakalcakmı = false;
            }
        }
        else
        {
            GameObject.FindWithTag("Player").GetComponent<InteractiveObject>().ObjeBrrakalcakmı = false;
            obj.GetComponent<Collider>().enabled = true;
            obj.GetComponent<Collider>().isTrigger = true;
            obj.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    
    public void RemoveObject(GameObject obj)
    {
        _placeIsFull = false;
        obj.transform.parent = null;
        _carryObj = null;

        state = State.Dirty;
        if (TryGetComponent(out MeshRenderer meshRenderer)) meshRenderer.material.color = new Color(-1, -1, -1);
    }
    private void Update()
    {
        if (_carryObj == null) return;
        if(_carryObj.GetComponent<İnteractable>().TabakinIcinde) return; 
        RemoveObject(_carryObj);
    }
}
