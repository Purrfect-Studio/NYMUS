using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public Slider slider;

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
