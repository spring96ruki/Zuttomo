using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour {
    private RunnerInput m_runnerInput;
    public int player_num;
    public int selectstate;
    public bool PushSubmit;
    public SelectController selectController;
    public GameObject RunnerSprite;
    public GameObject ChaserSprite;
    public GameObject SubmitSprite;

    // Use this for initialization
    void Awake () {
        m_runnerInput = GetComponent<RunnerInput>();
        selectstate = 0;
        PushSubmit = false;
        SubmitSprite.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        m_runnerInput.PController();

        if (m_runnerInput.button_A == true || m_runnerInput.button_B == true || Mathf.Abs(m_runnerInput.Laxis_x) >= 0.7) {
            if (PushSubmit == false)
            {
                if (m_runnerInput.Laxis_x >= 0.7)
                {
                    selectstate = 2;
                    RunnerSprite.transform.localScale = new Vector3(700, 700, 1);
                    ChaserSprite.transform.localScale = new Vector3(1300, 1300, 1);
                }
                else if (m_runnerInput.Laxis_x <= -0.7)
                {
                    selectstate = 1;
                    RunnerSprite.transform.localScale = new Vector3(1300, 1300, 1);
                    ChaserSprite.transform.localScale = new Vector3(700, 700, 1);
                }

                if (m_runnerInput.button_A == true)
                {
                    PushSubmit = true;
                    selectController.player_count++;
                    SubmitSprite.SetActive(true);
                }
            } else if (m_runnerInput.button_B == true)
            {
                PushSubmit = false;
                selectController.player_count--;
                SubmitSprite.SetActive(false);
            }

            selectController.allplayerSelectState[player_num - 1] = selectstate;
            selectController.Lottery();
        }
	}

    public void Title()
    {
        SceneController.Instance.LoadScene(SceneName.SELECT_SCENE);
    }
}
