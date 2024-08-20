using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAlavancas : MonoBehaviour
{
    public GameObject controladorAlavancas;
    public Transform[] alavancas;
    public bool[] alavancasBool;
    public int contadorAlavancasDesativadas = 0;

    public float cooldownAtivarAlavancas;
    public float cooldownRestanteAtivarAlavancas;
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
        if(contadorAlavancasDesativadas == alavancas.Length)
        {
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
    }

    public void ativarAlavancas()
    {
        for (int i = 0; i < alavancas.Length; i++)
        {
            alavancas[i].gameObject.SetActive(true);
            alavancasBool[i] = true;
        }
    }
}
