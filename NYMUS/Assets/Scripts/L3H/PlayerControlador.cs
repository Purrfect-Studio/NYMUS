using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerControlador : MonoBehaviour
{
    public Rigidbody2D rb;   // rb = rigidbody
    public BoxCollider2D bc; // bc = box collider 
    public int velocidade;   // Velocidade maxima do jogador
    private float direcao;   // Direcao que o jogador esta se movimentando (esquerda ou direita)

    [SerializeField] private LayerMask layerChao; //Variavel de apoio para rechonhecer a layer do chao;

    private int quantidadePulos = 2; // Quantidade de pulos que o jogador pode dar
    public bool possuiPuloDuplo;     // true = ativa o pulo duplo / false = desativa o pulo duplo
    private bool estaPulando;        // Diz se o jogador esta pulando ou nao
    public float forcaPulo;          // Quanto maior o valor mais alto o pulo
    public float tempoPulo;          // Tempo maximo do pulo antes de cair
    private float contadorTempoPulo; // Contador de qunato tempo esta pulando

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private bool estaChao()
    {
        RaycastHit2D chao = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0, Vector2.down, 0.05f, layerChao); // Cria um segundo box collider para reconhecer o chao
        return chao.collider != null; //Retorna um valor verdadeiro, dizendo que encostou no chao
    }

    private void FixedUpdate()
    {
        andar();
        pulo();
    }

    void andar()
    {
        direcao = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(direcao * velocidade, rb.velocity.y);
        // O primeiro parâmetro da Vector recebe o valor de força aplicada no vetor. A direção pega se o valor é positivo (direita) ou negativo (esquerda) e aplica a velocidade
    }

    void pulo()
    {
        if (estaPulando == true) 
        {
            if (contadorTempoPulo > 0) // se estaPulando for true e o tempo do pulo for maior que zero:
            {
                rb.velocity = new Vector2(rb.velocity.x, forcaPulo); //Aplica uma força vertical no jogador para faze-lo pular
            }
            else
            {
                estaPulando = false; // quando o contador do tempo do pulo chegar a 0 estaPulando=false
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        inputTeclas();
    }

    void inputTeclas()
    {
        if (possuiPuloDuplo == true) // verifica se o jogador possui pulo duplo para escolher qual metodo de pulo chamar
        {
            inputPuloDuplo();
        }
        else
        {
            inputPuloSimples();
        }
    }

    void inputPuloSimples()
    {
        if (Input.GetKeyDown(KeyCode.W) && estaChao() == true || Input.GetKeyDown(KeyCode.Space) && estaChao() == true)
        {
            // se o jogador preciona W ou Espaco e ele esta no chao estaPulando=true
            estaPulando = true;
        }

        if (Input.GetKey(KeyCode.W) && estaPulando == true || Input.GetKey(KeyCode.Space) && estaPulando == true)
        {
            // se o jogador segura W ou Espaco e estaPulando=true comeca a diminuir o contador do tempo de pulo e cria um vetor de velocidade para cima
            contadorTempoPulo -= Time.deltaTime;
            rb.velocity = Vector2.up * forcaPulo;
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
        {
            //quando o jogador solta o W ou o Espaco faz o jogador cair com estaPulando=false e reseta o contador de tempo do pulo
            estaPulando = false;
            contadorTempoPulo = tempoPulo;
        }
    }

    void inputPuloDuplo()
    {
        if (estaChao() == true)
        {
            //se o jogador encosta no chao reseta a quantidade de pulos
            quantidadePulos = 2;
        }

        if (Input.GetKeyDown(KeyCode.W) && quantidadePulos > 0 || Input.GetKeyDown(KeyCode.Space) && quantidadePulos > 0)
        {
            // se o jogador preciona W ou Espaco e a quantidade de pulos disponiveis for maior que 0-
            // estaPulando=true reseta o tempo do pulo e diminui 1 na quantidadePulos
            estaPulando = true;
            contadorTempoPulo = tempoPulo;
            quantidadePulos -= 1;
        }

        if (Input.GetKey(KeyCode.W) && estaPulando == true || Input.GetKey(KeyCode.Space) && estaPulando == true)
        {
            // se o jogador segura W ou Espaco e estaPulando=true comeca a diminuir o contador do tempo de pulo e cria um vetor de velocidade para cima
            contadorTempoPulo -= Time.deltaTime;
            rb.velocity = Vector2.up * forcaPulo;
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
        {
            //quando o jogador solta o W ou o Espaco faz o jogador cair com estaPulando=false e reseta o contador de tempo do pulo
            estaPulando = false;
            contadorTempoPulo = tempoPulo;
        }
    }

}
