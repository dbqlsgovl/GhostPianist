using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPCCommunicate : MonoBehaviour
{
    //NPC���� ��ȭ�� ����ϴ� Ŭ�����Դϴ�.

    public NPC[] NPCs; //�ʿ� �����ϴ� NPC���� �迭�Դϴ�.

    public Text GuideMessage; //ȭ�� ����� �ȳ� �����Դϴ�. NPC�� ��ȭ ����, ���� �� ������ ����˴ϴ�.

    public GameObject Communicate; //ȭ�� �ϴ��� ��ȭ �����Դϴ�. NPC�� ��ȭ ���� �� Ȱ��ȭ�˴ϴ�.
    public Text Content; //NPC���� ��ȭ ������ ǥ�õǴ� Text ������Ʈ�Դϴ�.

    public bool Talking; //���� ��ȭ ������ �����Դϴ�.
    public int TalkingNPC; //���� ��ȭ ���� NPC�� ��ȣ�Դϴ�.

    public void StartTalk(int npcNum)
    {
        //��ȭ�� ���۵� �� ����Ǵ� �Լ��Դϴ�.
        GuideMessage.text = "X : ��ȭ ����";
        Communicate.SetActive(true);
        if (!GManager.MI[npcNum - 1].Unlocked) { //������ ���� �رݵǾ��ִ����� üũ�ϰ�, �رݵ��� �ʾҴٸ� �ر��մϴ�.
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
        //��ȭ�� ����� �� ����Ǵ� �Լ��Դϴ�.
        GuideMessage.text = "���� �ֹ� ��ó���� V�� ���� �Ǻ��� �ް�, �ǾƳ�� ��������!";
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
