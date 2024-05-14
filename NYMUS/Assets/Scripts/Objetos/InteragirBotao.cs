using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class InteragirBotao : MonoBehaviour
{
    [Header("Jogador")]
    [SerializeField] private JogadorInterage jogador;
    [SerializeField] private UnityEvent botaoApertado;

    public bool podeExecutar;

    private void Start()
    {
        podeExecutar = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (jogador.estaInteragindo && podeExecutar)
        {
            botaoApertado.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision) //Entrou no range de interação
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            podeExecutar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //Saiu do range de interação
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            podeExecutar = false;
        }
    }
}
