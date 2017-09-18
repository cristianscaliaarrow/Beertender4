using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PanelRanking : MonoBehaviour {

    public RankingGo[] rankingGos;

    private void OnEnable()
    {
        PhpQuery.GetRanking(OnGeted);
    }

    private void OnGeted(string obj)
    {
        List<Shop> list = JsonParser<List<Shop>>.GetObject(obj);
        for (int i = 0; i<5 &&  i < list.Count; i++)
        {
            Shop s  = list[i];
            rankingGos[i].name.text = s.name;
            LoadImageShop(s, rankingGos[i].imageShop);
        }
    }

    private void LoadImageShop(Shop shop, Image img)
    {
        string path = "";
        bool saveToFile = false; ;

        if (CambioImagenUrl(shop))
        {
            saveToFile = true;
            path = shop.img_url;
        }
        else
        {
            path = Application.persistentDataPath + "/" + shop.name + ".png";
        }
        StartCoroutine(GetImageFrom(path, img, saveToFile, shop));
    }

    public IEnumerator GetImageFrom(string filePath, Image img, bool saveToFile, Shop shop)
    {
        WWW www = new WWW(filePath);
        yield return www;

        if(!saveToFile && File.Exists(filePath))
        {
            Texture2D tx = new Texture2D(1, 1);
            tx.LoadImage(File.ReadAllBytes(filePath));
            img.sprite = Sprite.Create(tx, new Rect(0, 0, tx.width, tx.height), Vector3.one / 2);
        }
        else
        {
            if (!string.IsNullOrEmpty(www.error))
            {
                print("POKE ERROR Ranking GetImageFrom");
            }
            else
            {
                Texture2D tx = (Texture2D)www.texture;
                img.sprite = Sprite.Create(tx, new Rect(0, 0, tx.width, tx.height), Vector3.one / 2);
                if (saveToFile)
                {
                    File.WriteAllBytes(Application.persistentDataPath + "/" + shop.name + ".png", (tx).EncodeToPNG());
                    PlayerPrefs.SetString(shop.name, shop.img_url);
                }
            }
        }

    }

    private bool CambioImagenUrl(Shop shop)
    {
        return PlayerPrefs.GetString(shop.name) != shop.img_url;
    }

    [System.Serializable]
    public class RankingGo
    {
        public Text name;
        public Image imageShop;
    }
}
