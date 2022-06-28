using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    //씬을 이동하는 함수입니다. 약간의 딜레이를 두고 씬이 변경됩니다.
    public string SceneName; //넘어갈 씬의 이름이 저장되는 변수입니다.
    public void MoveScene()
    {
        Invoke("MoveScene_2", 0.25f);
    }
    void MoveScene_2()
    {
        SceneManager.LoadScene(SceneName);
    }
}
