using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacoesL3H : MonoBehaviour
{
    [Header("Animator")]
    public Animator animacao;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerControlador.podeMover == false)
        {
            animacao.SetBool("estaAndando", false);
            animacao.SetBool("estaPulando", false);
            animacao.SetBool("estaAtacando", false);
        }
        else
        {
            andar();
            pular();
            empurrarCaixa();
        }
    }

    void andar()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            animacao.SetBool("estaAndando", true);
        }
        else
        {
            animacao.SetBool("estaAndando", false);
        }
    }

    void pular()
    {
        if (PlayerControlador.estaPulando == true)
        {
            animacao.SetBool("estaPulando", true);
        }
        else
        {
            animacao.SetBool("estaPulando", false);
        }
    }

    void empurrarCaixa()
    {
        if (GrudarObjeto.estaEmpurrando == true)
        {
            animacao.SetBool("EmpurrandoCaixa", true);
        }
        else
        {
            animacao.SetBool("EmpurrandoCaixa", false);
        }
    }
}
