using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorArmadilha : MonoBehaviour
{
    public Vector2 intervaloParaAtivarArmadilha;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(Random.Range(intervaloParaAtivarArmadilha.x, intervaloParaAtivarArmadilha.y));
        animator.enabled = true;
    }
}
