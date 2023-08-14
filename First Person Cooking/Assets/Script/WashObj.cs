using System;
using UnityEngine;

public class WashObj : MonoBehaviour
{
    public float washTime;
    private float x;

    public bool isWashing;

    private DishWasher dishWasher;

    public void WashStart(GameObject obj)
    {
        isWashing = true;
        x = washTime;
        dishWasher = obj.GetComponent<DishWasher>();
    }

    private void Update()
    {
        if (!isWashing) return;
        x -= Time.deltaTime;

        if (!(x <= 0)) return;
        if (TryGetComponent(out Plate plate)) plate.state = Plate.State.Clear;
        if (TryGetComponent(out MeshRenderer meshRenderer)) meshRenderer.material.color = new Color(1, 1, 1);
        isWashing = false;
    }

    public void WashEnd(Interactable interactable)
    {
        try
        {
            isWashing = false;
            interactable.dishWasher = null;
            dishWasher.washObjects.Remove(gameObject);
        }
        catch (Exception e)
        {
            print("A");
        }
    }
}
