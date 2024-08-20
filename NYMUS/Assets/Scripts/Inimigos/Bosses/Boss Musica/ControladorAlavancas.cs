using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAlavancas : MonoBehaviour
{
    private GameObject controladorAlavancas;
    public Transform[] alavancas;
    public bool[] alavancasBool;
    private int contadorAlavancasDesativadas = 0;

    public static bool podeAtivarAlavancas = true;
    public float cooldownAtivarAlavancas;
    private float cooldownRestanteAtivarAlavancas;
    // Start is called before the first frame update
    void Start()
    {
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
        if(contadorAlavancasDesativadas == alavancas.Length && podeAtivarAlavancas == true)
        {
            cooldownRestanteAtivarAlavancas -= Time.deltaTime;
            if(cooldownRestanteAtivarAlavancas < 0)
            {
                ativarAlavancas();
                contadorAlavancasDesativadas = 0;
                cooldownRestanteAtivarAlavancas = cooldownAtivarAlavancas;
                podeAtivarAlavancas = false;
            }
        }
    }

    public void desativarAlavanca(int index)
    {
        alavancasBool[index] = false;
        contadorAlavancasDesativadas++;
    }

    public void ativarAlavancas()
    {
        for (int i = 0; i < alavancas.Length; i++)
        {
            alavancas[i].gameObject.SetActive(true);
            alavancasBool[i] = true;
        }
    }

    public void desativarAlavancas()
    {
        for (int i = 0; i < alavancas.Length; i++)
        {
            alavancas[i].gameObject.SetActive(false);
            alavancasBool[i] = false;
        }
    }
}
