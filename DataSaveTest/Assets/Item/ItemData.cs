using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public enum ItemType
{
    Arrow, Key , Life,
}



public class ItemData : MonoBehaviour
{
    public ItemType Type;
    public int count = 1;
    public int arrangeId = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.gameObject.tag == "Player")
        {
            //if (Type == ItemType.Arrow) ItemKeeper.hasArrows += count;
            switch (Type)
            {
                case ItemType.Arrow:
                    ItemKeeper.hasArrows += count; 

                    // yield �� ���� �񵿱� �۾��� �����ϴµ�  
                    // ��������� �ð��� �ɸ����� �˼� ���⿡ ������ �ö����� ���ӵɼ��ְ�
                    StartCoroutine( SendArrowCount( ItemKeeper.hasArrows ) );

                    break;
                case ItemType.Key:
                    ItemKeeper.hasKeys += count; break;
                case ItemType.Life:
                    PlayerController.hp++;
                    PlayerPrefs.SetInt("PlayerHP", PlayerController.hp);
                    break;
            }

            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 2.5f;
            rb.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
            Destroy(gameObject, 0.5f);
           // SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SendArrowCount(int count)
    {
        // ����Ƽ ���� �����͸� ������ ������ 
        // 1. ������ �ּ�
        // 2. json ������ Ȯ��
        // 3. ���۹��
        // 4. Header Set
        // 5. ����

        string url = "http://localhost:8080/api/datatest";

        string data = JsonUtility.ToJson( new ArrowData(count) );

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        
        //���ڿ� json�� UTF-8 ����Ʈ�� ��ȯ - HTTP�� ����Ʈ�����ϹǷ� �ʼ�
        byte[] body = Encoding.UTF8.GetBytes(data);

        //��ȯ�� ����Ʈ�� ��û �������� ����,  UpLoadHandlerRaw�� ����Ʈ�� ����
        request.uploadHandler = new UploadHandlerRaw(body);

        // ���� ������ �޸𸮿� ���۸� �ؼ� �б� �����ϱ�
        request.downloadHandler = new DownloadHandlerBuffer();

        //��û ����� ������ Ÿ�� ��� ( ������ ������ json�̾�  ��� ������ �˼� �ְ�)
        request.SetRequestHeader("Content-Type", "application/json");

        // �񵿱� ���� ����
        // ������ ������ �ö����� ���
        yield return request.SendWebRequest(); 

        if( request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("���� : " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log("���� : " + request.error);
        }
    }

    [System.Serializable]  // ������ ����� Ŭ���� �ۼ���
    public class ArrowData
    {
        public string item = "arrow";
        public int count;

        public ArrowData(int count) {  this.count = count; }
    }
    
}
