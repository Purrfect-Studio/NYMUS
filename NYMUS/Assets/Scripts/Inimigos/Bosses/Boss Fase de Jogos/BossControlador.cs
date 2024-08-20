using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static SeletorDeDificuldade;

public class BossControlador : MonoBehaviour
{
    [Header("Configurações de ataque")]
    public float delayParaIniciarAcoes;
    public float cooldownAtaque;
    public float cooldownAtaqueFrenesi;
    public static bool podeExecutarAcoes = false;
    public enum ataquesBoss
    {
        Firewall,
        InjecaoDeDados,
        ExplosaoDeDados,
        //Adicionar todos os ataques e ações do boss
    }

    [Header("Variáveis privadas de apoio")]
    private float cooldownRestanteAtaque;
    private List<ataquesBoss> ataquesDisponiveis = new List<ataquesBoss>();
    private GameObject jogador;
    private Animator animatorPortas;
    private Collider2D collider2DPortas;
    private bool podeAtivarAnimacaoDeAtaque;
    private Animator animacao;
    private VidaBoss vidaBoss;

    [Header("FIREWALL")]
    [Header("Projetil do Prefab 'Firewall'")]
    public GameObject projetilFirewall;
    [Header("Atributos 'Firewall'")]
    public float velocidadeFirewall;
    public static float danoFirewall;
    [SerializeField] public static float velocidadeFirewallX; // Força do tiro
    public float duracaoDoFirewall; // Tempo que o tiro fica no ar até ser destruído 
    [Header("GameObject da Arma 'Firewall'")]
    public Transform armaFirewall; // Posição de onde o projétil será disparado

    [Header("INJECAO DE DADOS")]
    [Header("Projetil do Prefab 'Injecao de Dados'")]
    public GameObject projetilInjecaoDeDados;
    [Header("GameObject da Arma 'Injecao de Dados'")]
    public Transform armaInjecaoDeDados; // Posição de onde o projétil será disparado
    [Header("Configuracoes Injecao de Dados")]
    public static float danoInjecaoDeDados;

    [Header("EXPLOSAO DE DADOS")]
    [Header("Projetil do Prefab 'Explosao de Dados'")]
    public GameObject projetilExplosaoDeDados;
    [Header("GameObject da Arma 'Explosao de Dados'")]
    public Transform armaExplosaoDeDados; // Posição de onde o projétil será disparado
    [Header("Configuracoes 'Explosao de Dados'")]
    public float intervaloEntreExplosaoDeDados;
    public int quantidadeDeExplosaoDeDados;
    public static float danoExplosaoDeDados;
    private bool executarExplosaoDeDados = false;
    private float contadorExplosaoDeDados;
    private int quantidadeDeExplosaoDeDadosExecutadas = 0;



    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Jogador");
        animacao = GetComponent<Animator>();
        vidaBoss = GetComponent<VidaBoss>();
        
        contadorExplosaoDeDados = intervaloEntreExplosaoDeDados;

        danoInjecaoDeDados = SeletorDeDificuldade.danoInjecaoDeDados;
        danoFirewall = SeletorDeDificuldade.danoFirewall;
        danoExplosaoDeDados = SeletorDeDificuldade.danoExplosaoDeDados;
        quantidadeDeExplosaoDeDados = SeletorDeDificuldade.quantidadeDeExplosaoDeDados;
        cooldownAtaque = SeletorDeDificuldade.cooldownAtaqueVirut;
        cooldownAtaqueFrenesi = SeletorDeDificuldade.cooldownAtaqueFrenesiVirut;

