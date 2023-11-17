using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredSkeleton : MonoBehaviour, IDamage
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

private void Start() {
    animator = GetComponent<Animator>();
    rb2D = GetComponent<Rigidbody2D>();
    hunter = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    
}

private void Update() {
    float distanceHunter = Vector2.Distance(transform.position, hunter.position);
    animator.SetFloat("distanceHunter", distanceHunter);

    

}

public void TakeDamage(float damage) {
    if (!isDead) {

            health -= damage;

            if (health <= 0) {
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

    foreach(Collider2D colision in objects) {
        if(colision.CompareTag("Player")) {
            colision.GetComponent<HunterCombat>().TakeDamage(damageAttack);
        }
    }
}

private void OnDrawGizmos() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(skeletonAttackController.position, radioAttack);
}


}
