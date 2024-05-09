using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArmadilhaEspinho : MonoBehaviour
{
    [Header("Armadilha")]
    public EdgeCollider2D EdgeCollider2D;
    public Animator animacao;

    [Header("Jogador")]
    public Transform jogador;
    [SerializeField] private LayerMask layerJogador; //Variavel de apoio para rechonhecer a layer do chao;
    [SerializeField] private UnityEvent DanoCausado;
    

    [Header("Temporizadores")]
    public float tempoAtivarArmadilha;
    public float tempoDesativarArmadilha;
    private float temporizador;
    private float temporizador2;
    private bool armadilhaAtiva = false;
    
    // Start is called before the first frame update
    void Start()
    {
        animacao.SetBool("AtivarArmadilha", false);
        temporizador = tempoAtivarArmadilha;
        temporizador2 = tempoDesativarArmadilha;
    }
    private void FixedUpdate()
    {
        ativarArmadilha();
        desativarArmadilha();
    }
    // Update is called once per frame
    void Update()
    {
        if (colisaoJogador() == true && armadilhaAtiva == true)
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
        RaycastHit2D colisao = Physics2D.BoxCast(EdgeCollider2D.bounds.center, EdgeCollider2D.bounds.size, 0, Vector2.down, 0.05f, layerJogador); // Cria um segundo box collider para reconhecer o jogador
        return colisao.collider != null; //Retorna um valor verdadeiro, dizendo que encostou no jogador
    }

    void ativarArmadilha()
    {
        temporizador -= Time.deltaTime;
        if (temporizador < 0 && armadilhaAtiva == false)
        {
            animacao.SetBool("AtivarArmadilha", true);
            armadilhaAtiva = true;
            temporizador2 = tempoDesativarArmadilha;
        }
    }

    void desativarArmadilha()
    {
        temporizador2 -= Time.deltaTime;
        if (temporizador2 < 0 && armadilhaAtiva == true)
        {
            animacao.SetBool("AtivarArmadilha", false);
            armadilhaAtiva = false;
            temporizador = tempoAtivarArmadilha;
        }
    }
}
