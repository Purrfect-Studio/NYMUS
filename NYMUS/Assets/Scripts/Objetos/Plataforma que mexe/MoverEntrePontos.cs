using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEntrePontos : MonoBehaviour
{
    [Header("Pontos de Movimentacao")]
    public Transform[] pontosDeMovimentacao; //Array com todos os pontos de movimentacao
    [SerializeField] private LayerMask layerPontosDeMovimento;
    [Header("Variaveis de Controle")]
    public bool pararEntrePontos = false;
    public bool ligado = true;
    public int proximoPonto;
    public bool detectarPontoLigado;
    public float raioDetecao;
    [Header("Pra Aumentar a Velocidade precisa Diminuir o Tempo")]
    public float velocidade;
    public float tempoDetectarPontoFicaDesativado;

    // Start is called before the first frame update
    void Start()
    {
        if (pararEntrePontos && ligado == false)
        {
            proximoPonto = -1; // O valor é acrescentado em 1 quando for ligado
        }
        else
        {
            proximoPonto = 0;
        }
        detectarPontoLigado = true;
    }

    private void FixedUpdate()
    {
        if (detectarPontoLigado == false)
        {
            StartCoroutine("LigarDetectarPonto");
        }
        else
        {
            detectarPonto();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (proximoPonto == pontosDeMovimentacao.Length)
        {
            proximoPonto = 0;
        }
        if(ligado)
        movimentacao();
        if (VidaJogador.estaMorto == true)
        {
            resetEmMorte();
        }
    }

    void movimentacao()
    {
        transform.position = Vector2.MoveTowards(transform.position, pontosDeMovimentacao[proximoPonto].transform.position, velocidade * Time.deltaTime);
    }

    void detectarPonto()
    {
        Collider2D encontrarPonto = Physics2D.OverlapCircle(transform.position, raioDetecao, layerPontosDeMovimento);
        if (encontrarPonto != null)
        {
            if (pararEntrePontos)
            {
                desligar();
            }
            else
            {
                proximoPonto++;
                detectarPontoLigado = false;
            }

            //Debug.Log("Cheguei no ponto: "+encontrarPonto.gameObject.name);

            if (proximoPonto >= pontosDeMovimentacao.Length)
            {
                proximoPonto = 0;
            }
            //Debug.Log("Proximo Ponto = " + proximoPonto);
        }
    }

    IEnumerator LigarDetectarPonto()
    {
        yield return new WaitForSeconds(tempoDetectarPontoFicaDesativado);
        detectarPontoLigado = true;
    }

    private void OnDrawGizmos()
    {
        // Desenha uma esfera de gizmos para visualizar a área de busca
        Gizmos.DrawWireSphere(transform.position, raioDetecao);
    }

    public void ligar()
    {
        detectarPontoLigado = false;
        proximoPonto++;
        ligado = true;
       
    }
    public void desligar()
    {
        ligado = false;
    }

    void resetEmMorte()
    {
        proximoPonto = 0;
    }

}
