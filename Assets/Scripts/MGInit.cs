using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MGInit : MonoBehaviour
{
    // MusicGame ���� ó�� ���۵Ǿ��� �� ����Ǵ� ������ ��� Ŭ�����Դϴ�.

    public int musicNumber; //����� ������ ��ȣ�Դϴ�.
    public GameObject MGM;
    public GameObject BeforeStart; //������ ���۵Ǳ� �� ȭ�鿡 ǥ�õǴ� UI ������Ʈ�Դϴ�.
    public Text CountDown;
    void Start()
    {
        //Invoke�� ī��Ʈ�ٿ� ����� �����Ͽ����ϴ�.
        Invoke("CountDown2", 1);
        Invoke("CountDown1", 2);
        Invoke("CountDown0", 3);
        //ī��Ʈ�ٿ��� ���� �� BeforeStart�� �����մϴ�.
        Invoke("RemoveBeforeStart", 4);
        //MGM ������Ʈ�� Ȱ��ȭ�մϴ�.
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
        //MusicGame �� �ȿ��� ESCŰ�� ���� ��, MainWorld�� �ǵ��ư��ϴ�.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.GetComponent<SceneMove>().MoveScene();
        }
    }
    // Update is called once per frame
}
