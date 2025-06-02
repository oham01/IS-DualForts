using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Número del jugador (configurar en el inspector: 1 o 2)
    public int playerNumber = 1;

    // Para rotaciones
    public Quaternion q;
    public bool manual;

    [Header("Cursor visual opcional")]
    public Sprite handSprite; // ← Aquí puedes arrastrar el sprite desde el Inspector

    private GameObject cursorHand;

    // Start is called before the first frame update
    void Start()
    {
        TryCreateHandVisual();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Setter for position
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    // Getter for position
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    // Setter for rotation
    public void SetRotation(Quaternion rot)
    {
        transform.rotation = rot;
    }

    // Getter for rotation
    public Quaternion GetRotation()
    {
        return transform.rotation;
    }

    // Crea la mano visual solo si se ha asignado un sprite
void TryCreateHandVisual()
{
    if (handSprite == null) return; // Si no hay sprite, no hacer nada

    cursorHand = new GameObject("CursorHand");
    cursorHand.transform.SetParent(this.transform);
    cursorHand.transform.localPosition = new Vector3(0, 3f, 0); // Ajusta altura si hace falta

    // Gira la mano para que quede plana mirando hacia abajo
    cursorHand.transform.localRotation = Quaternion.Euler(90, 0, 0);
    cursorHand.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

    SpriteRenderer sr = cursorHand.AddComponent<SpriteRenderer>();
    sr.sprite = handSprite;
    sr.sortingOrder = 10; // Para que se vea por encima
}
}
