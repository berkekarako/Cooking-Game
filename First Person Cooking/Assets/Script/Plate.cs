using System;
using UnityEngine;

public class Plate : MonoBehaviour
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

    public void PlaceObject(GameObject obj, InteractiveObject interactiveObject)
    {
        if (obj.GetComponent<Interactable>().tabakaKoyulabilir)
        {
            if (!_placeIsFull && state == State.Clear)
            {
                _placeIsFull = true;
                
                interactiveObject.ObjectPut();

                obj.GetComponent<Interactable>().plate = this;
                
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
            }
        }
        else
        {
            obj.GetComponent<Collider>().enabled = true;
            obj.GetComponent<Collider>().isTrigger = true;
            obj.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    
    public void RemoveObject(Interactable interactable ,GameObject obj)
    {
        interactable.plate = null;
        
        _placeIsFull = false;
        obj.transform.parent = null;
        _carryObj = null;

        state = State.Dirty;
        if (TryGetComponent(out MeshRenderer meshRenderer)) meshRenderer.material.color = new Color(-1, -1, -1);
    }
}
