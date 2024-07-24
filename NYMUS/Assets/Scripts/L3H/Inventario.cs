using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    static public int chavesTotal;
    static public int chavesAtual;

    static public int chavesEspeciaisTotal;
    static public int chavesEspeciasAtual;

    static public bool temChaveEspecial;

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




}
