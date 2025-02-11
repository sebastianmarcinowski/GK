using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TunnelShaderChange : MonoBehaviour
{
    public Renderer tunnelRenderer;
    private Material tunnelMaterial;
    public Color enterColor = Color.red;
    public float enterMetallic = 0.4f;
    public Color defaultColor;
    public float defaultMetallic = 1.0f;
    void Start()
    {
        if(tunnelMaterial == null)
        {
            tunnelMaterial = tunnelRenderer.material;
            defaultColor = tunnelMaterial.color;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && tunnelMaterial != null)
        {
            tunnelMaterial.SetColor("_Color", enterColor);
            tunnelMaterial.SetFloat("_Metallic", enterMetallic);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && tunnelMaterial != null)
        {
            // Przywróæ domyœlny kolor i metalicznoœæ
            tunnelMaterial.SetColor("_Color", defaultColor);
            tunnelMaterial.SetFloat("_Metallic", defaultMetallic);
        }
    }
}
