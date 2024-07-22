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
        TiroSimples,
        TiroAmplo,
        InvocarInimigo,
        //Adicionar todos os ataques e a��es do boss
    }

    [Header("Vari�veis privadas de apoio")]
    private float cooldownRestante;
    private List<ataquesBoss> ataquesDisponiveis = new List<ataquesBoss>();

    [Header("Projetil do Prefab 'Tiro Simples'")]
    public GameObject projetilTiroSimples;
    [Header("Atributos 'Tiro Simples'")]
    public float velocidadeTiroSimples;
    [SerializeField] public static float velocidadeTiroSimplesX; // For�a do tiro
    public float duracaoDoTiroSimples; // Tempo que o tiro fica no ar at� ser destru�do 
    [Header("GameObject da Arma 'Tiro Simples'")]
    public Transform armaTiroSimples; // Posi��o de onde o proj�til ser� disparado

    [Header("Projetil do Prefab 'Tiro Amplo'")]
    public GameObject projetilTiroAmplo;

    [Header("GameObject da Arma")]
    public Transform[] armaTiroAmplo; // Posi��o de onde o proj�til ser� disparado

    [Header("Sobre o Tiro")]
    public float velocidadeTiroAmplo; // For�a do tiro
    [SerializeField] public static float velocidadeTiroAmploY; // For�a do tiro
    public float duracaoDoTiroAmplo; // Tempo que o tiro fica no ar at� ser destru�do 
    public float delayParaInstanciarProjetilTiroAmplo;

    // Start is called before the first frame update
    void Start()
    {
        //Definindo ataques dispon�veis iniciais

        ataquesDisponiveis.Add(ataquesBoss.TiroSimples);
        ataquesDisponiveis.Add(ataquesBoss.TiroAmplo);
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
            case ataquesBoss.TiroSimples:
                Debug.Log("Boss est� executando TiroSimples");

                velocidadeTiroSimplesX = velocidadeTiroSimples;
                // Instancia o proj�til e define sua posi��o e velocidade
                GameObject temp = Instantiate(projetilTiroSimples);
                temp.transform.position = armaTiroSimples.position;
                temp.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeTiroSimplesX, 0);
                // Destroi o proj�til depois de um tempo para evitar vazamento de mem�ria
                Destroy(temp.gameObject, duracaoDoTiroSimples);

                break;

            case ataquesBoss.TiroAmplo:
                Debug.Log("Boss est� executando TiroAmplo");

                for (int i = 0; i < armaTiroAmplo.Length; i++)
                {
                    velocidadeTiroAmploY = velocidadeTiroAmplo;
                    GameObject temp1 = Instantiate(projetilTiroAmplo);
                    temp1.transform.position = armaTiroAmplo[i].position;
                    // Define uma velocidade pro projetil
                    temp1.GetComponent<Rigidbody2D>().velocity = new Vector2(0 , velocidadeTiroAmploY);
                    // Destroi o proj�til depois de um tempo para evitar vazamento de mem�ria
                    Destroy(temp1.gameObject, duracaoDoTiroAmplo);
                    // Espera um tempo antes de criar o proximo projetil
                    //yield return new WaitForSeconds(delayParaInstanciarProjetilTiroAmplo);
                }

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
