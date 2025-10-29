using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKeeper : MonoBehaviour
{
    public static int hasKeys = 0; // ���� ����
    public static int hasArrows = 0;//ȭ�� ����


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
        // ������ �����ϱ�
        PlayerPrefs.SetInt("Keys", hasKeys);
        PlayerPrefs.SetInt("Arrows", hasArrows);
    }

    IEnumerator GetArrow()
    {

    }
}
