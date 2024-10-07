using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class alvosAcertados : MonoBehaviour
{
    public GameObject[] alvos;
    private VidaInimigo[] vidaInimigo;
    [SerializeField] private UnityEvent abrirPortao;

    public bool sala1;
    public bool sala2;
    public bool sala3;
    private bool portaoAberto;

    private bool alvo1;
    private bool alvo2;
    private bool alvo3; 
    private bool alvo4;
    private bool alvo5;
    private bool alvo6;
    private bool alvo7;
    private bool alvo8;
    private bool touro;
    // Start is called before the first frame update
    void Start()
    {
        vidaInimigo = new VidaInimigo[alvos.Length];
        for(int i = 0; i < alvos.Length; i++)
        {
            vidaInimigo[i] = alvos[i].GetComponent<VidaInimigo>();
        }
        alvo1 = false;
        alvo2 = false;
        alvo3 = false;
        alvo4 = false;
        alvo5 = false;
        alvo6 = false;
        alvo7 = false;
        alvo8 = false;
        touro = false;
        portaoAberto = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (sala1)
        {
            if(vidaInimigo[0].vidaAtual <= 0 && !alvo1)
            {
                alvo1 = true;
                //SpriteRenderer sprite = alvos[0].GetComponent<SpriteRenderer>();
                //sprite.enabled = false;
            }
            else if (vidaInimigo[1].vidaAtual <= 0 && !alvo2)
            {
                alvo2 = true;
                //alvos[1].GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (vidaInimigo[2].vidaAtual <= 0 && !alvo3)
            {
                alvo3 = true;
                //alvos[2].GetComponent<SpriteRenderer>().enabled = false;
            }
            if (alvo1 && alvo2 && alvo3 && !portaoAberto)
            {
                abrirPortao.Invoke();
                portaoAberto = true;
            }
        }
        if (sala2)
        {
            if (vidaInimigo[0].vidaAtual <= 0 && !alvo4)
            {
                alvo4 = true;
                //alvos[0].SetActive(false);
            }else if (vidaInimigo[1].vidaAtual <= 0 && !alvo5)
            {
                alvo5 = true;
                //alvos[1].SetActive(false);
            }else if (vidaInimigo[2].vidaAtual <= 0 && !alvo6)
            {
                alvo6 = true;
                //alvos[2].SetActive(false);
            }else if (vidaInimigo[3].vidaAtual <= 0 && !alvo7)
            {
                alvo7 = true;
                //alvos[3].SetActive(false);
            }else if (vidaInimigo[4].vidaAtual <= 0 && !alvo8)
            {
                alvo8 = true;
                //alvos[4].SetActive(false);
            }
            if (alvo4 && alvo5 && alvo6 && alvo7 && alvo8 && !portaoAberto)
            {
                abrirPortao.Invoke();
                portaoAberto = true;
            }
        }
        if (sala3)
        {
            if (vidaInimigo[0].vidaAtual <= 0 && !touro)
            {
                touro = true;
                //alvos[0].SetActive(false);
            }
            if (touro && !portaoAberto)
            {
                abrirPortao.Invoke();
                portaoAberto = true;
            }
        }

    }

}
