using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour
{
    public float[,] ResultStatus = new float[4, 3];
    public int Runner_EscapeCount;
    public int Runner_DeathCount;
    public int Runner_PCount;
    public GameObject TimeController;
    public GameObject UIController;
    private Text targetText;
    static int GetChaserNum;
    public bool RunnerEndFlag;
    public string[] ResultString = new string[3];

    // Use this for initialization
    void Start()
    {
        int GetChaserNum = SelectController.GetChaserplayer();
        Debug.Log(GetChaserNum);
        ResultStatus = new float[,]{
            { 0, 0, 0},
            { 0, 0, 0},
            { 0, 0, 0},
            { 0, 0, 0},
        };
        //Runnerの場合{捕まるまで、脱出までにかかった時間,何番目に逃げたか,何番目に捕まったか}
        //Chaserの場合{ゲーム終了までにかかった時間,何人逃げられたか,最終的に何人捕まえたか}

        TimeController = GameObject.Find("TimeController");
        UIController = GameObject.Find("UIController");

        Runner_EscapeCount = 0;
        Runner_DeathCount = 0;
        Runner_PCount = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RunnerEnd(int Count, bool flag)
    {
        if (flag == true)
        {
            Runner_EscapeCount++;
            ResultStatus[Count - 1, 1] = Runner_EscapeCount;
        }
        else
        {
            Runner_DeathCount++;
            ResultStatus[Count - 1, 2] = Runner_DeathCount;
        }
        ResultStatus[Count - 1, 0] = GameObject.Find("TimeController").GetComponent<TimeController>().time;

        ResultOn(Count);
    }

    public void ChaserResult()
    {
        int GetChaserNum = SelectController.GetChaserplayer();
        ResultStatus[GetChaserNum-1, 0] = GameObject.Find("TimeController").GetComponent<TimeController>().time;
        ResultStatus[GetChaserNum-1, 1] = Runner_EscapeCount;
        ResultStatus[GetChaserNum-1, 2] = Runner_DeathCount;
        Debug.Log(GetChaserNum);
        ResultOn(GetChaserNum);
    }

    public void ResultOn(int Count)
    {
        int GetChaserNum = SelectController.GetChaserplayer();
        if (Count == GetChaserNum)
        {
            //Chaserのリザルト表示
            ResultString[0] = "時間:";
            ResultString[1] = "逃げられた:";
            ResultString[2] = "捕まえた:";
        }
        else
        {
            //Runnerのリザルト表示
            ResultString[0] = "時間:";
            ResultString[1] = "逃げ番目:";
            ResultString[2] = "捕まり番目:";
        }

        Debug.Log(Count);
        UIController.GetComponent<UIController>().Result[Count - 1].SetActive(true);
        targetText = UIController.GetComponent<UIController>().ResultText1[Count - 1];
        targetText.GetComponent<Text>().text = ("かかった時間" + ResultStatus[Count - 1, 0]);
        targetText = UIController.GetComponent<UIController>().ResultText2[Count - 1];
        targetText.GetComponent<Text>().text = (ResultString[1] + ResultStatus[Count - 1, 1]);
        targetText = UIController.GetComponent<UIController>().ResultText3[Count - 1];
        targetText.GetComponent<Text>().text = (ResultString[2] + ResultStatus[Count - 1, 2]);
    }
}