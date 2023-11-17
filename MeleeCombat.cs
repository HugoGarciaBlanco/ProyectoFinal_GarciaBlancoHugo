using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    public Transform bangController;
    public float bangRadio;
    public float bangDamage;

    public float timeBetweenAttack;
    public float timeNextAttack;

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update() {

        if(timeNextAttack > 0) {
            timeNextAttack -= Time.deltaTime;
        }

        if(Input.GetMouseButtonDown(0) && timeNextAttack <= 0) {
            Bang();
            timeNextAttack = timeBetweenAttack;
        } 
    }

    private void Bang()
{
    animator.SetTrigger("Bang");

    // Determina la dirección del ataque en función de la orientación del personaje
    Vector3 attackDirection = spriteRenderer.flipX ? Vector3.left : Vector3.right;
    Vector3 attackPosition = transform.position + (attackDirection * bangController.localPosition.x);

    //Detecta las colisiones que hay en ese area
    Collider2D[] objects = Physics2D.OverlapCircleAll(attackPosition, bangRadio);

    //Recorre los objetos que estén en ese area
    foreach (Collider2D colisionator in objects)
    {
        //El IDamage es que con un solo script pueda atacar a todos los enemigos
        IDamage object1 = colisionator.GetComponent<IDamage>();

        if(object1 != null) {
            object1.TakeDamage(bangDamage);
        }
    }
}

//Crea el area
    private void OnDrawGizmos()
{
    Gizmos.color = Color.red;

    Vector3 offset = spriteRenderer.flipX ? new Vector3(-bangController.localPosition.x, bangController.localPosition.y, 0) : bangController.localPosition;
    Vector3 gizmoPosition = transform.position + offset;

    Gizmos.DrawWireSphere(gizmoPosition, bangRadio);
}



}
