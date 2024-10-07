using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class alvosAcertados : MonoBehaviour
{
    public GameObject[] alvos;
    public VidaInimigo[] desativado;
    [SerializeField] private UnityEvent abrirPortao;

    public bool sala1;
    public bool sala2;
    public bool sala3;
    // Start is called before the first frame update
    void Start()
    {
        desativado = new VidaInimigo[alvos.Length];
        for(int i = 0; i < alvos.Length; i++)
        {
            desativado[i] = alvos[i].GetComponent<VidaInimigo>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sala1)
        {
            if (desativado[0].vidaAtual <= 0 && desativado[1].vidaAtual <= 0 && desativado[2].vidaAtual <= 0)
            {
                abrirPortao.Invoke();
                gameObject.SetActive(false);
            }
        }
        if (sala2)
        {
            if (desativado[0].vidaAtual <= 0 && desativado[1].vidaAtual <= 0 && desativado[2].vidaAtual <= 0 && desativado[3].vidaAtual <= 0 && desativado[4].vidaAtual <= 0)
            {
                abrirPortao.Invoke();
                gameObject.SetActive(false);
            }
        }
        if (sala3)
        {
            if (desativado[0].vidaAtual <= 0)
            {
                abrirPortao.Invoke();
                gameObject.SetActive(false);
            }
        }

    }

}
