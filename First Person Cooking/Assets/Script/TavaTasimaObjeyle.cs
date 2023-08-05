using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavaTasimaObjeyle : MonoBehaviour
{
    //public GameObject obje1; 
    //public GameObject obje2; 
    public Transform FoodPosition;
    private bool tavaDolu = false; // Tava i�i kontrol�
    private GameObject TasinanObje;
    

    // Objeyi tavan�n i�ine yerle�tirme
    public void PlaceObject(GameObject obj)
    {
        if (obj.GetComponent<İnteractable>().tavayaKoyulabilir)
        {
            if (!tavaDolu)
            {
                tavaDolu = true;
                TasinanObje = obj;
                
                obj.GetComponent<Collider>().enabled = true;
                obj.GetComponent<Collider>().isTrigger = true;
                obj.GetComponent<Rigidbody>().isKinematic = true;
                
                obj.GetComponent<İnteractable>().TavanınIcindemi = true;
                
                obj.transform.rotation = transform.rotation;
                obj.transform.position = FoodPosition.position;
                obj.transform.parent = transform;
                
                Debug.Log("Obje tavan�n i�ine yerle�tirildi.");
            }

            else
            {
                GameObject.FindWithTag("Player").GetComponent<InteractiveObject>().ObjeBrrakalcakmı = false;
                Debug.Log("Tava zaten dolu.");
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

    // Objeyi tavan�n i�inden ��karma
    public void RemoveObject(GameObject obje)
    {
        obje.transform.parent = null;
        tavaDolu = false;
        Debug.Log("Obje tavan�n i�inden ��kar�ld�.");
        TasinanObje = null;
    }
    private void Update()
    {
        if (TasinanObje != null)
        {
            if(TasinanObje.GetComponent<İnteractable>().TavanınIcindemi == false)
            {
                RemoveObject(TasinanObje);
            }
        }
    }
}
