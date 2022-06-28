using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceMove : MonoBehaviour
{
    //리듬 게임 도중 관객(NPC)의 움직임을 담당하는 클래스입니다.

    public Animator ANM;
    public string ControllerName;
    public GameObject Target;
    bool Moved = false;
    // Update is called once per frame
    void Update()
    {
        if(transform.position != Target.transform.position) //관중이 아직 목적지에 이동을 완료하지 않았을 때
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 0.03f);
        }
        else{ //목적지에 이동을 완료하였을 때
            if (!Moved)
            {
                ANM.SetBool("Moved", true); //Moved 변수를 true로 바꿈으로써, 해당 관객의 애니메이션을 걷는 모션에서 감상 모션으로 변경합니다.
                Moved = true;
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Target.transform.rotation, 1.0f); //관객의 방향을 세팅된 방향으로 변경합니다. 피아노의 위치를 바라보게끔 설정되었습니다.
        }
    }
}
