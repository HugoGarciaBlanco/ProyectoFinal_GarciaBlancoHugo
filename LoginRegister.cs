using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginRegister : MonoBehaviour
{
[Header("Login")]

[SerializeField] private TMP_InputField loginUserName = null;
[SerializeField] private TMP_InputField loginPassword = null;

[Header("Register")]

    [SerializeField] private TMP_InputField userName = null;
    [SerializeField] private TMP_InputField email = null;
    [SerializeField] private TMP_InputField password = null;
    [SerializeField] private TMP_InputField repeatPass = null;
    [SerializeField] private TextMeshProUGUI errorText = null;

    private NetworkManager networkManager = null;

    public GameObject registerUI = null;
    public GameObject loginUI = null;

    public GameObject principalMenuUI = null;

    [Header("Player Data")]
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private float playerHealth;
    [SerializeField] private float playerDamage;

    private void Awake()
    {
        networkManager = GameObject.FindObjectOfType<NetworkManager>();
    }

    public void SubmitLogin() {

      if (loginUserName.text == "" || loginPassword.text == "")
        {
            errorText.text = "Faltan datos";
            return;
        }

        errorText.text = "Procesando...";

        if(networkManager != null) {

            networkManager.CheckUser(loginUserName.text, loginPassword.text, delegate(Response response)
            {
                if(response.done) {
                    Debug.Log("Inicio de sesión exitoso");
                    LoadPlayerData();
                } else {
                errorText.text = response.message;
                }
            });
        } else {
            Debug.LogError("El objeto networkManager no está asignado en el Inspector.");
        }
    }

    private void LoadPlayerData()
    {
        networkManager.LoadPlayerData(loginUserName.text, (playerData) =>
        {

            if(playerData != null) {

            // Actualiza las variables del jugador con los datos cargados
            playerPosition = new Vector2(playerData.playerPositionX, playerData.playerPositionY);
            playerHealth = playerData.playerHealth;
            playerDamage = playerData.playerDamage;
            Debug.Log("Datos del jugador cargados exitosamente");
            loginUI.SetActive(false);
            principalMenuUI.SetActive(true);

            } else {
                Debug.Log("El jugador no tiene datos guardados");
                loginUI.SetActive(false);
                principalMenuUI.SetActive(true);
            }
        });
    }

    public void SavePlayerData()
    {
        networkManager.SavePlayerData(loginUserName.text, transform.position.x, transform.position.y, playerHealth, playerDamage, (response) =>
        {
            if (response.done)
            {
                Debug.Log("Datos del jugador guardados exitosamente");
            }
            else
            {
                Debug.LogError("Error al guardar los datos del jugador: " + response.message);
            }
        });
    }

    public void SubmitRegister()
    {
        if (userName.text == "" || email.text == "" || password.text == "" || repeatPass.text == "")
        {
            errorText.text = "Faltan datos";
            return;
        }

        if (password.text == repeatPass.text)
        {
            errorText.text = "Procesando...";

            networkManager.CreateUser(userName.text, email.text, password.text, delegate(Response response)
            {
                errorText.text = response.message;
            });
        }
        else
        {
            errorText.text = "Las contrasenas no son iguales";
        }
    }

    public void ShowLogin()
    {
        registerUI.SetActive(false);
        loginUI.SetActive(true);
    }

    public void ShowRegister()
    {
        registerUI.SetActive(true);
        loginUI.SetActive(false);
    }

    
}