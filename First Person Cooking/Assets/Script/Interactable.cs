using UnityEngine;

public class Interactable : MonoBehaviour   
{
    public Pan pan;
    public Plate plate;
    public DishWasher dishWasher;
    
    public bool tavayaKoyulabilir = true;
    public bool tabakaKoyulabilir = true;
    public bool canPutDishwasher;

    public void ObjectTake()
    {
        if(pan) pan.RemoveObject(this, gameObject);
        if(plate) plate.RemoveObject(this, gameObject);
        if(dishWasher) GetComponent<WashObj>().WashEnd(this);
        if(TryGetComponent(out Cookable cookable)) cookable.PisirmeBitir();
    }
}
