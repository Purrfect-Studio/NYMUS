using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConfusaoTrojan : MonoBehaviour
{
    private irAteJogador irAteJogador;
    private ProcurarJogador procurarJogador;
    private Rigidbody2D rigidbody2d;
    private GameObject jogador;

    public float duracaoConfusao;
    public float danoNoJogador;
    public float velocidade;
    public float tempoPerseguirJogador;
    private float tempoRestantePerseguirJogador;

    private Vector3 posicaoJogador;
    private Vector2 direcaoMover;

    private bool definirDirecoes;
    // Start is called before the first frame update
    void Start()
    {
        irAteJogador = GetComponent<irAteJogador>();
        procurarJogador = GetComponent<ProcurarJogador>();
        rigidbody2d = GetComponent<Rigidbody2D>();

        jogador = GameObject.FindWithTag("Jogador");

        tempoRestantePerseguirJogador = tempoPerseguirJogador;
        definirDirecoes = true;

        duracaoConfusao = SeletorDeDificuldade.duracaoConfusaoTrojan;
        danoNoJogador = SeletorDeDificuldade.danoProjetilConfusaoTrojan;
    }

    // Update is called once per frame
    void Update()
    {
        if(tempoRestantePerseguirJogador > -1)
        {
            tempoRestantePerseguirJogador -= Time.deltaTime;
        }

        if (tempoRestantePerseguirJogador < 0)
        {
            if (definirDirecoes)
            {
                direcoes();
                moverReto();
            }
            irAteJogador.enabled = false;
            procurarJogador.enabled = false;
        }
    }

    public void moverReto()
    {
        rigidbody2d.velocity = direcaoMover * velocidade;
    }

    public void direcoes()
    {
        definirDirecoes = false;
        posicaoJogador = jogador.transform.position;
        direcaoMover = (posicaoJogador - this.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (colisaoJogador(collision))
        {
            CausarDanoNoJogador(collision);
        }
    }

    public void CausarDanoNoJogador(Collider2D collision)
    {
        VidaJogador vidaJogador = collision.GetComponent<VidaJogador>();
        PlayerControlador playerControlador = collision.GetComponent<PlayerControlador>();
        if (vidaJogador != null && !VidaJogador.invulneravel)
        {
            if (transform.position.x <= jogador.transform.position.x)
            {
                VidaJogador.knockbackParaDireita = -1;
            }
            else
            {
                VidaJogador.knockbackParaDireita = 1;
            }
            vidaJogador.tomarDano(danoNoJogador);
            playerControlador.causarConfusao(duracaoConfusao);
            Destroy(gameObject);
        }
    }

    private bool colisaoJogador(Collider2D collision)
    {
        return collision.gameObject.CompareTag("Jogador");
    }

}
