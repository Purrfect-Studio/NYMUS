using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuraCoracao : MonoBehaviour
{
    [Header("Cura")]
    public int cura; // Quantidade de vida que o cora��o cura

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu tem a tag "Player"
        if (collision.CompareTag("Jogador"))
        {
            // Tenta encontrar o componente VidaJogador no objeto colidido
            VidaJogador vidaJogador = collision.GetComponent<VidaJogador>();
            if (vidaJogador != null)
            {
                // Chama o m�todo de cura no script VidaJogador
                vidaJogador.curar(cura);
                // Destr�i o cora��o ap�s a cura
                Destroy(gameObject);
            }
        }
    }
}
