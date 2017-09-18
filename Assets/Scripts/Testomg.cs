using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testomg : MonoBehaviour {

    public List<PrizeStaff> list;

	void Start () {
        PhpQuery.GetPrizeStaff(OnReceive);
    }

    private void OnReceive(string result)
    {
        list = JsonParser<List<PrizeStaff>>.GetObject(result);
    }
}
