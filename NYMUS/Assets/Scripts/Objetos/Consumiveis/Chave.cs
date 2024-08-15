using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chave : MonoBehaviour
{
    public bool chaveEspecial = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            if (chaveEspecial)
            {
                Inventario.receberChaveEspecial();
            }
            else
            {            
                Inventario.receberChave();
            }
            Destroy(this.gameObject);

        }
    }

    public static void darChave()
    {
        Inventario.receberChave();
    }
}
