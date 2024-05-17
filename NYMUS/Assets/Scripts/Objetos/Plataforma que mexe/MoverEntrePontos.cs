using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEntrePontos : MonoBehaviour
{
    public Transform[] pontos;
    public float velocidade;
    public int proximoPonto;
    public bool procurarPonto = true;
    public float raioDetecao; // Raio de detecção do jogador
    [SerializeField] public LayerMask layerPontosDeMovimento;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (procurarPonto == true)
        {
            detectarPonto();
        }
        else
        {
            StartCoroutine("Esperar");
        }

        movimentacao();
    }

    void movimentacao()
    {
        transform.position = Vector2.MoveTowards(transform.position, pontos[proximoPonto].transform.position, velocidade * Time.deltaTime);
    }

    void detectarPonto()
    {
        Collider2D encontrarPonto = Physics2D.OverlapCircle(transform.position, raioDetecao, layerPontosDeMovimento);
        if (encontrarPonto != null)
        {
            //Debug.Log("Cheguei no ponto: "+encontrarPonto.gameObject.name);
            proximoPonto += 1;
            procurarPonto = false;
            if (proximoPonto >= pontos.Length)
            {
                proximoPonto = 0;
            }
        }
    }

    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(1f);
        procurarPonto = true;
    }

    private void OnDrawGizmos()
    {
        // Desenha uma esfera de gizmos para visualizar a área de busca
        Gizmos.DrawWireSphere(transform.position, raioDetecao);
    }
}
