using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_v : MonoBehaviour
{
    //���� ���� ���� ��, ������ ��Ʈ ������Ʈ�� �����Ǵ� ��ũ��Ʈ�Դϴ�.
    //��Ʈ�� ������ ����մϴ�.

    public int noteType = 0; //��Ʈ�� Ÿ��
    public int noteNum = 0; //��Ʈ�� ������ȣ. ��ȣ�� �������� ���� �������� ��Ʈ�Դϴ�.
    public int laneNum = 0; //��Ʈ�� ���� ��ȣ�Դϴ�. ��ȣ�� �������� ���ʿ� ��ġ�� �����Դϴ�.
    public float Y_this; //��Ʈ�� ���� Y�� ��ǥ�� ��Ÿ���ϴ�.
    public MGManager MGM; //MusicGame ���� �����ϴ� MGManager ��ü�� ��Ÿ���ϴ�.
    public Lane Lane; //Lane ��ü�� ��Ÿ���ϴ�.


    // Update is called once per frame
    void Update()
    {
        Y_this = this.transform.position.y; //���� ��Ʈ�� y�� ��ǥ�� �����մϴ�.
        if (Y_this < 12.4f)
        {

            if ((Y_this < 12.4f && Y_this >= 12.3f) || (Y_this < 11.6f && Y_this >= 11.5f) && (MGM.Lane.LS[laneNum] == LaneState.idle))
            {
                //��Ʈ�� Bad ��ġ�� ������ ���, ��Ʈ�� �����ϴ� ������ ���¸� bad�� �������ְ� �ش� ��Ʈ�� ������ȣ�� �����մϴ�.
                Lane.LS[laneNum] = LaneState.bad;
                Lane.thisNoteNum[laneNum] = noteNum;
            }
            else if ((Y_this < 12.3f && Y_this >= 12.15f) || (Y_this < 11.65f && Y_this >= 11.6f))
            {
                //��Ʈ�� Normal ��ġ�� ������ ���, ��Ʈ�� �����ϴ� ������ ���¸� Good�� �������ְ� �ش� ��Ʈ�� ������ȣ�� �����մϴ�.
                Lane.LS[laneNum] = LaneState.normal;
                Lane.thisNoteNum[laneNum] = noteNum;

            }
            else if ((Y_this < 12.15f && Y_this >= 12.0f) || (Y_this < 11.75f && Y_this >= 11.65f)) // great
            {
                //��Ʈ�� Great ��ġ�� ������ ���, ��Ʈ�� �����ϴ� ������ ���¸� Great�� �������ְ� �ش� ��Ʈ�� ������ȣ�� �����մϴ�.
                Lane.LS[laneNum] = LaneState.great;
                Lane.thisNoteNum[laneNum] = noteNum;

            }
            else if ((Y_this < 12.0f && Y_this >= 11.75f)) // perfect
            {
                //��Ʈ�� Perfect ��ġ�� ������ ���, ��Ʈ�� �����ϴ� ������ ���¸� Perfect�� �������ְ� �ش� ��Ʈ�� ������ȣ�� �����մϴ�.
                Lane.LS[laneNum] = LaneState.perfect;
                Lane.thisNoteNum[laneNum] = noteNum;

            }
            else if (Y_this < 11.5f)
            {
                //��Ʈ�� Bad �������� �� �Ʒ��� �������� ��, Missó���� �մϴ�.
                //Miss ó�� ��, Lane�� ���¿� �޺��� �ʱ�ȭ�ǰ� �ش� ��Ʈ�� ��Ȱ��ȭ�Ǿ� ȭ�鿡�� ������ϴ�.
                Lane.LS[laneNum] = LaneState.idle;
                Lane.thisNoteNum[laneNum] = -1;
                MGM.combo = 0;
                MGM.Combo.text = MGM.combo.ToString();
                MGM.Note_v[noteNum].SetActive(false);
                MGM.Panjung.text = "MISS";
            }


        }

    }
}
