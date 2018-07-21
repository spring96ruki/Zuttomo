using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour {

    float rayDistance = 10f;
    //上下する幅
    [SerializeField]
    float floatWidth = 3.0f;
    //高さ
    [SerializeField]
    float height = 1.0f;
    //上下の動きが変化する速さ
    [SerializeField]
    float moveChangeSpeed = 2.0f;
    [SerializeField]
    float moveSpeed = 3.0f;

    bool IsTouch;
    Vector3 nextPos;
    Transform findPlayer;

    void Start () {
        UpdateNextPos();
	}

    void Update()
    {

        //移動する位置
        Vector3 movePos = Vector3.zero;

        if (findPlayer == null)
        {
            movePos = new Vector3(nextPos.x, height + Mathf.Cos(Time.time * moveChangeSpeed) * floatWidth, nextPos.z);
            SearchPlayer();
        }
        else
        {
            ChasePlayer(ref movePos);
        }

        transform.position = Vector3.MoveTowards(transform.position, movePos, Time.deltaTime * moveSpeed);

        Quaternion lookRotation = Quaternion.LookRotation((movePos - transform.position).normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);

    }

    void SearchPlayer()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.gameObject.GetComponent<PlayerController>())
            {
                //発見したプレイヤーを格納
                findPlayer = hit.collider.gameObject.transform;
            }
        }

        //目的地に近づいた時
        if (Mathf.Abs(transform.position.x - nextPos.x) < 1.0f && Mathf.Abs(transform.position.z - nextPos.z) < 1.0f)
        {
            UpdateNextPos();
        }
    }

    void ChasePlayer(ref Vector3 movePos)
    {

        if (IsTouch)
        {
            //接触時に位置を知らせる
            Debug.Log(findPlayer.position);
            movePos = findPlayer.position;
        }
        else
        {
            movePos = new Vector3(findPlayer.position.x, findPlayer.position.y + Mathf.Cos(Time.time * moveChangeSpeed) * floatWidth, findPlayer.position.z);
        }

        if (Vector3.Distance(transform.position, findPlayer.position) > rayDistance)
        {
            //ターゲットと一定距離離れたら見失う
            findPlayer = null;
            UpdateNextPos();
        }

    }

    /// <summary>
    /// 次の目的地を決める
    /// </summary>
    void UpdateNextPos()
    {
        nextPos = new Vector3(transform.position.x + Random.Range(-50.0f, 50.0f), height, transform.position.z + Random.Range(-10.0f, 10.0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        IsTouch = true;
    }

    private void OnTriggerExit(Collider other)
    {
        IsTouch = false;
    }
}
