using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HunterRespawn : MonoBehaviour
{

public Animator animator;

    public void HunterDamaged() {

    animator.Play("Hit");
    TeleportPlayerToCoordinates(new Vector3(1f, -1.04f, 0f));

    }

    private void TeleportPlayerToCoordinates(Vector3 newPosition)
    {
        // Asegúrate de que tienes una referencia al GameObject del jugador (puedes ajustar esto según la estructura de tu juego)
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            // Accede al componente de transformación del jugador y establece las nuevas coordenadas
            player.transform.position = newPosition;
        }
        else
        {
            Debug.LogError("No se pudo encontrar el GameObject del jugador.");
        }
    }
}
