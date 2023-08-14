using System.Collections.Generic;
using UnityEngine;

public class DishWasher : MonoBehaviour
{
    public float maxWashCount;
    public List<GameObject> washObjects = new List<GameObject>();

    public Transform dishwashPoint;

    public bool Wash(GameObject obj, InteractiveObject interactiveObject)
    {
        if (obj.TryGetComponent(out Interactable interactable))
        {
            if (interactable.canPutDishwasher && obj.GetComponent<Plate>().state != Plate.State.Clear)
            {
                if (maxWashCount > washObjects.Count)
                {
                    if (obj.TryGetComponent(out WashObj washObj)) washObj.WashStart(gameObject);
                    washObjects.Add(obj);
                    interactable.dishWasher = this;
                    obj.transform.position = dishwashPoint.position;
                    obj.transform.eulerAngles = Vector3.zero;
                    interactiveObject.ObjectDrop();
                }
            }
        }

        obj.GetComponent<Collider>().enabled = true;
        obj.GetComponent<Collider>().isTrigger = true;
        obj.GetComponent<Rigidbody>().isKinematic = true;
        return false;
    }
}
