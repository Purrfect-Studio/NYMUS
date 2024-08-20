using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorTrojan : MonoBehaviour
{
    public static bool podeExecutarAcoes;
    public float delayParaIniciarAcoes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DelayParaIniciarAcoes()
    {
        yield return new WaitForSeconds(delayParaIniciarAcoes);
        podeExecutarAcoes = true;
    }
}
