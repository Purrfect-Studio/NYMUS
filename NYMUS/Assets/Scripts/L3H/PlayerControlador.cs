using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class PlayerControlador : MonoBehaviour
{
    [Header("RigidBody")]
    private Rigidbody2D rigidBody2D;   // rb = rigidbody
    [Header("BoxCollider")]
    private BoxCollider2D boxCollider2D; // bc = box collider 
    [Header("Animator")]
    private Animator animacao;

    [Header("Andar")]
    public int velocidade;   // Velocidade maxima do jogador
    private float direcao;   // Direcao que o jogador esta se movimentando (esquerda(-1) ou direita(1))
    public static bool olhandoDireita;           // Direcao para girar o sprite
    public static bool podeMover;    // Diz se o jogador pode se movimentar ou nao

    [Header("Layer do Chao")]
    [SerializeField] private LayerMask layerChao; //Variavel de apoio para rechonhecer a layer do chao

    [Header("Pulo")]
    public bool possuiPuloDuplo;     // true = ativa o pulo duplo / false = desativa o pulo duplo
    public float forcaPulo;          // Quanto maior o valor mais alto o pulo
    public float tempoPulo;          // Tempo maximo do pulo antes de cair
    public static bool estaPulando;  // Diz se o jogador esta pulando ou nao
    private float contadorTempoPulo; // Contador de qunato tempo esta pulando
    private int puloExtra = 1;       // Quantidade de pulos que o jogador pode dar

    [Header("Ataque")]
    public float dano;
    public GameObject pontoDeAtaque; // Ponto de onde se origina o ataque
    public float alcanceAtaque;     // Area de alcance do ataque

    public bool estaInteragindo { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animacao = GetComponent<Animator>();
        podeMover = true;
        contadorTempoPulo = tempoPulo;

        olhandoDireita = true;
        direcao = 1;
    }

    private bool estaChao()
    {
        RaycastHit2D chao = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.3f, layerChao); // Cria um segundo box collider para reconhecer o chao
        return chao.collider != null; //Retorna um valor verdadeiro, dizendo que encostou no chao
    }

    private void FixedUpdate()
    {
        if (podeMover == true)
        {
            andar();
            if(GrudarObjeto.jogadorEstaGrudadoEmUmaCaixa ==false)
            {
                pulo();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        interagir();
        if (podeMover == true && GrudarObjeto.jogadorEstaGrudadoEmUmaCaixa == false)
        {
            verificarPuloDuplo();
            ataque();
        }
    }

    void andar()
    {
        //<- = -1
        //-> = 1
        direcao = Input.GetAxis("Horizontal");
        rigidBody2D.velocity = new Vector2(direcao * velocidade, rigidBody2D.velocity.y);
        // O primeiro parâmetro da Vector recebe o valor de força aplicada no vetor. A direção pega se o valor é positivo (direita) ou negativo (esquerda) e aplica a velocidade
        if (direcao > 0 && olhandoDireita == false && GrudarObjeto.jogadorEstaGrudadoEmUmaCaixa == false || direcao < 0 && olhandoDireita == true && GrudarObjeto.jogadorEstaGrudadoEmUmaCaixa == false)
        {
           olhandoDireita = !olhandoDireita;
           transform.Rotate(0f, 180f, 0f);
        }
        //se estiver olhando a a direita e andando para a esquerda
        //ou olhando para a esquerda e andando para a direita
        //gira o sprite e inverte a variavel olhandoDireita
    }

    void pulo()
    {
        if (estaPulando == true) 
        {
            if (contadorTempoPulo > 0) // se estaPulando for true e o tempo do pulo for maior que zero:
            {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, forcaPulo); //Aplica uma força vertical no jogador para faze-lo pular
            }
            else
            {
                estaPulando = false; // quando o contador do tempo do pulo chegar a 0 estaPulando=false
            }
        }
    }


    void verificarPuloDuplo()
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
            //animacao.SetBool("estaPulando", true);
        }

        if (Input.GetKey(KeyCode.W) && estaPulando == true || Input.GetKey(KeyCode.Space) && estaPulando == true)
        {
            // se o jogador segura W ou Espaco e estaPulando=true comeca a diminuir o contador do tempo de pulo e cria um vetor de velocidade para cima
            contadorTempoPulo -= Time.deltaTime;
            rigidBody2D.velocity = Vector2.up * forcaPulo;
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
        {
            //quando o jogador solta o W ou o Espaco faz o jogador cair com estaPulando=false e reseta o contador de tempo do pulo
            estaPulando = false;
            contadorTempoPulo = tempoPulo;
            //animacao.SetBool("estaPulando", false);
        }
    }

    void inputPuloDuplo()
    {
        if (estaChao() == true)
        {
            //se o jogador encosta no chao reseta a quantidade de pulos
            puloExtra = 1;
        }

        if (Input.GetKeyDown(KeyCode.W) && puloExtra > 0 || Input.GetKeyDown(KeyCode.Space) && puloExtra > 0)
        {
            // se o jogador preciona W ou Espaco e a quantidade de pulos disponiveis for maior que 0-
            // estaPulando=true reseta o tempo do pulo e diminui 1 na quantidadePulos
            estaPulando = true;
            contadorTempoPulo = tempoPulo;
            --puloExtra;
        }

        if (Input.GetKey(KeyCode.W) && estaPulando == true || Input.GetKey(KeyCode.Space) && estaPulando == true)
        {
            // se o jogador segura W ou Espaco e estaPulando=true comeca a diminuir o contador do tempo de pulo e cria um vetor de velocidade para cima
            contadorTempoPulo -= Time.deltaTime;
            rigidBody2D.velocity = Vector2.up * forcaPulo;
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
        {
            //quando o jogador solta o W ou o Espaco faz o jogador cair com estaPulando=false e reseta o contador de tempo do pulo
            estaPulando = false;
            contadorTempoPulo = tempoPulo;
        }
    }

    void ataque()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            Collider2D acertarInimigo = Physics2D.OverlapCircle(pontoDeAtaque.transform.position, alcanceAtaque);
            if(acertarInimigo != null)
            {
                VidaInimigo inimigo = acertarInimigo.GetComponent<VidaInimigo>();
                if(inimigo != null && VidaInimigo.invulneravel == false)
                {
                    //Debug.Log("Atacando:" + acertarInimigo.name);
                    //Debug.Log("Causei:" + dano + " de dano");
                    inimigo.tomarDano(dano);
                }
            }
        }
    }

    void interagir()
    {
        if (Input.GetButtonDown("Interagir"))
        {
            estaInteragindo = true;
        }
        else
        {
            estaInteragindo = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pontoDeAtaque.transform.position, alcanceAtaque);
    }

    public void TravarMovimentacao()
    {
        VidaJogador.invulneravel = true;
        podeMover = false; // Impede que o jogador se mova
        estaPulando = false; // Impede que o jogador pule
        rigidBody2D.velocity = Vector2.zero; // Zera a velocidade do jogador
        animacao.SetBool("estaPulando", false); // Desativa a animação de pulo
        animacao.SetBool("estaAndando", false); // Desativa a animação de ataque
        animacao.SetBool("tomarDano", false); // Desativa a animação de ataque
    }

    public void LiberarMovimentacao()
    {
        podeMover = true; // Permite que o jogador se mova novamente
        VidaJogador.invulneravel = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "PlataformaMove")
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "PlataformaMove")
        {
            transform.parent = null;
        }
    }

}
