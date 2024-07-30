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
    
    // Start is called before the first frame update
    void Start()
    {
        //Definir dificuldade padrão como facil
        dificuldadeEscolhida = dificuldadesExistentes.Medio;
        definirValoresDaDificuldade();
    }

    public void definirValoresDaDificuldade()
    {
        switch(dificuldadeEscolhida)
        {
            case dificuldadesExistentes.Facil:
                vidaSlime = 10;
                danoSlime = 10;

                vidaMorcego = 20;
                danoMorcego = 10;
                break;

            case dificuldadesExistentes.Medio:
                vidaSlime = 15;
                danoSlime = 15;

                vidaMorcego = 25;
                danoMorcego = 15;
                break;

            case dificuldadesExistentes.Dificil:
                vidaSlime = 20;
                danoSlime = 20;

                vidaMorcego = 30;
                danoMorcego = 20;
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
