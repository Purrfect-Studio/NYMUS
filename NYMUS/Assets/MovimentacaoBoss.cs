using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoBoss : MonoBehaviour
{
    public bool seguindoJogador;
    public Transform posicaoJogador;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (seguindoJogador)
        {
            //Object.position.y = posicaoJogador.position.y;
        }
    }

    public void ligarMovimentação()
    {
        seguindoJogador = true;
    }
}
