using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    /*
     NPC 오브젝트들이 가지는 클래스입니다. NPC들과의 상호작용을 담당합니다.
    */
    public int NPCNum; //NPC의 고유 번호
    public string con1; //대화 내용 1입니다. NPC로부터 신청곡을 받기 전에 표시되는 대화 내용입니다.
    public string con2; //대화 내용 2입니다. NPC로부터 신청곡을 받고 나서 표시됩니다.


    public GameObject Pl; //Player 오브젝트입니다.
    public NPCCommunicate NPCC; //NPC와의 대화 스크립트를 관리하는 NPCCommunicate 오브젝트입니다.
    bool CanCommunicate; //상호작용 가능한지 여부가 저장됩니다. 플레이어와의 거리에 따라 결정됩니다.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Pl.transform); //NPC가 항상 플레이어 방향을 보도록 합니다.
        if (Vector3.Distance(transform.position, Pl.transform.position) <= 5) //거리가 5 이하일 시 대화할 수 있는 상태가 됩니다.
        {
            CanCommunicate = true;
        }
        else
        {
            CanCommunicate = false;
        }

        if (CanCommunicate && !NPCC.Talking)
        {
            if (Input.GetKeyDown(KeyCode.V))//거리가 5 이하이고, 이미 대화 중이 아닐 때 V를 누르면 대화가 활성화됩니다.
            {
                NPCC.TalkingNPC = NPCNum;
                NPCC.Talking = true;
                NPCC.StartTalk(NPCNum);
            }
        }

        if(NPCC.Talking && NPCC.TalkingNPC == NPCNum)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                NPCC.EndTalk(NPCNum);
            }
        }
        
            
    }


}
