using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public GameObject jogador;
    public GameObject porta;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void entrarPorta()
    {
        jogador.transform.position = new Vector2(porta.transform.position.x, porta.transform.position.y);
    }

}
