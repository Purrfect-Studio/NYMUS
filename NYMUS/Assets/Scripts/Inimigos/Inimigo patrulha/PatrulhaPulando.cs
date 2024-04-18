using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrulhaPulando : MonoBehaviour
{
    public Transform DetectaParede;
    public float distancia = 3;
    public bool olhandoParaEsquerda;
    public float velocidade = 4;
    public BoxCollider2D bcChao; // bc = box chao
    public BoxCollider2D bcParede; // bc = box parede
    public Rigidbody2D rb;

    [SerializeField] private LayerMask layerChao; //Variavel de apoio para rechonhecer a layer do chao;

    void Start()
    {

        olhandoParaEsquerda = true;
        Pular();
    }

    void Update()
    {
        Patrulha();
    }

    private bool parede()
    {
        RaycastHit2D chao = Physics2D.BoxCast(bcParede.bounds.center, bcParede.bounds.size, 0, Vector2.down, 0.05f, layerChao); // Cria um segundo box collider para reconhecer o chao
        return chao.collider != null; //Retorna um valor verdadeiro, dizendo que encostou no chao
    }

    private bool chao()
    {
        RaycastHit2D chao = Physics2D.BoxCast(bcChao.bounds.center, bcChao.bounds.size, 0, Vector2.down, 0.05f, layerChao); // Cria um segundo box collider para reconhecer o chao
        return chao.collider != null; //Retorna um valor verdadeiro, dizendo que encostou no chao
    }

    public void Patrulha()
    {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);

        if (chao() == false || parede() == true)
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
        }
    }

    public void Pular()
    {
        rb.AddForce(new Vector2(10, 100), ForceMode2D.Impulse);
        Invoke("Pular", 3);
    }
}