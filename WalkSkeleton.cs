using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSkeleton : StateMachineBehaviour
{

    private Skeleton skeleton;
    private Rigidbody2D rb2D;
    public float speedMovement;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // Obtiene referencias necesarias en el inicio del estado
       skeleton = animator.GetComponent<Skeleton>();
       rb2D = skeleton.rb2D;

       skeleton.lookHunter();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // Controla el movimiento del esqueleto ajustando la velocidad horizontal del Rigidbody2D.
       rb2D.velocity = new Vector2(speedMovement, rb2D.velocity.y) * animator.transform.right;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      // Restablece la velocidad horizontal del esqueleto al salir del estado.
       rb2D.velocity = new Vector2(0, rb2D.velocity.y);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}