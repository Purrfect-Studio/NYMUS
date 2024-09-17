using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorWorms : MonoBehaviour
{
    [Header("Dano")]
    public float dano;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador")) 
        {
            VidaJogador vidaJogador = collision.gameObject.GetComponent<VidaJogador>();
            if(vidaJogador != null && !VidaJogador.invulneravel)
            {
                vidaJogador.tomarDano(dano);
            }
        }
    }
}
