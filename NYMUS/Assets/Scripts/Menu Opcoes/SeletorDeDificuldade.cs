using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeletorDeDificuldade : MonoBehaviour
{
    [Header("Variaveis para selecionar dificuldade")]
    public static dificuldadesExistentes dificuldadeEscolhida;
    public enum dificuldadesExistentes
    {
        Facil,
        Medio,
        Dificil,
    }

    [Header("Valores dos inimigos")]

    [Header("Slime")]
    [SerializeField] public static float vidaSlime;
    [SerializeField] public static float danoSlime;

    [Header("Morcego")]
    [SerializeField] public static float vidaMorcego;
    [SerializeField] public static float danoMorcego;



    [Header("Valores dos Bosses")]

    [Header("VIRUT")]
    [SerializeField] public static float vidaMaximaVirut;
    [SerializeField] public static float danoInjecaoDeDados;
    [SerializeField] public static float danoExplosaoDeDados;
    [SerializeField] public static float danoFirewall;
    [SerializeField] public static int quantidadeDeExplosaoDeDados;
    [SerializeField] public static float cooldownAtaqueVirut;
    [SerializeField] public static float cooldownAtaqueFrenesiVirut;


    public static SeletorDeDificuldade Instancia { get; private set; }
    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
            definirDificuldadeMedia();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    public void definirValoresDaDificuldade()
    {
        switch(dificuldadeEscolhida)
        {
            case dificuldadesExistentes.Facil:
                //Inimigos Comuns
                //Slime
                vidaSlime = 10;
                danoSlime = 10;

                //Morcego
                vidaMorcego = 20;
                danoMorcego = 10;

                //Bosses
                //Virut
                vidaMaximaVirut = 175;
                cooldownAtaqueVirut = 4.5f;
                cooldownAtaqueFrenesiVirut = 3f;
                danoExplosaoDeDados = 7;
                quantidadeDeExplosaoDeDados = 2;
                danoFirewall = 20;
                danoInjecaoDeDados = 13;
                break;

            case dificuldadesExistentes.Medio:
                //Inimigos Comuns
                //Slime
                vidaSlime = 15;
                danoSlime = 15;

                //Morcego
                vidaMorcego = 25;
                danoMorcego = 15;

                //Bosses
                //Virut
                vidaMaximaVirut = 200;
                cooldownAtaqueVirut = 4.2f;
                cooldownAtaqueFrenesiVirut = 2.5f;
                danoExplosaoDeDados = 10;
                quantidadeDeExplosaoDeDados = 2;
                danoFirewall = 25;
                danoInjecaoDeDados = 15;
                break;

            case dificuldadesExistentes.Dificil:
                //Inimigos Comuns
                //Slime
                vidaSlime = 20;
                danoSlime = 20;

                //Morcego
                vidaMorcego = 30;
                danoMorcego = 20;

                //Bosses
                //Virut
                vidaMaximaVirut = 225;
                cooldownAtaqueVirut = 3.5f;
                cooldownAtaqueFrenesiVirut = 2.25f;
                danoExplosaoDeDados = 15;
                quantidadeDeExplosaoDeDados = 3;
                danoFirewall = 30;
                danoInjecaoDeDados = 20;
                break;
        }
    }

    public void definirDificuldadeFacil()
    {
        dificuldadeEscolhida = dificuldadesExistentes.Facil;
        definirValoresDaDificuldade();
    }

    public void definirDificuldadeMedia()
    {
        dificuldadeEscolhida = dificuldadesExistentes.Medio;
        definirValoresDaDificuldade();
    }

    public void definirDificuldadeDificil()
    {
        dificuldadeEscolhida = dificuldadesExistentes.Dificil;
        definirValoresDaDificuldade();
    }

}
