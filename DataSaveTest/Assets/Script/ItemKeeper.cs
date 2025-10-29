using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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

    IEnumerator GetArrow()  // �������� ���� ȭ�� ���� �޾ƿ���
    {
        string url = "http://localhost:8080/api/getarrow";


        using ( UnityWebRequest request = UnityWebRequest.Get(url) )
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            request.timeout = 10; // ������ �ִ� �ð�

            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                ArrowData arrowData = JsonUtility.FromJson<ArrowData>(json);

                ItemKeeper.hasArrows = arrowData.count;

                Debug.Log("���� : " + ItemKeeper.hasArrows);
            }
            else
            {
                Debug.Log("���� : " + request.error);
            }
        }
    }
}
