using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    
    public Camera cam;
    public Transform cubeTransform;
    public float lerpTime,maxDis, throwingForce;
    public bool ObjeBrrakalcakmı;
    private Rigidbody _rb;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_rb)
            {
                ObjeBrrakalcakmı = true;
                _rb.gameObject.GetComponent<Collider>().enabled = false;
                _rb.isKinematic = false;
                _rb.GetComponent<Collider>().isTrigger = false;
                
                var transform1 = cam.transform;
                var ray = new Ray(transform1.position, transform1.forward);
                
                if (Physics.Raycast(ray, out var raycastHit, maxDis))
                {
                    if(raycastHit.collider.CompareTag("Tava")) raycastHit.collider.GetComponent<TavaTasimaObjeyle>().PlaceObject(_rb.gameObject);
                    else if (raycastHit.collider.TryGetComponent(out Tabak tabak)) tabak.PlaceObject(_rb.gameObject);
                    else if (raycastHit.collider.TryGetComponent(out Gargabe gargabe)) ObjeBrrakalcakmı = gargabe.DeleteObj(_rb.gameObject);
                    else if (raycastHit.collider.TryGetComponent(out DishWasher dishWasher)) ObjeBrrakalcakmı = dishWasher.Wash(_rb.gameObject);
                }
                
                
                if (ObjeBrrakalcakmı)
                {
                    _rb.gameObject.GetComponent<Collider>().enabled = true;
                    _rb = null;
                }
                
            }
            else
            {
                var transform1 = cam.transform;
                var ray = new Ray(transform1.position, transform1.forward);
                
                if (Physics.Raycast(ray, out var raycastHit, maxDis))
                {
                    if(raycastHit.collider.TryGetComponent(out İnteractable interactable))
                    {
                        _rb = raycastHit.collider.gameObject.GetComponent<Rigidbody>();
                        if (_rb)
                        {
                            _rb.GetComponent<İnteractable>().TavanınIcindemi = false;
                            _rb.GetComponent<İnteractable>().TabakinIcinde = false;
                            
                            interactable.inDishwasher = false;
                            if(_rb.gameObject.TryGetComponent(out WashObj washObj)) washObj.WashEnd();
                            
                            if(_rb.TryGetComponent(out Pisirilebilir pisirilebilir)) pisirilebilir.PisirmeBitir();
                            
                            _rb.transform.SetParent(null);
                            _rb.isKinematic = true;
                            _rb.gameObject.GetComponent<Collider>().enabled = false;
                            _rb.GetComponent<Collider>().isTrigger = true;
                        }
                    }
                    
                    else if (raycastHit.collider.TryGetComponent(out Varil varil))
                    {
                        var a = varil.ObjeyiVer();
                        
                        if (a)
                        {
                            _rb = a.GetComponent<Rigidbody>();
                            if (_rb)
                            {
                                _rb.isKinematic = true;
                                _rb.gameObject.GetComponent<Collider>().enabled = false;
                            }
                        }
                    }
                }
            }
        }

        if (!_rb) return;
        _rb.MovePosition(Vector3.Lerp(_rb.transform.position, cubeTransform.position, lerpTime));
        _rb.MoveRotation(Quaternion.Lerp(_rb.transform.rotation, cubeTransform.rotation, lerpTime));
        if (!Input.GetMouseButtonDown(0)) return;
        _rb.isKinematic = false;
        _rb.gameObject.GetComponent<Collider>().enabled = true;
        _rb.gameObject.GetComponent<Collider>().isTrigger = false;
        _rb.AddForce(cam.transform.forward * throwingForce, ForceMode.VelocityChange);
        _rb = null;
    }
}
