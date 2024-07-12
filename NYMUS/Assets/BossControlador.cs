using System.Collections;
using System.Collections.Generic;
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
                break;

            case ataquesBoss.TiroAmplo:
                Debug.Log("Boss est� executando TiroAmplo");
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
