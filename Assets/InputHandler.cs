using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // インスペクターから操作したい銃（GunController.cs）をセットする
    public Gun gun;

    // Update is called once per frame
    void Update()
    {

        // gunControllerがセットされていない場合は、エラーを防ぐために何もしない
        if (gun == null)
        {
            Debug.LogError("GunControllerがセットされていません！");
            return;
        }
        
        // 元のコードの if-else if 構造をそのまま利用
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // 発射処理の実行を命令する
            gun.Fire();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            // リロード処理の実行を命令する
            gun.Reload();
        }

    }
}
