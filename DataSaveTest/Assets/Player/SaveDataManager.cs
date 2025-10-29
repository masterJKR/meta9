using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataList arrangeDataList; //  씬 배치 데이터


    void Start()
    {
        arrangeDataList = new SaveDataList();
        arrangeDataList.saveDatas = new SaveData[] { };

      
        // 마지막 플레이 맵 이름
        string stageName = PlayerPrefs.GetString("LastScene");
        // 맵(씬) 이름으로 저장 된 데이터 읽어 오기 
        string data = PlayerPrefs.GetString(stageName);
        data = data == "" ? null : data;
     
        if( data != null )  // 맵(씬)이름으로 저장된 데이터 가 있다면
        {
            // JSON 값을  SaveDataList로 변환
            arrangeDataList = JsonUtility.FromJson<SaveDataList>(data);
            for (int i = 0; i < arrangeDataList.saveDatas.Length; i++)
            {
                // 배열에 저장된것은 SaveData클래스 객체이다.
                SaveData saveData = arrangeDataList.saveDatas[i];
                string objTag = saveData.objectTag;
                // objTag의 태그명과 일치하는 유니티 객체 모두 찾기
                GameObject[] objects = GameObject.FindGameObjectsWithTag(objTag);
                for (int j = 0; j < objects.Length; j++)
                {
                    GameObject obj = objects[j]; // 게임 객체 하나씩 가져오기
                    // GameObject의 태그 확인하고 해당 객체가 저장되어있는 객체에 해당하는지 확인
                    if (objTag == "Enemy")
                    {
                        EnemyController enemy = obj.GetComponent<EnemyController>();
                        if (enemy.arrangeId == saveData.aId) Destroy(obj);
                    }
                    else if (objTag == "ItemBox")
                    {
                        ItemBox box = obj.GetComponent<ItemBox>();
                        if (box.arrangeId == saveData.aId)
                        {
                            box.isClosed = false;
                            box.GetComponent<SpriteRenderer>().sprite = box.openImg;
                            // 아이템 박스는 열려 있는 이미지로 표현 해야 된다.
                        }
                    }
                }
            }
        }

    }

   
    void Update()
    {
        
    }

    public static void SetArrangeId(int arrangeId, string objTag)
    {
        SaveData[] newSaveDatas = new SaveData[arrangeDataList.saveDatas.Length + 1];

        for(int i = 0; i< arrangeDataList.saveDatas.Length; i++)
        {
            newSaveDatas[i] = arrangeDataList.saveDatas[i];
        }
        // SaveData 객체 할당 하고 데이터 넣고 배열에 넣기
        SaveData saveData = new SaveData();
        saveData.aId = arrangeId; // 게임 객체의 arrangeId 저장
        saveData.objectTag = objTag; // 게임 객체의 tag 명 저장
        newSaveDatas[arrangeDataList.saveDatas.Length] = saveData;
        arrangeDataList.saveDatas = newSaveDatas;
    }

    // 맵(씬)의 이름을 key로   배열은 value 해서 json저장시키기
    public static void SaveArrageData(string stageName)
    {
        string saveJson = JsonUtility.ToJson(arrangeDataList);
        PlayerPrefs.SetString(stageName, saveJson); // 맵(씬)이름 으로 저장
    }
}
