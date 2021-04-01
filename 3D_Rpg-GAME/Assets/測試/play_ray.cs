using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play_ray : MonoBehaviour
{
    public GameObject tempobject;//先放一個暫存的空物件
    public GameObject tempobject2;//暫存的空物件，萬一暫存目標死亡可以替補
    public bool islock;
    // Start is called before the first frame update
    void Start()
    {
        islock = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Move();
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Enemy")//判斷射線是否打到敵人
            {
                islock = true;
                Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);//遊戲運行時，在SCENCE畫面可以預覽到射線
                Debug.Log(hit.transform.name);//顯示敵人名稱
                for (int j = 0; j < hit.transform.childCount; j++)
                {
                    if (hit.transform.GetChild(j).name == "Canvas")//判斷子物件裡哪一個是ui的子物件(限定名稱是預設名稱:Canvas)
                    {
                        if(tempobject!=tempobject2)
                        tempobject.SetActive(false);//將上一個血條關閉
                        hit.transform.GetChild(j).gameObject.SetActive(true);//打開目前怪物血條
                        tempobject = hit.transform.GetChild(j).gameObject;//將目前的怪物暫存起來
                                                                          
                    }
                }
            
            }
            else//打到其他地方時
            {
                tempobject.SetActive(false); //關掉暫存血條
                islock = false;
            }
        }
    }
    void Move()
    {
        if(tempobject != tempobject2) //鎖定目標不等於預設暫存目標時
        { 
        if (islock)//開啟鎖定
        {
            this.transform.LookAt(tempobject.transform.parent.position, Vector3.up);
        }
        else //關閉鎖定
        {
            this.transform.LookAt(null);
        }
        if (tempobject.GetComponentInParent<GolemPolyart>().HP <= 0)//目標死亡時，將目標改為預設的暫存目標
        {
            tempobject = tempobject2;
        }
        }
    }

    }
