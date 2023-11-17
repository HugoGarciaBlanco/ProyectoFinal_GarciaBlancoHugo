using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HunterMovement : MonoBehaviour
{

    public float runSpeed = 1.5f;
    public float jumpSpeed = 3;
    public float doubleJumpSpeed = 2.5f;
    public float rollSpeed = 1;

    private bool doDoubleJump;
    private bool hasWallJumped = false;
    private bool isRolling = false;

    bool isTouchingWall = false;
    bool wallSliding;
    public float wallSlidingSpeed = 0.75f;
    bool isTouchingRight;
    bool isTouchingLeft;

    Rigidbody2D rb2D; //Componente para simular las físicas de los objetos

    public SpriteRenderer spriteRenderer; // Componente para renderizar sprites
    public Animator animator; // Componente para animaciones
    private BoxCollider2D boxCollider;

    void Start()
    {
        //Referencias
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update() {

     if(Input.GetKey("space")) {

        if(Checkground.isGrounded) {
            doDoubleJump = true; //Puede saltar si no estuviera en el suelo no podría volver a saltar
           rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed); //Aplicar velocidad vertical para saltar
        } else {

            if(Input.GetKeyDown("space")) {

                if(doDoubleJump) {
                    animator.SetBool("DoubleJump", true);
                   rb2D.velocity = new Vector2(rb2D.velocity.x, doubleJumpSpeed);
                   doDoubleJump = false;
                }
            }
        }
            
        }

        if(Checkground.isGrounded == false) {
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
        }

        if(Checkground.isGrounded == true) {
            animator.SetBool("Jump", false);
            animator.SetBool("DoubleJump", false);
            animator.SetBool("Falling", false);
        }

        if(rb2D.velocity.y<0) {
            animator.SetBool("Falling", true);
        } else if(rb2D.velocity.y>0) {
            animator.SetBool("Falling", false);
        }

        if(isTouchingWall == true && Checkground.isGrounded == false) {

            wallSliding = true;
        } else {

            wallSliding = false;
        }

        if(wallSliding) {

            animator.Play("Slide");
            // Limitar la velocidad vertical durante el deslizamiento
            rb2D.velocity = new Vector2(rb2D.velocity.x, Mathf.Clamp(rb2D.velocity.y, -wallSlidingSpeed, float.MaxValue));

            if (Input.GetKeyDown("space"))
        {
            if (doDoubleJump)
            {
                animator.SetBool("DoubleJump", true);
                // Aplicar una velocidad vertical para el doble salto
                rb2D.velocity = new Vector2(rb2D.velocity.x, doubleJumpSpeed);
                doDoubleJump = false;
            }
            else if (!hasWallJumped && !Checkground.isGrounded)
            {
                
                    animator.SetBool("Jump", true);
                    rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                    hasWallJumped = true;
           
                
            }

            
        }

        

        } else {
            hasWallJumped = false;
        }

         if (Input.GetKeyDown(KeyCode.LeftShift) && !isRolling && Checkground.isGrounded)
        {
            
                animator.SetBool("Roll", true);
                isRolling = true;
                // Aplicar una velocidad horizontal para el rodar
                rb2D.velocity = new Vector2(rb2D.velocity.x + (spriteRenderer.flipX ? -rollSpeed : rollSpeed), rb2D.velocity.y);
            
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && isRolling)
        {
            animator.SetBool("Roll", false);
            isRolling = false;
        }
         // Ajusta el tamaño y la posición del colisionador al rodar
         if(animator.GetCurrentAnimatorStateInfo(0).IsName("Roll")) {
            boxCollider.size = new Vector2(0.18f, 0.23f);
            boxCollider.offset = new Vector2(boxCollider.offset.x, 0.14f);
         } else {
            boxCollider.size = new Vector2(0.18f, 0.42f);
            boxCollider.offset = new Vector2(boxCollider.offset.x, 0.23f);
         }

        //  if(Input.GetKeyDown(KeyCode.Escape)) {

        //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        //     }

    }


    void FixedUpdate()
    {
if(!isRolling) {

        if(Input.GetKey("d") || Input.GetKey("right")) {
         
         rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
         spriteRenderer.flipX = false;
         animator.SetBool("Run", true);

        } else if(Input.GetKey("a") || Input.GetKey("left")) {
            
            rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = true;
            animator.SetBool("Run", true);

        } else {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            animator.SetBool("Run", false);
        }

}

    }
// Detecta si el personaje está tocando una pared en el método OnCollisionStay.
    private void OnCollisionStay2D(Collision2D collision) {

        if(collision.gameObject.CompareTag("RightWall")) {

        isTouchingWall = true;
        isTouchingRight = true;
        }

        if(collision.gameObject.CompareTag("LeftWall")) {

         isTouchingWall = true;
        isTouchingLeft = true;
        }
    }
    // Cuando el personaje ya no está tocando ninguna pared, se restablecen las variables.
    private void OnCollisionExit2D(Collision2D collision) {

        isTouchingWall = false;
        isTouchingRight = false;
        isTouchingLeft = false;
    }
}
