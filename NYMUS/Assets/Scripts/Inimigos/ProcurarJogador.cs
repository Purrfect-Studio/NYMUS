using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProcurarJogador : MonoBehaviour
{
    [Header("Jogador")]
    public GameObject jogador; // Referência para o transform do jogador
    [Header("Layer do Jogador")]
    public LayerMask layerJogador; // Layer do jogador
    [Header("Raio do circulo para detectar o jogador")]
    public float raioDetecao; // Raio de detecção do jogador

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
    }
    public bool procurarJogador()
    {
        Collider2D encontrarJogador = Physics2D.OverlapCircle(transform.position, raioDetecao, layerJogador);
        if (encontrarJogador != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        // Desenha uma esfera de gizmos para visualizar a área de busca
        Gizmos.DrawWireSphere(transform.position, raioDetecao);
    }
}
