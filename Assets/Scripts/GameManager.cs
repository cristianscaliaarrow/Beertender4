using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public PopUpContactar popUpGraciasPorEnviar;
	// Use this for initialization
	void Start () {
        instance = this;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SendContactoUNITY(string nombre, string correo, string tema, string message)
    {
        StartCoroutine(SendContactoUNITY_CR(nombre,correo,tema,message));
    }

    private IEnumerator SendContactoUNITY_CR(string nombre, string correo, string tema, string message)
    {
        var jsonString = "{\"data\": {\"user_id\": \"" + Login.debugUser.id + "\",\"subject\": \"" + tema + "\",\"message\": \"" + message + "\",\"from\": \"" + correo + "\"}}";

        UnityWebRequest request = new UnityWebRequest(PhpQuery.url+"msjs", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        PanelResult.ShowMsg("Enviando...",2,()=>GameManager.instance.popUpGraciasPorEnviar.BTN_VisiblePopUpContactar(true));

        request.Send();
        yield return new WaitUntil(() => request.isDone);

        if (request.responseCode == 200)
            PanelResult.ShowMsg("El mensaje se envió correctamente!", 2);
        else
            PanelResult.ShowMsg("No se ha podido mandar el mensaje!", 2);

        Debug.Log("POKE Status Code: " + request.responseCode);
    }
}
