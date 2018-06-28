using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class gimmickScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] player;
    [SerializeField]
    private GameObject[] gimmickItem;
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject[] GimmickArea_Words;
    [SerializeField]
    private GameObject[] GimmickArea_Door;
    private GameObject GimmickItem;                     //ランダムで選択したドア、文字を格納する場所
    public int[] GetGimmickItem_word = new int[7];      //獲得された文字の種類
    public int[] GetGimmickItem_player = new int[4];    //各プレイヤーの獲得文字数
    public float ItemPosition_x;
    public float ItemPosition_z;
    public int RandomGimmickAreaNumber;

    // Use this for initialization
    void Start()
    {
        int m_getChaserNum = SelectController.GetChaserplayer();
        for (int i = 0; i < 4; i++)
        {
            player[i] = GameObject.Find("Player" + (i + 1)).transform.GetChild(0).gameObject;
        }

        RandomGimmickAreaNumber = (int)UnityEngine.Random.Range(0, GimmickArea_Door.Length);
        GimmickItem = GimmickArea_Door[RandomGimmickAreaNumber];
        Instantiate(GimmickItem, new Vector3(GimmickItem.transform.position.x, 0f, GimmickItem.transform.position.z), Quaternion.identity);
    }

    internal void RunnerKill(int target)
    {
        Debug.Log("kill");        
    }

    public void GimmickStart()
    {
        for (int i = 0; i < 7; i++)
        {
            RandomGimmickAreaNumber = (int)UnityEngine.Random.Range(0, GimmickArea_Words.Length);
            GimmickItem = GimmickArea_Words[RandomGimmickAreaNumber];

            //Debug.Log(GimmickItem.transform.localScale.x / 2);
            float x = UnityEngine.Random.Range(-System.Math.Abs(GimmickItem.transform.localScale.x / 2), (GimmickItem.transform.localScale.x / 2) + GimmickItem.transform.position.x);
            float z = UnityEngine.Random.Range(-System.Math.Abs(GimmickItem.transform.localScale.z / 2), (GimmickItem.transform.localScale.z / 2) + GimmickItem.transform.position.z);
            Debug.Log(i + "(" + x + "," + z + ")");
            Instantiate(gimmickItem[i], new Vector3(x, 1.3f, z), Quaternion.identity);
        }

        //for (int i = 0; i < 5; i++)
        //{
        //    float x = UnityEngine.Random.Range(-System.Math.Abs(ItemPosition_x), ItemPosition_x);
        //    float z = UnityEngine.Random.Range(-System.Math.Abs(ItemPosition_z), ItemPosition_z);
        //    Instantiate(gimmickItem[UnityEngine.Random.Range(0, 6)], new Vector3(x, 1.3f, z), Quaternion.identity);
        //}
        
        GetGimmickItem_word = new int[] { 0, 0, 0, 0, 0, 0, 0 };
        GetGimmickItem_player = new int[] { 0, 0, 0, 0 };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetWord()
    {
        if (GetGimmickItem_player[0] + GetGimmickItem_player[1] + GetGimmickItem_player[2] + GetGimmickItem_player[3] == 7)
        {
            GameObject.Find("GameController").GetComponent<GameController>().OpenDoor = true;
            int max = GetGimmickItem_player.Select(s => s).Max();
            for (int i = 0; i < 4; i++)
            {
                if (GetGimmickItem_player[i] >= max)
                {
                    GetGimmickItem_player[i] = 1;
                }
                else
                {
                    GetGimmickItem_player[i] = 0;
                }
            }
        }
    }
}
