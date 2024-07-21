using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnarInimigos : MonoBehaviour
{
    [Header("Prefab do Inimigo")]
    public GameObject prefabInimigo; // Prefab do inimigo a ser instanciado

    [Header("Número de Inimigos")]
    public int numeroDeInimigos = 3; // Número de instâncias do inimigo

    [Header("Intervalo de Checagem")]
    public float intervaloDeChecagem = 1.0f; // Intervalo entre checagens de detecção do jogador

    [Header("Área de Spawn")]
    public float raioSpawn = 5.0f; // Raio da área onde os inimigos podem nascer

    private ProcurarJogador procurarJogador;

    private void Start()
    {
        procurarJogador = GetComponent<ProcurarJogador>();
        StartCoroutine(ChecarJogadorPeriodicamente());
    }

    private IEnumerator ChecarJogadorPeriodicamente()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloDeChecagem);
            if (procurarJogador.procurarJogador())
            {
                InstanciarInimigos();
                break;
            }
        }
    }

    private void InstanciarInimigos()
    {
        for (int i = 0; i < numeroDeInimigos; i++)
        {
            Vector2 posicaoAleatoria = (Vector2)transform.position + Random.insideUnitCircle * raioSpawn;
            Instantiate(prefabInimigo, posicaoAleatoria, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        // Desenha uma esfera de gizmos para visualizar a área de busca
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, raioSpawn);
    }
}
