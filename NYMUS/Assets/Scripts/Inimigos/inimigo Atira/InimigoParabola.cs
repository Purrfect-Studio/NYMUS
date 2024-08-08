using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoParabola : MonoBehaviour
{
    private InimigoPatrulha inimigoPatrulha;
    private AtirarEmVariosPontos atirarEmVariosPontos;
    // Start is called before the first frame update
    void Start()
    {
        inimigoPatrulha = GetComponent<InimigoPatrulha>();
        atirarEmVariosPontos = GetComponent<AtirarEmVariosPontos>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inimigoPatrulha.direcao == 1 && atirarEmVariosPontos.velocidadeTirox < 0)
        {
            atirarEmVariosPontos.velocidadeTirox *= -1;
        }
        if (inimigoPatrulha.direcao == -1 && atirarEmVariosPontos.velocidadeTirox > 0)
        {
            atirarEmVariosPontos.velocidadeTirox *= -1;
        }
    }
}
