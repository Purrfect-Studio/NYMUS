using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorTrojan : MonoBehaviour
{
    public enum ataquesBoss
    {
        Laser,
        ataque3,
    }

    [Header("Componentes")]
    private VidaBoss vidaBoss;
    private MovimentacaoTrojan movimentacaoTrojan;
    private Animator animacao;
    [Header("Espinhos Alavanca")]
    public ControladorAlavancas controladorAlavancas;
    public GameObject espinhosAlavancasObject;
    private Transform[] espinhosAlavanca;
    public GameObject avisoEspinhoObject;
    private Transform[] avisoEspinho;
    private float contadorEspinhosAtivados;
    [Header("Laser Espinhos Ativos")]
    public GameObject pontosLaserEspinhoObject;
    private Transform[] pontosLaserEspinho;
    [Header("Laser")]
    public Transform[] pontosLaser;

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

    [Header("Laser Espinho")]
    public GameObject projetilLaserEspinho;
    public int quantidadeAtivacoesLaserEspinho;
    private int quantidadeLaserEspinhoAtivados;
    public float cooldownExecutarLaserEspinho;
    private float cooldownRestanteExecutarLaserEspinho;
    private int[] indexLaser = new int[2];

    [Header("Laser Espinho")]
    public GameObject projetilLaser;


    // Start is called before the first frame update
    void Start()
    {
        vidaBoss = GetComponent<VidaBoss>();
        movimentacaoTrojan = GetComponent<MovimentacaoTrojan>();
        //animacao = GetComponent<Animator>();

        ataquesDisponiveis.Add(ataquesBoss.Laser);
        ataquesDisponiveis.Add(ataquesBoss.ataque3);

        podeExecutarAcoes = false;
        MovimentacaoTrojan.podeMover = false;
        podeExecutarAnimacaoAtaque = true;
        contadorEspinhosAtivados = 0;
        quantidadeLaserEspinhoAtivados = 0;

        cooldownRestanteExecutarLaserEspinho = cooldownExecutarLaserEspinho;
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

        pontosLaserEspinho = new Transform[pontosLaserEspinhoObject.transform.childCount];
        for (int i = 0; i < pontosLaserEspinho.Length; i++)
        {
            pontosLaserEspinho[i] = pontosLaserEspinhoObject.transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (podeExecutarAcoes && contadorEspinhosAtivados != espinhosAlavanca.Length)
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
            cooldownRestanteExecutarLaserEspinho -= Time.deltaTime;

            if (cooldownRestanteExecutarLaserEspinho <= 0)
            {
                executarLaser();
                cooldownRestanteExecutarLaserEspinho = cooldownExecutarLaserEspinho;
                podeExecutarAnimacaoAtaque = true;
                quantidadeLaserEspinhoAtivados += 1;
            }
            if (quantidadeLaserEspinhoAtivados == quantidadeAtivacoesLaserEspinho)
            {
                //Debug.Log("cooldownRestanteDesativarEspinhos <= 0");
                desativarEspinhos();
                contadorEspinhosAtivados = 0;
                quantidadeLaserEspinhoAtivados = 0;
                cooldownRestanteExecutarLaserEspinho = cooldownExecutarLaserEspinho;
                podeExecutarAnimacaoAtaque = true;
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

    public void executarLaser()
    {
        Debug.Log("executarLaser");
        if (escolherPontosDoLaserEspinho())
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject projetilLaserEspinho = Instantiate(this.projetilLaserEspinho);
                projetilLaserEspinho.transform.position = pontosLaserEspinho[indexLaser[i]].position;
                Destroy(projetilLaserEspinho.gameObject, 4f);
            }
        }
    }

    public void ExecutarAtaque(ataquesBoss ataque)
    {
        switch (ataque)
        {
            case ataquesBoss.Laser:
                Debug.Log("Boss está executando Laser");
                GameObject projetilLaser = Instantiate(this.projetilLaser);
                projetilLaser.transform.position = pontosLaser[escolherPontoLaser()].position;
                Destroy(projetilLaser.gameObject, 3.5f);
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

    public bool escolherPontosDoLaserEspinho()
    {
        indexLaser[0] = UnityEngine.Random.Range(0, 3);
        do
        {
            indexLaser[1] = UnityEngine.Random.Range(0, 3);
        } while (indexLaser[1] == indexLaser[0]);
        return true;
    }

    public int escolherPontoLaser()
    {
        return UnityEngine.Random.Range(-1, pontosLaser.Length);
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
