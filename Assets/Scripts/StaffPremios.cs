using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StaffPremios : MonoBehaviour {
    public GameObject prefabPrize;
    public List<PrizeStaff> premios;
    public GameObject panelLayout;

    public GameObject popUpCanjear;

    private void Start()
    {
        PhpQuery.GetPrizeStaff(LoadPrices);
        Navigator.block = true;
    }


    private void LoadPrices(string result)
    {
        premios = JsonParser<List<PrizeStaff>>.GetObject(result);
        int i = 0;
        foreach (var item in premios)
        {
            GameObject go = Instantiate(prefabPrize);
            go.name = "PrizeStaff (" + i++ + ")";
            go.transform.SetParent(panelLayout.transform);
            go.transform.localScale = Vector3.one;
            GameObject.Find(go.name + "/txtTitle").GetComponent<Text>().text = item.name;
            PrizeStaff itemPrize = item;
            LoadImagePrize(item, GameObject.Find(go.name + "/Mask/prizeImage").GetComponent<Image>());
            if (Login.debugUser.role_id == (int)Rol.MANAGER && !CanManagerChange())
                GameObject.Find(go.name + "/Button").SetActive(false);
            else
            {
                GameObject goEvent = GameObject.Find(go.name + "/Button");

                EventTrigger.Entry onEntry = new EventTrigger.Entry();
                onEntry.eventID = EventTriggerType.PointerClick;
                onEntry.callback.RemoveAllListeners();
                onEntry.callback.AddListener((eventData) => { Click(itemPrize); });
                goEvent.AddComponent<EventTrigger>().triggers.Add(onEntry);
            }
        }
        Navigator.block = false;
    }

    private void LoadImagePrize(PrizeStaff prize,Image img)
    {
        string path="";
        bool saveToFile = false; 
      
        if (CambioImagenUrl(prize))
        {
            saveToFile = true;
            path = prize.img_url;
             PlayerPrefs.SetString(GetFileName(prize), prize.img_url);
       }
        else
        {
            print("Loading from file " + prize.name);
            path = Application.persistentDataPath + "/" + GetFileName(prize) + ".png";
        }
        StartCoroutine(GetImageFrom(path, img,saveToFile,prize));
    }

    private bool CambioImagenUrl(PrizeStaff prize)
    {
        if (PlayerPrefs.GetString(GetFileName(prize)) != prize.img_url)
        {
            print(PlayerPrefs.GetString(GetFileName(prize)) + " " + prize.img_url);
        }
        return PlayerPrefs.GetString(GetFileName(prize)) != prize.img_url;
    }


    public IEnumerator GetImageFrom(string filePath, Image img,bool saveToFile,PrizeStaff prize) {
        WWW www = new WWW(filePath);
        yield return www;
        if (!saveToFile && File.Exists(filePath))
        {
            Texture2D tx = new Texture2D(1, 1);
            tx.LoadImage(File.ReadAllBytes(filePath));
            img.sprite = Sprite.Create(tx, new Rect(0, 0, tx.width, tx.height), Vector3.one / 2);
        }
        else
        {
            if (!string.IsNullOrEmpty(www.error))
            {
                print("POKE ERROR Staff Premio GetImageFrom" + prize.name);
            }
            else
            {
                Texture2D tx = (Texture2D)www.texture;
                img.sprite = Sprite.Create(tx, new Rect(0, 0, tx.width, tx.height), Vector3.one / 2);
                if (saveToFile)
                    File.WriteAllBytes(Application.persistentDataPath + "/" + GetFileName(prize) + ".png", (tx).EncodeToPNG());
            }
        }
       
    }


    private string GetFileName(PrizeStaff p)
    {
        return "prizes-" + p.id;
    }

    private void Click(PrizeStaff prize)
    {
        PopupChangePrize.prize = prize;
        popUpCanjear.SetActive(true);
    }


    private bool CanManagerChange()
    {
        Debug.LogError("Chequear con el servidor! Manager Data Change Prize");
        return true;
    }

    private void OnPressedChange(PrizeStaff prize)
    {
        popUpCanjear.SetActive(true);
        PopupChangePrize.prize = prize;
    }

    
}
