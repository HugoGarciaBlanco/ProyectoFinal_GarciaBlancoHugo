using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{

    public static NetworkManager Instance;
    private Response lastPurchaseResponse;

    private void Awake()
    {
        Instance = this;  // Agrega esta línea
        Debug.Log("ShopMenu Awake");
    }

    public Response GetLastPurchaseResponse()
{
    return lastPurchaseResponse;
}

    public void HandleServerResponse(Response response)
    {
         Debug.Log("Manejando respuesta del servidor. Hecho: " + response.done + ", Mensaje: " + response.message);
    }

    public void RegisterPurchase(string itemName, float itemPrice, string userName)
    {
        StartCoroutine(CO_RegisterPurchase(itemName, itemPrice, userName));
    }

    private IEnumerator CO_RegisterPurchase(string itemName, float itemPrice, string userName)
    {

        Debug.Log("Inicio del método CO_RegisterPurchase");

        WWWForm form = new WWWForm();
        form.AddField("itemName", itemName);
        form.AddField("itemPrice", itemPrice.ToString());
        form.AddField("userName", userName);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/TheLastHunter/registerPurchase.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {

                Debug.Log("La solicitud fue exitosa");

                if (www.downloadHandler.text.Contains("Funciona"))
            {

                string jsonResponse = www.downloadHandler.text.Replace("Funciona", "").Trim();

                try
                {
                    // Intentar deserializar el JSON
                    PlayerDataResponse responseData = JsonUtility.FromJson<PlayerDataResponse>(jsonResponse);

                    // ... (continuar con el manejo normal del JSON)
                }
                catch (Exception e)
                {
                    Debug.LogError("Error parsing JSON: " + e.Message);
                }
            }
            else
            {
                Debug.LogError("Server Response (Invalid JSON): " + www.downloadHandler.text);
            }
                Debug.LogError("Error al registrar la compra en la base de datos: " + www.error);
            }
            else
            {
                Debug.LogError("Error al registrar la compra en la base de datos: " + www.error);
            yield break; // Salir del método si hay un error en la solicitud web
        }
                Debug.Log("Compra registrada exitosamente en la base de datos.");
                lastPurchaseResponse = JsonUtility.FromJson<Response>(www.downloadHandler.text);

                if (lastPurchaseResponse == null)
    {
        Debug.LogError("Respuesta del servidor no válida: " + www.downloadHandler.text);
        yield break; // Salir del método si la respuesta no es válida
    }

                Debug.Log("Respuesta del servidor: " + lastPurchaseResponse.message);

                if (lastPurchaseResponse.done)
    {
                // Llamar a HandleServerResponse para manejar la respuesta
            HandleServerResponse(lastPurchaseResponse);

            }
    else
    {
Debug.Log("Antes de la advertencia");
Debug.LogWarning("La compra de ítem de vida no se registró en la base de datos. Respuesta del servidor: " + lastPurchaseResponse.message);
Debug.Log("Después de la advertencia");    
}

Debug.Log("Fin del método CO_RegisterPurchase");
            }
        }
    

    
   
public void CreateUser(string userName, string email, string pass, Action<Response> response) {

StartCoroutine(CO_CreateUser(userName, email, pass, response));

}

private IEnumerator CO_CreateUser(string userName, string email, string pass, Action<Response> response) {

    WWWForm form = new WWWForm();
    form.AddField("userName", userName);
    form.AddField("email", email);
    form.AddField("pass", pass);

    using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/TheLastHunter/createUser.php", form)) {

    yield return www.SendWebRequest();

    if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Network Error: " + www.error);
                // Manejar el error aquí, por ejemplo, llamando a una función de respuesta de error
            }
            else
            {
                Debug.Log("Server Response: " + www.downloadHandler.text);
            string jsonResponse = www.downloadHandler.text;

            // Eliminar el texto no deseado si es necesario
            int jsonStart = jsonResponse.IndexOf("{");
            if (jsonStart != -1)
            {
                jsonResponse = jsonResponse.Substring(jsonStart);
            }

            response(JsonUtility.FromJson<Response>(jsonResponse));
            }
}
}

public void CheckUser(string userName, string pass, Action<Response> response) {

StartCoroutine(CO_CheckUser(userName, pass, response));

}

