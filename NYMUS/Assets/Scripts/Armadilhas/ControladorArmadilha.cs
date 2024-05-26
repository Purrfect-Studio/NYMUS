using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControladorArmadilha : MonoBehaviour
{
    [Header("Intervalo de Ativacao da armadilha")]
    public float tempoDesativada;
    public float quantidadeDeEspetadas;
    public float delayParaComecar;
    [Header("Animator")]
    private Animator animator;

    IEnumerator Ativar()
    {
        yield return new WaitForSeconds(tempoDesativada);
        animator.SetBool("ativarArmadilha", true);
        StartCoroutine("Desativar");
    }

    IEnumerator Desativar()
    {
        yield return new WaitForSeconds(quantidadeDeEspetadas);
        animator.SetBool("ativarArmadilha", false);
        StartCoroutine("Ativar");
    }

    IEnumerator Comecar()
    {
        yield return new WaitForSeconds(delayParaComecar);
        StartCoroutine("Ativar");
    }

    private void Start()
    {
        StartCoroutine("Comecar"); 
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
