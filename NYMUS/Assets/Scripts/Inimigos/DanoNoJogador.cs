using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DanoNoJogador : MonoBehaviour
{
    [SerializeField] public CircleCollider2D ccCookie;
    [SerializeField] private UnityEvent DanoCausado;
    [SerializeField] private LayerMask layerJogador; //Variavel de apoio para rechonhecer a layer do chao;
    public float dano;
     // 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (colisaoJogador() == true)
        {
            DanoCausado.Invoke();
        }
    }

    private bool colisaoJogador()
    {
        RaycastHit2D colisao = Physics2D.BoxCast(ccCookie.bounds.center, ccCookie.bounds.size, 0, Vector2.down, 0.05f, layerJogador); // Cria um segundo box collider para reconhecer o chao
        return colisao.collider != null; //Retorna um valor verdadeiro, dizendo que encostou no chao
    }
}
