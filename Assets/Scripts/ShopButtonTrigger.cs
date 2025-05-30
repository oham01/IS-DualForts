using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonTrigger : MonoBehaviour
{
    public enum TurretType { Turret1, Turret2, Turret3, Unselect }
    public TurretType turretType;

    public float triggerHeight = 1.0f;
    
    // Arrastra aquí el sonido desde Unity Inspector
    public AudioClip selectionSound;
    public AudioClip errorSound; // Sonido de error cuando no hay suficiente dinero
    
    // Indicadores separados para cada jugador
    private GameObject player1Indicator;
    private GameObject player2Indicator;
    
    // AudioSource para reproducir sonidos
    private AudioSource audioSource;

    void Start()
    {
        // Crear AudioSource para sonidos
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // No reproducir automáticamente
        
        // Crear indicador para Jugador 1
        player1Indicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        player1Indicator.name = "P1_Indicator_" + name;
        player1Indicator.transform.SetParent(transform);
        player1Indicator.transform.localPosition = new Vector3(1.5f, 2f, 10f);
        player1Indicator.transform.localRotation = Quaternion.Euler(90, 0, 0.1f);
        player1Indicator.transform.localScale = new Vector3(250f, 0.5f, 250f);
        
        Material mat1 = new Material(Shader.Find("Unlit/Color"));
        mat1.color = new Color(1.0f, 0.733f, 0.345f); // FFBB58 - Naranja amarillento
        player1Indicator.GetComponent<Renderer>().material = mat1;
        
        Destroy(player1Indicator.GetComponent<Collider>());
        player1Indicator.SetActive(false);
        
        // Crear indicador para Jugador 2
        player2Indicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        player2Indicator.name = "P2_Indicator_" + name;
        player2Indicator.transform.SetParent(transform);
        player2Indicator.transform.localPosition = new Vector3(1.5f, 2f, 10f);
        player2Indicator.transform.localScale = new Vector3(250f, 0.5f, 250f);
        
        Material mat2 = new Material(Shader.Find("Unlit/Color"));
        mat2.color = new Color(0.337f, 0.894f, 1.0f); // 56E4FF - Azul cian claro
        player2Indicator.GetComponent<Renderer>().material = mat2;
        
        Destroy(player2Indicator.GetComponent<Collider>());
        player2Indicator.SetActive(false);
        
        Debug.Log($"Indicadores creados para {name}");
    }

    public void ShowSelection(bool show)
    {
        // Método obsoleto - usar el que tiene playerNumber
        Debug.LogWarning("Usando método ShowSelection sin playerNumber - esto no debería pasar");
    }

    public void ShowSelection(bool show, int playerNumber)
    {
        if (playerNumber == 1 && player1Indicator != null)
        {
            player1Indicator.SetActive(show);
            Debug.Log($"JUGADOR 1 - Indicador NARANJA {(show ? "activado" : "desactivado")} en {name}");
            
            // Reproducir sonido solo cuando se activa
            if (show && selectionSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(selectionSound);
            }
        }
        else if (playerNumber == 2 && player2Indicator != null)
        {
            player2Indicator.SetActive(show);
            Debug.Log($"JUGADOR 2 - Indicador ROJO {(show ? "activado" : "desactivado")} en {name}");
            
            // Reproducir sonido solo cuando se activa
            if (show && selectionSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(selectionSound);
            }
        }
        else
        {
            Debug.LogWarning($"Jugador desconocido ({playerNumber}) en {name}");
        }
    }

    public void PlayErrorSound()
    {
        if (errorSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(errorSound);
            Debug.Log($"Sonido de ERROR reproducido en {name} - No hay suficiente dinero");
        }
    }

    // Check if the tracker is low enough when the player enter's the box collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float playerHeight = other.transform.position.y;

            TowerSpawner spawner = other.GetComponent<TowerSpawner>();

            if (playerHeight < triggerHeight)
            {
                Shop.Instance.SelectTurret(turretType,spawner);
            }
        }
    }

}
