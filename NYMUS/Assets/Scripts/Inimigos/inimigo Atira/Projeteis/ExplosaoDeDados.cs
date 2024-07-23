using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosaoDeDados : MonoBehaviour
{
    public Animator animacao;
    public CircleCollider2D circleCollider2D;
    public VidaJogador vidaJogador;
    [SerializeField] private LayerMask layerJogador;

    public int dano;

    // Start is called before the first frame update
    void Start()
    {
        animacao = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        vidaJogador = GetComponent<VidaJogador>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
