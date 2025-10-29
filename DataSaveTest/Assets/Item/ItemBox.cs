using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public Sprite openImg; // ���� ���� �̹���
    public GameObject itemfab;  // ���ھȿ� ����ִ� ������
    public bool isClosed = true;// �����־�� �������� ���´�.
    public int arrangeId = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.tag == "Player" && isClosed)
        {
            GetComponent<SpriteRenderer>().sprite = openImg; // �̹��� ����
            isClosed = false;
            Instantiate(itemfab , transform.position, Quaternion.identity) ; // ������ �����
            Debug.Log("box : " + arrangeId +"  , " +gameObject.tag);
            SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
