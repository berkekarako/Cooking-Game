using UnityEngine;

public class Gargabe : MonoBehaviour
{
    public bool DeleteObj(GameObject obj, InteractiveObject interactiveObject)
    {
        if (obj.TryGetComponent(out Meals meals))
        {
            Destroy(obj);
            interactiveObject.ObjectDrop();
        }

        if (obj.TryGetComponent(out Plate plate))
        {
            foreach (Transform altObjs in obj.transform)
            {
                if (altObjs.TryGetComponent(out Meals meals2))
                {
                    plate.RemoveObject(altObjs.GetComponent<Interactable>(), altObjs.gameObject);
                    Destroy(altObjs.gameObject);

                    obj.GetComponent<Collider>().enabled = true;
                    obj.GetComponent<Collider>().isTrigger = true;
                    obj.GetComponent<Rigidbody>().isKinematic = true;

                    return false;
                }
            }
        }

        if (obj.TryGetComponent(out Pan pan))
        {
            foreach (Transform altObjs in obj.transform)
            {
                if (altObjs.TryGetComponent(out Meals meals3))
                {
                    pan.RemoveObject(altObjs.GetComponent<Interactable>(), altObjs.gameObject);
                    Destroy(altObjs.gameObject);

                    obj.GetComponent<Collider>().enabled = true;
                    obj.GetComponent<Collider>().isTrigger = true;
                    obj.GetComponent<Rigidbody>().isKinematic = true;

                    return false;
                }
            }
        }

        obj.GetComponent<Collider>().enabled = true;
        obj.GetComponent<Collider>().isTrigger = true;
        obj.GetComponent<Rigidbody>().isKinematic = true;
        return false;
    }
}
