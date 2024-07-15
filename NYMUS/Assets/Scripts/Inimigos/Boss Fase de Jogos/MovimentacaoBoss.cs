using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoBoss : MonoBehaviour
{

    [Header("Jogador")]
    public Transform jogador;

    [Header("Configurações")]
    public bool seguindoJogador;
    public float velocidade = 2f; // Velocidade de seguimento do jogaor
    public float delay = 0.5f; //Atraso em segundos

    private Vector3 posicaoAlvo; //Posicao do alvo para seguir
    // Start is called before the first frame update
    void Start()
    {
        posicaoAlvo = jogador.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (seguindoJogador)
        {
            posicaoAlvo = Vector3.Lerp(posicaoAlvo, jogador.position, velocidade * Time.deltaTime / delay);
            Vector3 novaPosicao = new Vector3(transform.position.x, posicaoAlvo.y, transform.position.z);
            transform.position = novaPosicao;
        }
    }

    public void ligarMovimentação()
    {
        seguindoJogador = true;
    }
}
