using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    
    public Camera cam;
    public Transform cubeTransform;
    public float lerpTime,maxDis, throwingForce;
    private Rigidbody _rb;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_rb)
            {
                _rb.gameObject.GetComponent<Collider>().enabled = false;

                var transform1 = cam.transform;
                var ray = new Ray(transform1.position, transform1.forward);
                
                if (Physics.Raycast(ray, out var raycastHit, maxDis))
                {
                    if(raycastHit.collider.TryGetComponent(out Pan pan)) pan.PlaceObject(_rb.gameObject, this);
                    else if (raycastHit.collider.TryGetComponent(out Plate plate)) plate.PlaceObject(_rb.gameObject, this);
                    else if (raycastHit.collider.TryGetComponent(out Gargabe gargabe)) gargabe.DeleteObj(_rb.gameObject, this);
                    else if (raycastHit.collider.TryGetComponent(out DishWasher dishWasher)) dishWasher.Wash(_rb.gameObject, this);
                    else ObjectDrop();
                }
                else ObjectDrop();
            }
            else
            {
                var transform1 = cam.transform;
                var ray = new Ray(transform1.position, transform1.forward);
                
                if (Physics.Raycast(ray, out var raycastHit, maxDis))
                {
                    if(raycastHit.collider.TryGetComponent(out Interactable interactable))
                    {
                        _rb = raycastHit.collider.gameObject.GetComponent<Rigidbody>();
                        
                        interactable.ObjectTake();
                        TakeObject(_rb);
                    }
                    
                    else if (raycastHit.collider.TryGetComponent(out Barrel barrel))
                    {
                        var a = barrel.ObjeyiVer();
                        
                        if (a) TakeObject(a.GetComponent<Rigidbody>());
                    }
                }
            }
        }

        /*if (!_rb) return;
        _rb.MovePosition(Vector3.Lerp(_rb.transform.position, cubeTransform.position, lerpTime));
        _rb.MoveRotation(Quaternion.Lerp(_rb.transform.rotation, cubeTransform.rotation, lerpTime));
        if (!Input.GetMouseButtonDown(0)) return;
        _rb.isKinematic = false;
        _rb.gameObject.GetComponent<Collider>().enabled = true;
        _rb.gameObject.GetComponent<Collider>().isTrigger = false;
        _rb.AddForce(cam.transform.forward * throwingForce, ForceMode.VelocityChange);
        _rb = null;*/
    }

    public void ObjectDrop()
    {
        _rb.transform.SetParent(null);
        
        _rb.gameObject.GetComponent<Collider>().enabled = true;
        _rb.GetComponent<Collider>().isTrigger = false;
        _rb.isKinematic = false;
        _rb = null;
    }

    public void ObjectPut(bool colliderEnable = true, bool isTrigger = true, bool isKinematic = true)
    {
        _rb.transform.SetParent(null);
        
        _rb.GetComponent<Collider>().enabled = colliderEnable;
        _rb.GetComponent<Collider>().isTrigger = isTrigger;
        _rb.isKinematic = isKinematic;
        
        _rb = null;
    }
    
    public void TakeObject(Rigidbody rb)
    {
        _rb = rb;

        _rb.transform.SetParent(null);
        _rb.transform.position = cubeTransform.position;
        _rb.transform.rotation = cubeTransform.rotation;
        _rb.transform.SetParent(cubeTransform);
        
        _rb.isKinematic = true;
        _rb.gameObject.GetComponent<Collider>().enabled = false;
        _rb.GetComponent<Collider>().isTrigger = true;
    }
}
