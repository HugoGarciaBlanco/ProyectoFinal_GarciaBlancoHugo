using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDamage : MonoBehaviour
{
    
private void OnCollisionEnter2D(Collision2D collision) {
    if(collision.transform.CompareTag("Player")) {
        Debug.Log("Player Damaged");
        collision.transform.GetComponent<HunterRespawn>().HunterDamaged();
    }
}

}