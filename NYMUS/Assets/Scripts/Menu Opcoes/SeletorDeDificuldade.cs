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

    [Header("Fantasma")]
    [SerializeField] public static float vidaFantasma;
    [SerializeField] public static float danoFantasma;

    [Header("Inimigo Projetil Parabola")]
    [SerializeField] public static float vidaInimigoProjetilParabola;
    [SerializeField] public static float danoInimigoProjetilParabola;

    [Header("Cavaleiro")]
    [SerializeField] public static float vidaCavaleiro;
    [SerializeField] public static float danoCavaleiro;

    [Header("Touro")]
    [SerializeField] public static float vidaTouro;
    [SerializeField] public static float danoTouro;


    [Header("Valores dos Bosses")]

    [Header("VIRUT")]
    [SerializeField] public static float vidaMaximaVirut;
    [SerializeField] public static float danoInjecaoDeDados;
    [SerializeField] public static float danoExplosaoDeDados;
    [SerializeField] public static float danoFirewall;
    [SerializeField] public static int quantidadeDeExplosaoDeDados;
    [SerializeField] public static float cooldownAtaqueVirut;
    [SerializeField] public static float cooldownAtaqueFrenesiVirut;

    [Header("TROJAN")]
    [SerializeField] public static float vidaMaximaTrojan;
    [SerializeField] public static float cooldownDefinirNovaPosicaoTrojan;
    [SerializeField] public static float cooldownDefinirNovaPosicaoFrenesiTrojan;
    [SerializeField] public static int quantidadeDeAtivacoesLaserEspinho;
    [SerializeField] public static float cooldownExecutarLaserEspinhoTrojan;
    [SerializeField] public static float cooldownAtaqueTrojan;
    [SerializeField] public static float cooldownAtaqueFrenesiTrojan;
    [SerializeField] public static float danoLaserEspinhoTrojan;
    [SerializeField] public static float danoLaserTrojan;
    [SerializeField] public static float danoProjetilConfusaoTrojan;
    [SerializeField] public static float duracaoConfusaoTrojan;
    [SerializeField] public static float curaCloneRosaTrojan;
    [SerializeField] public static float curaCloneAzulTrojan;

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
        Debug.Log("Dificuldade alterada para: " + dificuldadeEscolhida);
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

                //Fantasma
                vidaFantasma = 15;
                danoFantasma = 10;

                //Inimigo Projetil Parabola
                vidaInimigoProjetilParabola = 15;
                danoInimigoProjetilParabola = 10;

                //Cavaleiro
                vidaCavaleiro = 45;
                danoCavaleiro = 10;

                //Touro
                vidaTouro = 100;
                danoTouro = 20;

                //Bosses
                //Virut
                vidaMaximaVirut = 175;
                cooldownAtaqueVirut = 4.5f;
                cooldownAtaqueFrenesiVirut = 3f;
                danoExplosaoDeDados = 7;
                quantidadeDeExplosaoDeDados = 2;
                danoFirewall = 20;
                danoInjecaoDeDados = 13;

                //Trojan
                vidaMaximaTrojan = 125;
                cooldownDefinirNovaPosicaoTrojan = 5f;
                cooldownDefinirNovaPosicaoFrenesiTrojan = 3.5f;
                quantidadeDeAtivacoesLaserEspinho = 4;
                cooldownExecutarLaserEspinhoTrojan = 4.5f;
                cooldownAtaqueTrojan = 5f;
                //cooldownAtaqueFrenesiTrojan = 3.5f;

                danoLaserEspinhoTrojan = 15;
                danoLaserTrojan = 10;
                danoProjetilConfusaoTrojan = 5;
                duracaoConfusaoTrojan = 3;
                curaCloneRosaTrojan = 10;
                curaCloneAzulTrojan = 15;
                break;

            case dificuldadesExistentes.Medio:
                //Inimigos Comuns
                //Slime
                vidaSlime = 15;
                danoSlime = 15;

                //Morcego
                vidaMorcego = 25;
                danoMorcego = 15;

                //Fantasma
                vidaFantasma = 20;
                danoFantasma = 15;

                //Inimigo Projetil Parabola
                vidaInimigoProjetilParabola = 20;
                danoInimigoProjetilParabola = 15;

                //Cavaleiro
                vidaCavaleiro = 50;
                danoCavaleiro = 15;

                //Touro
                vidaTouro = 150;
                danoTouro = 25;

                //Bosses
                //Virut
                vidaMaximaVirut = 200;
                cooldownAtaqueVirut = 4.2f;
                cooldownAtaqueFrenesiVirut = 2.5f;
                danoExplosaoDeDados = 10;
                quantidadeDeExplosaoDeDados = 2;
                danoFirewall = 25;
                danoInjecaoDeDados = 15;

                //Trojan
                vidaMaximaTrojan = 150;
                cooldownDefinirNovaPosicaoTrojan = 4.5f;
                cooldownDefinirNovaPosicaoFrenesiTrojan = 3f;
                quantidadeDeAtivacoesLaserEspinho = 5;
                cooldownExecutarLaserEspinhoTrojan = 4;
                cooldownAtaqueTrojan = 4f;
                //cooldownAtaqueFrenesiTrojan = 2.5f;

                danoLaserEspinhoTrojan = 15;
                danoLaserTrojan = 13;
                danoProjetilConfusaoTrojan = 5;
                duracaoConfusaoTrojan = 4.5f;
                curaCloneRosaTrojan = 15;
                curaCloneAzulTrojan = 20;
                break;

            case dificuldadesExistentes.Dificil:
                //Inimigos Comuns
                //Slime
                vidaSlime = 20;
                danoSlime = 20;

                //Morcego
                vidaMorcego = 30;
                danoMorcego = 20;

                //Fantasma
                vidaFantasma = 30;
                danoFantasma = 20;

                //Inimigo Projetil Parabola
                vidaInimigoProjetilParabola = 25;
                danoInimigoProjetilParabola = 15;

                //Cavaleiro
                vidaCavaleiro = 60;
                danoCavaleiro = 17;

                //Touro
                vidaTouro = 175;
                danoTouro = 30;

                //Bosses
                //Virut
                vidaMaximaVirut = 225;
                cooldownAtaqueVirut = 3.5f;
                cooldownAtaqueFrenesiVirut = 2.25f;
                danoExplosaoDeDados = 15;
                quantidadeDeExplosaoDeDados = 3;
                danoFirewall = 30;
                danoInjecaoDeDados = 20;

                //Trojan
                vidaMaximaTrojan = 175;
                cooldownDefinirNovaPosicaoTrojan = 4;
                cooldownDefinirNovaPosicaoFrenesiTrojan = 2.5f;
                quantidadeDeAtivacoesLaserEspinho = 7;
                cooldownExecutarLaserEspinhoTrojan = 3.8f;
                cooldownAtaqueTrojan = 3.5f;
                //cooldownAtaqueFrenesiTrojan = 2;

                danoLaserEspinhoTrojan = 15;
                danoLaserTrojan = 15;
                danoProjetilConfusaoTrojan = 5;
                duracaoConfusaoTrojan = 6;
                curaCloneRosaTrojan = 20;
                curaCloneAzulTrojan = 25;
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
