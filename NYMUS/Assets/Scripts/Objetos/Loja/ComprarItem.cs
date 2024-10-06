using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComprarItem : MonoBehaviour
{
    public int precoItem;
    [SerializeField] private UnityEvent botaoApertado;

    public void comprar()
    {
        if(Inventario.moedasTotal - precoItem >= 0)
        {
            Inventario.subtrairMoeda(precoItem);
            botaoApertado.Invoke();
            Destroy(gameObject);
        }
    }
}
