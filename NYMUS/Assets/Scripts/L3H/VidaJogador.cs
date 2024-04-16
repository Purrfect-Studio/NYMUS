using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaJogador : MonoBehaviour
{
    public float vidaMaxima;
    [SerializeField] private float vidaAtual;
    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaMaxima;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tomarDano(float danoTomado)
    {
        vidaAtual -= danoTomado;
    }
}
