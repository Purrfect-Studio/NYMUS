using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAlavancas : MonoBehaviour
{
    private GameObject controladorAlavancas;
    public Transform[] alavancas;
    public bool[] alavancasBool;
    private int contadorAlavancasDesativadas = 0;
    public float cooldownParaAtivarAlavanca;

    public bool todasAlavancasDesativadas;
    public float cooldownAtivarAlavancas;
    private float cooldownRestanteAtivarAlavancas;
    // Start is called before the first frame update
    void Start()
    {
        todasAlavancasDesativadas = false;
        controladorAlavancas = this.gameObject;
        alavancas = new Transform[controladorAlavancas.transform.childCount];
        alavancasBool = new bool[controladorAlavancas.transform.childCount];
        for (int i = 0; i < alavancas.Length; i++)
        {
            alavancas[i] = controladorAlavancas.transform.GetChild(i);
            alavancasBool[i] = true;
        }
        cooldownRestanteAtivarAlavancas = cooldownAtivarAlavancas;
    }

    // Update is called once per frame
    void Update()
    {
        if(contadorAlavancasDesativadas == alavancas.Length)
        {
            todasAlavancasDesativadas = true;
            cooldownRestanteAtivarAlavancas -= Time.deltaTime;
            if(cooldownRestanteAtivarAlavancas < 0)
            {
                ativarAlavancas();
                contadorAlavancasDesativadas = 0;
                cooldownRestanteAtivarAlavancas = cooldownAtivarAlavancas;
            }
        }
    }

    public void desativarAlavanca(int index)
    {
        alavancasBool[index] = false;
        contadorAlavancasDesativadas++;
        StartCoroutine(cooldownAtivarAlavanca());
    }

    IEnumerator cooldownAtivarAlavanca()
    {
        for(int i = 0; i < alavancas.Length; i++)
        {
            alavancas[i].GetComponent<InteragirBotao>().enabled = false;
        }
        yield return new WaitForSeconds(cooldownParaAtivarAlavanca);
        for (int i = 0; i < alavancas.Length; i++)
        {
            alavancas[i].GetComponent<InteragirBotao>().enabled = true;
        }
    }

    public void ativarAlavancas()
    {
        for (int i = 0; i < alavancas.Length; i++)
        {
            alavancas[i].gameObject.SetActive(true);
            alavancasBool[i] = true;
        }
        todasAlavancasDesativadas = false;
    }

    public void desativarAlavancas()
    {
        for (int i = 0; i < alavancas.Length; i++)
        {
            alavancas[i].gameObject.SetActive(false);
            alavancasBool[i] = false;
        }
        todasAlavancasDesativadas = true;
    }
}
