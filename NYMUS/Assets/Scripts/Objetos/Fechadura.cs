using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fechadura : MonoBehaviour
{

    private bool travado = true;
    public bool precisa_Chave = true;
    public bool precisa_ChaveEspecial = false;

    public UnityEvent eventoDestravado;
    private Animator animacao;
    // Start is called before the first frame update
    private void Start()
    {
        animacao = GetComponent<Animator>();
    }
    
    public void destravar ()
    {
        if (travado) 
        {
            if (precisa_ChaveEspecial == true && Inventario.chavesEspeciasAtual > 0)
            {
                destravarAlavanca();
                Inventario.chavesEspeciasAtual -= 1;
            }
            if (precisa_Chave == true && Inventario.chavesAtual > 0)
            {
                destravarAlavanca();
                Inventario.chavesAtual -= 1;
            }
            if (precisa_Chave == false && precisa_ChaveEspecial == false)
            {
                destravarAlavanca();
            }
        }
    }

    public void travar ()
    {
        travado = true;
    }

    private void destravarAlavanca()
    {
        travado = false;
        animacao.SetBool("ativarAlavanca", true);
        eventoDestravado.Invoke();
    }
}
