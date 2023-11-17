using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrincipalMenu : MonoBehaviour
{

public GameObject principalMenu = null;
    
public void Play() {

principalMenu.SetActive(false);
}

public void Left() {
    Debug.Log("Salir...");
    Application.Quit();
}

}
