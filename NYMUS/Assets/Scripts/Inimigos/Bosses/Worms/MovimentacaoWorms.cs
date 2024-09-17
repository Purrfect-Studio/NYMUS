using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoWorms : MonoBehaviour
{
    [Header("Jogador")]
    private GameObject jogador;

    [Header("Posição Mínima do X")]
    public float minX; // Movimentação máxima para a esquerda
    [Header("Posição Máxima do X")]
    public float maxX; // Movimentação máxima para a direita

    [Header("Delay para alcançar o Jogador")]
    public float delayTempo; // Delay da câmera chegar no jogador
    private bool seguindoJogador; // Flag para indicar se a câmera está seguindo o jogador

    [Header("Velocidade quando tiver perto")]
    public float velocidade;

    // Start is called before the first frame update
    void Start()
    {
        seguindoJogador = true;
        jogador = GameObject.FindGameObjectWithTag("Jogador");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (seguindoJogador)
        {
            Vector3 novaPosicao = jogador.transform.position; // Afasta a câmera do cenário
            if (transform.position.x - jogador.transform.position.x > 10 || transform.position.x - jogador.transform.position.x < -10)
            {
                novaPosicao = Vector3.Lerp(transform.position, novaPosicao, delayTempo); // Delay para a câmera chegar no jogador
                transform.position = new Vector3(Mathf.Clamp(novaPosicao.x, minX, maxX), transform.position.y, novaPosicao.z);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(jogador.transform.position.x, transform.position.y), velocidade * Time.deltaTime);
            }
        }
    }


}
