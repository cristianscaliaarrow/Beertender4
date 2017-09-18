using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TerminosYCondiciones : MonoBehaviour {
    public static TerminosYCondiciones instance;
    public Toggle toggle;
    public GameObject PanelTerminos;
    public GameObject buttonAccept;
    public Text textTOS1;
    public Text textTOS2;

    public Text textTOS3;
    public Text textTOS4;

    string fileTos = "/tos.txt";

    private void Start()
    {
        instance = this;
        gameObject.SetActive(false);
        buttonAccept.SetActive(false);
        PhpQuery.GetTOS(OnTos);

    }

    TOS tos;
    private void OnTos(string obj)
    {
        tos = JsonParser<TOS>.GetObject(obj);
        File.WriteAllText(Application.persistentDataPath + fileTos, tos.value);
        WriteInGuiTos(tos.value);
    }

   public void WriteInGuiTos(string str)
    {
        int halfIndex = tos.value.Length / 2;
        string str1 = tos.value.Substring(0, halfIndex);
        string str2 = tos.value.Substring(halfIndex);
        textTOS1.text = str1;
        textTOS2.text = str2;
        textTOS3.text = str1;
        textTOS4.text = str2;
        buttonAccept.SetActive(true);
        if (tos.updated != PlayerPrefs.GetString("tos"))
        {
            gameObject.SetActive(true);
        }
    }

    public void BTN_AceptarTerminos()
    {
        if (toggle.isOn)
        {
            PanelTerminos.SetActive(false);
            PlayerPrefs.SetString("tos",tos.updated);
        }
    }

   
   
}
