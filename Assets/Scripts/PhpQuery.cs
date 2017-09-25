using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PhpQuery : MonoBehaviour {
    public static PhpQuery instance;
    public static string url = "http://api.beer-tender.com/";
  //  public static string url = "http://api.nextcode.ml/";
    void Awake()
    {
        instance = this;
    }

    public static void GetPrizeStaff(Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery(url +"prizestaff/", callBack));
    }

    internal static void GetTOS(Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery(url + "params/4", callBack));
    }

    internal static void GetHash(string username,string hash,int first,Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery(url + "params/4", callBack));
    }

    #region "Users"
    public static void GetUsers(Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery(url + "users", callBack));
    }

    public static void GetUser(int id , Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery(url + "users/" + id, callBack));
    }
    #endregion

    #region "Shops"
    public static void GetShops(Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery(PhpQuery.url + "shops", callBack));
    }

    public static void GetShopRegisters(int shopid,long ini, long fin, Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery(PhpQuery.url + "shoppoints/" + shopid + "/"+ ini +"/"+ fin, callBack));
    }

    public static void GetUserRegisters(Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery(PhpQuery.url + "userpoints/" + Login.debugUser.id, callBack));
    }

    public static void GetShop(int id, Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery(PhpQuery.url + "shops/" + id, callBack));
    }

    #endregion

    public static void EditUser(int id)
    {
        instance.StartCoroutine(StartQuery(url + "users/" + id, "{\"data\": {\"firstName\":\"cambio\"}}"));
    }

    internal static void GetMessages(Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery(url + "messages/", callBack));
    }

    public static void AddUser()
    {
        //instance.StartCoroutine(StartQuery("api.nextcode.ml/users", ""));

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "users");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";
        httpWebRequest.Headers.Add("Authentication", "Bearer "+User.authorization);

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = "{\"data\": {\"dni\": \"12345678\",\"email\": \"12345678t@email.com\",\"shop_id\": \"9\",\"role_id\": \"2\"}}";
            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
        }

    }

    public static void SendContact(string nombre, string correo, string tema, string message)
    {
        
    }

    public static void GetRanking(Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery(url + "rankings/shop/", callBack));
    }

    public static void GetDates(Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery(url + "params/contest_periods/", callBack));
    }

    internal static void GetShopRegisters(int id, object onGetRegisterShop)
    {
        
    }

    private static IEnumerator StartQuery(string query,Action<string> callBack)
    {
        //PhpQuery.GetShopRegisters(u.id,)

        UnityWebRequest webRequest = UnityWebRequest.Get(query);
        webRequest.SetRequestHeader("Authorization", "Bearer " + User.authorization);
        yield return webRequest.Send();

        if (!webRequest.isError)
        {
            callBack(webRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log(webRequest.error);
        }

      
    }

    

    public static void SendQueryResponse(string url, string jsonData, Action<UnityWebRequest> act)
    {
        instance.StartCoroutine(SC_SendQueryResponse(url,jsonData, act));
    }

    private static IEnumerator SC_SendQueryResponse(string url,string jsonData,Action<UnityWebRequest>act)
    {
        var jsonString = jsonData;

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        request.Send();
        yield return new WaitUntil(() => request.isDone);

        act(request);
    }


    private static IEnumerator StartQuery(string query, string body)
    {
      
        UnityWebRequest webRequest;
        byte[] bytes = Encoding.UTF8.GetBytes(body);
        webRequest = UnityWebRequest.Put(query, bytes);
        webRequest.SetRequestHeader("X-HTTP-Method-Override", "PUT");

        yield return webRequest.Send();

        if (!webRequest.isError)
        {
            Debug.Log("Upload complete!");
        }
        else
        {
            Debug.Log(webRequest.error);
        }

    }

}
