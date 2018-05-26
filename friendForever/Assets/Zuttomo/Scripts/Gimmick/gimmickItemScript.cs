using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gimmickItemScript : MonoBehaviour {

    [SerializeField, Header("文字の番号")]
    private int word_num;
	[SerializeField, Header("ドアか否か")]
	private bool Door;	//trueがドア
						//falseがアイテム

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject GimmickScript = GameObject.Find("Gimmick Script");
			if (Door) {
				if (GimmickScript.GetComponent<gimmickScript> ().GamePhase == 1) {
					if (GimmickScript.GetComponent<gimmickScript> ().GetGimmickItem_player [collision.gameObject.GetComponent<RunnerInput> ().runnerNum - 1] == 1) {
						Debug.Log ("goal");
					}
				} 
			} else {
				if (GimmickScript.GetComponent<gimmickScript> ().GetGimmickItem_word [word_num - 1] == 0) {
					GimmickScript.GetComponent<gimmickScript> ().GetGimmickItem_word [word_num - 1] = collision.gameObject.GetComponent<RunnerInput> ().runnerNum;
					GimmickScript.GetComponent<gimmickScript> ().GetGimmickItem_player [collision.gameObject.GetComponent<RunnerInput> ().runnerNum - 1]++;
				}
				GimmickScript.GetComponent<gimmickScript> ().GetWord ();
				Destroy (gameObject, 0);
			}
        }
    }
}
