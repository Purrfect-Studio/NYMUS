using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControladorArmadilha : MonoBehaviour
{
    [Header("Intervalo de Ativacao da armadilha")]
    public Vector2 intervaloParaAtivarArmadilha;
    public bool ativarArmadilha;
    [Header("Animator")]
    private Animator animator;

    IEnumerator ativar()
    {
        yield return new WaitForSeconds(Random.Range(intervaloParaAtivarArmadilha.x, intervaloParaAtivarArmadilha.y));
        animator.SetBool("ativarArmadilha", true);
    }

    private void Update()
    {
        StartCoroutine("ativar"); 
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /*IEnumerator Start()
    {
        yield return new WaitForSeconds(Random.Range(intervaloParaAtivarArmadilha.x, intervaloParaAtivarArmadilha.y));
        animator.enabled = true;
    }*/
}
