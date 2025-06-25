using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // インスペクターから操作したい銃（Gun.cs）をセットする
    public Gun gun;

    // Update is called once per frame
    void Update()
    {
        // gunがセットされていない場合は、エラーを防ぐために何もしない
        if (gun == null)
        {
            Debug.LogError("Gunがセットされていません！");
            return;
        }

        // --- ここからが修正部分 ---

        // 右コントローラーの人差し指トリガーが押された瞬間、またはマウスの左クリックが押されたら発射
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            // 発射処理の実行を命令する
            gun.Fire();
        }
        // 左コントローラーのXボタンが押された瞬間、またはRキーが押されたらリロード
        // (リロードもコントローラーに対応させる場合の例)
        else if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch) || Input.GetKeyDown(KeyCode.R))
        {
            // リロード処理の実行を命令する
            gun.Reload();
        }
    }
}