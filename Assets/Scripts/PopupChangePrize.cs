using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PopupChangePrize : MonoBehaviour {
    public static PopupChangePrize instance;

    public static PrizeStaff prize;
    public InputField input;
    public Text textPts;
    public Text title;
    Action _a = delegate { };

    public GameObject panelResult;


    private void Awake()
    {
        instance = this;
        print("popupChangePrize ON!");
        gameObject.SetActive(false);        
    }

    public void OnEnable()
    {
        textPts.text = Login.debugUser.total_pts*2+"";
        title.text = "Pesos a canjear en orden de compra para " + (prize.name);
    }

    public void BTN_ChangePrize()
    {
        if (int.Parse(input.text) <= 0) { PanelResult.ShowMsg("Error al ingresar el monto!", 1); return; }
        if (Login.debugUser.total_pts*2 >= int.Parse(input.text)) 
        {
            PanelResult.ShowMsg("Espere...", 2f);
            int pesos = int.Parse(input.text);
            float decimals = (float.Parse(input.text) / 2 - (int)int.Parse(input.text) / 2);
            int discount = (int)(pesos / 2 + ((decimals > 0) ? 1 : 0));
            Login.debugUser.total_pts -= discount;
            Login.UpdateGUI();
            PhpQuery.SendQueryResponse(PhpQuery.url + "userprize", "{ \"data\": { \"user_id\": \"" + Login.debugUser.id + "\", \"prize_staff_id\": \"" + prize.id + "\", \"pts_used\": \"" + discount + "\" }}", OnRequest);

        }
        else
        {
            PanelResult.ShowMsg("No Tiene Suficientes Puntos", 1,()=>BTN_ClosePopUp());
        }

       
    }

    private void OnRequest(UnityWebRequest obj)
    {
        if(obj.responseCode == 200)
        {
            PanelResult.ShowMsg("La operacoin se realizo correctamente.", 2);
        }
        else
        {
            PanelResult.ShowMsg("La operacoin Falló!. "+obj.downloadHandler.text, 2);
        }
    }

    private IEnumerator SendBuyPrize()
    {
        PanelResult.ShowMsg("Espere...",2,() => PanelResult.ShowMsg("La operacoin se realizo correctamente.", 2,()=> {
            
            Login.UpdateGUI();
            BTN_ClosePopUp();
        }));
        
        yield return null;
    }

    public void BTN_ClosePopUp()
    {
        gameObject.SetActive(false);
        input.text = "";
    }

   
}
