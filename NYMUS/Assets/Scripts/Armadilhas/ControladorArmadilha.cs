using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorArmadilha : MonoBehaviour
{
    [Header("Intervalo de Ativacao da armadilha")]
    public Vector2 intervaloParaAtivarArmadilha;
    [Header("Animator")]
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(Random.Range(intervaloParaAtivarArmadilha.x, intervaloParaAtivarArmadilha.y));
        animator.enabled = true;
    }
}
