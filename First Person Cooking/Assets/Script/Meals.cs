using System;
using UnityEngine;

public class Meals : MonoBehaviour
{
    [Serializable]
    public enum Yemekler
    {
        Et,
        Mantar,
        Ekmek
    }

    public Yemekler yemek;

    #region Cooked

    [Serializable]
    public enum CookingDegree
    {
        Cig,
        AzPismis,
        OrtaPismis,
        CokPismis,
        Yanmis
    }

    [Header("Cooked")]
    public bool cookAble;
    public CookingDegree cookingDegree = CookingDegree.Cig;
    public GameObject nextCookedObj;
    public float cookTime;

    public void CookedStart()
    {
        if (TryGetComponent(out Interactable interactable))
        {
            if (interactable.pan) Invoke(nameof(Cooked), cookTime);
            else if (interactable.plate) print("a");
            else Burn();
        }
    }

    public void CookedExit()
    {
        CancelInvoke(nameof(Cooked));
        print("Cisim Çýktý");
    }

    private void Cooked()
    {
        var newMeal = Instantiate(nextCookedObj, transform.position, Quaternion.identity);
        newMeal.transform.parent = transform.parent;
        newMeal.GetComponent<Interactable>().pan = GetComponent<Interactable>().pan;
        if (newMeal.GetComponent<Meals>().cookAble) newMeal.GetComponent<Meals>().CookedStart();
        Destroy(gameObject);
    }

    private void Burn()
    {
        if (!cookAble) return;
        if (TryGetComponent(out MeshRenderer meshRenderer))
        {
            cookingDegree = CookingDegree.Yanmis;
            meshRenderer.material.color = Color.black;
        }
    }

    public void CookedEnd() => CancelInvoke(nameof(Cooked));

    #endregion

    #region Cut

    [Header("Cut")]
    public bool cutAble;
    public float cutTime;
    public GameObject cuttedObj;

    #endregion
}
