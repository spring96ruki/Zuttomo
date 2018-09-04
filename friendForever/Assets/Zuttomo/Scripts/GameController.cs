using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonMono<GameController> {

    public GameObject m_itimatsu;
    public GameObject m_drug;
    public GameObject m_amulets;
    public GameObject[] itemStorage = new GameObject[3];

    private void Start()
    {
        AddItemStorage(m_itimatsu, m_drug, m_amulets);
    }

    void AddItemStorage(GameObject itimatsu, GameObject drug, GameObject amulets)
    {
        itemStorage[0] = itimatsu;
        itemStorage[1] = drug;
        itemStorage[2] = amulets;
    }
}
