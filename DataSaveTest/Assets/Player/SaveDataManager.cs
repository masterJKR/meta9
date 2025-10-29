using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataList arrangeDataList; //  �� ��ġ ������


    void Start()
    {
        arrangeDataList = new SaveDataList();
        arrangeDataList.saveDatas = new SaveData[] { };

      
        // ������ �÷��� �� �̸�
        string stageName = PlayerPrefs.GetString("LastScene");
        // ��(��) �̸����� ���� �� ������ �о� ���� 
        string data = PlayerPrefs.GetString(stageName);
        data = data == "" ? null : data;
     
        if( data != null )  // ��(��)�̸����� ����� ������ �� �ִٸ�
        {
            // JSON ����  SaveDataList�� ��ȯ
            arrangeDataList = JsonUtility.FromJson<SaveDataList>(data);
            for (int i = 0; i < arrangeDataList.saveDatas.Length; i++)
            {
                // �迭�� ����Ȱ��� SaveDataŬ���� ��ü�̴�.
                SaveData saveData = arrangeDataList.saveDatas[i];
                string objTag = saveData.objectTag;
                // objTag�� �±׸�� ��ġ�ϴ� ����Ƽ ��ü ��� ã��
                GameObject[] objects = GameObject.FindGameObjectsWithTag(objTag);
                for (int j = 0; j < objects.Length; j++)
                {
                    GameObject obj = objects[j]; // ���� ��ü �ϳ��� ��������
                    // GameObject�� �±� Ȯ���ϰ� �ش� ��ü�� ����Ǿ��ִ� ��ü�� �ش��ϴ��� Ȯ��
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
                            // ������ �ڽ��� ���� �ִ� �̹����� ǥ�� �ؾ� �ȴ�.
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
        // SaveData ��ü �Ҵ� �ϰ� ������ �ְ� �迭�� �ֱ�
        SaveData saveData = new SaveData();
        saveData.aId = arrangeId; // ���� ��ü�� arrangeId ����
        saveData.objectTag = objTag; // ���� ��ü�� tag �� ����
        newSaveDatas[arrangeDataList.saveDatas.Length] = saveData;
        arrangeDataList.saveDatas = newSaveDatas;
    }

    // ��(��)�� �̸��� key��   �迭�� value �ؼ� json�����Ű��
    public static void SaveArrageData(string stageName)
    {
        string saveJson = JsonUtility.ToJson(arrangeDataList);
        PlayerPrefs.SetString(stageName, saveJson); // ��(��)�̸� ���� ����
    }
}
