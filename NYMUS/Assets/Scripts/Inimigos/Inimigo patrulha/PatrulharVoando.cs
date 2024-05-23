using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrulharVoando : MonoBehaviour
{
    [Header("Movimenta��o")]
    public float velocidade;
    public float raioMovimentacao;
    private Vector2 direcao;
    private Vector3 pontoInicial;

    [Header("Bater Asas")]
    public float intervaloBaterAsas = 0.5f; // Intervalo entre os pulinhos
    public float alturaPulo = 0.5f; // Altura dos pulinhos

    [Header("Detec��o de Colis�es")]
    public LayerMask layerColisao;
    public float distanciaDeteccao = 0.5f;

    [Header("Procurar Jogador")]
    public ProcurarJogador procurarJogadorScript; // Refer�ncia ao script de procurar jogador

    private float tempoDesdeUltimoPulo;

    void Start()
    {
        pontoInicial = transform.position;
        EscolherNovaDirecao();
        tempoDesdeUltimoPulo = 0;
    }

    void Update()
    {
        tempoDesdeUltimoPulo += Time.deltaTime;

        if (tempoDesdeUltimoPulo >= intervaloBaterAsas)
        {
            BaterAsas();
            tempoDesdeUltimoPulo = 0;
        }

        if (procurarJogadorScript.procurarJogador())
        {
            VoarParaJogador();
        }
        else
        {
            Mover();
        }

        VerificarLimites();
        VerificarColisoes();
    }

    void EscolherNovaDirecao()
    {
        float anguloAleatorio = Random.Range(0, 360) * Mathf.Deg2Rad;
        direcao = new Vector2(Mathf.Cos(anguloAleatorio), Mathf.Sin(anguloAleatorio));
    }

    void BaterAsas()
    {
        // Adiciona um pulinho na dire��o vertical
        direcao += Vector2.up * alturaPulo;
        direcao.Normalize(); // Normaliza a dire��o para manter a velocidade constante
    }

    void Mover()
    {
        transform.Translate(direcao * velocidade * Time.deltaTime);
    }

    void VerificarLimites()
    {
        float distanciaDoCentro = Vector3.Distance(transform.position, pontoInicial);
        if (distanciaDoCentro > raioMovimentacao)
        {
            Vector3 direcaoParaCentro = (pontoInicial - transform.position).normalized;
            direcao = new Vector2(direcaoParaCentro.x, direcaoParaCentro.y);
        }
    }

    void VerificarColisoes()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direcao, distanciaDeteccao, layerColisao);
        if (hit.collider != null)
        {
            EscolherNovaDirecao();
        }
    }

    void VoarParaJogador()
    {
        Vector3 direcaoParaJogador = (procurarJogadorScript.jogador.transform.position - transform.position).normalized;
        direcao = new Vector2(direcaoParaJogador.x, direcaoParaJogador.y);
        Mover();
    }

    // M�todo para desenhar o raio limite no Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pontoInicial, raioMovimentacao);
    }

    // M�todo para atualizar o ponto inicial ao editar o script no Editor
    void OnValidate()
    {
        pontoInicial = transform.position;
    }
}