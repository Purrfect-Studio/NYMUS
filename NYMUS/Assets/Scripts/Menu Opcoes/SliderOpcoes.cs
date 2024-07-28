using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderOpcoes : MonoBehaviour
{
    public Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void aumentarValorDoSlide(int valor)
    {
        slider.value += valor;
    }

    public void diminuirValorDoSlide(int valor)
    {
        slider.value -= valor;
    }

}
