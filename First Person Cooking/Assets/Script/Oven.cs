using UnityEngine;

public class Oven : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Meals meals))
            if (meals.cookAble) meals.CookedStart();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Meals meals))
            if (meals.cookAble) meals.CookedExit();
    }
}
