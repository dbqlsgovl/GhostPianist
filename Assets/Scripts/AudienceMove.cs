using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceMove : MonoBehaviour
{
    //���� ���� ���� ����(NPC)�� �������� ����ϴ� Ŭ�����Դϴ�.

    public Animator ANM;
    public string ControllerName;
    public GameObject Target;
    bool Moved = false;
    // Update is called once per frame
    void Update()
    {
        if(transform.position != Target.transform.position) //������ ���� �������� �̵��� �Ϸ����� �ʾ��� ��
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 0.03f);
        }
        else{ //�������� �̵��� �Ϸ��Ͽ��� ��
            if (!Moved)
            {
                ANM.SetBool("Moved", true); //Moved ������ true�� �ٲ����ν�, �ش� ������ �ִϸ��̼��� �ȴ� ��ǿ��� ���� ������� �����մϴ�.
                Moved = true;
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Target.transform.rotation, 1.0f); //������ ������ ���õ� �������� �����մϴ�. �ǾƳ��� ��ġ�� �ٶ󺸰Բ� �����Ǿ����ϴ�.
        }
    }
}
