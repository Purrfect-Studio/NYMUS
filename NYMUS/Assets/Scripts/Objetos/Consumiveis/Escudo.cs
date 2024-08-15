using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : MonoBehaviour
{
    public float valorEscudo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            VidaJogador vidaJogador = collision.GetComponent<VidaJogador>();
            vidaJogador.receberEscudo(valorEscudo);
            Destroy(gameObject);
        }
    }
}
