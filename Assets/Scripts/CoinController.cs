using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinController : MonoBehaviour
{

    public int coins;
    public TextMeshProUGUI coinCountText;

    void Start() {

      coins = 0;

    }

    private void OnTriggerEnter2D(Collider2D collision) {

      if(collision.gameObject.tag == "Coin") {

       coins++;
       coinCountText.text = "" + coins;

      }

    }

}
