using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    static public int chavesTotal;
    static public int chavesAtual;


    public static void receberChave()
    {
        chavesTotal += 1;
        chavesAtual += 1;
    }



}
