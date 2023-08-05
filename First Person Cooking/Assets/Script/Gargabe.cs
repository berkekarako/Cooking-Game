using UnityEngine;

public class Gargabe : MonoBehaviour
{
    public bool DeleteObj(GameObject obj)
    {
        if (obj.TryGetComponent(out Pisirilebilir pisirilebilir))
        {
            Destroy(obj);
            return true;
        }

        if (obj.TryGetComponent(out Tabak tabak))
        {
            foreach (Transform altObjs in obj.transform)
            {
                if (altObjs.TryGetComponent(out Pisirilebilir pisirilebilir2))
                {
                    tabak.RemoveObject(altObjs.gameObject);
                    Destroy(altObjs.gameObject);
                    
                    obj.GetComponent<Collider>().enabled = true;
                    obj.GetComponent<Collider>().isTrigger = true;
                    obj.GetComponent<Rigidbody>().isKinematic = true;
                    
                    return false;
                }
            }
        }
        
        if (obj.TryGetComponent(out TavaTasimaObjeyle tava))
        {
            foreach (Transform altObjs in obj.transform)
            {
                if (altObjs.TryGetComponent(out Pisirilebilir pisirilebilir3))
                {
                    tava.RemoveObject(altObjs.gameObject);
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
