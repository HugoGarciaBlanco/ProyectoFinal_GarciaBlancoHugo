using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
private float health;
private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage) {
        health -= damage;

        if(health <= 0) {
            Death();
        }
    }

    private void Death() {
        animator.SetTrigger("Death");
    }
}
