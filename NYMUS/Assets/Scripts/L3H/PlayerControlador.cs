using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerControlador : MonoBehaviour
{
    public Rigidbody2D rb; // rb = rigidbody
    public BoxCollider2D bc; //bc = box collider 
    public int velocidade; // Velocidade maxima do jogador
    private float direcao; // Direcao que o jogador esta se movimentando (esquerda ou direita)

    public string estadoPulo;        // Diz se o jogador esta no chao ou no ar
    private string Chao = "Chao";    // Variavel de apoio para estadoPulo
    private string Ar = "Ar";        // Variavel de apoio para estadoPulo

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

    /*
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
                quantidadePulos = 2;
                estadoPulo = Ar; //Jogador esta no ar
        }
    }
    */

    private bool estaChao()
    {
        RaycastHit2D chao = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0, Vector2.down, 0.1f, layerChao); // Cria um segundo box collider para reconhecer o chao
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
        inputTeclas();
    }

    void inputTeclas()
    {
        if (possuiPuloDuplo == true)
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
            estaPulando = true;
        }

        if (Input.GetKey(KeyCode.W) && estaPulando == true || Input.GetKey(KeyCode.Space) && estaPulando == true)
        {
            contadorTempoPulo -= Time.deltaTime;
            rb.velocity = Vector2.up * forcaPulo;
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
        {
            estaPulando = false;
            contadorTempoPulo = tempoPulo;
        }
    }

    void inputPuloDuplo()
    {
        if (estaChao() == true)
        {
            quantidadePulos = 2;
        }

        if (Input.GetKeyDown(KeyCode.W) && quantidadePulos > 0 || Input.GetKeyDown(KeyCode.Space) && quantidadePulos > 0)
        {
            estaPulando = true;
            contadorTempoPulo = tempoPulo;
            quantidadePulos -= 1;
        }

        if (Input.GetKey(KeyCode.W) && estaPulando == true || Input.GetKey(KeyCode.Space) && estaPulando == true)
        {
            contadorTempoPulo -= Time.deltaTime;
            rb.velocity = Vector2.up * forcaPulo;
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
        {
            estaPulando = false;
            contadorTempoPulo = tempoPulo;
        }
    }

}
