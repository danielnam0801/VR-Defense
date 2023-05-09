using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEmissionColor : MonoBehaviour
{
    Renderer target;
    public float emissionIntensity = 5;

    private void Awake()
    {
        target = GetComponent<Renderer>();    
    }

    public void Call(Color color)
    {
        target.material.SetColor("_EmissionColor", color * emissionIntensity);
    }
}