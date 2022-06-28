using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MGInit : MonoBehaviour
{
    // MusicGame 씬이 처음 시작되었을 때 실행되는 내용이 담긴 클래스입니다.

    public int musicNumber; //선곡된 음악의 번호입니다.
    public GameObject MGM;
    public GameObject BeforeStart; //음악이 시작되기 전 화면에 표시되는 UI 오브젝트입니다.
    public Text CountDown;
    void Start()
    {
        //Invoke로 카운트다운 기능을 구현하였습니다.
        Invoke("CountDown2", 1);
        Invoke("CountDown1", 2);
        Invoke("CountDown0", 3);
        //카운트다운이 종료 후 BeforeStart를 제거합니다.
        Invoke("RemoveBeforeStart", 4);
        //MGM 오브젝트를 활성화합니다.
        Invoke("ActivateMGM", 5);
    }

    void CountDown2()
    {
        CountDown.text = "2";
    }
    void CountDown1()
    {
        CountDown.text = "1";
    }

    void CountDown0()
    {
        CountDown.text = "GO!";
    }
    void RemoveBeforeStart()
    {
       BeforeStart.SetActive(false);
    }

    void ActivateMGM()
    {
        MGM.SetActive(true);
    }

    private void Update()
    {
        //MusicGame 씬 안에서 ESC키를 누를 때, MainWorld로 되돌아갑니다.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.GetComponent<SceneMove>().MoveScene();
        }
    }
    // Update is called once per frame
}
