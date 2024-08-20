using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeEnergia : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI numeroEnergia;
    private float energiaMaxima;

    public void definirEnergiaMaxima(float energiaMaxima)
    {
        this.energiaMaxima = energiaMaxima;
        slider.maxValue = energiaMaxima;
        slider.value = energiaMaxima;
        numeroEnergia.text = energiaMaxima.ToString("F1") + " / " + energiaMaxima.ToString("F1");
    }

    public void ajustarBarraDeEnergia(float energiaAtual)
    {
        slider.value = energiaAtual;
        numeroEnergia.text = energiaAtual.ToString("F1") + " / " + energiaMaxima.ToString("F1");
    }
}
