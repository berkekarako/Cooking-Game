using UnityEngine;

public class Pan : MonoBehaviour
{
    //public GameObject obje1; 
    //public GameObject obje2; 
    public Transform FoodPosition;
    private bool _placeIsFull = false; // Tava i�i kontrol�
    private GameObject carryObj;


    // Objeyi tavan�n i�ine yerle�tirme
    public void PlaceObject(GameObject obj, InteractiveObject interactiveObject)
    {
        if (obj.GetComponent<Interactable>().tavayaKoyulabilir)
        {
            if (!_placeIsFull)
            {
                carryObj = obj;
                _placeIsFull = true;

                interactiveObject.ObjectPut();

                obj.GetComponent<Interactable>().pan = this;

                obj.transform.rotation = transform.rotation;
                obj.transform.position = FoodPosition.position;
                obj.transform.parent = transform;

                Debug.Log("Obje tavan�n i�ine yerle�tirildi.");
            }

            else
            {
                Debug.Log("Tava zaten dolu.");
            }
        }
        else
        {
            obj.GetComponent<Collider>().enabled = true;
            obj.GetComponent<Collider>().isTrigger = true;
            obj.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    // Objeyi tavan�n i�inden ��karma
    public void RemoveObject(Interactable interactable, GameObject obj)
    {
        interactable.pan = null;

        _placeIsFull = false;
        carryObj = null;

        obj.transform.parent = null;
    }
}
