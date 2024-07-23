using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class BossControlador : MonoBehaviour
{
    [Header("Configurações de ataque")]
    public float cooldownAtaque;
    private bool podeAtacar = false;
    public enum ataquesBoss
    {
        Firewall,
        InjecaoDeDados,
        ExplosaoDeDados,
        //Adicionar todos os ataques e ações do boss
    }

    [Header("Variáveis privadas de apoio")]
    private float cooldownRestante;
    private List<ataquesBoss> ataquesDisponiveis = new List<ataquesBoss>();
    private GameObject jogador;

    [Header("Projetil do Prefab 'Firewall'")]
    public GameObject projetilFirewall;
    [Header("Atributos 'Firewall'")]
    public float velocidadeFirewall;
    [SerializeField] public static float velocidadeFirewallX; // Força do tiro
    public float duracaoDoFirewall; // Tempo que o tiro fica no ar até ser destruído 
    [Header("GameObject da Arma 'Firewall'")]
    public Transform armaFirewall; // Posição de onde o projétil será disparado

    [Header("Projetil do Prefab 'Injecao de Dados'")]
    public GameObject projetilInjecaoDeDados;
    [Header("GameObject da Arma 'Injecao de Dados'")]
    public Transform armaInjecaoDeDados; // Posição de onde o projétil será disparado

    [Header("Projetil do Prefab 'Explosao de Dados'")]
    public GameObject projetilExplosaoDeDados;
    [Header("GameObject da Arma 'Explosao de Dados'")]
    public Transform armaExplosaoDeDados; // Posição de onde o projétil será disparado
    public float intervaloEntreExplosaoDeDados;
    public int quantidadeDeExplosaoDeDados;
    private bool executarExplosaoDeDados = false;
    private float contadorExplosaoDeDados;
    private int quantidadeDeExplosaoDeDadosExecutadas = 0;

    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Jogador");
        contadorExplosaoDeDados = intervaloEntreExplosaoDeDados;
        //Definindo ataques disponíveis iniciais
        ataquesDisponiveis.Add(ataquesBoss.Firewall);
        ataquesDisponiveis.Add(ataquesBoss.InjecaoDeDados);
        ataquesDisponiveis.Add(ataquesBoss.ExplosaoDeDados);
        // if vida <50%, adicionarAtaque(ataquesBoss.InvocarInimigo) ...

        //procurarJogador = GetComponent<ProcurarJogador>();
        cooldownRestante = cooldownAtaque;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownRestante -= Time.deltaTime;
        if(cooldownRestante <= 0 && podeAtacar == true)
        {
            ExecutarAtaque(EscolherAtaqueAtual());
            cooldownRestante = cooldownAtaque;
        }

        ExecutarExplosaoDeDados();
        delayParaIniciarAtaque();
    }

    void delayParaIniciarAtaque()
    {
        if(MovimentacaoBoss.podeMover == true)
        {
            if(podeAtacar == false)
            {
                podeAtacar = true;
            }
        }
        else
        {
            podeAtacar = false;
        }
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
                Debug.Log("Boss está executando Firewall");

                velocidadeFirewallX = velocidadeFirewall;
                // Instancia o projétil e define sua posição e velocidade
                GameObject firewall = Instantiate(projetilFirewall);
                firewall.transform.position = armaFirewall.position;
                firewall.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeFirewallX, 0);
                // Destroi o projétil depois de um tempo para evitar vazamento de memória
                Destroy(firewall.gameObject, duracaoDoFirewall);

                break;

            case ataquesBoss.InjecaoDeDados:
                Debug.Log("Boss está executando RaioDeVeneno");
                GameObject raioDeVeneno = Instantiate(projetilInjecaoDeDados);
                raioDeVeneno.transform.position = armaInjecaoDeDados.position;
                Destroy(raioDeVeneno.gameObject, 2.5f);
                break;

            case ataquesBoss.ExplosaoDeDados:
                Debug.Log("Boss está executando ExplosaoDeDados");
                executarExplosaoDeDados = true;
                break;
        }
    }

    public void LigarNovoAtaque(ataquesBoss ataqueNovo)
    {
        ataquesDisponiveis.Add(ataqueNovo);
    }
}
