using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ContatoTrigger : MonoBehaviour
{

    [Header("Evento")]
    public UnityEvent evento;

    [Header("Configurações")]
    public int quantidadeAtivacoes = 0; // Quantidade de ativações permitidas
    public bool ativarSempre = true; // Se verdadeiro, ativa o evento sempre


    private int ativacoesRestantes; // Contador de ativações restantes

    private void Start()
    {
        // Inicializa o contador de ativações restantes
        ativacoesRestantes = quantidadeAtivacoes;
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
