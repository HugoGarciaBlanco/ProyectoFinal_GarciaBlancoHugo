using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HunterCombat : MonoBehaviour
{
   
public Animator animator;
public float hunterHealth;
public float deathTime = 1.0f;
private bool isDead = false;

public bool isParrying = false;
private bool isParalyzed = false;
private bool isParrySuccessful = false;
public float parryDuration = 1.0f;
public Skeleton enemyScript; 

public float parryCooldown = 2.0f;
private float lastParryTime;

private void Update() {
    if (Input.GetKeyDown(KeyCode.W) && !isParrying && !isParalyzed && Time.time - lastParryTime >= parryCooldown) {
    isParrying = true;
    animator.SetTrigger("Parry");
    isParrySuccessful = true;
    lastParryTime = Time.time; 
    Invoke("EndParry", parryDuration);
}

}

public void TakeDamage(float damage) {
    if(!isDead) {
    hunterHealth -= damage;

    if(hunterHealth <= 0) {
        isDead = true;
    animator.SetTrigger("Death");
    
    Invoke("Respawn", deathTime);
    
    
    
} else {
    animator.SetTrigger("Hit");
}
    }
}

public void Respawn() {
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

private void OnTriggerEnter2D(Collider2D collision)
    {
        // Maneja eventos cuando el cazador entra en colisión con objetos etiquetados como "VoidDamage".
        if (collision.CompareTag("VoidDamage"))
        {
            isDead = true;
            animator.SetTrigger("Death");
            Invoke("Respawn", deathTime);
        }
    }

    private void EndParry() {
    if (isParrySuccessful) {
        enemyScript.StunEnemy();
    }
    isParrying = false;
    isParrySuccessful = false;
}



}
