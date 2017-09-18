using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpHistory : MonoBehaviour {

    public static List<Point> m1;
    public static List<Point> m2;
    public static List<Point> m3;


    public void OnEnable()
    {
        if (m1 != null)
        {
            GameObject.Find("d (1)").GetComponent<Text>().text = m1[0].amount + " Puntos";
            GameObject.Find("v (1)").GetComponent<Text>().text = m1[1].amount + " Puntos";
            GameObject.Find("r (1)").GetComponent<Text>().text = m1[2].amount + " Puntos";
        }

        if (m2 != null)
        {
            GameObject.Find("d (2)").GetComponent<Text>().text = m2[0].amount + " Puntos";
            GameObject.Find("v (2)").GetComponent<Text>().text = m2[1].amount + " Puntos";
            GameObject.Find("r (2)").GetComponent<Text>().text = m2[2].amount + " Puntos";
        }

        if (m3 != null)
        {
            GameObject.Find("d (3)").GetComponent<Text>().text = m3[0].amount + " Puntos";
            GameObject.Find("v (3)").GetComponent<Text>().text = m3[1].amount + " Puntos";
            GameObject.Find("r (3)").GetComponent<Text>().text = m3[2].amount + " Puntos";
        }
    }
  
}
