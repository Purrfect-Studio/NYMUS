using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulo : MonoBehaviour
{

    public Rigidbody2D rigidbody2d;
    //public float alcanceEsquerda;
    public float alcanceDireita;
    public float velocidadeLimite;


    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.angularVelocity = velocidadeLimite;
    }

    // Update is called once per frame
    void Update()
    {
        movimentoPendulo();
    }

    public void movimentoPendulo()
    {
        if(transform.rotation.z > 0 && transform.rotation.z < alcanceDireita && (rigidbody2d.angularVelocity > 0) && rigidbody2d.angularVelocity < velocidadeLimite)
        {
            rigidbody2d.angularVelocity = velocidadeLimite;
        }else if(transform.rotation.z < 0 && transform.rotation.z > alcanceDireita && (rigidbody2d.angularVelocity < 0) && rigidbody2d.angularVelocity > velocidadeLimite * -1)
        {
            rigidbody2d.angularVelocity = velocidadeLimite * -1;
        }
    }
}
