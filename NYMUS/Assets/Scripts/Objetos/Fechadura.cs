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
            if (precisa_ChaveEspecial && Inventario.temChaveEspecial == true)
            {
                destravarAlavanca();
            }
            if (precisa_Chave == false && precisa_ChaveEspecial == false)
            {
                destravarAlavanca();
            }
            else
            {
                if (Inventario.chavesAtual > 0)
                {
                    Inventario.chavesAtual -= 1;
                    destravarAlavanca();
                }
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
