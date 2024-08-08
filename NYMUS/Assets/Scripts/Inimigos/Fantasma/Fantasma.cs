using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantasma : MonoBehaviour
{
    private bool olhandoParaEsquerda;
    private irAteJogador irAteJogador;
    private VidaInimigo vidaInimigo;
    private GameObject jogador;
    private MoverEntrePontos moverEntrePontos;
    private ProcurarJogador procurarJogador;
    // Start is called before the first frame update
    void Start()
    {
        irAteJogador = GetComponent<irAteJogador>();
        jogador = GameObject.FindGameObjectWithTag("Jogador");
        vidaInimigo = GetComponent<VidaInimigo>();
        procurarJogador = GetComponent<ProcurarJogador>();
        if(GetComponent<MoverEntrePontos>() != null)
        {
            moverEntrePontos = GetComponent<MoverEntrePontos>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (vidaInimigo.fantasma == true)
        {
            if (transform.position.x - jogador.transform.position.x < 0 && PlayerControlador.olhandoDireita || transform.position.x - jogador.transform.position.x > 0 && !PlayerControlador.olhandoDireita)
            {
                irAteJogador.enabled = true; 
            }
            else
            {
                irAteJogador.enabled = false;
            }

            if(moverEntrePontos != null)
            {
                if (procurarJogador.procurarJogador())
                {                   
                    moverEntrePontos.enabled = false;
                }
                else
                {                    
                    moverEntrePontos.enabled = true;                    
                }
            }
            
        }
        if (!olhandoParaEsquerda && transform.position.x - jogador.transform.position.x < 0 || olhandoParaEsquerda && transform.position.x - jogador.transform.position.x > 0)
        {
            flipSprite();
        }
    }

    public void flipSprite()
    {
        olhandoParaEsquerda = !olhandoParaEsquerda;
        transform.Rotate(0f, 180f, 0f);
    }
}
