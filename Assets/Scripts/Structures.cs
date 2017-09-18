
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JsonParser<T>
{
    public T data;

    public static T GetObject(string str)
    {
        return JsonUtility.FromJson<JsonParser<T>>(str).data;
    }

    internal static List<PrizeStaff> GetObject(string result, Action<string> onReceive)
    {
        throw new NotImplementedException();
    }
}

[System.Serializable]
public class DatesShop
{
    public long ini_month1;
    public long ini_month2;
    public long ini_month3;
    public long end_month1;
    public long end_month2;
    public long end_month3;
}

[System.Serializable]
public class User
{
    public static User instance;
    public static string authorization = "2341ebbb630cda9b7342a947c95b5499";
    public int id;
    public string firstName;
    public string lastName;
    public int dni;
    public string email;
    public string phone;
    public int total_pts;
    public int total_used_pts;
    public int shop_id;
    public int role_id;
    public string created;
    public string updated;

    public int rol { get { return role_id; } }

    public User()
    {
        instance = this;
    }
}

[System.Serializable]
public class DateServer
{
    public int id;
    public string key;
    public List<SingleDate> value;
    public string type;
    public string updated;
    //"value":"[{\"id\":1,\"name\":\"Septiembre\",\"start-date\":\"1504224000\",\"end-date\":\"1506815999\"},{\"id\":2,\"name\":\"Octubre\",\"start-date\":\"1506816000\",\"end-date\":\"1509483599\"},{\"id\":3,\"name\":\"Per\u00edodo 3\",\"start-date\":\"1509494400\",\"end-date\":\"1512086399\"}]","
}

[System.Serializable]
public class SingleDate
{
    public int id;
    public string name;
    public long startdate;
    public long enddate;
}

[System.Serializable]
public class ErrorMsg
{
    public Error error;
}

[System.Serializable]
public class Error
{
    public int code;
    public int long_code;
    public string message;
    public string developerMessage;
    public string moreInfo; 
}

[System.Serializable]
public class Point
{
    public int user_id;
    public string motive;
    public int amount;
    public int total_motive_pts;

    public bool IsRegistro { get { return motive == "registro"; } }
    public bool IsDisponibility { get { return motive == "disponibilidad"; } }
    public bool IsVolume { get { return motive == "volumen"; } }
}

[System.Serializable]
public class ShopScore
{
    public List<Point> mes1;
    public List<Point> mes2;
    public List<Point> mes3;
}

[System.Serializable]
public class UserPoints
{
    public List<Point> points;
}

[System.Serializable]
public class PrizeStaff
{
    public int id;
    public string description;
    public string name;
    public string img_url;
    public int pts_cost;
    public string catagoloID;
    public int SKU;
    public int enabled;
    public int stock;
}

[System.Serializable]
public enum Rol
{
    OWNER = 1, MANAGER, STAFF
}

[System.Serializable]
public class Shop
{
    public static Shop instance;
    public int id;
    public string name;
    public int idPM;
    public string img_url;
    public int objVolumen;
    public int objDisponibilidad;
    public int total_pts;

    public Shop()
    {
        instance = this;
    }
}

[System.Serializable]
public class TOS
{
    public string updated;
    public string value;
}
