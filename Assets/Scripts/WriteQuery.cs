using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WriteQuery : MonoBehaviour {

    public InputField nombre;
    public InputField correo;
    public InputField tema;
    public InputField message;

    public PopUpContactar popupContactar;

    public void ClearFields()
    {
        nombre.text = "";
        correo.text = "";
        tema.text = "";
        message.text = "";
    }

    public void BTN_Enviar()
    {
        if(message.text == "")
        {
            PanelResult.ShowMsg("El Campo 'mensaje' esta vacio.",2);
        }
        else
        {
            GameManager.instance.SendContactoUNITY(nombre.text,correo.text, tema.text, message.text);
            SendAlgo.instance.LogEvent("Contacto", "SendMessage");

        }
    }

    public IEnumerator SendContact(string nombre,string correo,string tema,string message)
    {
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(PhpQuery.url + "msjs");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";
        httpWebRequest.Headers.Add("Authentication", "Bearer " + User.authorization);

        yield return null;
        /*
        user_id, subject, msj, from 
        */
        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = "{\"data\": {\"user_id\": \"" + Login.debugUser.id + "\",\"subject\": \"" + tema + "\",\"message\": \"" + message + "\",\"from\": \"" + correo + "\"}}";
            streamWriter.Write(json);
            yield return null;

            streamWriter.Flush();
            streamWriter.Close();
        }
        yield return null;

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            yield return null;

        }
        yield return null;

    }

   
}

