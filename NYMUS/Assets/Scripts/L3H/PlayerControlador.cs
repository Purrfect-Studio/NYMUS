using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class PlayerControlador : MonoBehaviour
{
    [Header("RigidBody")]
    public Rigidbody2D rigidBody2D;   // rb = rigidbody
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
    [Header("Layer da Escada")]
    [SerializeField] private LayerMask layerEscada;
    [Header("Layer da Plataforma")]
    [SerializeField] private LayerMask layerPlataforma;

    [Header("Pulo")]
    public bool possuiPuloDuplo;     // true = ativa o pulo duplo / false = desativa o pulo duplo
    public float forcaPulo;          // Quanto maior o valor mais alto o pulo
    public float tempoPulo;          // Tempo maximo do pulo antes de cair
    public static bool estaPulando;  // Diz se o jogador esta pulando ou nao
    private float contadorTempoPulo; // Contador de qunato tempo esta pulando
    public int quantidadeDePulosExtras;
    public int puloExtra;       // Quantidade de pulos que o jogador pode dar
    public float gravidade;

    [Header("Ataque Melee")]
    public float dano;
    public GameObject pontoDeAtaque; // Ponto de onde se origina o ataque
    public float alcanceAtaque;     // Area de alcance do ataque

    [Header("Ataque Ranged")]
    public bool possuiAtaqueRanged;
    public BarraDeEnergia barraDeEnergia;
    public GameObject projetilL3h;
    public float danoAtaqueRanged;
    public float velocidadeAtaqueRanged;
    public float duracaoAtaqueRanged;
    public float energiaMaximaAtaqueRanged;
    private float energiaRestanteAtaqueRanged;
    //public float cooldownParaRestaurarEnergia;
    //private float cooldownRestanteParaRestaurarEnergia;
    public float cooldownEntreAtaquesRanged;
    private float cooldownRestanteEntreAtaquesRanged;
    [SerializeField] public static float danoRanged;

    [Header("Escada")]
    public bool estaSubindoEscada;
    public bool estaDescendoEscada;
    public bool podeInteragirEscada;
    public float velocidadeEscada;

    [Header("CheckPoint")]
    public GameObject ultimoCheckpoint;

    [Header("Chave")]
    public Inventario inventario;
    public bool estaInteragindo { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animacao = GetComponent<Animator>();
        inventario = GetComponent<Inventario>();
        if(possuiAtaqueRanged)
        {
            barraDeEnergia.definirEnergiaMaxima(energiaMaximaAtaqueRanged);
        }

        podeMover = true;
        estaSubindoEscada = false;
        estaDescendoEscada = false;
        podeInteragirEscada = false;
        estaPulando = false;

        contadorTempoPulo = tempoPulo;
        puloExtra = quantidadeDePulosExtras;
        gravidade = rigidBody2D.gravityScale;

        olhandoDireita = true;
        direcao = 1;

        danoRanged = danoAtaqueRanged;
        energiaRestanteAtaqueRanged = energiaMaximaAtaqueRanged;
        //cooldownRestanteParaRestaurarEnergia = cooldownParaRestaurarEnergia;
        cooldownRestanteEntreAtaquesRanged = cooldownEntreAtaquesRanged;

        Physics2D.IgnoreLayerCollision(8, 13, false);
    }

    private bool estaPlataforma()
    {
        RaycastHit2D plataforma = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.3f, layerPlataforma); // Cria um segundo box collider para reconhecer o chao
        return plataforma.collider != null; //Retorna um valor verdadeiro, dizendo que encostou no chao
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
            if (GrudarObjeto.jogadorEstaGrudadoEmUmaCaixa == false && estaSubindoEscada == false && estaDescendoEscada == false)
            {
                pulo();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(podeMover == true)
        {
            verificarSubirEscada();
            if (podeInteragirEscada == true)
            {
                rigidBody2D.gravityScale = 0;
            }
            else
            {
                rigidBody2D.gravityScale = gravidade;
                estaSubindoEscada = false;
                estaDescendoEscada = false;
            }

            interagir();
            if (GrudarObjeto.jogadorEstaGrudadoEmUmaCaixa == false && estaSubindoEscada == false && estaDescendoEscada == false)
            {
                verificarPuloDuplo();
                if (possuiAtaqueRanged)
                {
                    ataqueRanged();
                }
                //ataque();
            }

            CairDaPlataforma();
        }
        else
        {
            rigidBody2D.velocity = Vector2.zero;
        }

        restaurarQuantidadeAtaqueRangedDisponiveis();
    }

    void andar()
    {
        //<- = -1
        //-> = 1
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1 * velocidade * Time.deltaTime, 0, 0);
        }
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(1 * velocidade * Time.deltaTime, 0, 0);
        }
        direcao = Input.GetAxis("Horizontal");
        //rigidBody2D.velocity = new Vector2(direcao * velocidade, rigidBody2D.velocity.y);
        // O primeiro par�metro da Vector recebe o valor de for�a aplicada no vetor. A dire��o pega se o valor � positivo (direita) ou negativo (esquerda) e aplica a velocidade
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
            if (contadorTempoPulo > 0 && possuiPuloDuplo == false || possuiPuloDuplo == true && (puloExtra > 0 && contadorTempoPulo > 0)) // se estaPulando for true e o tempo do pulo for maior que zero:
            {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, forcaPulo); //Aplica uma for�a vertical no jogador para faze-lo pular
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
        if (Input.GetKeyDown(KeyCode.Space) && estaChao() == true || Input.GetKeyDown(KeyCode.W) && estaChao() == true || Input.GetKeyDown(KeyCode.UpArrow) && estaChao() == true)
        {
            // se o jogador preciona W ou Espaco e ele esta no chao estaPulando=true
            estaPulando = true;
        }

        if (Input.GetKey(KeyCode.Space) && estaPulando == true || Input.GetKey(KeyCode.W) && estaPulando == true || Input.GetKey(KeyCode.UpArrow) && estaPulando == true)
        {
            // se o jogador segura W ou Espaco e estaPulando=true comeca a diminuir o contador do tempo de pulo e cria um vetor de velocidade para cima
            contadorTempoPulo -= Time.deltaTime;
            rigidBody2D.velocity = Vector2.up * forcaPulo;
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
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
            puloExtra = quantidadeDePulosExtras;
        }

        if (Input.GetKeyDown(KeyCode.W) && puloExtra > 0 || Input.GetKeyDown(KeyCode.Space) && puloExtra > 0 || Input.GetKeyDown(KeyCode.UpArrow) && puloExtra > 0)
        {
            // se o jogador preciona W ou Espaco e a quantidade de pulos disponiveis for maior que 0-
            // estaPulando=true reseta o tempo do pulo e diminui 1 na quantidadePulos
            estaPulando = true;
            contadorTempoPulo = tempoPulo;
            puloExtra--;
        }

        if (Input.GetKey(KeyCode.W) && estaPulando == true || Input.GetKey(KeyCode.Space) && estaPulando == true || Input.GetKey(KeyCode.UpArrow) && estaPulando == true)
        {
            // se o jogador segura W ou Espaco e estaPulando=true comeca a diminuir o contador do tempo de pulo e cria um vetor de velocidade para cima
            contadorTempoPulo -= Time.deltaTime;
            rigidBody2D.velocity = Vector2.up * forcaPulo;
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            //quando o jogador solta o W ou o Espaco faz o jogador cair com estaPulando=false e reseta o contador de tempo do pulo
            estaPulando = false;
            contadorTempoPulo = tempoPulo;
        }
    }

    void ataqueRanged()
    {
        if (Input.GetKeyDown(KeyCode.Q) && energiaRestanteAtaqueRanged > 0 /*&& cooldownRestanteEntreAtaquesRanged <= 0*/)
        {
            energiaRestanteAtaqueRanged--;
            barraDeEnergia.ajustarBarraDeEnergia(energiaRestanteAtaqueRanged);
            //cooldownRestanteEntreAtaquesRanged = cooldownEntreAtaquesRanged;
            if (velocidadeAtaqueRanged > 0 && !olhandoDireita || velocidadeAtaqueRanged < 0 && olhandoDireita)
            {
                velocidadeAtaqueRanged *= -1;
            }
            GameObject temp = Instantiate(projetilL3h);
            temp.transform.position = pontoDeAtaque.transform.position;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeAtaqueRanged, 0);
            if(velocidadeAtaqueRanged < 0)
            {
                temp.transform.Rotate(0f, 180f, 0f);
            }
            Destroy(temp.gameObject, duracaoAtaqueRanged); 
        }
    }

    void restaurarQuantidadeAtaqueRangedDisponiveis()
    {
        if (energiaRestanteAtaqueRanged < energiaMaximaAtaqueRanged)
        {
            energiaRestanteAtaqueRanged += Time.deltaTime/2;
            barraDeEnergia.ajustarBarraDeEnergia(energiaRestanteAtaqueRanged);
            //if (cooldownRestanteParaRestaurarEnergia <= 0)
            //{
            //    energiaRestanteAtaqueRanged++;
            //    cooldownRestanteParaRestaurarEnergia = cooldownParaRestaurarEnergia;
            //}
        }
        //if(cooldownRestanteEntreAtaquesRanged > -1)
        //{
        //    cooldownRestanteEntreAtaquesRanged -= Time.deltaTime;
        //}
        
    }

    /*void ataque()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Collider2D acertarInimigo = Physics2D.OverlapCircle(pontoDeAtaque.transform.position, alcanceAtaque);
            if(acertarInimigo != null)
            {
                VidaInimigo inimigo = acertarInimigo.GetComponent<VidaInimigo>();
                if(inimigo != null && VidaInimigo.invulneravel == false)
                {
                    inimigo.tomarDano(dano);
                }
                VidaBoss boss = acertarInimigo.GetComponent<VidaBoss>();
                if (boss != null && VidaBoss.invulneravel == false && VidaBoss.invulnerabilidade == false)
                {
                    Debug.Log("Acertei o boss");
                    boss.tomarDano(dano);
                }
                if(acertarInimigo.CompareTag("PortaBoss") && VidaBoss.invulnerabilidade == true)
                {
                    int index = int.Parse(acertarInimigo.name);
                    GameObject Boss = GameObject.FindGameObjectWithTag("Boss");
                    VidaBoss DerrubarBoss = Boss.GetComponent<VidaBoss>();
                    GameObject PortaVirut = GameObject.FindGameObjectWithTag("ControladorPortasVirut");
                    PortaVirut portaVirut = PortaVirut.GetComponent<PortaVirut>();
                    portaVirut.jogadorDesativaPorta(index);
                    DerrubarBoss.derrubarBoss();
                }

            }
        }
    }*/

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
    void subirEscada()
    {
        if (estaDescendoEscada == true || estaSubindoEscada == true)
        {
            if (estaDescendoEscada == true)
            {
                rigidBody2D.velocity = Vector2.down * velocidadeEscada;
            }
            if (estaSubindoEscada == true)
            {
                rigidBody2D.velocity = Vector2.up * velocidadeEscada;
            }
        }
        else
        {
            rigidBody2D.velocity = Vector2.zero;
        }
    }
    void verificarSubirEscada()
    {
        if(colisaoEscada())
        {
            podeInteragirEscada = true;
            if (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                estaSubindoEscada = true;
            }
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                estaSubindoEscada = false;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                estaDescendoEscada = true;
            }
            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                estaDescendoEscada = false;
            }
            subirEscada();
        }
        else
        {
            podeInteragirEscada = false;
        }
    }

    public void CairDaPlataforma()
    {
        if(estaPlataforma() == true && Input.GetKeyDown(KeyCode.S) || estaPlataforma() == true && Input.GetKeyDown(KeyCode.DownArrow)/* && Input.GetKeyDown(KeyCode.W)*/)
        {
            StartCoroutine("CairPlataforma");
        }
    }

    IEnumerator CairPlataforma()
    {
        Physics2D.IgnoreLayerCollision(8, 13, true);
        yield return new WaitForSeconds(0.6f);
        Physics2D.IgnoreLayerCollision(8, 13, false);
    }

    public bool colisaoEscada()
    {
        RaycastHit2D colisao = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.05f, layerEscada);
        return colisao.collider != null; 
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
        rigidBody2D.gravityScale = 50;
        animacao.SetBool("estaPulando", false); // Desativa a anima��o de pulo
        animacao.SetBool("estaAndando", false); // Desativa a anima��o de ataque
        animacao.SetBool("tomarDano", false); // Desativa a anima��o de ataque
        animacao.Play("Parado");
    }

    public void LiberarMovimentacao()
    {
        podeMover = true; // Permite que o jogador se mova novamente
        rigidBody2D.gravityScale = gravidade;
        VidaJogador.invulneravel = false;
    }

    public void TravarMovimentacaoPorUmTempo(float tempo)
    {
        StartCoroutine(travarPorUmTempo(tempo));
    }

    IEnumerator travarPorUmTempo(float tempo)
    {

        podeMover = false;
        VidaJogador.invulneravel = true;
        rigidBody2D.velocity = Vector2.zero; // Zera a velocidade do jogador
        yield return new WaitForSeconds(tempo);
        podeMover = true;
        VidaJogador.invulneravel = false;
    }

    public void DefinirCheckpoint(GameObject checkpoint)
    {
        ultimoCheckpoint = checkpoint;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "PlataformaMove")
        {
            transform.parent = collision.transform;
        }
        if (collision.transform.tag == "Escada")
        {
            podeInteragirEscada = true;
            rigidBody2D.gravityScale = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "PlataformaMove")
        {
            transform.parent = null;
        }
        if (collision.transform.tag == "Escada")
        {
           podeInteragirEscada = false;
           rigidBody2D.gravityScale = gravidade;
        }
    }
}