        //Definindo ataques disponíveis iniciais
        ataquesDisponiveis.Add(ataquesBoss.Firewall);
        ataquesDisponiveis.Add(ataquesBoss.InjecaoDeDados);
        ataquesDisponiveis.Add(ataquesBoss.ExplosaoDeDados);
        /*if(SeletorDeDificuldade.dificuldadeEscolhida == dificuldadesExistentes.Dificil)
        {
            ataquesDisponiveis.Add(ataquesBoss.novoAtaque);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        cooldownRestanteAtaque -= Time.deltaTime;
        if(cooldownRestanteAtaque <= 0.6f && podeExecutarAcoes == true && podeAtivarAnimacaoDeAtaque == true)
        {
            podeAtivarAnimacaoDeAtaque = false;
            animacao.SetTrigger("Atacar");
        }
        if (cooldownRestanteAtaque <= 0 && podeExecutarAcoes == true)
        {
            ExecutarAtaque(EscolherAtaqueAtual());
            if (vidaBoss.frenesi == true)
            {
                cooldownAtaque = cooldownAtaqueFrenesi;
            }
            cooldownRestanteAtaque = cooldownAtaque;
            podeAtivarAnimacaoDeAtaque = true;
        }

        ExecutarExplosaoDeDados();
        iniciarAtaque();
    }

    void iniciarAtaque()
    {
        if(MovimentacaoBoss.podeMover == true)
        {
            if(podeExecutarAcoes == false)
            {
                StartCoroutine("DelayParaIniciarAcoes");
            }
        }
        else
        {
            podeExecutarAcoes = false;
            cooldownRestanteAtaque = cooldownAtaque;
        }
    }

    IEnumerator DelayParaIniciarAcoes()
    {
        yield return new WaitForSeconds(delayParaIniciarAcoes);
        podeExecutarAcoes = true;
        podeAtivarAnimacaoDeAtaque = true;
    }

    public void ExecutarExplosaoDeDados()
    {
        if (executarExplosaoDeDados == true)
        {
            if (contadorExplosaoDeDados < 0)
            {
                GameObject[] explosaoDeDados = new GameObject[quantidadeDeExplosaoDeDados];
                explosaoDeDados[quantidadeDeExplosaoDeDadosExecutadas] = Instantiate(projetilExplosaoDeDados);
                explosaoDeDados[quantidadeDeExplosaoDeDadosExecutadas].transform.position = jogador.transform.position; //armaExplosaoDeDados.position; 
                Destroy(explosaoDeDados[quantidadeDeExplosaoDeDadosExecutadas].gameObject, 1.5f);

                contadorExplosaoDeDados = intervaloEntreExplosaoDeDados;
                quantidadeDeExplosaoDeDadosExecutadas++;

                if (quantidadeDeExplosaoDeDadosExecutadas == quantidadeDeExplosaoDeDados)
                {
                    quantidadeDeExplosaoDeDadosExecutadas = 0;
                    executarExplosaoDeDados = false;
                    contadorExplosaoDeDados = intervaloEntreExplosaoDeDados;
                }
            }
            else
            {
                contadorExplosaoDeDados -= Time.deltaTime;
            }
        }
    }

    public ataquesBoss EscolherAtaqueAtual()
    {
        return ataquesDisponiveis[Random.Range(0, ataquesDisponiveis.Count)];
    }

    public void ExecutarAtaque(ataquesBoss ataque)
    {

        switch (ataque)
        {
            case ataquesBoss.Firewall:
                //Debug.Log("Boss está executando Firewall");
                velocidadeFirewallX = velocidadeFirewall;
                // Instancia o projétil e define sua posição e velocidade
                GameObject firewall = Instantiate(projetilFirewall);
                firewall.transform.position = armaFirewall.position;
                firewall.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeFirewallX, 0);
                Destroy(firewall.gameObject, duracaoDoFirewall);
                break;

            case ataquesBoss.InjecaoDeDados:
                //Debug.Log("Boss está executando InjecaoDeDados");
                GameObject raioDeVeneno = Instantiate(projetilInjecaoDeDados);
                raioDeVeneno.transform.position = armaInjecaoDeDados.position;
                Destroy(raioDeVeneno.gameObject, 2.5f);
                break;

            case ataquesBoss.ExplosaoDeDados:
                //Debug.Log("Boss está executando ExplosaoDeDados");
                executarExplosaoDeDados = true;
                break;
        }
    }

    public void LigarNovoAtaque(ataquesBoss ataqueNovo)
    {
        ataquesDisponiveis.Add(ataqueNovo);
    }

    
}
