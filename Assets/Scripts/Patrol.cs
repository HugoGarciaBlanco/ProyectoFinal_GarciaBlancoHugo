using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSkeleton : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float patrolSpeed;

    private int currentPatrolPoint = 0;
    private Rigidbody2D rb2D;
    private Skeleton skeleton;
    private Animator animator;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        skeleton = GetComponent<Skeleton>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (patrolPoints.Length == 0)
        {
            return; // No hay puntos de patrulla definidos.
        }

        Vector2 targetPosition = patrolPoints[currentPatrolPoint].position;
        Vector2 currentPosition = transform.position;

        // Calcula la dirección hacia el punto de patrulla actual.
        Vector2 direction = (targetPosition - currentPosition).normalized;

        // Mueve al Skeleton hacia el punto de patrulla actual.
        rb2D.velocity = new Vector2(direction.x * patrolSpeed, rb2D.velocity.y);

        // Si el Skeleton está cerca del punto de patrulla actual, cambia al siguiente punto.
        if (Vector2.Distance(currentPosition, targetPosition) < 0.2f)
        {
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
            skeleton.lookHunter();
        }
    }
}
