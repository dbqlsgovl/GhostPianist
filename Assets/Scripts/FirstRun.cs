using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstRun : MonoBehaviour
{
    //게임을 처음 실행했을 때의 씬인 FirstRun에서 실행되는 스크립트입니다.
    //FirstRun 씬엔 게임의 가이드 문구가 들어가있으며, 여기서 V키를 눌렀을 때 MainWorld 씬으로 넘어갑니다.
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            this.GetComponent<SceneMove>().MoveScene();
        }
    }
}
