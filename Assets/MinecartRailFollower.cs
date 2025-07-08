using UnityEngine;
using System.Collections.Generic;

public class MinecartRailFollower : MonoBehaviour
{
    public Transform[] railPoints; // レールパスのポイントを格納する配列
    public float speed = 5f; // トロッコの移動速度
    public float rotationSpeed = 5f; // トロッコの回転速度
    public float stickThreshold = 0.5f; // スティックの入力閾値

    private int currentPointIndex = 0;
    private Rigidbody rb;
    private bool isGameStarted = false; // ゲームが開始されているかどうか
    private int direction = 1; // 1:時計回り, -1:反時計回り

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // 物理演算ではなく、スクリプトで位置を制御する場合
        }

        if (railPoints == null || railPoints.Length == 0)
        {
            Debug.LogError("Rail Pointsが設定されていません。");
            enabled = false;
            return;
        }

        // 初期位置を最初のポイントに設定
        transform.position = railPoints[0].position;
        transform.LookAt(railPoints[1].position); // 最初の方向を設定
    }

    void Update()
    {
        // ゲームが開始されているかチェック（myakumyaku3シーンが読み込まれているか）
        if (!isGameStarted && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "myakumyaku3")
        {
            isGameStarted = true;
        }

        if (!isGameStarted)
        {
            return;
        }

        // メタクエストの右コントローラーのスティック入力を取得
        Vector2 rightStickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
        // 矢印キーの入力を取得
        float arrowInput = 0f;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            arrowInput = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            arrowInput = -1f;
        }
        // スティックまたは矢印キーの入力で進行方向を即座に切り替え
        float horizontalInput = Mathf.Abs(rightStickInput.x) > Mathf.Abs(arrowInput) ? rightStickInput.x : arrowInput;
        if (horizontalInput > stickThreshold)
        {
            direction = 1; // 時計回り
        }
        else if (horizontalInput < -stickThreshold)
        {
            direction = -1; // 反時計回り
        }
    }

    void FixedUpdate()
    {
        if (!isGameStarted)
        {
            return;
        }

        // 次のポイントのインデックスを進行方向に応じて計算
        int nextIndex = (currentPointIndex + direction + railPoints.Length) % railPoints.Length;
        Vector3 targetPosition = railPoints[nextIndex].position;

        // 目標地点への移動
        float step = speed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        // 目標地点への回転
        Vector3 directionVec = targetPosition - transform.position;
        if (directionVec != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionVec);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        // 目標地点に到達したかどうかの判定
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPointIndex = nextIndex;
        }
    }

    // エディタでレールポイントを可視化するため
    void OnDrawGizmos()
    {
        if (railPoints == null || railPoints.Length == 0)
            return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < railPoints.Length; i++)
        {
            Gizmos.DrawSphere(railPoints[i].position, 0.5f); // ポイントを球で表示
            if (i < railPoints.Length - 1)
            {
                Gizmos.DrawLine(railPoints[i].position, railPoints[i + 1].position); // ポイント間を線で結ぶ
            }
        }
    }
}