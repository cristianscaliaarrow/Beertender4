using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeStaff : MonoBehaviour {

    public Image userIco;
    public Text userName;
    public Text barName;
    public Text txtPuntos;
    public Text txtRegistros;

    private void OnEnable()
    {
        //txtPuntos.text = "TENÉS <size=55> "+Login.debugUser.total_pts+" </size> PUNTOS";
        Login.UpdateGUI();
        //SendAlgo.instance.OpenScreen("Home Staff");
        SendAlgo.instance.LoadScreen("Home STAFF");
        print("Log staff");
    }

    public void BTN_UploadPhoto()
    {
        Init();
    }

    #region "ALgo NUEVO"

                public delegate void Callback(string acition, bool success);
                private Callback callback;
            #if UNITY_ANDROID
                private AndroidJavaObject androidPlugin;
            #endif

                public void Init()
                {
                    callback = (s, b) => {
                        Debug.Log("ALGO BIEN SALIO " + s + " bool " + b);
                    };

               
            #if UNITY_ANDROID
                    androidPlugin = new AndroidJavaObject("com.redoceanred.unity.android.PickerImagePlugin");
                    androidPlugin.Call("initPlugin", gameObject.name);
            #endif
                }

                    public string GetImagePath()
                    {
                        string result = "";
                #if UNITY_ANDROID
                        result = androidPlugin.Call<string>("getImagePath");
                #endif
                        return result;
                    }

                    public bool PickImage()
                    {
                        bool result = false;
                #if UNITY_ANDROID
                        result = androidPlugin.Call<bool>("pickImage");
                #endif
                        return result;
                    }

                    public bool CaptureImage()
                    {
                        bool result = false;
                #if UNITY_ANDROID
                        result = androidPlugin.Call<bool>("captureImage");
                #endif
                        return result;
                    }

                    public void NativeMessage(string message)
                    {
                        if (callback != null)
                        {
                            string[] delimiter = { "," };
                            string[] splitMessage = message.Split(delimiter, System.StringSplitOptions.None);
                            callback(splitMessage[0], bool.Parse(splitMessage[1]));
                        }
                    }
    #endregion
}
