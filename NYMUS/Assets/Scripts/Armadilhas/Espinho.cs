using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Espinho : MonoBehaviour
{
    public BoxCollider2D bcEspinho;
    public Transform jogador;
    [SerializeField] private UnityEvent DanoCausado;
    [SerializeField] private LayerMask layerJogador; //Variavel de apoio para rechonhecer a layer do chao;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (colisaoJogador() == true)
        {
            if (VidaJogador.invulneravel == false)
            {
                if (transform.position.x <= jogador.transform.position.x)
                {
                    VidaJogador.knockbackParaDireita = -1;
                }
                else
                {
                    VidaJogador.knockbackParaDireita = 1;
                }
                DanoCausado.Invoke();
            }
        }
    }

    private bool colisaoJogador()
    {
        RaycastHit2D colisao = Physics2D.BoxCast(bcEspinho.bounds.center, bcEspinho.bounds.size, 0, Vector2.down, 0.05f, layerJogador); // Cria um segundo box collider para reconhecer o jogador
        return colisao.collider != null; //Retorna um valor verdadeiro, dizendo que encostou no jogador
    }
}