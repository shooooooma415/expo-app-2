using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject bulletPrefab;
    public float shotSpeed;
    public int shotCount = 30;

    public void Fire()
    {
        // 弾があるかチェック
        if (shotCount > 0)
        {
            shotCount -= 1;
 
                GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.AddForce(transform.forward * shotSpeed);

                //射撃されてから3秒後に銃弾のオブジェクトを破壊する.
 
                Destroy(bullet, 3.0f);
        }
        
    }

    // 「リロード処理を実行せよ」という命令を受け取るメソッド
    public void Reload()
    {
        shotCount = 30;
    }

}
