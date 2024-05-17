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
            contadorIntervaloTiro = tempoRegarga; //pausa entre tiros
            quantidadesTiros = municao;
        }
        else
        {
            contadorIntervaloTiro = intervaloTiro; //intervalo entre as balas
            quantidadesTiros -= 1;
        }
        GameObject temp = Instantiate(balaProjetil); // invoca a bala do prefab
        temp.transform.position = arma.position; // define a posicao inicial da bala
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(forcaTiro, 0); // aplica uma forca para a bala se mover
        Destroy(temp.gameObject, 3f); // destroi a bala depois de um tempo
    }

    void procurarJogador()
    {
        Collider2D encontrarJogador = Physics2D.OverlapCircle(transform.position, 25, layerJogador);
        if(encontrarJogador != null) // encontrou o jogador
        {
            PlayerControlador PlayerControlador = encontrarJogador.GetComponent<PlayerControlador>(); // verifica se o jogador tem o playerControlador
            if (PlayerControlador != null)
            {
                if (PlayerControlador.transform.position.x > transform.position.x && olhandoEsquerda == false) // inimigo virado para a direita e jogador a esquerda
                {
                    olhandoEsquerda = true;
                    transform.Rotate(0f, 180f, 0f); // flipa o sprite
                    forcaTiro *= -1; // altera a direcao do tiro
                }
                if(PlayerControlador.transform.position.x < transform.position.x && olhandoEsquerda == true) // inimigo virado para a esquerda e jogador a direita
                {
                    olhandoEsquerda = false;
                    transform.Rotate(0f, 180f, 0f); // flipa o sprite
                    forcaTiro *= -1; // altera a direcao do tiro
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 25);
    }
}
