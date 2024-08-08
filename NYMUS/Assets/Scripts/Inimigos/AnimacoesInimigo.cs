using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacoesInimigo : MonoBehaviour
{
    private Animator animacao;
    private VidaInimigo vidaInimigo;
    // Start is called before the first frame update
    void Start()
    {
        animacao = GetComponent<Animator>();
        vidaInimigo = GetComponent<VidaInimigo>();
    }

    // Update is called once per frame
    void Update()
    {
        morrer();
    }

    void morrer()
    {
        /*if(vidaInimigo.vidaAtual <= 0)
        {
            animacao.SetTrigger("estaMorto");
        }*/
    }
}
