using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class PlayerControlador : MonoBehaviour
{
    [Header("RigidBody")]
    public Rigidbody2D rigidBody2D;
    [Header("BoxCollider")]
    private BoxCollider2D boxCollider2D;
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
    [SerializeField] private LayerMask layerEscada; //Variavel de apoio para rechonhecer a layer da escada
    [Header("Layer da Plataforma")]
    [SerializeField] private LayerMask layerPlataforma; //Variavel de apoio para rechonhecer a layer da plataforma one-way

    [Header("Pulo")]
    public bool possuiPuloDuplo;     // true = ativa o pulo duplo / false = desativa o pulo duplo
    public float forcaPulo;          // Quanto maior o valor mais alto o pulo
    public float tempoPulo;          // Tempo maximo do pulo antes de cair
    public static bool estaPulando;  // Diz se o jogador esta pulando ou nao
    private float contadorTempoPulo; // Contador de qunato tempo esta pulando
    public int quantidadeDePulosExtras;
    public int puloExtra;       // Quantidade de pulos que o jogador pode dar
    public float gravidade;
    [Header("CoyoteTime")]
    [SerializeField] private float tempoMaximoCoyote = 0.1f;
    [SerializeField] private bool coyoteTime;
    private float tempoCoyote = 0;

    [Header("Ataque Melee")]
    public float dano;
    public GameObject pontoDeAtaque; // Ponto de onde se origina o ataque
    public float alcanceAtaque;     // Area de alcance do ataque

    [Header("Ataque Ranged")]
    public bool possuiAtaqueRanged;
    public BarraDeEnergia barraDeEnergia;
    public GameObject[] projetilL3h;
    public float velocidadeAtaqueRanged;
    public float duracaoAtaqueRanged;
    public float energiaMaxima;
    private float energiaRestante;
    private float contadorCarregarAtaqueRanged;
    private bool podeRestaurarEnergia = true;
    private float reduzirEnergiaEnquantoCarrega;
    public float definirTipoTiro;

    [Header("Dash")]
    public bool possuiDash;
    public float forcaDashX;
    public float forcaDashY;
    public float tempoMaximoDash;
    public float cooldownDash;
    private bool podeDarDash = true;
    private bool estaUsandoDash; 
    private TrailRenderer trailRenderer;

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
        trailRenderer = GetComponent<TrailRenderer>();
        if(possuiAtaqueRanged)
        {
            barraDeEnergia.definirEnergiaMaxima(energiaMaxima);
        }

        podeMover = true;
        estaUsandoDash = false;
        estaSubindoEscada = false;
        estaDescendoEscada = false;
        podeInteragirEscada = false;
        estaPulando = false;

        contadorTempoPulo = tempoPulo;
        puloExtra = quantidadeDePulosExtras;
        gravidade = rigidBody2D.gravityScale;

        olhandoDireita = true;
        direcao = 1;

        energiaRestante = energiaMaxima;
        definirTipoTiro = 0;
        contadorCarregarAtaqueRanged = 0;

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
                if (possuiPuloDuplo)
                {
                    inputPuloDuplo();
                }
                else
                {
                    inputPuloSimples();
                }
                if (possuiAtaqueRanged)
                {
                    ataqueRanged();
                }
                if (possuiDash)
                {
                    inputDash();
                }
            }

            CairDaPlataforma();
        }
        else
        {
            rigidBody2D.velocity = Vector2.zero;
        }

        if (possuiAtaqueRanged && podeRestaurarEnergia)
        {
            restaurarEnergia();
        }
        
        if(!estaChao() && coyoteTime)
        {
            tempoCoyote += Time.deltaTime;
            if(tempoCoyote > tempoMaximoCoyote)
            {
                coyoteTime = false;
            }
        }
        if(estaChao())
        {
            coyoteTime = true;
            tempoCoyote = 0;
        }
    }

    void andar()
    {
        if (estaUsandoDash)
        {
            return;
        }
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

        if (direcao > 0 && olhandoDireita == false && GrudarObjeto.jogadorEstaGrudadoEmUmaCaixa == false || direcao < 0 && olhandoDireita == true && GrudarObjeto.jogadorEstaGrudadoEmUmaCaixa == false)
        {
            Flip();
        }
        //se estiver olhando a a direita e andando para a esquerda
        //ou olhando para a esquerda e andando para a direita
        //gira o sprite e inverte a variavel olhandoDireita
    }

    void Flip()
    {
        olhandoDireita = !olhandoDireita;
        float x = transform.localScale.x;
        x*=-1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    void pulo()
    {
        if (estaPulando == true) 
        {
            if (contadorTempoPulo > 0 && possuiPuloDuplo == false || possuiPuloDuplo == true && (puloExtra > 0 && contadorTempoPulo > 0)) // se estaPulando for true e o tempo do pulo for maior que zero:
            {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, forcaPulo); //Aplica uma força vertical no jogador para faze-lo pular
            }
            else
            {
                estaPulando = false; // quando o contador do tempo do pulo chegar a 0 estaPulando=false
            }
        }
    }

    void inputPuloSimples()
    {
        if (estaUsandoDash)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && (estaChao() == true || coyoteTime) || Input.GetKeyDown(KeyCode.W) && (estaChao() == true || coyoteTime) || Input.GetKeyDown(KeyCode.UpArrow) && (estaChao() == true || coyoteTime))
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
        if (estaUsandoDash)
        {
            return;
        }
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

    void inputDash()
    {
        if (podeDarDash)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
            {
                StartCoroutine("DashDiagonal");
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
            {
                StartCoroutine("DashCima");
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine("DashReto");
            }
        }
        
    }

    IEnumerator DashCima()
    {
        podeDarDash = false;
        estaUsandoDash = true;
        rigidBody2D.gravityScale = 0f;
        rigidBody2D.velocity = new Vector2(transform.localScale.x, transform.localScale.y * forcaDashY);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(tempoMaximoDash);
        estaUsandoDash = false;
        rigidBody2D.gravityScale = gravidade;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(cooldownDash);
        podeDarDash = true;
    }

    IEnumerator DashDiagonal()
    {
        podeDarDash = false;
        estaUsandoDash = true;
        rigidBody2D.gravityScale = 0f;
        forcaDashX -= 5f;
        forcaDashY -= 5f;
        rigidBody2D.velocity = new Vector2(transform.localScale.x * forcaDashX, transform.localScale.y * forcaDashY);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(tempoMaximoDash);
        estaUsandoDash = false;
        rigidBody2D.gravityScale = gravidade;
        trailRenderer.emitting = false;
        forcaDashY += 5f;
        forcaDashX += 5f;
        yield return new WaitForSeconds(cooldownDash);
        podeDarDash = true;
    }

    IEnumerator DashReto()
    {
        podeDarDash = false;
        estaUsandoDash = true;
        rigidBody2D.gravityScale = 0f;
        rigidBody2D.velocity = new Vector2(transform.localScale.x * forcaDashX, transform.localScale.y);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(tempoMaximoDash);
        estaUsandoDash = false;
        rigidBody2D.gravityScale = gravidade;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(cooldownDash);
        podeDarDash = true;
    }

    void ataqueRanged()
    {
        if(Input.GetKey(KeyCode.Q) && energiaRestante > 0)
        {
            podeRestaurarEnergia = false;
            contadorCarregarAtaqueRanged += Time.deltaTime;
            if (contadorCarregarAtaqueRanged >= 1)
            {
                contadorCarregarAtaqueRanged = 0;
                if (definirTipoTiro < 2)
                {
                    definirTipoTiro += 1;
                }
            }
            if (reduzirEnergiaEnquantoCarrega < 2 && energiaRestante > 0.1f)
            {
                reduzirEnergiaEnquantoCarrega += Time.deltaTime;
                energiaRestante -= Time.deltaTime;
                barraDeEnergia.ajustarBarraDeEnergia(energiaRestante);
            }           
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (velocidadeAtaqueRanged > 0 && !olhandoDireita || velocidadeAtaqueRanged < 0 && olhandoDireita)
            {
                velocidadeAtaqueRanged *= -1;
            }

            if (definirTipoTiro == 2)
            {
                //energiaRestante -= 3;
                //barraDeEnergia.ajustarBarraDeEnergia(energiaRestante);
                GameObject temp = Instantiate(projetilL3h[2]);
                temp.transform.position = pontoDeAtaque.transform.position;
                temp.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeAtaqueRanged, 0);
                if (velocidadeAtaqueRanged < 0)
                {
                    temp.transform.Rotate(0f, 180f, 0f);
                }
                Destroy(temp.gameObject, duracaoAtaqueRanged);
            }else if (definirTipoTiro == 1)
            {
                //energiaRestante -= 2;
                //barraDeEnergia.ajustarBarraDeEnergia(energiaRestante);
                GameObject temp = Instantiate(projetilL3h[1]);
                temp.transform.position = pontoDeAtaque.transform.position;
                temp.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeAtaqueRanged, 0);
                if (velocidadeAtaqueRanged < 0)
                {
                    temp.transform.Rotate(0f, 180f, 0f);
                }
                Destroy(temp.gameObject, duracaoAtaqueRanged);
            }else if (energiaRestante >= 0.5f)
            {
                energiaRestante -= 0.5f;
                barraDeEnergia.ajustarBarraDeEnergia(energiaRestante);
                GameObject temp = Instantiate(projetilL3h[0]);
                temp.transform.position = pontoDeAtaque.transform.position;
                temp.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeAtaqueRanged, 0);
                if (velocidadeAtaqueRanged < 0)
                {
                    temp.transform.Rotate(0f, 180f, 0f);
                }
                Destroy(temp.gameObject, duracaoAtaqueRanged);
            }

            definirTipoTiro = 0;
            contadorCarregarAtaqueRanged = 0;
            reduzirEnergiaEnquantoCarrega = 0;
            podeRestaurarEnergia = true;
        }
    }

    void restaurarEnergia()
    {
        if (energiaRestante < energiaMaxima)
        {
            energiaRestante += Time.deltaTime/2;
            barraDeEnergia.ajustarBarraDeEnergia(energiaRestante);
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
        animacao.SetBool("estaPulando", false); // Desativa a animação de pulo
        animacao.SetBool("estaAndando", false); // Desativa a animação de ataque
        animacao.SetBool("tomarDano", false); // Desativa a animação de ataque
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