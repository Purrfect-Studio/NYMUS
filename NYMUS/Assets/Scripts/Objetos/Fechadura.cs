using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fechadura : MonoBehaviour
{

    public bool travado = true;
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
        if (travado && Inventario.chavesAtual>0)
        {
            travado = false;
            animacao.SetBool("ativarAlavanca", true);
            eventoDestravado.Invoke();
        }
    }

    public void travar ()
    {
        travado = true;
    }
}
