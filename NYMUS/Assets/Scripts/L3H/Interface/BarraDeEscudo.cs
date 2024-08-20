using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeEscudo : MonoBehaviour
{
    public Slider slider;

    public void definirEscudoMaximo(float escudoMaximo)
    {
        slider.maxValue = escudoMaximo;
        slider.value = 0;
    }

    public void ajustarBarraDeEscudo(float escudoAtual)
    {
        slider.value = escudoAtual;
    }
}
