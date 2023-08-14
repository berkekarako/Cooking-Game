using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Pan pan;
    public Plate plate;
    public DishWasher dishWasher;
    public KnifeTable knifeTable;

    public bool tavayaKoyulabilir = true;
    public bool canPutKnifeTable = true;
    public bool tabakaKoyulabilir = true;
    public bool canPutDishwasher;

    public void ObjectTake()
    {
        if (pan) pan.RemoveObject(this, gameObject);
        if (plate) plate.RemoveObject(this, gameObject);
        if (dishWasher) GetComponent<WashObj>().WashEnd(this);
        if (TryGetComponent(out Meals meals)) if (meals.cookAble) meals.CookedEnd();
        if (knifeTable) knifeTable.RemoveObject(this, gameObject);
    }
}
