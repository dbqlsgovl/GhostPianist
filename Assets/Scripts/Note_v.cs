using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_v : MonoBehaviour
{
    //리듬 게임 진행 시, 각각의 노트 오브젝트에 부착되는 스크립트입니다.
    //노트의 판정을 담당합니다.

    public int noteType = 0; //노트의 타입
    public int noteNum = 0; //노트의 고유번호. 번호가 작을수록 먼저 내려오는 노트입니다.
    public int laneNum = 0; //노트의 레인 번호입니다. 번호가 작을수록 왼쪽에 위치한 레인입니다.
    public float Y_this; //노트의 현재 Y축 좌표를 나타냅니다.
    public MGManager MGM; //MusicGame 씬에 존재하는 MGManager 객체를 나타냅니다.
    public Lane Lane; //Lane 객체를 나타냅니다.


    // Update is called once per frame
    void Update()
    {
        Y_this = this.transform.position.y; //현재 노트의 y축 좌표를 저장합니다.
        if (Y_this < 12.4f)
        {

            if ((Y_this < 12.4f && Y_this >= 12.3f) || (Y_this < 11.6f && Y_this >= 11.5f) && (MGM.Lane.LS[laneNum] == LaneState.idle))
            {
                //노트가 Bad 위치에 존재할 경우, 노트가 존재하는 레인의 상태를 bad로 변경해주고 해당 노트의 고유번호를 저장합니다.
                Lane.LS[laneNum] = LaneState.bad;
                Lane.thisNoteNum[laneNum] = noteNum;
            }
            else if ((Y_this < 12.3f && Y_this >= 12.15f) || (Y_this < 11.65f && Y_this >= 11.6f))
            {
                //노트가 Normal 위치에 존재할 경우, 노트가 존재하는 레인의 상태를 Good로 변경해주고 해당 노트의 고유번호를 저장합니다.
                Lane.LS[laneNum] = LaneState.normal;
                Lane.thisNoteNum[laneNum] = noteNum;

            }
            else if ((Y_this < 12.15f && Y_this >= 12.0f) || (Y_this < 11.75f && Y_this >= 11.65f)) // great
            {
                //노트가 Great 위치에 존재할 경우, 노트가 존재하는 레인의 상태를 Great로 변경해주고 해당 노트의 고유번호를 전달합니다.
                Lane.LS[laneNum] = LaneState.great;
                Lane.thisNoteNum[laneNum] = noteNum;

            }
            else if ((Y_this < 12.0f && Y_this >= 11.75f)) // perfect
            {
                //노트가 Perfect 위치에 존재할 경우, 노트가 존재하는 레인의 상태를 Perfect로 변경해주고 해당 노트의 고유번호를 전달합니다.
                Lane.LS[laneNum] = LaneState.perfect;
                Lane.thisNoteNum[laneNum] = noteNum;

            }
            else if (Y_this < 11.5f)
            {
                //노트가 Bad 판정보다 더 아래로 떨어졌을 때, Miss처리를 합니다.
                //Miss 처리 시, Lane의 상태와 콤보가 초기화되고 해당 노트가 비활성화되어 화면에서 사라집니다.
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
