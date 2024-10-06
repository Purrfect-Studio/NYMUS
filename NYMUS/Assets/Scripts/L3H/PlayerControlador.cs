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
    [Header("SpriteRenderer")]
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Color corOriginal;
    [Header("PlayerData")]
    public PlayerData playerData;

    [Header("Andar")]
    public Vector2 direcao;
    public static bool olhandoDireita;           // Direcao para girar o sprite
    public static bool podeMover;    // Diz se o jogador pode se movimentar ou nao

    [Header("Layer do Chao")]
    [SerializeField] private LayerMask layerChao; //Variavel de apoio para rechonhecer a layer do chao
    [Header("Layer da Escada")]
    [SerializeField] private LayerMask layerEscada; //Variavel de apoio para rechonhecer a layer da escada
    [Header("Layer da Plataforma")]
    [SerializeField] private LayerMask layerPlataforma; //Variavel de apoio para rechonhecer a layer da plataforma one-way

    [Header("Pulo")]
    public static bool estaPulando;  // Diz se o jogador esta pulando ou nao
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
    //Jump
    public bool cortarPulo;
    private bool estaCaindoPulo;
    #region timers pulo
    public float ultimaVezNoChao { get; private set; }
    public float ultimaVezQuePressionouPular { get; private set; }
    #endregion

    [Header("Ataque Melee")]
    public GameObject pontoDeAtaque; // Ponto de onde se origina o ataque

    [Header("Pulo Duplo")]
    public bool podeExecutarPuloDuplo;
    public bool resetPulo;

    [Header("Energia")]
    public BarraDeEnergia barraDeEnergia;
    private float energiaRestante;
    private bool podeRestaurarEnergia = true;
    private float reduzirEnergiaEnquantoCarrega;

    [Header("Ataque Ranged")]
    public GameObject[] projetilL3h;
    private float definirTipoTiro;
    private float contadorCarregarAtaqueRanged;

    [Header("Dash")]
    [HideInInspector] public bool podeDarDash = true;
    private bool estaUsandoDash;
    private TrailRenderer trailRenderer;

    [Header("Escada")]
    public bool estaSubindoEscada;
    public bool estaDescendoEscada;
    public bool podeInteragirEscada;

    [Header("CheckPoint")]
    public GameObject ultimoCheckpoint;

    [Header("Inventario")]
    public Inventario inventario;
    public bool estaInteragindo { get; set; }

    [Header("Efeitos")]
    public bool confusao;
    public SpriteRenderer efeitoVisualConfusao;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animacao = GetComponent<Animator>();
        inventario = GetComponent<Inventario>();
        trailRenderer = GetComponent<TrailRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (playerData.possuiAtaqueRanged || playerData.possuiDash)
        {
            barraDeEnergia.definirEnergiaMaxima(playerData.energiaMaxima);
        }

        podeMover = true;
        estaUsandoDash = false;
        estaSubindoEscada = false;
        estaDescendoEscada = false;
        podeInteragirEscada = false;
        estaPulando = false;

        olhandoDireita = true;

        energiaRestante = playerData.energiaMaxima;
        definirTipoTiro = 0;
        contadorCarregarAtaqueRanged = 0;

        Physics2D.IgnoreLayerCollision(8, 13, false);
        corOriginal = spriteRenderer.color;

        playerData.gravityScale = rigidBody2D.gravityScale;
        SetGravityScale(playerData.gravityScale);

        efeitoVisualConfusao.enabled = false;
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region Timers
        ultimaVezNoChao -= Time.deltaTime;
        ultimaVezQuePressionouPular -= Time.deltaTime;
        #endregion

        #region input movimento
        direcao.x = Input.GetAxis("Horizontal");
        direcao.y = Input.GetAxisRaw("Vertical");
        #endregion

        #region collision checks
        if (!estaPulando)
        {
            //Ground Check
            if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, layerChao) && !estaPulando) //checks if set box overlaps with ground
            {
                ultimaVezNoChao = playerData.coyoteTime; //if so sets the lastGrounded to coyoteTime
                podeExecutarPuloDuplo = false;
                resetPulo = false;
            }
        }
        #endregion

        #region JUMP CHECKS
        if (estaPulando && rigidBody2D.velocity.y < 0)
        {
            estaPulando = false;
        }

        if (ultimaVezNoChao > 0 && !estaPulando)
        {
            cortarPulo = false;

            if (!estaPulando)
                estaCaindoPulo = false;
        }

        //pulo
        if (podePular() && ultimaVezQuePressionouPular > 0)
        {
            estaPulando = true;
            cortarPulo = false;
            estaCaindoPulo = false;
            pulo();
        }
        #endregion

        #region GRAVITY
        if (cortarPulo)
        {
            //maior gravidade apos soltar o botao de pulo
            SetGravityScale(playerData.gravityScale * playerData.multiplicadorGravidadeCortarPulo);
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, Mathf.Max(rigidBody2D.velocity.y, -playerData.velocidadeMaximaCaindo));
            cortarPulo = false;
        }
        else if (rigidBody2D.velocity.y < 0)
        {
            //maior gravidade se estiver caindo
            SetGravityScale(playerData.gravityScale * playerData.multiplicadorGravidadeCaindo);
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, Mathf.Max(rigidBody2D.velocity.y, -playerData.velocidadeMaximaCaindo));
        }
        else
        {
            //gravidade padrao se estiver parado
            SetGravityScale(playerData.gravityScale);
        }
        #endregion

        if (podeMover == true)
        {
            verificarSubirEscada();
            if (podeInteragirEscada == true)
            {
                rigidBody2D.gravityScale = 0;
            }
            else
            {
                //rigidBody2D.gravityScale = playerData.gravityScale;
                estaSubindoEscada = false;
                estaDescendoEscada = false;
            }

            interagir();
            if (GrudarObjeto.jogadorEstaGrudadoEmUmaCaixa == false && estaSubindoEscada == false && estaDescendoEscada == false)
            {
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !podeExecutarPuloDuplo && !resetPulo)
                {
                    inputPulo();
                }
                if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
                {
                    inputUpPulo();
                }
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && resetPulo)
                {
                    estaPulando = true;
                    cortarPulo = false;
                    estaCaindoPulo = false;
                    ResetPulo();
                }
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && podeExecutarPuloDuplo && playerData.possuiPuloDuplo && !resetPulo)
                {
                    estaPulando = true;
                    cortarPulo = false;
                    estaCaindoPulo = false;
                    puloDuplo();
                }


                if (playerData.possuiAtaqueRanged)
                {
                    ataqueRanged();
                }

                if (playerData.possuiDash)
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

        if ((playerData.possuiAtaqueRanged || playerData.possuiDash) && podeRestaurarEnergia)
        {
            restaurarEnergia();
        }       
    }

    void andar()
    {

        /*if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rigidBody2D.velocity = new Vector2(direcao.x * playerData.velocidade * Time.deltaTime, rigidBody2D.velocity.y);
        }*/
        //direcao.x != 0
        if (confusao)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.position -= new Vector3(direcao.x * playerData.velocidade * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-direcao.x * playerData.velocidade * Time.deltaTime, 0, 0);
            }

            if (direcao.x < 0 && olhandoDireita == false && GrudarObjeto.jogadorEstaGrudadoEmUmaCaixa == false || direcao.x > 0 && olhandoDireita == true && GrudarObjeto.jogadorEstaGrudadoEmUmaCaixa == false)
            {
                Flip();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(direcao.x * playerData.velocidade * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.position -= new Vector3(-direcao.x * playerData.velocidade * Time.deltaTime, 0, 0);
            }

            if (direcao.x > 0 && olhandoDireita == false && GrudarObjeto.jogadorEstaGrudadoEmUmaCaixa == false || direcao.x < 0 && olhandoDireita == true && GrudarObjeto.jogadorEstaGrudadoEmUmaCaixa == false)
            {
                Flip();
            }
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

    //teste pulo
    public void SetGravityScale(float scale)
    {
        rigidBody2D.gravityScale = scale;
    }

    private bool podePular()
    {
        return ultimaVezNoChao > 0 && !estaPulando;
    }

    private void pulo()
    {
        if (estaUsandoDash)
        {
            return;
        }
        //Ensures we can't call Jump multiple times from one press
        ultimaVezQuePressionouPular = 0;
        ultimaVezNoChao = 0;
        cortarPulo = false;
        if (playerData.possuiPuloDuplo)
        {
            podeExecutarPuloDuplo = true;
        }

        #region Perform Jump
        //We increase the force applied if we are falling
        //This means we'll always feel like we jump the same amount 
        //(setting the player's Y velocity to 0 beforehand will likely work the same, but I find this more elegant :D)
        float force = playerData.forcaPulo;
        if (rigidBody2D.velocity.y < 0)
            force -= rigidBody2D.velocity.y;

        rigidBody2D.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        #endregion
    }

    private void puloDuplo()
    {
        if (estaUsandoDash || resetPulo)
        {
            return;
        }
        //Ensures we can't call Jump multiple times from one press
        ultimaVezQuePressionouPular = 0;
        ultimaVezNoChao = 0;
        cortarPulo = false;
        podeExecutarPuloDuplo = false;
        #region Perform Jump
        //We increase the force applied if we are falling
        //This means we'll always feel like we jump the same amount 
        //(setting the player's Y velocity to 0 beforehand will likely work the same, but I find this more elegant :D)
        float force = playerData.forcaPulo;
        if (rigidBody2D.velocity.y < 0)
            force -= rigidBody2D.velocity.y;

        rigidBody2D.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        #endregion
    }

    private void ResetPulo()
    {
        if (estaUsandoDash)
        {
            return;
        }
        //Ensures we can't call Jump multiple times from one press
        ultimaVezQuePressionouPular = 0;
        ultimaVezNoChao = 0;
        cortarPulo = false;
        resetPulo = false;
        podeExecutarPuloDuplo = false;
        #region Perform Jump
        //We increase the force applied if we are falling
        //This means we'll always feel like we jump the same amount 
        //(setting the player's Y velocity to 0 beforehand will likely work the same, but I find this more elegant :D)
        float force = playerData.forcaPulo;
        if (rigidBody2D.velocity.y < 0)
            force -= rigidBody2D.velocity.y;

        rigidBody2D.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        #endregion
    }

    public void inputPulo()
    {
        ultimaVezQuePressionouPular = playerData.tempoBufferInputPulo;
    }

    public void inputUpPulo()
    {
        if (rigidBody2D.velocity.y > 0)
        {
            rigidBody2D.AddForce(Vector2.down * rigidBody2D.velocity.y * (1 - playerData.multiplicadorGravidadeCortarPulo), ForceMode2D.Impulse);
        }
        cortarPulo = true;
        ultimaVezQuePressionouPular = 0;
    }

    #region EDITOR METHODS
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
        Gizmos.color = Color.blue;
    }
    #endregion

    void inputDash()
    {
        if (podeDarDash /*&& energiaRestante >= playerData.energiaNecessariaParaDash*/)
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
        rigidBody2D.AddForce(new Vector2(transform.localScale.x, transform.localScale.y + (transform.localScale.y * playerData.forcaDashY)), ForceMode2D.Impulse);
        //rigidBody2D.velocity = new Vector2(transform.localScale.x, transform.localScale.y + (transform.localScale.y * playerData.forcaDashY));
        trailRenderer.emitting = true;       
        //energiaRestante -= playerData.energiaNecessariaParaDash;
        //barraDeEnergia.ajustarBarraDeEnergia(energiaRestante);
        yield return new WaitForSeconds(playerData.tempoMaximoDash);
        estaUsandoDash = false;
        rigidBody2D.gravityScale = playerData.gravityScale;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(playerData.cooldownDash);
        podeDarDash = true;
        spriteRenderer.color = new Color(r: (100 / 255f), g: (149 / 255f), b: (237 / 255f));
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = corOriginal;
        
    }

    IEnumerator DashDiagonal()
    {
        podeDarDash = false;
        estaUsandoDash = true;
        rigidBody2D.gravityScale = 0f;
        playerData.forcaDashX -= 3f;
        playerData.forcaDashY -= 3f;
        rigidBody2D.AddForce(new Vector2(transform.localScale.x + (transform.localScale.x * playerData.forcaDashX), transform.localScale.y + (transform.localScale.y * playerData.forcaDashY)), ForceMode2D.Impulse);
        //rigidBody2D.velocity = new Vector2(transform.localScale.x + (transform.localScale.x * playerData.forcaDashX), transform.localScale.y + (transform.localScale.y * playerData.forcaDashY));
        trailRenderer.emitting = true;
        //energiaRestante -= playerData.energiaNecessariaParaDash;
        //barraDeEnergia.ajustarBarraDeEnergia(energiaRestante);
        yield return new WaitForSeconds(playerData.tempoMaximoDash);
        estaUsandoDash = false;
        rigidBody2D.gravityScale = playerData.gravityScale;
        trailRenderer.emitting = false;
        playerData.forcaDashY += 3f;
        playerData.forcaDashX += 3f;
        yield return new WaitForSeconds(playerData.cooldownDash);
        podeDarDash = true;
        spriteRenderer.color = new Color(r: (100 / 255f), g: (149 / 255f), b: (237 / 255f));
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = corOriginal;
    }

    IEnumerator DashReto()
    {
        podeDarDash = false;
        estaUsandoDash = true;
        rigidBody2D.gravityScale = 0f;
        rigidBody2D.AddForce(new Vector2(transform.localScale.x * playerData.forcaDashX, transform.localScale.y), ForceMode2D.Impulse);
        //rigidBody2D.velocity = new Vector2(transform.localScale.x + (transform.localScale.x * playerData.forcaDashX), transform.localScale.y);
        trailRenderer.emitting = true;
        //energiaRestante -= playerData.energiaNecessariaParaDash;
        //barraDeEnergia.ajustarBarraDeEnergia(energiaRestante);
        yield return new WaitForSeconds(playerData.tempoMaximoDash);
        estaUsandoDash = false;
        rigidBody2D.gravityScale = playerData.gravityScale;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(playerData.cooldownDash);
        podeDarDash = true;
        spriteRenderer.color = new Color(r: (100 / 255f), g: (149 / 255f), b: (237 / 255f));
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = corOriginal;
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
            if (playerData.velocidadeAtaqueRanged > 0 && !olhandoDireita || playerData.velocidadeAtaqueRanged < 0 && olhandoDireita)
            {
                playerData.velocidadeAtaqueRanged *= -1;
            }

            if (definirTipoTiro == 2)
            {
                GameObject temp = Instantiate(projetilL3h[2]);
                temp.transform.position = pontoDeAtaque.transform.position;
                temp.GetComponent<Rigidbody2D>().velocity = new Vector2(playerData.velocidadeAtaqueRanged, 0);
                if (playerData.velocidadeAtaqueRanged < 0)
                {
                    temp.transform.Rotate(0f, 180f, 0f);
                }
                Destroy(temp.gameObject, playerData.duracaoAtaqueRanged);
            }else if (definirTipoTiro == 1)
            {
                GameObject temp = Instantiate(projetilL3h[1]);
                temp.transform.position = pontoDeAtaque.transform.position;
                temp.GetComponent<Rigidbody2D>().velocity = new Vector2(playerData.velocidadeAtaqueRanged, 0);
                if (playerData.velocidadeAtaqueRanged < 0)
                {
                    temp.transform.Rotate(0f, 180f, 0f);
                }
                Destroy(temp.gameObject, playerData.duracaoAtaqueRanged);
            }else if (energiaRestante >= 0.5f)
            {
                energiaRestante -= 0.5f;
                barraDeEnergia.ajustarBarraDeEnergia(energiaRestante);
                GameObject temp = Instantiate(projetilL3h[0]);
                temp.transform.position = pontoDeAtaque.transform.position;
                temp.GetComponent<Rigidbody2D>().velocity = new Vector2(playerData.velocidadeAtaqueRanged, 0);
                if (playerData.velocidadeAtaqueRanged < 0)
                {
                    temp.transform.Rotate(0f, 180f, 0f);
                }
                Destroy(temp.gameObject, playerData.duracaoAtaqueRanged);
            }

            definirTipoTiro = 0;
            contadorCarregarAtaqueRanged = 0;
            reduzirEnergiaEnquantoCarrega = 0;
            podeRestaurarEnergia = true;
        }
    }

    void restaurarEnergia()
    {
        if (energiaRestante < playerData.energiaMaxima)
        {
            energiaRestante += Time.deltaTime*playerData.velocidadeRegeneracaoDeEnergia;
            barraDeEnergia.ajustarBarraDeEnergia(energiaRestante);
        }        
    }

    void interagir()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
                rigidBody2D.velocity = Vector2.down * playerData.velocidadeEscada;
            }
            if (estaSubindoEscada == true)
            {
                rigidBody2D.velocity = Vector2.up * playerData.velocidadeEscada;
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
        rigidBody2D.gravityScale = playerData.gravityScale;
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

    #region Efeitos
    public void causarConfusao(float duracao)
    {
        confusao = true;
        efeitoVisualConfusao.enabled = true;
        StartCoroutine(removerConfusao(duracao));
    }

    IEnumerator removerConfusao(float duracao)
    {
        yield return new WaitForSeconds(duracao-2);
        for(int i = 0; i < 5; i++)
        {
            efeitoVisualConfusao.enabled = !efeitoVisualConfusao.enabled;
            yield return new WaitForSeconds(0.2f);
        }
        
        for(int i = 0; i < 10; i++)
        {
            efeitoVisualConfusao.enabled = !efeitoVisualConfusao.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        efeitoVisualConfusao.enabled = false;
        confusao = false;
    }
    #endregion

    public void receberPuloDuplo()
    {
        playerData.possuiPuloDuplo = true;
    }

    public void aumentarDano(float novoDano)
    {
        playerData.dano += novoDano;
    }

    public void receberEscudoPermanente(float novoEscudoPermanente)
    {
        playerData.escudoPermanente += novoEscudoPermanente;
        VidaJogador vida = GetComponent<VidaJogador>();
        vida.barraDeEscudo.ajustarBarraDeEscudo(novoEscudoPermanente);
        vida.receberEscudo(novoEscudoPermanente);
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
           rigidBody2D.gravityScale = playerData.gravityScale;
        }
    }
}