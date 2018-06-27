using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    private RunnerInput m_runnerInput;
    public int player_num;
    public GameObject StartSprite;
    public GameObject HowtoplaySprite;
    public bool SelectState;

    // Use this for initialization
    void Awake()
    {
        m_runnerInput = GetComponent<RunnerInput>();
        SelectState = true;
        StartSprite.transform.localScale = new Vector3(50, 50, 1);
        HowtoplaySprite.transform.localScale = new Vector3(20, 20, 1);
        StartSprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        HowtoplaySprite.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        m_runnerInput.PController();

        if (m_runnerInput.button_A || m_runnerInput.button_B || m_runnerInput.button_Y || m_runnerInput.button_X || Mathf.Abs(m_runnerInput.Laxis_x) >= 0.7)
        {
            if (m_runnerInput.Laxis_x >= 0.7)
            {
                SelectState = false;

                HowtoplaySprite.transform.localScale = new Vector3(50, 50, 1);
                StartSprite.transform.localScale = new Vector3(20, 20, 1);
                HowtoplaySprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                StartSprite.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.75f);
            }
            else if (m_runnerInput.Laxis_x <= -0.7)
            {
                SelectState = true;

                StartSprite.transform.localScale = new Vector3(50, 50, 1);
                HowtoplaySprite.transform.localScale = new Vector3(20, 20, 1);
                StartSprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                HowtoplaySprite.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.75f);
            }

            if (m_runnerInput.button_Y == true)
            {
                SceneController.Instance.LoadScene(SceneName.SELECT_SCENE);
            }
        }
    }
}
