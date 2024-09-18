using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RefletirJogador : MonoBehaviour
{
    private GameObject jogador;
    private Rigidbody2D rigidbody2d;
    private PlayerControlador playerControlador;
    public float forca;
    void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
        rigidbody2d = jogador.GetComponent<Rigidbody2D>();
        playerControlador = jogador.GetComponent<PlayerControlador>();
    }

    public void Refletir()
    {
        Vector2 direction = (jogador.transform.position - transform.position).normalized;
        rigidbody2d.AddForce(direction * forca, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Jogador"))
        {
            playerControlador.podeDarDash = true;
            playerControlador.resetPulo = true;
            if (playerControlador.playerData.possuiPuloDuplo)
            {
                playerControlador.podeExecutarPuloDuplo = false;
            }
            Refletir();
        }
    }

}
