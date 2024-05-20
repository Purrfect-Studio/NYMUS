using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoPatrulha : MonoBehaviour
{
    [Header("Movimentacao")]
    public bool olhandoParaEsquerda;
    public float velocidade;
    private float variavelDeSuporte;
    [Header("Box Collider da Parede")]
    public BoxCollider2D bCparede; // bc = box collider
    [Header("Box Collider do Chao")]
    public BoxCollider2D bcChao; // bc = box collider
    [Header("Layer do Chao")]
    [SerializeField] private LayerMask layerChao; //Variavel de apoio para rechonhecer a layer do chao;

    void Start()
    {
        olhandoParaEsquerda = true;
        variavelDeSuporte = velocidade;
    }

    void Update()
    {
        Patrulha();
    }

    private bool parede()
    {
        RaycastHit2D parede = Physics2D.BoxCast(bCparede.bounds.center, bCparede.bounds.size, 0, Vector2.down, 0.05f, layerChao); // Cria um segundo box collider para reconhecer o chao
        return parede.collider != null; //Retorna um valor verdadeiro, dizendo que encostou no chao
    }

    private bool chao()
    {
        RaycastHit2D chao = Physics2D.BoxCast(bcChao.bounds.center, bcChao.bounds.size, 0, Vector2.down, 0.05f, layerChao); // Cria um segundo box collider para reconhecer o chao
        return chao.collider != null; //Retorna um valor verdadeiro, dizendo que encostou no chao
    }
    
    public void Patrulha()
    {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);
        if(DetectaInimigo.encontrouInimigo == true)
        {
            StartCoroutine("aumentarVelocidade");
        }
        if (chao() == false || parede() == true || DetectaInimigo.encontrouInimigo == true)
        {
            if (olhandoParaEsquerda == false)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                olhandoParaEsquerda = true;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                olhandoParaEsquerda = false;
            }
            DetectaInimigo.encontrouInimigo = false;
        }
    }

    IEnumerator aumentarVelocidade()
    {
        velocidade = velocidade + 0.8f;
        yield return new WaitForSeconds(Random.Range(0.1f, 1f));
        velocidade = variavelDeSuporte;
    }
}