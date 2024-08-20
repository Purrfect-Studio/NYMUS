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

    public void definirVidaMaxima(float vidaMaxima, float vidaAtual)
    {
        slider.maxValue = vidaMaxima;
        slider.value = vidaAtual;
        this.vidaMaxima = vidaMaxima;
        numeroVida.text = vidaAtual.ToString("F0") + " / " + vidaMaxima.ToString("F0");
    }

    public void ajustarBarraDeVida(float vidaAtual)
    {
        slider.value = vidaAtual;
        numeroVida.text = vidaAtual.ToString("F0") + " / " + vidaMaxima.ToString("F0");
    }
}
