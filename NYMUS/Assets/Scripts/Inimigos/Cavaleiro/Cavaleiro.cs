using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cavaleiro : MonoBehaviour
{
    private InimigoPatrulha patrulha;
    private ProcurarJogador procurarJogador;
    private GameObject jogador;
    private Animator animacao;
    public Collider2D ColliderEspada;
    public InimigoDanoDeColisao danoColisao;

    private int direcao;
    public float cooldownDeAtaque = 2f;
    private float cooldownRestanteAtaque;
    private bool estaAtacando;

    void Start()
    {
        patrulha = GetComponent<InimigoPatrulha>();
        procurarJogador = GetComponent<ProcurarJogador>();
        animacao = GetComponent<Animator>();

        ColliderEspada.enabled = false;

        cooldownRestanteAtaque = cooldownDeAtaque;

        jogador = GameObject.FindWithTag("Jogador");
        estaAtacando = false;
    }

    // Update is called once per frame
    void Update()
    {
        direcao = patrulha.direcao;
        if(cooldownRestanteAtaque > -1)
        {
            cooldownRestanteAtaque -= Time.deltaTime;
        }
        if (procurarJogador.procurarJogador() && cooldownRestanteAtaque < 0)
        {
            if(transform.position.x - jogador.transform.position.x < 0 && direcao == 1 || transform.position.x - jogador.transform.position.x > 0 && direcao == -1 && !estaAtacando)
            {
                patrulha.enabled = false;
                estaAtacando = true;
                StartCoroutine(ExecutarAtaque());
            }
        }     
    }

    IEnumerator ExecutarAtaque()
    {
        animacao.SetTrigger("estaAtacando");
        cooldownRestanteAtaque = cooldownDeAtaque;
        yield return new WaitForSeconds(1f);
        patrulha.enabled = true;
        estaAtacando = false;
    }

    public void ativarColliderAtaque()
    {
        ColliderEspada.enabled = true;
    }

    public void DesativarColliderAtaque()
    {
        ColliderEspada.enabled = false;
    }

}
