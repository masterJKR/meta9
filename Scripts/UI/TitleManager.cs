using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{

    public void StartClick() // ���� ��ŸƮ ��ư
    {
        SceneManager.LoadScene("WorldMap");
    }

    public void ExitClick() // ���� ���� ��ư
    {
        Application.Quit(); // ����Ƽ ���� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ����Ƽ �������� ���۴��ߺκ�
#endif
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
