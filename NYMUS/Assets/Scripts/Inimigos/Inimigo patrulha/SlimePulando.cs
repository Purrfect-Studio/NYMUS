using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePulando : MonoBehaviour
{
    public float direcao;
    public float forcaPulo;
    public float cooldownPulo;
    private float cooldownRestantePulo;

    public Rigidbody2D rigidbody2d;
    private ProcurarJogador procurarJogador;

    [Header("Jogador")]
    private GameObject jogador;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        procurarJogador = GetComponent<ProcurarJogador>();
        jogador = GameObject.FindGameObjectWithTag("Jogador");
        cooldownRestantePulo = cooldownPulo;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownRestantePulo -= Time.deltaTime;
        if (procurarJogador.procurarJogador() == true)
        {
            if (cooldownRestantePulo <= 0)
            {
                cooldownRestantePulo = cooldownPulo;
            }
        }
        else
        {
            cooldownRestantePulo = cooldownPulo;
        }
    }

    public float definirDirecaoDoPulo()
    {
        if(transform.position.x - jogador.transform.position.x < 0) // esta a esquerda do l3h
        {
            direcao = 1;
        }else if(transform.position.x - jogador.transform.position.x > 0) // esta a direita do l3h
        {
            direcao = -1;
        }
        return direcao;
    }
}
