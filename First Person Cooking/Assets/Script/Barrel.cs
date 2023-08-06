using UnityEngine;

public class Barrel : MonoBehaviour
{
    public GameObject verilcekObje;
    
    public float maxObj;
    private float x;

    public GameObject ObjeyiVer()
    {
        if (maxObj > x)
        {
            x++;
            return Instantiate(verilcekObje, transform.position, Quaternion.identity);
        }
        
        return null;
    }
}
