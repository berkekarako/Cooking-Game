using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pisirilebilir : MonoBehaviour
{
    [SerializeField] public enum CookingDegree
    {
        Cig,
        AzPismis,
        OrtaPismis,
        CokPismis,
        Yanmis
    }

    public CookingDegree cookingDegree = CookingDegree.Cig;

    public float pisirmeSuresi;
    
    public void PisirmeStart()
    {
        if (TryGetComponent(out İnteractable interactable))
        {
            if(interactable.TavanınIcindemi) Invoke(nameof(Pisir), pisirmeSuresi);
            else if(interactable.TabakinIcinde) print("a");
            else Yakma();
        }
    }

    public void PisirmeEnd()
    {
        CancelInvoke(nameof(Pisir));
        print("Cisim Çıktı");
    }

    private void Pisir()
    {
        if (cookingDegree != CookingDegree.Yanmis)
        {
            if (TryGetComponent(out MeshRenderer meshRenderer))
            {
                int sonrakiEnum = ((int)cookingDegree + 1) % (System.Enum.GetValues(typeof(CookingDegree)).Length);
                cookingDegree = (CookingDegree)sonrakiEnum;
            
                var material = meshRenderer.material;
                material.color = material.color + new Color(-.25f, -.25f, -.25f);
                print("Kademe Arttı");
            }
            
            Invoke(nameof(Pisir), pisirmeSuresi);
        }
    }

    private void Yakma()
    {
        if (TryGetComponent(out MeshRenderer meshRenderer))
        {
            cookingDegree = CookingDegree.Yanmis;
            meshRenderer.material.color = Color.black;
        }
    }

    public void PisirmeBitir() => CancelInvoke(nameof(Pisir));
}
