using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorInterage : MonoBehaviour
{
    public bool estaInteragindo { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interagir"))
        {
            estaInteragindo = true;
        }
        else
        {
            estaInteragindo = false;
        }
    }
}
