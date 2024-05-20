using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ContatoTrigger : MonoBehaviour
{

    [Header("Evento")]
    public UnityEvent evento;

    [Header("Configura��es")]
    public int quantidadeAtivacoes = 0; // Quantidade de ativa��es permitidas
    public bool ativarSempre = true; // Se verdadeiro, ativa o evento sempre


    private int ativacoesRestantes; // Contador de ativa��es restantes

    private void Start()
    {
        // Inicializa o contador de ativa��es restantes
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
            // Invoca o evento se ainda houver ativa��es restantes
            evento.Invoke();
            ativacoesRestantes--; // Decrementa o contador de ativa��es restantes
        }
    }
}
