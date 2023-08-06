using UnityEngine;

public class Oven : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Cookable cookable))
            cookable.PisirmeStart();
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Cookable cookable))
            cookable.PisirmeEnd();
    }
}
