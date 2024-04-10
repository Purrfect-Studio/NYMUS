using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    public Rigidbody2D rb; // rb = rigidbody
    public int velocidade; // Velocidade maxima do jogador
    private float direcao; // Direcao que o jogador esta se movimentando (esquerda ou direita)

    public string estadoPulo;        // Diz se o jogador esta no chao ou no ar
    private string Chao = "Chao";    // Variavel de apoio para estadoPulo
    private string Ar = "Ar";        // Variavel de apoio para estadoPulo

    private bool estaPulando;        // Diz se o jogador esta pulando ou nao
    public float forcaPulo;          // Quanto maior o valor mais alto o pulo
    public float tempoPulo;          // Tempo maximo do pulo antes de cair
    private float contadorTempoPulo; // Contador de qunato tempo esta pulando

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Chao")
        {
            estadoPulo = Chao; //Jogador esta no chao (reseta o pulo)
            estaPulando = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (estadoPulo == Chao)
        {
            if (collision.gameObject.tag == "Chao")
                estadoPulo = Ar; //Jogador esta no ar
        }
    }

    private void FixedUpdate()
    {
        andar();
        if (estaPulando == true)
        {
            if (contadorTempoPulo > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, forcaPulo);
            }
            else
            {
                estaPulando = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && estadoPulo == Chao)
        {
            estaPulando = true;
        }

        if (Input.GetKey(KeyCode.Space) && estaPulando == true)
        {
            contadorTempoPulo -= Time.deltaTime;
            rb.velocity = Vector2.up * forcaPulo;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            estaPulando = false;
            contadorTempoPulo = tempoPulo;
        }
    }

    void andar()
    {
        direcao = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(direcao * velocidade, rb.velocity.y);
        // O primeiro parâmetro da Vector recebe o valor de força aplicada no vetor. A direção pega se o valor é positivo (direita) ou negativo (esquerda) e aplica a velocidade
    }

}
