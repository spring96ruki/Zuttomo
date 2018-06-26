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

    // Use this for initialization
    void Awake()
    {
        m_runnerInput = GetComponent<RunnerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        m_runnerInput.PController();

        if (m_runnerInput.button_A == true || m_runnerInput.button_B == true || Mathf.Abs(m_runnerInput.Laxis_x) >= 0.7)
        {
            if (m_runnerInput.Laxis_x >= 0.7)
            {
                StartSprite.transform.localScale = new Vector3(20, 20, 1);
                HowtoplaySprite.transform.localScale = new Vector3(60, 60, 1);
            }
            else if (m_runnerInput.Laxis_x <= -0.7)
            {
                StartSprite.transform.localScale = new Vector3(60, 60, 1);
                HowtoplaySprite.transform.localScale = new Vector3(20, 20, 1);
            }

            if (m_runnerInput.button_A == true)
            {
                SceneController.Instance.LoadScene(SceneName.SELECT_SCENE);
            }
        }        
    }
}
