using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shotmanage : MonoBehaviour
{
    public Text shellLabel;
    public int shotCount = 30;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    public void currentshot()
    {
        if (shotCount > 0)
        {
            shotCount -= 1;
            UpdateUI();
        }
    }

    public void Reload()
    {
        shotCount = 30; // 弾数をリセット
        UpdateUI();     // UIを更新
    }

    // UIを更新する処理を一つのメソッドにまとめておくと便利
    void UpdateUI()
    {
        shellLabel.text = "残り弾数：" + shotCount;
    }
}
