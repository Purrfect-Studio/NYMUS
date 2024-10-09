using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ContatoTrigger : MonoBehaviour
{

    [Header("Evento")]
    public UnityEvent evento;

    [Header("Configurações")]
    public bool ativarSempre = true; // True = ativa o evento sempre / False = LimitarAtivacoes
    public int quantidadeAtivacoes; // Quantidade de ativações permitidas
    

    private int ativacoesRestantes = 0; // Contador de ativações restantes

    private void Start()
    {
        // Inicializa o contador de ativações restantes
        ativacoesRestantes = quantidadeAtivacoes;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
                if (ativarSempre)
            {
                // Invoca o evento sempre que o gatilho for acionado
                evento.Invoke();
            }
            else if (ativacoesRestantes > 0)
            {
                // Invoca o evento se ainda houver ativações restantes
                evento.Invoke();
                ativacoesRestantes--; // Decrementa o contador de ativações restantes
            }
        }
    }

    
}
