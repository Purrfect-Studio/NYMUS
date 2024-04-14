using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class InteragirBotao : MonoBehaviour
{
    [SerializeField]
    private JogadorInterage jogador;

    [SerializeField]
    private UnityEvent botaoApertado;

    private bool podeExecutar;


    // Update is called once per frame
    void Update()
    {
        if(podeExecutar)
        {
            if(jogador.estaInteragindo == true)
            {
                botaoApertado.Invoke();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) //Entrou no range de interação
    {
        podeExecutar = true;
    }

    private void OnTriggerExit2D(Collider2D collision) //Saiu do range de interação
    {
        podeExecutar = false;
    }
}
