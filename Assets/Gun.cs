using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{

    public shotmanage shotManager; 
    public GameObject bulletPrefab;
    public float shotSpeed;

    // Start is called before the first frame update
    //void Start()
    //{
        //torokko = GameObject.Find("torokko_perfect");   //敵情報を取得
        //shotmanage = torokko.GetComponent<shotmanage>();   
    //}

    public void Fire()
    {
        // 弾があるかチェック
        if (shotManager != null && shotManager.shotCount > 0)
        {
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            //bulletRb.AddForce(transform.forward * shotSpeed);

            bulletRb.velocity = transform.forward * shotSpeed;
            shotManager.currentshot();
 

            //射撃されてから3秒後に銃弾のオブジェクトを破壊する.
 
            Destroy(bullet, 3.0f);
        }
        
    }

    // 「リロード処理を実行せよ」という命令を受け取るメソッド
    public void Reload()
    {
        if (shotManager != null)
        {
            shotManager.Reload(); // shotManagerにReloadメソッドを新しく作る
        }
    }

}
