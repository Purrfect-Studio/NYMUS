using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProcurarJogador : MonoBehaviour
{
    // Variáveis para configurar a busca do jogador
    [Header("Jogador")]
    public Transform jogador; // Referência para o transform do jogador
    [Header("Layer do Jogador")]
    public LayerMask layerJogador; // Layer do jogador
    public UnityEvent JogadorEncontrado; // Evento a ser acionado quando o jogador é encontrado

    [Header("Raio do circulo para detectar o jogador")]
    public float raioDetecao = 25f; // Raio de detecção do jogador

    void Update()
    {
        // Procura o jogador dentro do raio de detecção
        Collider2D encontrarJogador = Physics2D.OverlapCircle(transform.position, raioDetecao, layerJogador);
        if (encontrarJogador != null)
        {   
            // Se o jogador é encontrado, aciona o evento
            //JogadorEncontrado.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        // Desenha uma esfera de gizmos para visualizar a área de busca
        Gizmos.DrawWireSphere(transform.position, raioDetecao);
    }
}
