using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoPatrulha : MonoBehaviour
{
    [Header("Movimentacao")]
    public bool olhandoParaEsquerda;
    public float velocidade;
    public int direcao = 1;
    public Rigidbody2D rigidbody2d;
    public Vector2 offset;
    private float variavelDeSuporte;
    
    [Header("Layer do Chao")]
    [SerializeField] private LayerMask layerChao; //Variavel de apoio para rechonhecer a layer do chao;
    [SerializeField] private LayerMask layerInimigo;

    private RaycastHit2D paredeDireita;
    private RaycastHit2D paredeEsquerda;
    private RaycastHit2D chaoDireita;
    private RaycastHit2D chaoEsquerda;
    private RaycastHit2D inimigoDireita;
    private RaycastHit2D inimigoEsquerda;


    void Start()
    {
        olhandoParaEsquerda = true;
        variavelDeSuporte = velocidade;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (VidaInimigo.podeMover == true)
        {
            DetectarColisoesParede();
            DetectarColisoesChao();
            //DetectarColisoesInimigo();
            Mover();
        }
        if (!olhandoParaEsquerda && direcao == 1 || olhandoParaEsquerda && direcao == -1)
        {
            flipSprite();
        }
    }

    public void Mover()
    {
        rigidbody2d.velocity = new Vector2(velocidade * direcao, rigidbody2d.velocity.y);
    }

    public void flipSprite()
    {
        olhandoParaEsquerda = !olhandoParaEsquerda;
        transform.Rotate(0f, 180f, 0f);
    }

    public void DetectarColisoesChao()
    {
        chaoDireita = Physics2D.Raycast(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), Vector2.down, 1f, layerChao);
        Debug.DrawRay(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), Vector2.down, Color.red);
        if(chaoDireita.collider == null)
        {
            direcao = -1;
        }

        chaoEsquerda = Physics2D.Raycast(new Vector2(transform.position.x - offset.x, transform.position.y + offset.y), Vector2.down, 1f, layerChao);
        Debug.DrawRay(new Vector2(transform.position.x - offset.x, transform.position.y + offset.y), Vector2.down, Color.red);
        if (chaoEsquerda.collider == null)
        {
            direcao = 1;
        }
    }

    /*public void DetectarColisoesInimigo()
    {
        inimigoDireita = Physics2D.Raycast(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), Vector2.right, 1f, layerInimigo);
        Debug.DrawRay(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), Vector2.right, Color.blue);
        if (inimigoDireita.collider != null)
        {
            direcao = -1;
        }

        inimigoEsquerda = Physics2D.Raycast(new Vector2(transform.position.x - offset.x, transform.position.y + offset.y), Vector2.left, 1f, layerInimigo);
        Debug.DrawRay(new Vector2(transform.position.x - offset.x, transform.position.y + offset.y), Vector2.left, Color.blue);
        if (inimigoEsquerda.collider != null)
        {
            direcao = 1;
        }
    }*/


    public void DetectarColisoesParede()
    {
        paredeDireita = Physics2D.Raycast(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), Vector2.right, 1.5f, layerChao);
        Debug.DrawRay(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), Vector2.right, Color.yellow);
        if (paredeDireita.collider != null)
        {
            direcao = -1;
        }

        paredeEsquerda = Physics2D.Raycast(new Vector2(transform.position.x - offset.x, transform.position.y + offset.y), Vector2.left, 1.5f, layerChao);
        Debug.DrawRay(new Vector2(transform.position.x - offset.x, transform.position.y + offset.y), Vector2.left, Color.yellow);
        if (paredeEsquerda.collider != null)
        {
            direcao = 1;
        }
        
    }

    IEnumerator aumentarVelocidade()
    {
        velocidade = velocidade + 0.8f;
        yield return new WaitForSeconds(Random.Range(0.1f, 1f));
        velocidade = variavelDeSuporte;
    }
}