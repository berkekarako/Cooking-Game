using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface IInteractable
{
    public void Interact();
}

public class Etkileşim : MonoBehaviour
{
    public Camera cam;
    public Transform InteractorSource;
    public float InteractRange;
    

    bool isHand = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.E))
        {
            Ray newRay = new Ray(cam.transform.position, cam.transform.forward);
            if(Physics.Raycast(newRay, out RaycastHit hitInfo, InteractRange))
            {
                if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }
}
