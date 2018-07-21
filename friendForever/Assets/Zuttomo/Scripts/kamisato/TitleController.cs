using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    private PlayerInput m_playerInput;
    RunnerController m_runnerController;
    public int player_num;
    public GameObject StartSprite;
    public GameObject HowtoplaySprite;
    public bool SelectState;

    // Use this for initialization
    void Awake()
    {
        SelectState = true;
        StartSprite.transform.localScale = new Vector3(50, 50, 1);
        HowtoplaySprite.transform.localScale = new Vector3(20, 20, 1);
        StartSprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        HowtoplaySprite.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        m_playerInput.PController(m_runnerController.m_playerNum);

        if (m_playerInput.button_A || m_playerInput.button_B || m_playerInput.button_Y || m_playerInput.button_X || Mathf.Abs(m_playerInput.Laxis_x) >= 0.7)
        {
            if (m_playerInput.Laxis_x >= 0.7)
            {
                SelectState = false;

                HowtoplaySprite.transform.localScale = new Vector3(50, 50, 1);
                StartSprite.transform.localScale = new Vector3(20, 20, 1);
                HowtoplaySprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                StartSprite.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.75f);
            }
            else if (m_playerInput.Laxis_x <= -0.7)
            {
                SelectState = true;

                StartSprite.transform.localScale = new Vector3(50, 50, 1);
                HowtoplaySprite.transform.localScale = new Vector3(20, 20, 1);
                StartSprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                HowtoplaySprite.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.75f);
            }

            if (m_playerInput.button_Y == true)
            {
                SceneController.Instance.LoadScene(SceneName.SELECT_SCENE);
            }
        }
    }
}
