using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piano : MonoBehaviour
{
    //피아노 오브젝트와의 상호작용 기능을 담당합니다.
    

    public GameObject Player;
    public Text GuideText;
    public GameObject MessageBox;
    public GameObject MessageBox_NPC;
    public GameObject SelectMusic;

    void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) <= 5) //플레이어와의 거리가 5 이하일 때 피아노와 상호작용할 수 있습니다.
        {
            MessageBox.SetActive(true);
            if(!SelectMusic.activeSelf)
            {
                MessageBox_NPC.SetActive(false);
                GuideText.text = "V를 눌러 피아노를 칠 수 있습니다.";
            }
            //GuideText.
            if (Input.GetKeyDown(KeyCode.V) && !SelectMusic.activeSelf)
            {
                //피아노 근처에서 V키를 눌렀을 때, 선곡 화면이 활성화됩니다.
                GuideText.text = "J로 이전 곡, K로 다음 곡 선택 후 V로 연주를 시작하세요. 취소 : X키";
                SelectMusic.SetActive(true);
                SelectMusic.GetComponent<SelectMusic>().MusicSelect(GManager.SelectedMusic);
            }
        }
        else
        {
            if(MessageBox.activeSelf == true) //선곡 화면이 활성화된 상태에서 플레이어와 피아노 간 거리가 멀어졌을 때, 이를 비활성화합니다.
            {
                MessageBox.SetActive(false);
                MessageBox_NPC.SetActive(true);
                GuideText.text = "";
            }
            
        }
    }
    public void DeactivateSM() //선곡 화면을 비활성화하는 함수입니다.
    {
        SelectMusic.SetActive(false);
    }
}