private IEnumerator CO_CheckUser(string userName, string pass, Action<Response> response)
{
    WWWForm form = new WWWForm();
    form.AddField("userName", userName);
    form.AddField("pass", pass);

    using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/TheLastHunter/checkUser.php", form))
    {
        
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Network Error: " + www.error);
            // Manejar el error aquí, por ejemplo, llamando a una función de respuesta de error
        }
        else
        {
            Debug.Log("Server Response: " + www.downloadHandler.text);
            string jsonResponse = www.downloadHandler.text;

            // Eliminar el texto no deseado si es necesario
            int jsonStart = jsonResponse.IndexOf("{");
            if (jsonStart != -1)
            {
                jsonResponse = jsonResponse.Substring(jsonStart);
            }

            Response serverResponse = JsonUtility.FromJson<Response>(jsonResponse);

            // Verificar si el inicio de sesión fue exitoso o no
            if (serverResponse.done)
            {
                // Procesar los datos del jugador si están presentes
                int dataStart = jsonResponse.IndexOf("{", serverResponse.message.Length);
                if (dataStart != -1)
                {
                    string playerDataJson = jsonResponse.Substring(dataStart);
                    PlayerData playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);

                    // Aquí puedes hacer algo con los datos del jugador si es necesario
                }

                response(serverResponse);
            }
            else
            {
                // El inicio de sesión falló, muestra el mensaje de error al usuario
                Debug.LogError("Inicio de sesión fallido: " + serverResponse.message);
                // Puedes mostrar un mensaje de error al usuario o manejarlo de otra manera
            }
        }
    }
}

public void SavePlayerData(string userName, float playerPositionX, float playerPositionY, float playerHealth, float playerDamage, Action<Response> response)
    {
        StartCoroutine(CO_SavePlayerData(userName, playerPositionX, playerPositionY, playerHealth, playerDamage, response));
    }

    private IEnumerator CO_SavePlayerData(string userName, float playerPositionX, float playerPositionY, float playerHealth, float playerDamage, Action<Response> response)
    {
        WWWForm form = new WWWForm();
        form.AddField("userName", userName);
        form.AddField("playerPositionX", playerPositionX.ToString());
        form.AddField("playerPositionY", playerPositionY.ToString());
        form.AddField("playerHealth", playerHealth.ToString());
        form.AddField("playerDamage", playerDamage.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/TheLastHunter/savePlayerData.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
        {
            // Verificar si la respuesta contiene "Funciona"
            if (www.downloadHandler.text.Contains("Funciona"))
            {
                // Extrayendo el JSON de la respuesta (excluyendo "Funciona")
                string jsonResponse = www.downloadHandler.text.Replace("Funciona", "").Trim();

                try
                {
                    // Intentar deserializar el JSON
                    PlayerDataResponse responseData = JsonUtility.FromJson<PlayerDataResponse>(jsonResponse);

                    // ... (continuar con el manejo normal del JSON)
                }
                catch (Exception e)
                {
                    Debug.LogError("Error parsing JSON: " + e.Message);
                }
            }
            else
            {
                Debug.LogError("Server Response (Invalid JSON): " + www.downloadHandler.text);
            }
        }
        else
        {
            Debug.LogError("Network error: " + www.error);
        }
    }
}

[Serializable]
public class PlayerDataResponse
{
    public bool done;
    public string message;
    public float playerPositionX;
    public float playerPositionY;
    public float playerHealth;
    public float playerDamage;
    public string userName;
}


    public void LoadPlayerData(string userName, Action<PlayerData> response)
    {
        StartCoroutine(CO_LoadPlayerData(userName, response));
    }

    private IEnumerator CO_LoadPlayerData(string userName, Action<PlayerData> response)
    {
        WWWForm form = new WWWForm();
        form.AddField("userName", userName);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/TheLastHunter/loadPlayerData.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Network Error: " + www.error);
            }
            else
            {
                Debug.Log("Server Response: " + www.downloadHandler.text);
                string jsonResponse = www.downloadHandler.text;

                // Eliminar el texto no deseado si es necesario
            int jsonStart = jsonResponse.IndexOf("{");
            if (jsonStart != -1)
            {
                jsonResponse = jsonResponse.Substring(jsonStart);
            }

            // Verificar si la respuesta contiene datos del jugador
            if (!string.IsNullOrEmpty(jsonResponse.Trim()))
            {
                PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonResponse);
                response(playerData);
            }
            else
            {
                Debug.Log("El jugador no tiene datos guardados");
                response(null); // Puedes pasar null o algún indicador para manejar la falta de datos
            }
        }
    }

}
}

[Serializable] public class Response {

    public bool done = false;
    public string message = ""; 
}