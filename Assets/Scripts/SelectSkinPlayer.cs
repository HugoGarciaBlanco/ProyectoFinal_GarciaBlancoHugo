using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSkinPlayer : MonoBehaviour
{
    
    public enum Player{Hunter1, Hunter2}
    public Player selectedSkinPlayer;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public RuntimeAnimatorController[] playerController;
    public Sprite[] playerRenderer;

    void Start()
    {
       switch(selectedSkinPlayer) { 

        case Player.Hunter1:
        spriteRenderer.sprite = playerRenderer[0];
        animator.runtimeAnimatorController = playerController[0];
        break;
        case Player.Hunter2:
        spriteRenderer.sprite = playerRenderer[1];
        animator.runtimeAnimatorController = playerController[1];
        break;
        default:
        break;
       } 
    }

    
}
