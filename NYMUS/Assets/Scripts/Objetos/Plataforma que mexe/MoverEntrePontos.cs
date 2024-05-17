using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEntrePontos : MonoBehaviour
{
    [Header("Pontos de Movimentacao")]
    public Transform[] pontos;
    [SerializeField] public LayerMask layerPontosDeMovimento;
    [Header("Variaveis de Controle")]
    public int proximoPonto;
    public bool procurarPonto;
    public float raioDetecao;
    [Header("Pra Aumentar a Velocidade precisa Diminuir o Tempo")]
    public float velocidade;
    public float tempoSegundos;

    // Start is called before the first frame update
    void Start()
    {
        proximoPonto = 0;
        procurarPonto = true;
    }

    private void FixedUpdate()
    {
        if (procurarPonto == false)
        {
            StartCoroutine("Esperar");
        }
        else
        {
            detectarPonto();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (proximoPonto == pontos.Length)
        {
            proximoPonto = 0;
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
            procurarPonto = false;
            //Debug.Log("Cheguei no ponto: "+encontrarPonto.gameObject.name);
            proximoPonto++;
            if (proximoPonto >= pontos.Length)
            {
                proximoPonto = 0;
            }
            //Debug.Log("Proximo Ponto = " + proximoPonto);
        }
    }

    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(tempoSegundos);
        procurarPonto = true;
    }

    private void OnDrawGizmos()
    {
        // Desenha uma esfera de gizmos para visualizar a área de busca
        Gizmos.DrawWireSphere(transform.position, raioDetecao);
    }
}
