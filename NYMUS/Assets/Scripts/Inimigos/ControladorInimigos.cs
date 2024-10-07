using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorInimigos : MonoBehaviour
{
    public GameObject controlador;
    public Transform[] inimigos;
    public VidaInimigo vidaInimigo;
    public bool ativandoInimigos;
    // Start is called before the first frame update
    void Start()
    {
        controlador = this.gameObject;
        inimigos = new Transform[controlador.transform.childCount];
        for (int i = 0; i < controlador.transform.childCount; i++)
        {
            inimigos[i] = controlador.transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (VidaJogador.estaMorto && ativandoInimigos == false)
        {
            StartCoroutine("AtivarInimigos");
        }
    }

    IEnumerator AtivarInimigos()
    {
        ativandoInimigos = true;
        for (int i = 0; i < controlador.transform.childCount; i++)
        {
            if (inimigos[i].gameObject.activeSelf == false)
            {
                inimigos[i].gameObject.SetActive(true);
            }
            if (inimigos[i].GetComponent<VidaInimigo>() != null)
            {
                vidaInimigo = inimigos[i].GetComponent<VidaInimigo>();
                vidaInimigo.vidaAtual = vidaInimigo.vidaMaxima;
                VidaInimigo.podeMover = true;
            }
        }
        yield return new WaitForSeconds(1.5f);
        ativandoInimigos = false;
    }
}
