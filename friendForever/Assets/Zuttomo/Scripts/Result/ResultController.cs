using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultController : MonoBehaviour {
	public float[,] ResultStatus = new float[4, 3];
    public int Runner_EscapeCount;
    public int Runner_DeathCount;
    public int Runner_PCount;
    public GameObject TimeController;

	// Use this for initialization
	void Start () {
        ResultStatus = new float[,]{
            { 0, 0, 0},
            { 0, 0, 0},
            { 0, 0, 0},
            { 0, 0, 0},
        };

        //Runnerの場合{捕まるまで、脱出までにかかった時間,何番目に逃げたか,何番目に捕まったか}
        //Chaserの場合{ゲーム終了までにかかった時間,最終的に何人捕まえたか,0}

        TimeController = GameObject.Find("TimeController");

        Runner_EscapeCount = 1;
        Runner_DeathCount = 1;
}

    // Update is called once per frame
    void Update () {
        if (Input.GetKey("1"))
        {
            Runner_PCount = 1;
        }
        if (Input.GetKey("2"))
        {
            Runner_PCount = 2;
        }
        if (Input.GetKey("z"))
        {
            Debug.Log(GameObject.Find("TimeController").GetComponent<TimeController>().time);
        }
        if (Input.GetKey("z"))
        {
            Debug.Log(GameObject.Find("TimeController").GetComponent<TimeController>().time);
        }
        if (Input.GetKey("x"))
        {
            RunnerEscape(Runner_PCount);
        }
        if (Input.GetKey("c"))
        {
            RunnerDeath(Runner_PCount);
        }
        if (Input.GetKey("v"))
        {
            Debug.Log(ResultStatus[0, 0]);
        }

        Runner_PCount = 0;
    }

    public void RunnerEscape(int Count)
    {
        Runner_EscapeCount++;
        Debug.Log(Count);
    }

    public void RunnerDeath(int Count)
    {
        ResultStatus[Count - 1, 0] = GameObject.Find("TimeController").GetComponent<TimeController>().time;
        ResultStatus[Count - 1, 2] = Runner_DeathCount;
        Debug.Log(Count + ":" + ResultStatus[Count - 1, 0]);
        Runner_DeathCount++;
        Debug.Log(Count);
    }
}