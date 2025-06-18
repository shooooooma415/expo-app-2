using UnityEngine;
using System.Collections.Generic;

public class MinecartRailFollower : MonoBehaviour
{
    public Transform[] railPoints; // レールパスのポイントを格納する配列
    public float speed = 5f; // トロッコの移動速度
    public float rotationSpeed = 5f; // トロッコの回転速度

    private int currentPointIndex = 0;
    private Rigidbody rb;

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

    void FixedUpdate()
    {
        if (currentPointIndex >= railPoints.Length - 1)
        {
            // 終点に到達したら、ループするか停止するかを決定
            // ここではループする例
            currentPointIndex = 0;
            // または、このスクリプトを無効にするなど
            // enabled = false;
            // return;
        }

        Vector3 targetPosition = railPoints[currentPointIndex + 1].position;
        
        // 目標地点への移動
        float step = speed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        // 目標地点への回転
        Vector3 direction = targetPosition - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        // 目標地点に到達したかどうかの判定
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f) // ある程度の誤差を許容
        {
            currentPointIndex++;
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