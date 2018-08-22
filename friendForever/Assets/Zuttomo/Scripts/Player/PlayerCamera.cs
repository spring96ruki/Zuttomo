using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera
{
    float m_cameraMoveSpeed = 0.5f;

    public void CameraMovement(GameObject cameraObject, GameObject player)
    {
        Vector3 distance = new Vector3(0f, 0f, 0f);
        distance = cameraObject.transform.position - player.transform.position;

        Vector3 updatePosition = cameraObject.transform.position;
        updatePosition.x = player.transform.position.x + distance.x;
        updatePosition.y = player.transform.position.y + distance.y;
        updatePosition.z = player.transform.position.z + distance.z;
        cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position, updatePosition, Time.deltaTime * 5f);

        CameraCheck(cameraObject, player);

    }

    void CameraCheck(GameObject cameraObject, GameObject player)
    {
        float x = Mathf.Pow(cameraObject.transform.position.x - player.transform.position.x, 2);
        float y = Mathf.Pow(cameraObject.transform.position.y - player.transform.position.y, 2);
        float z = Mathf.Pow(cameraObject.transform.position.z - player.transform.position.z, 2);
        float sqrDis = Mathf.Sqrt(x + y + z);

        Ray ray = new Ray(player.transform.position, cameraObject.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, sqrDis))
        {
            //cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position, hit.point, m_cameraMoveSpeed * Time.deltaTime);
            Debug.Log("壁ドン");
        }
    }
}
