using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    [Header("Chave")]
    static public int chavesTotal;
    static public int chavesAtual;

    static public int chavesEspeciaisTotal;
    static public int chavesEspeciasAtual;

    static public bool temChaveEspecial;

    [Header("Moeda")]
    static public TextMeshProUGUI textoMoeda;
    static public int moedasTotal;
    

    private void Start()
    {
        temChaveEspecial = false;
    }
    public static void receberChave()
    {
        chavesTotal += 1;
        chavesAtual += 1;
    }
    public static void receberChaveEspecial()
    {
        chavesEspeciaisTotal += 1;
        chavesEspeciasAtual += 1;
    }

    public static void receberMoeda(int valor)
    {
        moedasTotal += valor;
        textoMoeda.text = moedasTotal.ToString();
    }
    public static void subtrairMoeda(int valor)
    {
        moedasTotal -= valor;
        textoMoeda.text = moedasTotal.ToString();
    }

}
