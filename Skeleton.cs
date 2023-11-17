using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour, IDamage
{
    
private Animator animator;
public Rigidbody2D rb2D;
public Transform hunter;
private bool lookingRight = true;

public float health;

public Transform skeletonAttackController;
public float radioAttack;
public float damageAttack;

public float deathTime = 1.0f;
private bool isDead = false;

private bool isStunned = false;
public float stunDuration = 2.0f;

private void Start() {
    animator = GetComponent<Animator>();
    rb2D = GetComponent<Rigidbody2D>();
    hunter = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    
}

private void Update() {
    float distanceHunter = Vector2.Distance(transform.position, hunter.position);
    animator.SetFloat("distanceHunter", distanceHunter);

    if(Checkground.isGrounded == true) {
            animator.SetBool("Falling", false);
        }

        if(rb2D.velocity.y<0) {
            animator.SetBool("Falling", true);
        } else if(rb2D.velocity.y>0) {
            animator.SetBool("Falling", false);
        }
}

public void TakeDamage(float damage) {

if(!isDead) {
health -= damage;

if(health <= 0) {
    isDead = true;
    animator.SetTrigger("Death");
    Invoke("Death", deathTime);
} else {
    animator.SetTrigger("Hit");
}
}
}

private void Death() {
    Destroy(gameObject);
}

public void lookHunter() {

    if((hunter.position.x > transform.position.x && !lookingRight) || (hunter.position.x < transform.position.x && lookingRight)) {
        lookingRight = !lookingRight;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }
}

public void Attack() {
    Collider2D[] objects = Physics2D.OverlapCircleAll(skeletonAttackController.position, radioAttack);

    foreach (Collider2D collision in objects) {
        if (collision.CompareTag("Player")) {
            HunterCombat playerCombat = collision.GetComponent<HunterCombat>();

            if (playerCombat.isParrying) {

            } else {
                playerCombat.TakeDamage(damageAttack);
            }
        }
    }
}


private void OnDrawGizmos() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(skeletonAttackController.position, radioAttack);
}

private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("VoidDamage"))
        {
            isDead = true;
            animator.SetTrigger("Death");
            Invoke("Death", deathTime);
        }
    }

    public void StunEnemy() {
    isStunned = true;
    animator.SetTrigger("Stun");
    Invoke("EndStun", stunDuration);
}

private void EndStun() {
    isStunned = false;
}


}
