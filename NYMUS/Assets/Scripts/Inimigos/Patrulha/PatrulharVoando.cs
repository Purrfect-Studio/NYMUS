using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrulharVoando : MonoBehaviour
{
    [Header("Movimentação")]
    public float velocidade;
    public float raioMovimentacao;
    private Vector2 direcao;
    private Vector3 pontoInicial;
    //public bool olhandoDireita;
    //public SpriteRenderer sprite;

    [Header("Bater Asas")]
    public float intervaloBaterAsas = 0.5f; // Intervalo entre os pulinhos
    public float alturaPulo = 0.5f; // Altura dos pulinhos

    [Header("Detecção de Colisões")]
    public LayerMask layerColisao;
    public float distanciaDeteccao = 0.5f;

    [Header("Procurar Jogador")]
    public ProcurarJogador procurarJogadorScript; // Referência ao script de procurar jogador

    private float tempoDesdeUltimoPulo;

    void Start()
    {
        //olhandoDireita = true;
        //sprite = GetComponent<SpriteRenderer>();
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
        // Adiciona um pulinho na direção vertical
        direcao += Vector2.up * alturaPulo;
        direcao.Normalize(); // Normaliza a direção para manter a velocidade constante
    }

    void Mover()
    {
        transform.Translate(direcao * velocidade * Time.deltaTime);
        /*if ((transform.position.x - procurarJogadorScript.jogador.transform.position.x) < 0 && olhandoDireita == false || (transform.position.x - procurarJogadorScript.jogador.transform.position.x) > 0 && olhandoDireita == true)
        {
            olhandoDireita = !olhandoDireita;
            sprite.transform.Rotate(0, -180, 0);
        }*/
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
        Vector2 direcaoParaJogador = (procurarJogadorScript.jogador.transform.position - transform.position).normalized;
        // Movimenta o morcego em direção ao jogador sem alterar a direção original
        transform.position = Vector2.MoveTowards(transform.position, procurarJogadorScript.jogador.transform.position, velocidade * Time.deltaTime);
    }

    // Método para desenhar o raio limite no Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pontoInicial, raioMovimentacao);
    }

    // Método para atualizar o ponto inicial ao editar o script no Editor
    void OnValidate()
    {
        pontoInicial = transform.position;
    }
}
