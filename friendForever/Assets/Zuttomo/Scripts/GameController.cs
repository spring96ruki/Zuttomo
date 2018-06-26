using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance
    {
        get;private set;
    }

    public static int m_getChasernum;
    public GameObject playerandcamera;
    public GameObject itimathu;
    public float ItemPosition_x;
    public float ItemPosition_z;
    public float rect_x;
    public float rect_y;
    public int GamePhase;
    public bool OpenDoor;
    [SerializeField]
    private List<Sprite> itemList = new List<Sprite>();

    // Use this for initialization
    void Start()
    {
        Instance = this.GetComponent<GameController>();
        int m_getChaserNum = SelectController.GetChaserplayer();

        for (int i = 0; i < 4; i++)
        {
            float x = Random.Range(-System.Math.Abs(ItemPosition_x), ItemPosition_x);
            float z = Random.Range(-System.Math.Abs(ItemPosition_z), ItemPosition_z);
            GameObject PlayerAndCamera = Instantiate(playerandcamera, new Vector3(x, 1.3f, z), Quaternion.identity);

            PlayerAndCamera.name = ("Player" + (i + 1));
            var player = PlayerAndCamera.transform.GetChild(0).gameObject;
            var camera = PlayerAndCamera.transform.GetChild(1).gameObject;

            if (i == 0 || i == 2)
            {
                rect_x = 0f;
            }
            else
            {
                rect_x = 0.5f;
            }

            if (i == 0 || i == 1)
            {
                rect_y = 0.5f;
            }
            else
            {
                rect_y = 0f;
            }
            camera.GetComponent<Camera>().rect = new Rect(rect_x, rect_y, 0.5f, 0.5f);

            player.GetComponent<RunnerInput>().runnerNum = i + 1;
            camera.GetComponent<RunnerInput>().runnerNum = i + 1;

            if (1 == i + 1)
            {
                player.GetComponent<RunnerController>().ChaserFlag = true;
            }
        }

        GameObject.Find("Gimmick Script").GetComponent<gimmickScript>().GimmickStart();
        for (int i = 0; i < 2; i++)
        {
            ItemSpawn();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ItemSpawn()
    {
        Instantiate(itimathu, new Vector3(Random.Range(-System.Math.Abs(ItemPosition_x), ItemPosition_x), 1.3f, Random.Range(-System.Math.Abs(ItemPosition_z), ItemPosition_z)), Quaternion.identity);
    }

    public void GamePhaseAdd() {
        GamePhase++;

        if (GamePhase == 3) {
            EndGame();
        }
    }

    public void GamePhaseChange()
    {
        GamePhaseAdd();
        GameObject.Find("Gimmick Script").GetComponent<gimmickScript>().GimmickStart();
    }

    public void EndGame()
    {
        Debug.Log("EndGame");
        Debug.Log(SceneController.Instance);
        SceneController.Instance.LoadScene(SceneName.TITLE_SCENE);
    }
    public Sprite GetItemImage(int itemNum)
    {
        return itemList[itemNum];
    }
}