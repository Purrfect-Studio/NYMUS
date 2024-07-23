using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaBoss : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void definirVidaMaxima(float vidaMaxima)
    {
        slider.maxValue = vidaMaxima;
        slider.value = vidaMaxima;
    }

    public void ajustarBarraDeVida(float vidaAtual)
    {
        slider.value = vidaAtual;
    }
}
