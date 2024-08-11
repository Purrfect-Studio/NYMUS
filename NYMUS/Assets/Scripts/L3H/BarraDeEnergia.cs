using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeEnergia : MonoBehaviour
{
    public Slider slider;

    public void definirEnergiaMaxima(float energiaMaxima)
    {
        slider.maxValue = energiaMaxima;
        slider.value = energiaMaxima;
    }

    public void ajustarBarraDeEnergia(float energiaAtual)
    {
        slider.value = energiaAtual;
    }
}
