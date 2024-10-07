using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorTrojan : MonoBehaviour
{
    public enum ataquesBoss
    {
        Laser,
        invocarInimigo,
        confusao,
    }
    [Header("Cópias")]
    public GameObject[] copias = new GameObject[2];
    public bool podeAtivarCopia1;
    public bool podeAtivarCopia2;

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
    [HideInInspector] public bool podeExecutarAnimacaoAtaque;
    
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

    [Header("Invocar Inimigo")]
    public GameObject invocarInimigoObject;
    private Transform[] pontosInvocarInimigo;
    public GameObject[] arrayInimigos;

    [Header("Confusao Trojan")]
    public GameObject projetilConfusaoTrojan;


    // Start is called before the first frame update
    void Start()
    {
        vidaBoss = GetComponent<VidaBoss>();
        movimentacaoTrojan = GetComponent<MovimentacaoTrojan>();
        animacao = GetComponent<Animator>();

        ataquesDisponiveis.Add(ataquesBoss.Laser);
        ataquesDisponiveis.Add(ataquesBoss.invocarInimigo);
        ataquesDisponiveis.Add(ataquesBoss.confusao);


        podeExecutarAcoes = false;
        MovimentacaoTrojan.podeMover = false;
        podeExecutarAnimacaoAtaque = false;
        contadorEspinhosAtivados = 0;
        quantidadeLaserEspinhoAtivados = 0;

        cooldownRestanteExecutarLaserEspinho = cooldownExecutarLaserEspinho;
        cooldownRestanteParaAtacar = cooldownParaAtacar;

        for (int i = 0; i < copias.Length; i++)
        {
            copias[i].SetActive(false);
        }
        podeAtivarCopia1 = true;
        podeAtivarCopia2 = true;

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

        pontosInvocarInimigo = new Transform[invocarInimigoObject.transform.childCount];
        for (int i = 0; i < pontosInvocarInimigo.Length; i++)
        {
            pontosInvocarInimigo[i] = invocarInimigoObject.transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (podeExecutarAcoes && contadorEspinhosAtivados != espinhosAlavanca.Length)
        {
            cooldownRestanteParaAtacar -= Time.deltaTime;

            if(cooldownRestanteParaAtacar <= 0)
            {
                podeExecutarAnimacaoAtaque = true;
                cooldownRestanteParaAtacar = cooldownParaAtacar;
                ExecutarAtaque(EscolherAtaqueAtual());              
            }
        }

        /*if (vidaBoss.frenesi)
        {
            cooldownParaAtacar = cooldownAtaqueFrenesi;
        }*/

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

        if (podeExecutarAnimacaoAtaque)
        {
            podeExecutarAnimacaoAtaque = false;
            MovimentacaoTrojan.executarAnimacaoAtaqueCopia = true;
            animacao.SetTrigger("Atacar");
        }

        if (podeAtivarCopia1 && vidaBoss.vidaAtual <= vidaBoss.vidaMaxima / 2)
        {
            podeAtivarCopia1 = false;
            copias[0].SetActive(true);
        }
        if (podeAtivarCopia2 && vidaBoss.vidaAtual <= vidaBoss.vidaMaxima / 3)
        {
            podeAtivarCopia2 = false;
            copias[1].SetActive(true);
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
                cooldownRestanteParaAtacar += 3.5f;
                GameObject projetilLaser = Instantiate(this.projetilLaser);
                int index = escolherPontoLaser();
                projetilLaser.transform.position = pontosLaser[index].position;
                if (index == 2 || index == 3)
                {
                    projetilLaser.transform.rotation = new Quaternion(0, 0, 180f, 0);
                }
                Destroy(projetilLaser.gameObject, 4f);
                break;

            case ataquesBoss.invocarInimigo:
                Debug.Log("Boss está executando invocarInimigo");
                GameObject invocarInimigo = Instantiate(arrayInimigos[UnityEngine.Random.Range(0, arrayInimigos.Length)]);
                invocarInimigo.transform.position = pontosInvocarInimigo[UnityEngine.Random.Range(0, pontosInvocarInimigo.Length)].position;
                break;

            case ataquesBoss.confusao:
                Debug.Log("Boss está executando confusao");
                GameObject confusaoTrojan = Instantiate(projetilConfusaoTrojan);
                confusaoTrojan.transform.position = gameObject.transform.position;
                Destroy(confusaoTrojan.gameObject, 6f);
                break;
        }
    }

    public ataquesBoss EscolherAtaqueAtual()
    {
        int index;
        int i = UnityEngine.Random.Range(0, 11);

        if (i < 7)
        {
            index = 0;
        }
        else if (i < 9)
        {
            index = 1;
        }
        else
        {
            index = 2;
        }

        return ataquesDisponiveis[index];
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
        return UnityEngine.Random.Range(0, 4);
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
