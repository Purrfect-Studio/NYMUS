using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moeda : MonoBehaviour
{
    public int valorMoeda;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            Inventario.receberMoeda(valorMoeda);
            Destroy(this.gameObject);
            //gameObject.SetActive(false);
        }
    }

    public void darMoeda()
    {
        Inventario.receberMoeda(valorMoeda);
    }
}
