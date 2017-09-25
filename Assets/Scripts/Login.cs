using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour {

    public InputField txtUser;
    public InputField txtPassword;

    public Text nameTopRigth;
    public Text premiosStaffPuntos;

    public Text puntosOwnerManager;
    public Text puntosStaff;
    public Text registrosStaff;


    public static User debugUser;
    public static Login instance;

    public Image icoUserHome;

    public Image icoUserNavigator;
    public Text txtUserNavigator;

    public InputField txtNombreWritecontacto;
    public InputField txtMailWritecontacto;
    Shop u;

    public string register;

    void Start () {
        instance = this;
	}

    public void BTN_Ingresar()
    {
        var jsonString = "{\"data\": {\"user\": \""+txtUser.text+"\",\"hash\": \""+SHA512(SHA512(txtPassword.text) +GetDate())+"\"}}";
        PanelResult.ShowMsg("Conectando...", 1);
        PhpQuery.SendQueryResponse(PhpQuery.url+"login", jsonString, OnResultLogin);
    }

    private void OnResultLogin(UnityWebRequest obj)
    {
        if(obj.responseCode == 200)
        {
            print(obj.downloadHandler.text);
            HashLogin hl = JsonParser<HashLogin>.GetObject(obj.downloadHandler.text);
            if (hl!=null)
            {
                print("OK");
            }
            User.authorization = hl.token;
            PhpQuery.GetUser(int.Parse(hl.user_id), OnLoginOK);
        }
        else
        {
            ErrorGet error = JsonUtility.FromJson<ErrorGet>(obj.downloadHandler.text);
            PanelResult.ShowMsg(error.error,2,nohacernada);
        }
    }

    private void nohacernada()
    {
        //throw new NotImplementedException();
    }

    private void OnLoginOK(string result)
    {
        SendAlgo.instance.OpenScreen("Login");
        User u = JsonUtility.FromJson<JsonParser<User>>(result).data;
        debugUser = u;
        PhpQuery.GetUserRegisters(OnGetUserRegisters);
        PhpQuery.GetShop(u.shop_id, OnGetShopInfo);
    }

    public static int registers = 0;

    private void OnGetUserRegisters(string obj)
    {
        obj = obj.Replace("SUM(amount)", "amount");
        obj = obj.Replace("SUM(total_motive_pts)", "total_motive_pts");
        print(obj);
        List<Point> points = JsonParser<List<Point>>.GetObject(obj);
        Analytics.CustomEvent("Login");
        var l = points.Where((p) => p.IsRegistro).First();
        registers = l.amount;
        UpdateGUI();
    }

    public static void UpdateGUI() {

        if(debugUser.rol == (int)Rol.STAFF)
            instance.nameTopRigth.text = debugUser.firstName.ToUpper() + " " + debugUser.lastName.ToUpper();
        else
            instance.nameTopRigth.text = instance.u.name;

        string path = PlayerPrefs.GetString("imagePath-"+debugUser.id);
        if (path != "")
        {
            GameManager.instance.StartCoroutine(instance.LoadImage(path, instance.icoUserNavigator));
            GameManager.instance.StartCoroutine(instance.LoadImage(path, instance.icoUserHome));
        }

        instance.premiosStaffPuntos.text = "TENÉS <size=70> " + debugUser.total_pts + " </size> PUNTOS";
        instance.puntosOwnerManager.text = "TENÉS <size=70> " + debugUser.total_pts + " </size> PUNTOS";
        instance.puntosStaff.text = "TENÉS <size=55> " + debugUser.total_pts + " </size> PUNTOS";
        instance.registrosStaff.text = "HICISTE <size=55> " + registers + " </size> REGISTROS";
        instance.txtNombreWritecontacto.text = debugUser.firstName;
        instance.txtMailWritecontacto.text = debugUser.email;


    }

    public static string GetRegistersDoit()
    {
        if (debugUser.total_used_pts == 0) return "0";
        return ""+(int)((debugUser.total_pts + debugUser.total_used_pts) / 20);
    }


    private void OnGetShopInfo(string result)
    {
        try
        {
            u = JsonUtility.FromJson<JsonParser<Shop>>(result).data;
            Home.instance.gameObject.SetActive(true);
            Home.instance.BTN_ShowHome();
            gameObject.SetActive(false);
            PhpQuery.GetDates(OnDateUpdate); 
            UpdateGUI();
        }
        catch
        {

        }
    }

    private void OnDateUpdate(string obj)
    {
        int Septiembre = 0;
        int Octubre = 1;
        int Noviembre = 2;
        obj = obj.Replace("start-date", "startdate");
        obj = obj.Replace("end-date", "enddate");
        obj = obj.Replace("\\", "");
        obj = obj.Replace("\"[", "[");
        obj = obj.Replace("]\"", "]");
        DateServer dates = JsonParser<DateServer>.GetObject(obj);
        PhpQuery.GetShopRegisters(u.id, dates.value[Septiembre].startdate, dates.value[Septiembre].enddate, OnGetRegisterShopS);
        PhpQuery.GetShopRegisters(u.id, dates.value[Octubre].startdate, dates.value[Octubre].enddate, OnGetRegisterShopO);
        PhpQuery.GetShopRegisters(u.id, dates.value[Noviembre].startdate, dates.value[Noviembre].enddate, OnGetRegisterShopN);
    }

    private void OnGetRegisterShopN(string obj)
    {
        obj = obj.Replace("SUM(amount)", "amount");
        obj = obj.Replace("SUM(total_motive_pts)", "total_motive_pts");
        print(obj);
        try
        {
            PopUpHistory.m3 = JsonParser<List<Point>>.GetObject(obj);
        }
        catch
        {

        }
    }

    private void OnGetRegisterShopO(string obj)
    {
        obj = obj.Replace("SUM(amount)", "amount");
        obj = obj.Replace("SUM(total_motive_pts)", "total_motive_pts");
        print(obj);
        try
        {
            PopUpHistory.m2 = JsonParser<List<Point>>.GetObject(obj);
        }
        catch
        {

        }
    }

    private void OnGetRegisterShopS(string obj)
    {
        obj = obj.Replace("SUM(amount)", "amount");
        obj = obj.Replace("SUM(total_motive_pts)", "total_motive_pts");
        print(obj);
        try
        {
            PopUpHistory.m1 = JsonParser<List<Point>>.GetObject(obj);
        }
        catch
        {

        }
    }


    private IEnumerator LoadImage(string path, Image output)
    {
        var url = "file://" + path;
        var www = new WWW(url);
        yield return www;
        yield return new WaitForSeconds(0.5f);
    
        var texture = www.texture;
        if (texture == null)
        {
            Debug.LogError("Failed to load texture url:" + url);
        }else
        {
            Debug.Log("POKE SE CARGO LA IMAGEN!");
        }
        icoUserNavigator.transform.eulerAngles = new Vector3(0, 0, PlayerPrefs.GetFloat("rotate"));
        output.transform.eulerAngles = new Vector3(0, 0, PlayerPrefs.GetFloat("rotate"));
        output.sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), Vector3.one / 2);
    }


    private string GetDate()
    {
        return System.DateTime.Now.ToString("yyyy-MM-dd");
    }

    public string SHA512(string input)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(input);
        using (var hash = System.Security.Cryptography.SHA512.Create())
        {
            var hashedInputBytes = hash.ComputeHash(bytes);

            var hashedInputStringBuilder = new System.Text.StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString().ToLower();
        }
    }
}

[System.Serializable]
public class HashLogin
{
    public string user_id;
    public string token;
}

[System.Serializable]
public class ErrorGet
{
    public string error;
}