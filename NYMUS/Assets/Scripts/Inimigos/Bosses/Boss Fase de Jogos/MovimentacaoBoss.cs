using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoBoss : MonoBehaviour
{

    [Header("Jogador")]
    public Transform jogador;

    [Header("Configurações")]
    [SerializeField] public static bool podeMover;
    [SerializeField] public static bool seguindoJogador;
    public float velocidade = 2f; // Velocidade de seguimento do jogaor
    public float delay = 0.5f; //Atraso em segundos

    [Header("RigidBody")]
    private Rigidbody2D rb;   // rb = rigidbody

    private Vector3 novaPosicao;

    private Vector3 posicaoAlvo; //Posicao do alvo para seguir
    // Start is called before the first frame update
    void Start()
    {
        posicaoAlvo = jogador.position;
        rb = GetComponent<Rigidbody2D>();
        podeMover = false;
    }

    // Update is called once per frame
    void Update()
    {
        posicaoAlvo = Vector3.Lerp(posicaoAlvo, jogador.position, velocidade * Time.deltaTime / delay);
        novaPosicao = new Vector3(transform.position.x, posicaoAlvo.y, transform.position.z);

        SeguirJogador();
    }

    public void SeguirJogador()
    {
        if (seguindoJogador)
        {
            transform.position = novaPosicao;
        }
    }

    public void ligarMovimentação()
    {
        seguindoJogador = true;
        podeMover = true;
    }
}
