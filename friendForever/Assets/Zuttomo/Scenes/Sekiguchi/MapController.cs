using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour {

    [SerializeField]
    Transform player;
    [SerializeField]
    Image playerImage;

	void Start () {
		
	}
	
	void Update () {
        playerImage.transform.rotation = Quaternion.Euler(0, 0, -player.transform.eulerAngles.y);
	}
}
