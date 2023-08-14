using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeTable : MonoBehaviour
{
    public Transform foodPosition;
    public Meals meals;

    private bool isFull;

    public void PlaceObj(GameObject obj, InteractiveObject interactiveObject)
    {
        if (isFull) return;
        if (obj.TryGetComponent(out Interactable interactable))
        {
            if (interactable.canPutKnifeTable)
            {
                isFull = true;
                interactiveObject.ObjectDrop();

                obj.GetComponent<Interactable>().knifeTable = this;
                meals = obj.GetComponent<Meals>();

                obj.transform.rotation = transform.rotation;
                obj.transform.position = foodPosition.position;
                obj.transform.parent = transform;
            }
        }
    }

    public void RemoveObject(Interactable interactable, GameObject obj)
    {
        isFull = false;
        interactable.knifeTable = null;
        meals = null;

        obj.transform.parent = null;
    }

    public void Cut()
    {
        if (!meals) return;
        if (!meals.cutAble) return;
        meals.cutTime -= Time.deltaTime;
        if (0 >= meals.cutTime)
        {
            CutObj();
        }
    }

    private void CutObj()
    {
        Instantiate(meals.cuttedObj, meals.transform.position, Quaternion.identity).transform.parent = null;
        Destroy(meals.gameObject);


        isFull = false;
        meals = null;
    }
}
