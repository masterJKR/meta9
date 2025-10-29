using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKeeper : MonoBehaviour
{
    public static int hasKeys = 0; // 열쇠 수량
    public static int hasArrows = 0;//화살 수량


    void Start()
    {
        hasKeys = PlayerPrefs.GetInt("Keys");
        //hasArrows = PlayerPrefs.GetInt("Arrows");
        StartCoroutine(GetArrow());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SaveItem()
    {
        // 아이템 저장하기
        PlayerPrefs.SetInt("Keys", hasKeys);
        PlayerPrefs.SetInt("Arrows", hasArrows);
    }

    IEnumerator GetArrow()
    {

    }
}
