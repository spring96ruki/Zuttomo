using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public static class CameraStaticFunction
{
    public static void SetLayerRecursively(this GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetLayerRecursively(layer);
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        do
        {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;
        } while (angle < -360 || angle > 360);

        return Mathf.Clamp(angle, min, max);
    }

    public static ClipPlanePoints NearClipPlanePoints(this Camera camera, Vector3 pos, float clipPlaneMargin)
    {
        var clipPlanePoints = new ClipPlanePoints();

        var transform = camera.transform;
        var halfFOV = (camera.fieldOfView / 2) * Mathf.Deg2Rad;
        var aspect = camera.aspect;
        var distance = camera.nearClipPlane;
        var height = distance * Mathf.Tan(halfFOV);
        var width = height * aspect;
        height *= 1 + clipPlaneMargin;
        width *= 1 + clipPlaneMargin;
        clipPlanePoints.LowerRight = pos + transform.right * width;
        clipPlanePoints.LowerRight -= transform.up * height;
        clipPlanePoints.LowerRight += transform.forward * distance;

        clipPlanePoints.LowerLeft = pos - transform.right * width;
        clipPlanePoints.LowerLeft -= transform.up * height;
        clipPlanePoints.LowerLeft += transform.forward * distance;

        clipPlanePoints.UpperRight = pos + transform.right * width;
        clipPlanePoints.UpperRight += transform.up * height;
        clipPlanePoints.UpperRight += transform.forward * distance;

        clipPlanePoints.UpperLeft = pos - transform.right * width;
        clipPlanePoints.UpperLeft += transform.up * height;
        clipPlanePoints.UpperLeft += transform.forward * distance;

        return clipPlanePoints;
    }
  
    public static BoxPoint GetBoxPoint(this BoxCollider boxCollider)
    {
        BoxPoint bp = new BoxPoint();
        bp.center = boxCollider.transform.TransformPoint(boxCollider.center);
        var height = boxCollider.transform.lossyScale.y * boxCollider.size.y;
        var ray = new Ray(bp.center, boxCollider.transform.up);

        bp.top = ray.GetPoint((height * 0.5f));
        bp.bottom = ray.GetPoint(-(height * 0.5f));

        return bp;
    }

}
public struct BoxPoint
{
    public Vector3 top;
    public Vector3 center;
    public Vector3 bottom;

}
public struct ClipPlanePoints
{
    public Vector3 UpperLeft;
    public Vector3 UpperRight;
    public Vector3 LowerLeft;
    public Vector3 LowerRight;
}
[System.Flags]
public enum HitBarPoints
{
    None = 0,
    Top = 1,
    Center = 2,
    Bottom = 4
}

