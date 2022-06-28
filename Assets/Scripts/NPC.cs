using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    /*
     NPC ������Ʈ���� ������ Ŭ�����Դϴ�. NPC����� ��ȣ�ۿ��� ����մϴ�.
    */
    public int NPCNum; //NPC�� ���� ��ȣ
    public string con1; //��ȭ ���� 1�Դϴ�. NPC�κ��� ��û���� �ޱ� ���� ǥ�õǴ� ��ȭ �����Դϴ�.
    public string con2; //��ȭ ���� 2�Դϴ�. NPC�κ��� ��û���� �ް� ���� ǥ�õ˴ϴ�.


    public GameObject Pl; //Player ������Ʈ�Դϴ�.
    public NPCCommunicate NPCC; //NPC���� ��ȭ ��ũ��Ʈ�� �����ϴ� NPCCommunicate ������Ʈ�Դϴ�.
    bool CanCommunicate; //��ȣ�ۿ� �������� ���ΰ� ����˴ϴ�. �÷��̾���� �Ÿ��� ���� �����˴ϴ�.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Pl.transform); //NPC�� �׻� �÷��̾� ������ ������ �մϴ�.
        if (Vector3.Distance(transform.position, Pl.transform.position) <= 5) //�Ÿ��� 5 ������ �� ��ȭ�� �� �ִ� ���°� �˴ϴ�.
        {
            CanCommunicate = true;
        }
        else
        {
            CanCommunicate = false;
        }

        if (CanCommunicate && !NPCC.Talking)
        {
            if (Input.GetKeyDown(KeyCode.V))//�Ÿ��� 5 �����̰�, �̹� ��ȭ ���� �ƴ� �� V�� ������ ��ȭ�� Ȱ��ȭ�˴ϴ�.
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
