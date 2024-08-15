using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComprarItem : MonoBehaviour
{
    public int precoItem;

    public void comprar()
    {
        if(Inventario.moedasTotal - precoItem >= 0)
        {
            Inventario.subtrairMoeda(precoItem);
            Destroy(gameObject);
        }
    }
}
