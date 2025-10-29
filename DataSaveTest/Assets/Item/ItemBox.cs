using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public Sprite openImg; // 상자 열린 이미지
    public GameObject itemfab;  // 상자안에 담겨있는 아이템
    public bool isClosed = true;// 닫혀있어야 아이템이 나온다.
    public int arrangeId = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.tag == "Player" && isClosed)
        {
            GetComponent<SpriteRenderer>().sprite = openImg; // 이미지 변경
            isClosed = false;
            Instantiate(itemfab , transform.position, Quaternion.identity) ; // 아이템 만들기
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
