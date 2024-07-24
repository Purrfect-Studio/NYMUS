using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BossControlador : MonoBehaviour
{
    [Header("Configura��es de ataque")]
    public float delayParaIniciarAcoes;
    public float cooldownAtaque;
    public static bool podeExecutarAcoes = false;
    public enum ataquesBoss
    {
        Firewall,
        InjecaoDeDados,
        ExplosaoDeDados,
        //Adicionar todos os ataques e a��es do boss
    }

    [Header("Vari�veis privadas de apoio")]
    private float cooldownRestanteAtaque;
    private List<ataquesBoss> ataquesDisponiveis = new List<ataquesBoss>();
    private GameObject jogador;

    [Header("Projetil do Prefab 'Firewall'")]
    public GameObject projetilFirewall;
    [Header("Atributos 'Firewall'")]
    public float velocidadeFirewall;
    [SerializeField] public static float velocidadeFirewallX; // For�a do tiro
    public float duracaoDoFirewall; // Tempo que o tiro fica no ar at� ser destru�do 
    [Header("GameObject da Arma 'Firewall'")]
    public Transform armaFirewall; // Posi��o de onde o proj�til ser� disparado

    [Header("Projetil do Prefab 'Injecao de Dados'")]
    public GameObject projetilInjecaoDeDados;
    [Header("GameObject da Arma 'Injecao de Dados'")]
    public Transform armaInjecaoDeDados; // Posi��o de onde o proj�til ser� disparado

    [Header("Projetil do Prefab 'Explosao de Dados'")]
    public GameObject projetilExplosaoDeDados;
    [Header("GameObject da Arma 'Explosao de Dados'")]
    public Transform armaExplosaoDeDados; // Posi��o de onde o proj�til ser� disparado
    [Header("Configuracoes 'Explosao de Dados'")]
    public float intervaloEntreExplosaoDeDados;
    public int quantidadeDeExplosaoDeDados;
    private bool executarExplosaoDeDados = false;
    private float contadorExplosaoDeDados;
    private int quantidadeDeExplosaoDeDadosExecutadas = 0;

    [Header("Mecanica de causar dano no Boss")]
    public GameObject[] portas;
    public float tempoParaDesativarPorta;
    public float cooldownParaInvocarPorta;
    private float cooldownRestanteParaInvocarPorta;
    private bool portaInvocada;
    
    

    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Jogador");
        
        contadorExplosaoDeDados = intervaloEntreExplosaoDeDados;
        //Definindo ataques dispon�veis iniciais
        ataquesDisponiveis.Add(ataquesBoss.Firewall);
        ataquesDisponiveis.Add(ataquesBoss.InjecaoDeDados);
        ataquesDisponiveis.Add(ataquesBoss.ExplosaoDeDados);
        // if vida <50%, adicionarAtaque(ataquesBoss.InvocarInimigo) ...

        cooldownRestanteAtaque = cooldownAtaque;
        cooldownRestanteParaInvocarPorta = cooldownParaInvocarPorta;

        for(int i = 0; i < portas.Length; i++)
        {
            portas[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        cooldownRestanteAtaque -= Time.deltaTime;
        cooldownRestanteParaInvocarPorta -= Time.deltaTime;

        if(cooldownRestanteAtaque <= 0 && podeExecutarAcoes == true)
        {
            ExecutarAtaque(EscolherAtaqueAtual());
            cooldownRestanteAtaque = cooldownAtaque;
        }

        ExecutarExplosaoDeDados();
        iniciarAtaque();


        if(cooldownRestanteParaInvocarPorta <= 0 && portaInvocada == false && podeExecutarAcoes == true)
        {
            InvocarPorta(EscolherPorta());
            cooldownRestanteParaInvocarPorta = cooldownParaInvocarPorta;
            portaInvocada = true;
        }
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
        }
    }

    IEnumerator DelayParaIniciarAcoes()
    {
        yield return new WaitForSeconds(delayParaIniciarAcoes);
        podeExecutarAcoes = true;
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
                //Debug.Log("Boss est� executando Firewall");
                velocidadeFirewallX = velocidadeFirewall;
                // Instancia o proj�til e define sua posi��o e velocidade
                GameObject firewall = Instantiate(projetilFirewall);
                firewall.transform.position = armaFirewall.position;
                firewall.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeFirewallX, 0);
                Destroy(firewall.gameObject, duracaoDoFirewall);
                break;

            case ataquesBoss.InjecaoDeDados:
                //Debug.Log("Boss est� executando InjecaoDeDados");
                GameObject raioDeVeneno = Instantiate(projetilInjecaoDeDados);
                raioDeVeneno.transform.position = armaInjecaoDeDados.position;
                Destroy(raioDeVeneno.gameObject, 2.5f);
                break;

            case ataquesBoss.ExplosaoDeDados:
                //Debug.Log("Boss est� executando ExplosaoDeDados");
                executarExplosaoDeDados = true;
                break;
        }
    }

    public void LigarNovoAtaque(ataquesBoss ataqueNovo)
    {
        ataquesDisponiveis.Add(ataqueNovo);
    }

    public void InvocarPorta(int indexPorta)
    {
        portas[indexPorta].SetActive(true);
        StartCoroutine(DesativarPorta(indexPorta));
    }

    public int EscolherPorta()
    {
        return Random.Range(0, portas.Length);
    }

    IEnumerator DesativarPorta(int indexPorta)
    {
        yield return new WaitForSeconds(tempoParaDesativarPorta);
        portas[indexPorta].SetActive(false);
        portaInvocada = false;
    }
}
