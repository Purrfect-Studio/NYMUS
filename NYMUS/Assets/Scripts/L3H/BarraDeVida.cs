using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI numeroVida;
    private float vidaMaxima;

    public void definirVidaMaxima(float vidaMaxima)
    {
        slider.maxValue = vidaMaxima;
        slider.value = vidaMaxima;
        this.vidaMaxima = vidaMaxima;
        numeroVida.text = vidaMaxima.ToString("F0") + " / " + vidaMaxima.ToString("F0");
    }

    public void ajustarBarraDeVida(float vidaAtual)
    {
        slider.value = vidaAtual;
        numeroVida.text = vidaAtual.ToString("F0") + " / " + vidaMaxima.ToString("F0");
    }
}
