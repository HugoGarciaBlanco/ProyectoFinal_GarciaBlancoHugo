using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public GameObject shopMenu = null;
    public GameObject principalMenu = null;
    public GameObject login = null;
    [SerializeField] private GameObject pauseMenu;

    private bool pausedGame = false;

    public CoinController coinController;
    public TextMeshProUGUI coinCountText;
    public HunterCombat hunterCombat;
    public MeleeCombat meleeCombat;

    private Response lastPurchaseResponse;
    public static ShopMenu Instance;
    private TMP_InputField loginUserName;

    public GameObject principalMenuUI = null;

    private string response;

    public void HandleServerResponse(Response response)
    {
        lastPurchaseResponse = response;

        // Puedes agregar más lógica aquí si es necesario

        if (NetworkManager.Instance != null) {
    // Llamar a BuyLifeItem() u otros métodos aquí si es necesario
    BuyLifeItem();
} else {
    Debug.LogError("NetworkManager.Instance no está inicializado. Asegúrate de que el objeto NetworkManager esté en la escena y se haya inicializado correctamente.");
}
    }

    

    private void Update() {

        if(Input.GetKeyDown(KeyCode.Q)) {

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
    shopMenu.SetActive(true);
}

public void Resume() {
    pausedGame = false;
    Time.timeScale = 1f;
    shopMenu.SetActive(false);
}
 
public void BuyLifeItem()
    {

        Debug.Log("BuyLifeItem start");

        Canvas loginCanvas = GameObject.Find("LoginRegisterCanvas").GetComponent<Canvas>();

if (loginCanvas != null)
{
    // Buscar el objeto imagen llamado "Login" dentro del Canvas
    Image loginImage = loginCanvas.transform.Find("Login").GetComponent<Image>();

    if (loginImage != null)
    {
        // Buscar el componente TMP_InputField dentro de la imagen "Login"
        loginUserName = loginImage.GetComponentInChildren<TMP_InputField>();

        if (loginUserName != null)
        {

            Debug.Log("userName: " + loginUserName.text);
            // Hacer algo con loginUserName
            Debug.Log("Valor de loginUserName: " + loginUserName.text);
        } 
        else
        {
            Debug.LogError("La imagen Login no contiene un componente TMP_InputField.");
            return;
        }
    }
    else
    {
        Debug.LogError("No se pudo encontrar la imagen Login en el Canvas LoginRegisterCanvas.");
        return;
    }
}
else
{
    Debug.LogError("No se pudo encontrar el Canvas LoginRegisterCanvas en la escena.");
    return;
}

        int lifeItemCost = 10;  // Costo del ítem de vida

        

        if (coinController != null && coinCountText != null && hunterCombat != null && NetworkManager.Instance != null)
    {

        // Verificar si el jugador tiene suficientes monedas para comprar el ítem
        if (coinController.coins >= lifeItemCost)
        {
            // Restar el costo del ítem de vida y actualizar la UI de las monedas
            coinController.coins -= lifeItemCost;
            coinCountText.text = "" + coinController.coins;

            Debug.Log("itemName: Vida");
            Debug.Log("itemPrice: " + lifeItemCost);

            // Aumentar la vida del jugador
            hunterCombat.hunterHealth += 10;  // Ajusta según tus necesidades

            if (lastPurchaseResponse == null)
    {

    
        Debug.LogWarning("LastPurchaseResponse es null. Intentando obtener una respuesta actualizada.");
        // Intenta obtener una respuesta actualizada
        lastPurchaseResponse = NetworkManager.Instance.GetLastPurchaseResponse(); // Reemplaza con el método real que obtiene la respuesta

        if (lastPurchaseResponse == null)
        {
            Debug.LogWarning("LastPurchaseResponse sigue siendo nulo. No se puede continuar con la compra de ítem de vida.");
            return;
        }
    }

         if (lastPurchaseResponse != null) {

            Debug.Log("LastPurchaseResponse antes de la verificación: " + lastPurchaseResponse);

            if (lastPurchaseResponse.done)
        {
            Debug.Log("Compra de ítem de vida registrada exitosamente en la base de datos.");
            NetworkManager.Instance.RegisterPurchase("Vida", lifeItemCost, loginUserName.text);
        }
        else
            {
                Debug.Log("LastPurchaseResponse antes de la verificación: " + lastPurchaseResponse);
Debug.Log("Mensaje de la respuesta del servidor: " + lastPurchaseResponse.message);
Debug.Log("Antes de la advertencia");
Debug.LogWarning("La compra de ítem de vida no se registró en la base de datos. Respuesta del servidor: " + lastPurchaseResponse.message);
Debug.Log("LastPurchaseResponse después de la verificación: " + lastPurchaseResponse);
Debug.Log("Después de la advertencia");            }

} else {
    Debug.LogWarning("lastPurchaseResponse es nulo. Asegúrate de inicializarlo correctamente antes de usarlo.");
}
         
        }
        else
        {
            // Manejar caso en el que el jugador no tiene suficientes monedas
            Debug.Log("No tienes suficientes monedas para comprar el ítem de vida.");
        }

        }
    else
    {
        Debug.LogError("Al menos una de las referencias en BuyLifeItem() es nula...");
    }
    
    }

    public void BuyDamageItem()
    {

        Canvas loginCanvas = GameObject.Find("LoginRegisterCanvas").GetComponent<Canvas>();

if (loginCanvas != null)
{
    // Buscar el objeto imagen llamado "Login" dentro del Canvas
    Image loginImage = loginCanvas.transform.Find("Login").GetComponent<Image>();

    if (loginImage != null)
    {
        // Buscar el componente TMP_InputField dentro de la imagen "Login"
        loginUserName = loginImage.GetComponentInChildren<TMP_InputField>();

        if (loginUserName != null)
        {
            // Hacer algo con loginUserName
            Debug.Log("Valor de loginUserName: " + loginUserName.text);
        } 
        else
        {
            Debug.LogError("La imagen Login no contiene un componente TMP_InputField.");
            return;
        }
    }
    else
    {
        Debug.LogError("No se pudo encontrar la imagen Login en el Canvas LoginRegisterCanvas.");
        return;
    }
}
else
{
    Debug.LogError("No se pudo encontrar el Canvas LoginRegisterCanvas en la escena.");
    return;
}

        int damageItemCost = 10;  // Costo del ítem de daño

      if (coinController != null && coinCountText != null && hunterCombat != null && NetworkManager.Instance != null)
    {

        // Verificar si el jugador tiene suficientes monedas para comprar el ítem
        if (coinController.coins >= damageItemCost)
        {
            // Restar el costo del ítem de daño y actualizar la UI de las monedas
            coinController.coins -= damageItemCost;
            coinCountText.text = "" + coinController.coins;

            // Aumentar el daño del jugador
            meleeCombat.bangDamage += 5;  // Ajusta según tus necesidades

            if (lastPurchaseResponse == null)
    {
        Debug.LogWarning("LastPurchaseResponse es null. Intentando obtener una respuesta actualizada.");
        // Intenta obtener una respuesta actualizada
        lastPurchaseResponse = NetworkManager.Instance.GetLastPurchaseResponse(); // Reemplaza con el método real que obtiene la respuesta

        if (lastPurchaseResponse == null)
        {
            Debug.LogWarning("LastPurchaseResponse sigue siendo nulo. No se puede continuar con la compra de ítem de daño.");
            return;
        }
    }

            if (lastPurchaseResponse != null) {

            Debug.Log("LastPurchaseResponse antes de la verificación: " + lastPurchaseResponse);

            if (lastPurchaseResponse.done)
        {
            Debug.Log("Compra de ítem de daño registrada exitosamente en la base de datos.");
            NetworkManager.Instance.RegisterPurchase("Daño", damageItemCost, loginUserName.text);
        }
        else
            {
                Debug.LogWarning("La compra de ítem de daño no se registró en la base de datos. Respuesta del servidor: " + lastPurchaseResponse.message);
            }

            } else {
    Debug.LogWarning("lastPurchaseResponse es nulo. Asegúrate de inicializarlo correctamente antes de usarlo.");
}
         
        }
        else
        {
            // Manejar caso en el que el jugador no tiene suficientes monedas
            Debug.Log("No tienes suficientes monedas para comprar el ítem de daño.");
        }

        }
    else
    {
       Debug.LogError("Al menos una de las referencias en BuyLifeItem() es nula. " +
                   "CoinController: " + coinController +
                   ", CoinCountText: " + coinCountText +
                   ", HunterCombat: " + hunterCombat +
                   ", LastPurchaseResponse: " + lastPurchaseResponse +
                   ", NetworkManager.Instance: " + NetworkManager.Instance);
    }

    }

    public void SetLastPurchaseResponse(Response response)
    {
        lastPurchaseResponse = response;
    }

    

}
