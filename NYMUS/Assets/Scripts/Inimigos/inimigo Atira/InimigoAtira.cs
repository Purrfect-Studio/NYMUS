using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InimigoAtira : MonoBehaviour
{
    [Header("Projetil")]
    public GameObject balaProjetil; // projetil

    [Header("Arma")]
    public Transform arma; // posicao de onde o projetil sai

    [Header("Caracteristicas do Projetil")]
    public float forcaTiro;                 // velocidade do projetil
    public int municao;                     // quantidade de tiros antes de "recarregar"
    public float intervaloTiro;             // intervalo entre tiros
    public float tempoRegarga;              // tempo de recarga depois de atirar
    private int quantidadesTiros;           // variavel de apoio
    private float contadorIntervaloTiro;    // variavel de apoio

    [Header("Sprite")]
    private bool olhandoEsquerda; // variavel para virar o inimigo

    [Header("Layer do Jogador")]
    [SerializeField] private LayerMask layerJogador; //Variavel de apoio para rechonhecer a layer do jogador
    public Transform jogador;                        //Posicao do jogador

    [Header("Interacoes com o Jogador")]
    [SerializeField] private UnityEvent DanoCausado; // evento de causar dano
    public static bool acertouJogador;               // diz se o tiro acertou ou nao

    // Start is called before the first frame update
    void Start()
    {
        contadorIntervaloTiro = intervaloTiro;
        acertouJogador = false;
        quantidadesTiros = municao;
    }

    // Update is called once per frame
    void Update()
    {
        procurarJogador();

        if (contadorIntervaloTiro < 0)
        {
            atirar();
        }
        else
        {
            contadorIntervaloTiro -= Time.deltaTime;
        }

        if (acertouJogador == true)
        {
            if (VidaJogador.invulneravel == false)
            {
                if (transform.position.x <= jogador.transform.position.x)
                {
                    VidaJogador.knockbackParaDireita = -1;
                }
                else
                {
                    VidaJogador.knockbackParaDireita = 1;
                }
                DanoCausado.Invoke();
                acertouJogador = false;
            }
        }
    }

    void atirar()
    {
        if(quantidadesTiros == 0)
        {
            contadorIntervaloTiro = tempoRegarga;
            quantidadesTiros = municao;
        }
        else
        {
            contadorIntervaloTiro = intervaloTiro;
            quantidadesTiros -= 1;
        }
        GameObject temp = Instantiate(balaProjetil);
        temp.transform.position = arma.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(forcaTiro, 0);
        Destroy(temp.gameObject, 3f);
    }

    void procurarJogador()
    {
        Collider2D encontrarJogador = Physics2D.OverlapCircle(transform.position, 25, layerJogador);
        if(encontrarJogador != null)
        {
            PlayerControlador PlayerControlador = encontrarJogador.GetComponent<PlayerControlador>();
            if (PlayerControlador != null)
            {
                Debug.Log("encontrei:" + PlayerControlador.name);
                if (PlayerControlador.transform.position.x > transform.position.x && olhandoEsquerda == false)
                {
                    olhandoEsquerda = true;
                    transform.Rotate(0f, 180f, 0f);
                    forcaTiro *= -1;
                }
                if(PlayerControlador.transform.position.x < transform.position.x && olhandoEsquerda == true)
                {
                    olhandoEsquerda = false;
                    transform.Rotate(0f, 180f, 0f);
                    forcaTiro *= -1;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 25);
    }



}
