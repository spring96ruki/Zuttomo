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
    public int DammyWord;                               //ダミー文字を置く個数
    public int[] SelectGimmickAreaWord;                //すでに選ばれているギミックエリア番号

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
        Instantiate(door, new Vector3(GimmickItem.transform.position.x, 0f, GimmickItem.transform.position.z), Quaternion.identity);
    }

    internal void RunnerKill(int KillRunnerNum)
    {
        for (int i = 0; i < GetGimmickItem_word.Length + DammyWord; i++) {
            if (GetGimmickItem_word[i] == KillRunnerNum)
            {
                GetGimmickItem_word[i] = 0;
                WordSpawn(i);
            }
        }
    }

    public void GimmickStart()
    {
        SelectGimmickAreaWord = new int[GimmickArea_Words.Length];
        for (int i = 0; i < SelectGimmickAreaWord.Length; i++)
        {
            SelectGimmickAreaWord[i] = 0;
        }

        for (int i = 0; i < GetGimmickItem_word.Length + DammyWord; i++)
        {
            WordSpawn(i);
        }

        GetGimmickItem_word = new int[] { 0, 0, 0, 0, 0, 0, 0 };
        GetGimmickItem_player = new int[] { 0, 0, 0, 0 };

    }

    void WordSpawn(int i)
    {
        //SelectGimmickAreaWord = new int[GimmickArea_Words.Length];
        while (true) {
            RandomGimmickAreaNumber = (int)UnityEngine.Random.Range(0, GimmickArea_Words.Length);
            if (SelectGimmickAreaWord[RandomGimmickAreaNumber] == 0)
            {
                SelectGimmickAreaWord[RandomGimmickAreaNumber] = 1;
                break;
            }else if (SelectGimmickAreaWord.Min() == 1)
            {
                for (int j = 0; j < SelectGimmickAreaWord.Length - 1; j++)
                {
                    SelectGimmickAreaWord[j] = 0;
                }
            }
        }

        //Debug.Log(RandomGimmickAreaNumber);
        GimmickItem = GimmickArea_Words[RandomGimmickAreaNumber];
        float x = UnityEngine.Random.Range(GimmickItem.transform.position.x - System.Math.Abs(GimmickItem.transform.localScale.x / 4), GimmickItem.transform.position.x + GimmickItem.transform.localScale.x / 4);
        float z = UnityEngine.Random.Range(GimmickItem.transform.position.z - System.Math.Abs(GimmickItem.transform.localScale.z / 4), GimmickItem.transform.position.z + GimmickItem.transform.localScale.z / 4);
        if (i < GetGimmickItem_word.Length)
        {
            Instantiate(gimmickItem[i], new Vector3(x, 1.3f, z), Quaternion.identity);
        }
        else
        {
            Instantiate(gimmickItem[UnityEngine.Random.Range(0, GetGimmickItem_word.Length - 1)], new Vector3(x, 1.3f, z), Quaternion.identity);
        }
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
