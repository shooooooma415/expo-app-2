using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void toGameView(){
        SceneManager.LoadScene("myakumyaku3");
    }

    // Update is called once per frame
    void Update()
    {
        // メタクエストの右コントローラーのAボタンが押されたかチェック
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            toGameView();
        }
    }
}
