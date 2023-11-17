using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private LoginRegister loginRegister;

    public GameObject principalMenu = null;

    public GameObject login = null;

    private bool pausedGame = false;

    private void Update() {

        if(Input.GetKeyDown(KeyCode.Escape)) {

            if(pausedGame) {

                Resume();
            } else {

                Pause();
            }
        }
    }
    
public void Pause() {
    pausedGame = true;
    Time.timeScale = 0f;
    pauseMenu.SetActive(true);
}

public void Resume() {
    pausedGame = false;
    Time.timeScale = 1f;
    pauseMenu.SetActive(false);
}

public void Restart() {
    pausedGame = false;
Time.timeScale = 1f;
TeleportPlayerToCoordinates(new Vector3(1f, -1.04f, 0f));
pauseMenu.SetActive(false);
principalMenu.SetActive(false);
login.SetActive(false);
}

public void Quit() {

    pauseMenu.SetActive(false);
    login.SetActive(false);
    principalMenu.SetActive(true);
    
}

public void SavePlayerDataButton()
    {
        if (loginRegister != null)
        {
            // Llama a la función de guardado de datos del jugador desde LoginRegister
            loginRegister.SavePlayerData();
        }
        else
        {
            Debug.LogError("LoginRegister no asignado en el Inspector.");
        }
    }

    private void TeleportPlayerToCoordinates(Vector3 newPosition)
    {
        // Asegúrate de que tienes una referencia al GameObject del jugador (puedes ajustar esto según la estructura de tu juego)
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            // Accede al componente de transformación del jugador y establece las nuevas coordenadas
            player.transform.position = newPosition;
        }
        else
        {
            Debug.LogError("No se pudo encontrar el GameObject del jugador.");
        }
    }

}