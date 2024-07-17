using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fechadura : MonoBehaviour
{

    private bool travado = true;
    public bool precisa_Chave = true;

    public UnityEvent eventoDestravado;
    private Animator animacao;
    // Start is called before the first frame update
    private void Start()
    {
        animacao = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void destravar ()
    {
        if (travado) 
        {
            if (precisa_Chave == false)
            {
                travado = false;
                animacao.SetBool("ativarAlavanca", true);
                eventoDestravado.Invoke();
            }
            else
            {
                if (Inventario.chavesAtual > 0)
                {
                    Inventario.chavesAtual -= 1;
                    travado = false;
                    animacao.SetBool("ativarAlavanca", true);
                    eventoDestravado.Invoke();
                }
            }
        }
    }

    public void travar ()
    {
        travado = true;
    }
}
