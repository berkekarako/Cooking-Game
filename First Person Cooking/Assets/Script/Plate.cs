using System;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    private CraftPlate craftPlate;
    
    [Serializable] public enum State
    {
        Clear,
        Dirty
    }
    
    public State state = State.Clear;
    
    public Transform foodPosition;
    private List<GameObject> _carryObjs = new List<GameObject>();

    private void Start()
    {
        craftPlate = FindObjectOfType<CraftPlate>();
    }

    public void PlaceObject(GameObject obj, InteractiveObject interactiveObject)
    {
        if (obj.GetComponent<Interactable>().tabakaKoyulabilir)
        {
            if (state == State.Clear)
            {
                interactiveObject.ObjectPut();

                obj.GetComponent<Interactable>().plate = this;
                
                _carryObjs.Add(obj);

                var craftObj = craftPlate.Check(_carryObjs);
                if (craftObj == null)
                {
                    obj.transform.rotation = transform.rotation;
                    obj.transform.position = foodPosition.position;
                    obj.transform.parent = transform;
                }
                else
                {
                    foreach (var t in _carryObjs)
                    {
                        Destroy(t);
                    }
                    
                    for (int i = 0; i < _carryObjs.Count + 1; i++)
                    {
                        _carryObjs.RemoveAt(0);
                        print(_carryObjs.Count);
                    }

                    craftObj = Instantiate(craftObj, transform.position, Quaternion.identity);

                    craftObj.transform.SetParent(transform);
                    
                    craftObj.GetComponent<Interactable>().plate = this;
                    
                    craftObj.GetComponent<Collider>().enabled = true;
                    craftObj.GetComponent<Collider>().isTrigger = true;
                    craftObj.GetComponent<Rigidbody>().isKinematic = true;
                    
                    _carryObjs.Add(craftObj);
                }
            }
        }
    }
    
    public void RemoveObject(Interactable interactable ,GameObject obj)
    {
        interactable.plate = null;
        
        obj.transform.parent = null;
        _carryObjs.Remove(obj);
        print("asd");

        if(_carryObjs.Count != 0) return;
        state = State.Dirty;
        if (TryGetComponent(out MeshRenderer meshRenderer)) meshRenderer.material.color = new Color(-1, -1, -1);
    }
}
