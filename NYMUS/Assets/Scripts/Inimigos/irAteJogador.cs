using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class irAteJogador : MonoBehaviour
{
    [Header("Script Procurar Jogador")]
    public ProcurarJogador procurarJogador;
    [Header("Jogador")]
    public GameObject jogador;
    public VidaInimigo vidaInimigo;
    public PlayerControlador playerControlador;

    public float velocidade;

    private void Start()
    {
        if(GetComponent<VidaInimigo>() != null)
        {
            vidaInimigo = GetComponent<VidaInimigo>();
        }
        jogador = GameObject.FindGameObjectWithTag("Jogador");
        playerControlador = jogador.GetComponent<PlayerControlador>();
    }
    // Update is called once per frame
    void Update()
    {
        if(vidaInimigo != null)
        {
            if (vidaInimigo.fantasma == true && procurarJogador.procurarJogador() == true)
            {
                if (transform.position.x - jogador.transform.position.x < 0 && PlayerControlador.olhandoDireita || transform.position.x - jogador.transform.position.x > 0 && !PlayerControlador.olhandoDireita)
                {
                    irJogador();
                }
            }
        }        
        else
        {
            if(procurarJogador.procurarJogador() == true)
            {
                irJogador();
            }
        }
    }

    void irJogador()
    {
        transform.position = Vector2.MoveTowards(transform.position, jogador.transform.position, velocidade * Time.deltaTime);
    }
}
