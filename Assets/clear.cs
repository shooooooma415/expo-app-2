using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void toGameView(){
        SceneManager.LoadScene("gameStart");
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            toGameView();
        }
    }
}
