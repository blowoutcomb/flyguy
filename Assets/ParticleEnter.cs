using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEnter : MonoBehaviour
{
    public ParticleSystem rain;
   
    void Start()
    {
        rain.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            rain.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            rain.Stop();
        }
    }
}
