using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstRun : MonoBehaviour
{
    //������ ó�� �������� ���� ���� FirstRun���� ����Ǵ� ��ũ��Ʈ�Դϴ�.
    //FirstRun ���� ������ ���̵� ������ ��������, ���⼭ VŰ�� ������ �� MainWorld ������ �Ѿ�ϴ�.
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
