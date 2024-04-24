using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InimigoAtira : MonoBehaviour
{
    public GameObject balaProjetil; // projetil
    public Transform arma; // posicao de onde o projetil sai
    public float forcaTiro;
    private bool flipX;

    public float intervaloTiro;
    public float contadorIntervaloTiro;
    public Transform jogador;

    [SerializeField] private UnityEvent DanoCausado;
    public static bool acertouJogador;
    


    // Start is called before the first frame update
    void Start()
    {
        contadorIntervaloTiro = intervaloTiro;
        acertouJogador = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(contadorIntervaloTiro < 0)
        {
            atirar();
        }
        else
        {
            contadorIntervaloTiro -= Time.deltaTime;
        }

        if (acertouJogador == true)
        {
            if (VidaJogador.invulneravel == false)
            {
                if (transform.position.x <= jogador.transform.position.x)
                {
                    VidaJogador.knockbackParaDireita = -1;
                }
                else
                {
                    VidaJogador.knockbackParaDireita = 1;
                }
                DanoCausado.Invoke();
                acertouJogador = false;
            }
        }
    }

    void atirar()
    {
        contadorIntervaloTiro = intervaloTiro;
        GameObject temp = Instantiate(balaProjetil);
        temp.transform.position = arma.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(forcaTiro, 0);
        Destroy(temp.gameObject, 3f);
    }

    

}
