using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piano : MonoBehaviour
{
    //�ǾƳ� ������Ʈ���� ��ȣ�ۿ� ����� ����մϴ�.
    

    public GameObject Player;
    public Text GuideText;
    public GameObject MessageBox;
    public GameObject MessageBox_NPC;
    public GameObject SelectMusic;

    void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) <= 5) //�÷��̾���� �Ÿ��� 5 ������ �� �ǾƳ�� ��ȣ�ۿ��� �� �ֽ��ϴ�.
        {
            MessageBox.SetActive(true);
            if(!SelectMusic.activeSelf)
            {
                MessageBox_NPC.SetActive(false);
                GuideText.text = "V�� ���� �ǾƳ븦 ĥ �� �ֽ��ϴ�.";
            }
            //GuideText.
            if (Input.GetKeyDown(KeyCode.V) && !SelectMusic.activeSelf)
            {
                //�ǾƳ� ��ó���� VŰ�� ������ ��, ���� ȭ���� Ȱ��ȭ�˴ϴ�.
                GuideText.text = "J�� ���� ��, K�� ���� �� ���� �� V�� ���ָ� �����ϼ���. ��� : XŰ";
                SelectMusic.SetActive(true);
                SelectMusic.GetComponent<SelectMusic>().MusicSelect(GManager.SelectedMusic);
            }
        }
        else
        {
            if(MessageBox.activeSelf == true) //���� ȭ���� Ȱ��ȭ�� ���¿��� �÷��̾�� �ǾƳ� �� �Ÿ��� �־����� ��, �̸� ��Ȱ��ȭ�մϴ�.
            {
                MessageBox.SetActive(false);
                MessageBox_NPC.SetActive(true);
                GuideText.text = "";
            }
            
        }
    }
    public void DeactivateSM() //���� ȭ���� ��Ȱ��ȭ�ϴ� �Լ��Դϴ�.
    {
        SelectMusic.SetActive(false);
    }
}
