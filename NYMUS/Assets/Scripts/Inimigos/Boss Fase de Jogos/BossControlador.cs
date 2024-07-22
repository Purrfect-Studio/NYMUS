using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class BossControlador : MonoBehaviour
{

    [Header("Script Procurar Jogador")]
    //public ProcurarJogador procurarJogador;

    [Header("Configura��es de ataque")]
    public float cooldownAtaque;
    public enum ataquesBoss
    {
        Firewall,
        RaioDeVeneno,
        InvocarInimigo,
        //Adicionar todos os ataques e a��es do boss
    }

    [Header("Vari�veis privadas de apoio")]
    private float cooldownRestante;
    private List<ataquesBoss> ataquesDisponiveis = new List<ataquesBoss>();

    [Header("Projetil do Prefab 'Firewall'")]
    public GameObject projetilFirewall;
    [Header("Atributos 'Firewall'")]
    public float velocidadeFirewall;
    [SerializeField] public static float velocidadeFirewallX; // For�a do tiro
    public float duracaoDoFirewall; // Tempo que o tiro fica no ar at� ser destru�do 
    [Header("GameObject da Arma 'Firewall'")]
    public Transform armaFirewall; // Posi��o de onde o proj�til ser� disparado

    [Header("Projetil do Prefab 'Raio de Veneno'")]
    public GameObject projetilRaioDeVeneno;
    [Header("GameObject da Arma 'Raio de Veneno'")]
    public Transform armaRaioDeVeneno; // Posi��o de onde o proj�til ser� disparado

    // Start is called before the first frame update
    void Start()
    {
        //Definindo ataques dispon�veis iniciais

        ataquesDisponiveis.Add(ataquesBoss.Firewall);
        ataquesDisponiveis.Add(ataquesBoss.RaioDeVeneno);
        // if vida <50%, adicionarAtaque(ataquesBoss.InvocarInimigo) ...

        //procurarJogador = GetComponent<ProcurarJogador>();
        cooldownRestante = cooldownAtaque;

    }

    // Update is called once per frame
    void Update()
    {
        cooldownRestante -= Time.deltaTime;
        if(cooldownRestante<=0)
        {
            ExecutarAtaque(EscolherAtaqueAtual());
            cooldownRestante = cooldownAtaque;
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
                Debug.Log("Boss est� executando Firewall");

                velocidadeFirewallX = velocidadeFirewall;
                // Instancia o proj�til e define sua posi��o e velocidade
                GameObject firewall = Instantiate(projetilFirewall);
                firewall.transform.position = armaFirewall.position;
                firewall.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeFirewallX, 0);
                // Destroi o proj�til depois de um tempo para evitar vazamento de mem�ria
                Destroy(firewall.gameObject, duracaoDoFirewall);

                break;

            case ataquesBoss.RaioDeVeneno:
                Debug.Log("Boss est� executando RaioDeVeneno");
                GameObject raioDeVeneno = Instantiate(projetilRaioDeVeneno);
                raioDeVeneno.transform.position = armaRaioDeVeneno.position;
                Destroy(raioDeVeneno.gameObject, 2.5f);
                break;

            case ataquesBoss.InvocarInimigo:
                Debug.Log("Boss est� executando InvocarInimigo");
                break;
        }
    }
    public void LigarNovoAtaque(ataquesBoss ataqueNovo)
    {
        ataquesDisponiveis.Add(ataqueNovo);
    }
}
