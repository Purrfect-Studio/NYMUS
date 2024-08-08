using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueMeleeL3h : MonoBehaviour
{
    public GameObject jogador;
    public PlayerControlador playerControlador;
    public CircleCollider2D circleCollider;
    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Jogador");
        playerControlador = jogador.GetComponent<PlayerControlador>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D acertarInimigo)
    {
        //Collider2D acertarInimigo = Physics2D.OverlapCircle(transform.position, playerControlador.alcanceAtaque);
        if (acertarInimigo != null)
        {
            VidaInimigo inimigo = acertarInimigo.GetComponent<VidaInimigo>();
            if (inimigo != null && VidaInimigo.invulneravel == false)
            {
                inimigo.tomarDano(playerControlador.dano);
            }
            VidaBoss boss = acertarInimigo.GetComponent<VidaBoss>();
            if (boss != null && VidaBoss.invulneravel == false && VidaBoss.invulnerabilidade == false)
            {
                Debug.Log("Acertei o boss");
                boss.tomarDano(playerControlador.dano);
            }
            if (acertarInimigo.CompareTag("PortaBoss") && VidaBoss.invulnerabilidade == true)
            {
                int index = int.Parse(acertarInimigo.name);
                GameObject Boss = GameObject.FindGameObjectWithTag("Boss");
                VidaBoss DerrubarBoss = Boss.GetComponent<VidaBoss>();
                GameObject PortaVirut = GameObject.FindGameObjectWithTag("ControladorPortasVirut");
                PortaVirut portaVirut = PortaVirut.GetComponent<PortaVirut>();
                portaVirut.jogadorDesativaPorta(index);
                DerrubarBoss.derrubarBoss();
            }

        }
    }

    public void ativarCircleCollider()
    {
        circleCollider.enabled = true;
    }

    public void desativarCircleCollider()
    {
        circleCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
