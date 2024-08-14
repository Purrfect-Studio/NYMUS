using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComprarItem : MonoBehaviour
{
    public int precoItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void comprar()
    {
        if(Inventario.moedasTotal - precoItem >= 0)
        {
            Inventario.subtrairMoeda(precoItem);
            Destroy(gameObject);
        }
    }
}
