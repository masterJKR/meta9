using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]   // �ش� Ŭ������ ���� ����̴�.
public class SaveData
{
    public int aId = 0;
    public string objectTag = "";  // ��ü�� �±� 
}


public class SaveDataList
{
    public SaveData[] saveDatas;
}
