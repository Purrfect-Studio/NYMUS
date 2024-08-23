using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorTrojan : MonoBehaviour
{
    public enum ataquesBoss
    {
        ataque1,
        ataque2,
        ataque3,
    }

    [Header("Componentes")]
    private VidaBoss vidaBoss;
    private MovimentacaoTrojan movimentacaoTrojan;
    private Animator animacao;
    public ControladorAlavancas controladorAlavancas;
    public GameObject espinhosAlavancasObject;
    private Transform[] espinhosAlavanca;
    public GameObject avisoEspinhoObject;
    private Transform[] avisoEspinho;
    private float contadorEspinhosAtivados;
    private float cooldownDesativarEspinhos;
    private float cooldownRestanteDesativarEspinhos;

    [Header("Variaveis de controle")]
    public float delayParaIniciarAcoes;
    public static bool podeExecutarAcoes;
    private bool podeExecutarAnimacaoAtaque;
    
    [Header("Variaver de apoio")]
    private List<ataquesBoss> ataquesDisponiveis = new List<ataquesBoss>();

    [Header("Ataques")]
    public float cooldownParaAtacar;
    public float cooldownAtaqueFrenesi;
    private float cooldownRestanteParaAtacar;
    // Start is called before the first frame update
    void Start()
    {
        vidaBoss = GetComponent<VidaBoss>();
        movimentacaoTrojan = GetComponent<MovimentacaoTrojan>();
        //animacao = GetComponent<Animator>();

        ataquesDisponiveis.Add(ataquesBoss.ataque1);
        ataquesDisponiveis.Add(ataquesBoss.ataque2);
        ataquesDisponiveis.Add(ataquesBoss.ataque3);

        podeExecutarAcoes = false;
        MovimentacaoTrojan.podeMover = false;
        podeExecutarAnimacaoAtaque = true;
        contadorEspinhosAtivados = 0;

        cooldownDesativarEspinhos = vidaBoss.tempoParaLevantar;
        cooldownRestanteDesativarEspinhos = cooldownDesativarEspinhos;
        cooldownRestanteParaAtacar = cooldownParaAtacar;

        espinhosAlavanca = new Transform[espinhosAlavancasObject.transform.childCount];
        for (int i = 0; i < espinhosAlavanca.Length; i++)
        {
            espinhosAlavanca[i] = espinhosAlavancasObject.transform.GetChild(i);
        }

        avisoEspinho = new Transform[avisoEspinhoObject.transform.childCount];
        for (int i = 0; i < avisoEspinho.Length; i++)
        {
            avisoEspinho[i] = avisoEspinhoObject.transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (podeExecutarAcoes)
        {
            cooldownRestanteParaAtacar -= Time.deltaTime;

            if(cooldownRestanteParaAtacar <= 0.6f && podeExecutarAnimacaoAtaque)
            {
                podeExecutarAnimacaoAtaque = false;
                //animacao.setTrigger("Atacar");
            }

            if(cooldownRestanteParaAtacar <= 0)
            {
                ExecutarAtaque(EscolherAtaqueAtual());
                cooldownRestanteParaAtacar = cooldownParaAtacar;
                podeExecutarAnimacaoAtaque = true;
            }
        }
        if (vidaBoss.frenesi)
        {
            cooldownParaAtacar = cooldownAtaqueFrenesi;
        }

        iniciarAtaque();

        if(contadorEspinhosAtivados == espinhosAlavanca.Length)
        {
            //Debug.Log("contadorEspinhosAtivados == espinhosAlavanca.Length");
            cooldownRestanteDesativarEspinhos -= Time.deltaTime;
            if (cooldownRestanteDesativarEspinhos <= 0)
            {
                //Debug.Log("cooldownRestanteDesativarEspinhos <= 0");
                desativarEspinhos();
                contadorEspinhosAtivados = 0;
                cooldownRestanteDesativarEspinhos = cooldownDesativarEspinhos;
            }
        }
    }

    void iniciarAtaque()
    {
        if (MovimentacaoTrojan.podeMover == true)
        {
            if (podeExecutarAcoes == false)
            {
                StartCoroutine("DelayParaIniciarAcoes");
            }
        }
        else
        {
            podeExecutarAcoes = false;
            cooldownRestanteParaAtacar = cooldownParaAtacar;
        }
    }

    public void ExecutarAtaque(ataquesBoss ataque)
    {
        switch (ataque)
        {
            case ataquesBoss.ataque1:
                Debug.Log("Boss está executando ataque1");
                
                break;

            case ataquesBoss.ataque2:
                Debug.Log("Boss está executando ataque2");
                
                break;

            case ataquesBoss.ataque3:
                Debug.Log("Boss está executando ataque3");

                break;
        }
    }

    public ataquesBoss EscolherAtaqueAtual()
    {
        return ataquesDisponiveis[UnityEngine.Random.Range(0, ataquesDisponiveis.Count)];
    }

    public void LigarNovoAtaque(ataquesBoss ataqueNovo)
    {
        ataquesDisponiveis.Add(ataqueNovo);
    }

    public void ativarEspinho(int index)
    {
        StartCoroutine(ativaEspinho(index));
    }

    IEnumerator ativaEspinho(int index)
    {
        yield return new WaitForSeconds(vidaBoss.tempoParaLevantar);
        avisoEspinho[index].gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        espinhosAlavanca[index].gameObject.SetActive(true);
        avisoEspinho[index].gameObject.SetActive(false);
        contadorEspinhosAtivados++;
    }

    public void desativarEspinhos()
    {
        StartCoroutine("desativaEspinhos");
    }

    IEnumerator desativaEspinhos()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < espinhosAlavanca.Length; i++)
        {
            espinhosAlavanca[i].gameObject.SetActive(false);
        }
    }


    public void delayParaIniciarAcao()
    {
        StartCoroutine("DelayParaIniciarAcoes");
    }

    IEnumerator DelayParaIniciarAcoes()
    {
        yield return new WaitForSeconds(delayParaIniciarAcoes);
        podeExecutarAcoes = true;
        MovimentacaoTrojan.podeMover = true;
    }
}
