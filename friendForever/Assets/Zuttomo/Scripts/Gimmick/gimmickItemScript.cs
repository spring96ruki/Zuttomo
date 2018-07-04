using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gimmickItemScript : MonoBehaviour
{
    [SerializeField, Header("文字の番号")]
    private int word_num;
    [SerializeField, Header("ドアか否か")]
    private bool Door;  //trueがドア
                        //falseがアイテム

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<RunnerController>().ChaserFlag == false)
        {
            GameObject GimmickScript = GameObject.Find("Gimmick Script");
            if (Door)
            {
                if (GameObject.Find("GameController").GetComponent<GameController>().OpenDoor == true)
                {
                    if (GimmickScript.GetComponent<gimmickScript>().GetGimmickItem_player[collision.gameObject.GetComponent<RunnerStatus>().runnerNum - 1] == 1)
                    {
                        GameObject.Find("GameController").GetComponent<GameController>().GamePhaseChange();
                        collision.gameObject.SetActive(false);
                        Debug.Log("goal");
                    }
                }
            }
            else
            {
                if (GimmickScript.GetComponent<gimmickScript>().GetGimmickItem_word[word_num - 1] == 0)
                {
                    GimmickScript.GetComponent<gimmickScript>().GetGimmickItem_word[word_num - 1] = collision.gameObject.GetComponent<RunnerStatus>().runnerNum;
                    GimmickScript.GetComponent<gimmickScript>().GetGimmickItem_player[collision.gameObject.GetComponent<RunnerStatus>().runnerNum - 1]++;
                }
                GimmickScript.GetComponent<gimmickScript>().GetWord();
                Destroy(gameObject, 0);
            }
        }
    }
}