public class PlayerTPCamera : MonoBehaviour
{
    private static PlayerTPCamera _instance;
    public static PlayerTPCamera instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerTPCamera>();

            }

            return _instance;
        }
    }

    RunnerInput m_pInput;
    public int playerNum;

    #region inspector properties    

    public Transform target;
    public float smoothCameraRotation = 12f;
    public LayerMask cullingLayer = 1 << 0;
    public bool lockCamera;

    public float rightOffset = 0f;
    public float defaultDistance = 2.5f;
    public float height = 1.4f;
    public float smoothFollow = 10f;
    public float xLightStickSensitivity = 3f;
    public float yLightStickSensitivity = 3f;
    public float yMinLimit = -40f;
    public float yMaxLimit = 80f;

    [Header("Lockon Param")]
    [Range(-1, 1), Tooltip("-1 ～ 1. 1で同じ向き、-1で真逆、0で垂直")]
    public float lockonAngleLimit = 0.2f;
    public float lockonDistanceMax = 10.0f;
    public float lockonXSensitivity = 0.7f;
    public float lockonYLimitMin = -2.0f;
    public float lockonYLimitMax = 15.0f;

    #endregion

    #region hide properties    

    [HideInInspector]
    public int indexList, indexLookPoint;
    [HideInInspector]
    public float offSetPlayerPivot;
    [HideInInspector]
    public string currentStateName;
    [HideInInspector]
    public Transform currentTarget;
    [HideInInspector]
    public Vector2 movementSpeed;

    private Transform targetLookAt;
    private Vector3 currentTargetPos;
    private Vector3 lookPoint;
    private Vector3 current_cPos;
    private Vector3 desired_cPos;
    private Camera _camera;
    private float distance = 5f;
    private float mouseY = 0f;
    private float mouseX = 0f;
    private float currentHeight;
    private float cullingDistance;
    private float checkHeightRadius = 0.4f;
    private float clipPlaneMargin = 0f;
    private float forward = -1f;
    private float xMinLimit = -360f;
    private float xMaxLimit = 360f;
    private float cullingHeight = 0.2f;
    private float cullingMinDist = 0.1f;

    private float lockonDistance = 10.0f;
    Vector3 twoPointsCenter;
    #endregion

    void Start()
    {
        if(VRSettings.enabled)
        {
            InputTracking.Recenter();
        }

        Init();
        m_pInput = GetComponent<RunnerInput>();
    }

    public void Init()
    {
        if (target == null)
            return;

        _camera = transform.GetComponentInChildren<Camera>();
        currentTarget = target;
        currentTargetPos = new Vector3(currentTarget.position.x, currentTarget.position.y + offSetPlayerPivot, currentTarget.position.z);

        targetLookAt = new GameObject("targetLookAt").transform;
        targetLookAt.position = currentTarget.position;
        targetLookAt.hideFlags = HideFlags.HideInHierarchy;
        targetLookAt.rotation = currentTarget.rotation;

        mouseY = currentTarget.eulerAngles.x;
        mouseX = currentTarget.eulerAngles.y;

        distance = defaultDistance;
        currentHeight = height;
    }

    void Update()
    {
        if (VRSettings.enabled)
        {
            if (Input.GetKeyDown(KeyCode.F1) /*|| GamePadManager.Instance.GetKeyDown(DS4KeyCode.PS)*/)
            {
                InputTracking.Recenter();
            }
        }

        if (target == null || targetLookAt == null) return;

        m_pInput.PController(playerNum);
        CameraMoveFake();
        CameraMovement();
        
    }

    void CameraMoveFake()
    {
        var m_lookAt = currentTarget.position + Vector3.up * height;
        // targetの移動量分、カメラも移動する
        transform.position = m_lookAt - transform.forward * defaultDistance;
        transform.LookAt(m_lookAt);
        Vector3 m_targetPos = currentTarget.position;

        float h = m_pInput.Raxis_x * 150 * Time.deltaTime;
        float v = m_pInput.Raxis_y * 150 * Time.deltaTime;



        transform.LookAt(m_lookAt);

        // targetの位置のY軸を中心に、回転する
        transform.RotateAround(m_targetPos, Vector3.up, v);

        // targetの位置のY軸を中心に、回転する
        transform.RotateAround(m_targetPos, Vector3.up, -v);

        // カメラの垂直移動（角度制限なし）
        //transform.RotateAround(targetPos, transform.right, h);
        transform.RotateAround(m_targetPos, Vector3.up, -v);
    }

    //public void RotateCamera(float x, float y)
    //{

    //    if (VRSettings.enabled)
    //    {
    //        mouseX += x * xLightStickSensitivity;

    //        movementSpeed.x = x;
    //        if (!lockCamera)
    //        {
    //            mouseX = CameraStaticFunction.ClampAngle(mouseX, xMinLimit, xMaxLimit);
    //        }
    //        else
    //        {
    //            LookTargetUpdate();
    //        }
    //        return;
    //    }

    //    mouseX += x * xLightStickSensitivity;
    //    mouseY -= y * yLightStickSensitivity;

    //    movementSpeed.x = x;
    //    movementSpeed.y = -y;
    //    if (lockCamera)
    //    {
    //        LookTargetUpdate();
    //    }

    //    mouseY = CameraStaticFunction.ClampAngle(mouseY, yMinLimit, yMaxLimit);
    //    mouseX = CameraStaticFunction.ClampAngle(mouseX, xMinLimit, xMaxLimit);
    //}

    /// <summary>
    /// ロックオンするかどうか
    /// </summary>
    /// <param name="isLook"></param>
    //public void SetLookTarget(bool isLook)
    //{
    //    if (ActiveEnemyManager.Instance.NearEnemy == null)
    //    {
    //        return;
    //    }

    //    lockCamera = isLook;
    //}

    /// <summary>
    /// ロックオン中のカメラの更新
    /// </summary>
    //void LookTargetUpdate()
    //{
    //    // ターゲットするべき敵がいなければロックオンを解除
    //    if(ActiveEnemyManager.Instance.NearEnemy == null)
    //    {
    //        lockCamera = false;
    //        return;
    //    }
    //    // プレイヤーと敵の距離を求めて
    //    var playerPos = GameManager.Instance.PlayerTransform.position;
    //    var enemyPos = ActiveEnemyManager.Instance.NearEnemy.transform.position;
    //    // プレイヤーと敵の2点間の距離の中心を求める
    //    twoPointsCenter = Vector3.Lerp(enemyPos, playerPos, 0.75f);

    //    // mouseXとmouseYの自動補完処理？？
    //    // ex. 例えばこんなのとか
    //    // a.プレイヤーから敵への向き
    //    var offset = enemyPos - playerPos;
    //    var player2EnemyDir = (offset).normalized;
    //    offset.y = playerPos.y;
    //    offset = offset.normalized;
    //    // b.カメラの向き
    //    var camera2CenterDir = transform.forward;
    //    // aとbの内積を調べる(向きが等しければ1, 真逆なら-1)
    //    float dot = Vector3.Dot(player2EnemyDir, camera2CenterDir);
    //    // 一定の数値でカメラの向きを自動補完を行う
    //    if (dot < lockonAngleLimit)
    //    {
    //        // ここは適当
    //        mouseX += dot * 0.7f * xLightStickSensitivity;
    //    }
    //    // lockonDistanceの自動調整（距離を上げたり下げたり）
    //    float mag = (enemyPos - playerPos).magnitude;
    //    if (!VRSettings.enabled)
    //    {
    //        float dotY = Vector3.Dot(player2EnemyDir, offset);
    //        mouseY = Mathf.Min(Mathf.Max(lockonYLimitMin, mouseY - dotY), lockonYLimitMax);

    //        lockonDistance = Mathf.Min(Mathf.Max(defaultDistance + mag * 0.25f, mag * 1.5f * Mathf.Abs(dot)), lockonDistanceMax);
    //    }
    //    else
    //    {
    //        lockonDistance = Mathf.Min(Mathf.Max(defaultDistance + (mag * 0.5f), mag * 1.5f), lockonDistanceMax);
    //    }
    //}

    void CameraMovement()
    {
        if (currentTarget == null)
            return;

        float dt = Time.deltaTime;


        float targetDistance = defaultDistance;
        distance = Mathf.Lerp(distance, targetDistance, smoothFollow * dt);
        cullingDistance = Mathf.Lerp(cullingDistance, distance, dt);
        var camDir = (forward * targetLookAt.forward) + (rightOffset * targetLookAt.right);

        camDir = camDir.normalized;

        Vector3 targetPos = currentTarget.position;
        targetPos.y += offSetPlayerPivot;
        
        currentTargetPos = targetPos;
        desired_cPos = targetPos + new Vector3(0, height, 0);
        current_cPos = currentTargetPos + new Vector3(0, currentHeight, 0);

        RaycastHit hitInfo;

        ClipPlanePoints planePoints = _camera.NearClipPlanePoints(current_cPos + (camDir * (distance)), clipPlaneMargin);
        ClipPlanePoints oldPoints = _camera.NearClipPlanePoints(desired_cPos + (camDir * distance), clipPlaneMargin);

        if (Physics.SphereCast(targetPos, checkHeightRadius, Vector3.up, out hitInfo, cullingHeight + 0.2f, cullingLayer))
        {
            var t = hitInfo.distance - 0.2f;
            t -= height;
            t /= (cullingHeight - height);
            cullingHeight = Mathf.Lerp(height, cullingHeight, Mathf.Clamp(t, 0.0f, 1.0f));
        }

        if (CullingRayCast(desired_cPos, oldPoints, out hitInfo, distance + 0.2f, cullingLayer, Color.blue))
        {
            distance = hitInfo.distance - 0.2f;
            if (distance < targetDistance)
            {
                var t = hitInfo.distance;
                t -= cullingMinDist;
                t /= cullingMinDist;
                currentHeight = Mathf.Lerp(cullingHeight, height, Mathf.Clamp(t, 0.0f, 1.0f));
                current_cPos = currentTargetPos + new Vector3(0, currentHeight, 0);
            }
        }
        else
        {
            currentHeight = height;
        }
        if (CullingRayCast(current_cPos, planePoints, out hitInfo, distance, cullingLayer, Color.cyan)) distance = Mathf.Clamp(cullingDistance, 0.0f, defaultDistance);
        var lookPoint = current_cPos + targetLookAt.forward * 2f;
        lookPoint += (targetLookAt.right * Vector3.Dot(camDir * (distance), targetLookAt.right));

        Quaternion newRot = Quaternion.Euler(mouseY, mouseX, 0);
        var targetRot = Quaternion.Slerp(targetLookAt.rotation, newRot, smoothCameraRotation * dt);

        targetLookAt.SetPositionAndRotation(current_cPos, targetRot);
        var position = current_cPos + (camDir * (distance));
        Quaternion rotation = Quaternion.LookRotation((lookPoint) - position);

        transform.SetPositionAndRotation(position, rotation);
        movementSpeed = Vector2.zero;
    }


    bool CullingRayCast(Vector3 from, ClipPlanePoints _to, out RaycastHit hitInfo, float distance, LayerMask cullingLayer, Color color)
    {
        bool value = false;

        if (Physics.Raycast(from, _to.LowerLeft - from, out hitInfo, distance, cullingLayer))
        {
            value = true;
            cullingDistance = hitInfo.distance;
        }

        if (Physics.Raycast(from, _to.LowerRight - from, out hitInfo, distance, cullingLayer))
        {
            value = true;
            if (cullingDistance > hitInfo.distance) cullingDistance = hitInfo.distance;
        }

        if (Physics.Raycast(from, _to.UpperLeft - from, out hitInfo, distance, cullingLayer))
        {
            value = true;
            if (cullingDistance > hitInfo.distance) cullingDistance = hitInfo.distance;
        }

        if (Physics.Raycast(from, _to.UpperRight - from, out hitInfo, distance, cullingLayer))
        {
            value = true;
            if (cullingDistance > hitInfo.distance) cullingDistance = hitInfo.distance;
        }

        return value;
    }
}
