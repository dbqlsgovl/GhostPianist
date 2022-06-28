using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPCCommunicate : MonoBehaviour
{
    //NPC와의 대화를 담당하는 클래스입니다.

    public NPC[] NPCs; //맵에 존재하는 NPC들의 배열입니다.

    public Text GuideMessage; //화면 상단의 안내 문구입니다. NPC와 대화 시작, 종료 시 내용이 변경됩니다.

    public GameObject Communicate; //화면 하단의 대화 상자입니다. NPC와 대화 시작 시 활성화됩니다.
    public Text Content; //NPC와의 대화 내용이 표시되는 Text 오브젝트입니다.

    public bool Talking; //현재 대화 중인지 여부입니다.
    public int TalkingNPC; //현재 대화 중인 NPC의 번호입니다.

    public void StartTalk(int npcNum)
    {
        //대화가 시작될 때 실행되는 함수입니다.
        GuideMessage.text = "X : 대화 종료";
        Communicate.SetActive(true);
        if (!GManager.MI[npcNum - 1].Unlocked) { //음악이 현재 해금되어있는지를 체크하고, 해금되지 않았다면 해금합니다.
            GManager.MI[npcNum - 1].Unlocked = true;
            Content.text = NPCs[npcNum - 1].con1;
        }
        else
        {
            Content.text = NPCs[npcNum - 1].con2;
        }   
    }
    public void EndTalk(int npcNum)
    {
        //대화가 종료될 때 실행되는 함수입니다.
        GuideMessage.text = "마을 주민 근처에서 V를 눌러 악보를 받고, 피아노로 연주하자!";
        Communicate.SetActive(false);
        Talking = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
