using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    //���� �̵��ϴ� �Լ��Դϴ�. �ణ�� �����̸� �ΰ� ���� ����˴ϴ�.
    public string SceneName; //�Ѿ ���� �̸��� ����Ǵ� �����Դϴ�.
    public void MoveScene()
    {
        Invoke("MoveScene_2", 0.25f);
    }
    void MoveScene_2()
    {
        SceneManager.LoadScene(SceneName);
    }
}
