using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishWasher : MonoBehaviour
{
    public float maxWashCount;
    public List<GameObject> washObjects = new List<GameObject>();
    
    public Transform dishwashPoint;

    public bool Wash(GameObject obj)
    {
        if (obj.TryGetComponent(out İnteractable interactable))
        {
            if (interactable.canPutDishwasher && obj.GetComponent<Tabak>().state != Tabak.State.Clear)
            {
                if (maxWashCount > washObjects.Count)
                {
                    if(obj.TryGetComponent(out WashObj washObj)) washObj.WashStart(gameObject);
                    washObjects.Add(obj);
                    interactable.inDishwasher = true;
                    obj.transform.position = dishwashPoint.position;
                    obj.transform.eulerAngles = Vector3.zero;
                    return true;
                }
            }
        }
        
        obj.GetComponent<Collider>().enabled = true;
        obj.GetComponent<Collider>().isTrigger = true;
        obj.GetComponent<Rigidbody>().isKinematic = true;
        return false;
    }
}
