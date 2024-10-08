using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorTouro : MonoBehaviour
{
    private InimigoPatrulha patrulha;
    private Animator animacao;
    private Rigidbody2D rigidbody2d;
    public bool enchergou;

    [Header("Detectores de colisao")]
    [SerializeField] private LayerMask layerChao;
    public Vector2 offset;
    private RaycastHit2D paredeDireita;
    private RaycastHit2D paredeEsquerda;
    private RaycastHit2D jogadorDireita;
    private RaycastHit2D jogadorEsquerda;

    [Header("Arrancada")]
    public float velocidadeArrancada;
    private int direcaoArrancada;
    public bool podeArrancada;
    public float tempoAtordoado;
    private float cooldownArrancada;

    [Header("Layer do Jogador")]
    public LayerMask layerJogador; // Layer do jogador

    // Start is called before the first frame update
    void Start()
    {
        podeArrancada = false;
        patrulha = GetComponent<InimigoPatrulha>();
        animacao = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();

        patrulha.enabled = true; 
    }

    // Update is called once per frame
    void Update()
    {
        direcaoArrancada = patrulha.direcao;
        //Collider2D encontrarJogador = Physics2D.OverlapBox(transform.position, tamanho, layerJogador);
        if (enchergou == true /*&& (transform.position.x - jogador.transform.position.x < 0 && direcaoArrancada == 1|| transform.position.x - jogador.transform.position.x > 0 && direcaoArrancada == -1)*/)
        {
            patrulha.enabled = false;
            podeArrancada = true;
            enchergou = false;
            animacao.SetBool("estaPuto", true);
        }

        if (podeArrancada)
        {
            arrancada();
            DetectarColisoesParede();
        }

        if(cooldownArrancada > -0.5f)
        {
            cooldownArrancada -=Time.deltaTime;
        }
        if(cooldownArrancada <= 0 && !podeArrancada)
        {
            DetectarJogador();
        }
            
        

        
    }

    public void arrancada()
    {
        rigidbody2d.velocity = new Vector2(velocidadeArrancada * direcaoArrancada, rigidbody2d.velocity.y);
    }

    public void DetectarColisoesParede()
    {
        paredeDireita = Physics2D.Raycast(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), Vector2.right, 1.5f, layerChao);
        Debug.DrawRay(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), Vector2.right, Color.yellow);

        paredeEsquerda = Physics2D.Raycast(new Vector2(transform.position.x - offset.x, transform.position.y + offset.y), Vector2.left, 1.5f, layerChao);
        Debug.DrawRay(new Vector2(transform.position.x - offset.x, transform.position.y + offset.y), Vector2.left, Color.yellow);
        if (paredeEsquerda.collider != null || paredeDireita.collider != null)
        {
            podeArrancada = false;
            animacao.SetBool("Atordoado", true);
            animacao.SetBool("estaPuto", false);
            StartCoroutine("Atordoado");
        }
    }

    IEnumerator Atordoado()
    {
        yield return new WaitForSeconds(tempoAtordoado);
        animacao.SetBool("Atordoado", false);
        patrulha.enabled = true;
        cooldownArrancada = 0.5f;
    }

    private void OnDrawGizmos()
    {
        // Desenha uma esfera de gizmos para visualizar a área de busca
        //Gizmos.DrawWireCube(transform.position, tamanho);
    }

    public void DetectarJogador()
    {
        jogadorDireita = Physics2D.Raycast(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), new Vector2(20, 0), 20f, layerJogador);
        Debug.DrawRay(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), new Vector2(20, 0), Color.blue);

        if (jogadorDireita.collider != null && direcaoArrancada == 1 && !podeArrancada)
        {
            enchergou = true;
        }

        jogadorEsquerda = Physics2D.Raycast(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), new Vector2(-20, 0), 20f, layerJogador);
        Debug.DrawRay(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), new Vector2(-20, 0), Color.blue);

        if (jogadorEsquerda.collider != null && direcaoArrancada == -1 && !podeArrancada)
        {
            enchergou = true;
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jogador"))
        {
            enchergou = true;
        }
        else
        {
            enchergou = false;
        }
    }*/
}
