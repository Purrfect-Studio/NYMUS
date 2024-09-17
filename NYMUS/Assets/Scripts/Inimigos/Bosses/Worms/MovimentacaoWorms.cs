using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoWorms : MonoBehaviour
{
    [Header("Jogador")]
    private GameObject jogador;

    [Header("Posi��o M�nima do X")]
    public float minX; // Movimenta��o m�xima para a esquerda
    [Header("Posi��o M�xima do X")]
    public float maxX; // Movimenta��o m�xima para a direita

    [Header("Delay para alcan�ar o Jogador")]
    public float delayTempo; // Delay da c�mera chegar no jogador
    private bool seguindoJogador; // Flag para indicar se a c�mera est� seguindo o jogador

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
            Vector3 novaPosicao = jogador.transform.position; // Afasta a c�mera do cen�rio
            if (transform.position.x - jogador.transform.position.x > 10 || transform.position.x - jogador.transform.position.x < -10)
            {
                novaPosicao = Vector3.Lerp(transform.position, novaPosicao, delayTempo); // Delay para a c�mera chegar no jogador
                transform.position = new Vector3(Mathf.Clamp(novaPosicao.x, minX, maxX), transform.position.y, novaPosicao.z);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(jogador.transform.position.x, transform.position.y), velocidade * Time.deltaTime);
            }
        }
    }


}
