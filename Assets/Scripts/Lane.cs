using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//키보드가 클릭되었을 때 노트의 판정을 처리하는 스크립트입니다.
//점수에 따라 관중의 위치를 변경하는 기능 또한 담당합니다.

public enum LaneState
{
    idle,
    perfect,
    great,
    normal,
    bad
}

public class Lane : MonoBehaviour
{
    //레인은 총 네 줄로 이뤄져있습니다. 가장 왼쪽 레인의 번호가 0, 가장 오른쪽 레인의 번호가 3입니다.
    //왼쪽 레인부터 각각 D, F, J, K를 눌렀을 때 처리됩니다.

    public MGManager MGM;

    public LaneState[] LS = new LaneState[4]; //각 레인의 상태를 나타내는 배열입니다.
    public int[] thisNoteNum = new int[4]; //각 레인에서 현재 판정 범위에 있는 노트의 번호를 나타냅니다.

    public GameObject[] Targets; //관중이 이동할 위치를 지정하는 역할입니다. 숫자가 작을수록 먼저 등장하는 위치입니다.
    public Text Panjung; //판정을 표시할 텍스트 오브젝트입니다.

    public GameObject[] LaneEffects = new GameObject[4]; //각 레인에서 재생될 효과들입니다.

    public Material MT_Perfect; //Perfect 판정 시의 표시될 이펙트의 마테리얼입니다.
    public Material MT_Great; //Great 판정 시의 마테리얼입니다.
    public Material MT_GBI; //Good, Bad, Idle 판정 시의 마테리얼입니다.

    public int NumOfNotes;
    // Start is called before the first frame update

    void ScoreUpdate(double p) //점수를 업데이트하고 이에 따라 관중을 이동시키는 함수입니다. p는 각 판정에 따른 계수입니다.
    {
        MGM.Score += (100000 / NumOfNotes) * p; //점수의 경우 10만점이 만점입니다. 따라서 모든 노트를 perfect로 처리했을 때, 점수가 10만점이 됩니다.
        MGM.ScoreText.text = ((int)MGM.Score).ToString();
        if(MGM.Score >= 20000 && MGM.Audiences_Chr[MGM.Audiences_Arr[1]].activeSelf == false) //점수가 2만점이 되었을 때, 두 번째 관객이 나타납니다.
        {
            MGM.Audiences_Chr[MGM.Audiences_Arr[1]].GetComponent<AudienceMove>().Target = Targets[1];
            MGM.Audiences_Chr[MGM.Audiences_Arr[1]].SetActive(true);
        }
        else if (MGM.Score >= 50000 && MGM.Audiences_Chr[MGM.Audiences_Arr[2]].activeSelf == false) //점수가 5만점이 되었을 때, 세 번째 관객이 나타납니다.
        {
            MGM.Audiences_Chr[MGM.Audiences_Arr[2]].GetComponent<AudienceMove>().Target = Targets[2];
            MGM.Audiences_Chr[MGM.Audiences_Arr[2]].SetActive(true);
        }
        else if (MGM.Score >= 70000 && MGM.Audiences_Chr[MGM.Audiences_Arr[3]].activeSelf == false) //점수가 7만점이 되었을 때, 마지막 관객이 나타납니다.
        {
            MGM.Audiences_Chr[MGM.Audiences_Arr[3]].GetComponent<AudienceMove>().Target = Targets[3];
            MGM.Audiences_Chr[MGM.Audiences_Arr[3]].SetActive(true);
        }
    }

    public void LaneButtonPressed(int ln) //D, F, J, K를 눌렀을 때 실행되는 함수입니다. 각각 0, 1, 2, 3번째 레인입니다. 이 때 노트의 위치에 따라 판정이 다르게 처리됩니다.
    {
        if (LS[ln] == LaneState.bad) //해당 레인에서의 노트가 Bad판정 위치에 존재할 때입니다. 콤보가 초기화되고 점수 계수는 0.3입니다.
        {
            MGM.Note_v[thisNoteNum[ln]].SetActive(false);
            thisNoteNum[ln] = -1;
            LS[ln] = LaneState.idle;
            MGM.combo = 0;
            MGM.Combo.text = MGM.combo.ToString();

            LaneEffects[ln].GetComponent<Renderer>().material = MT_GBI;
            LaneEffects[ln].SetActive(false);
            LaneEffects[ln].SetActive(true);

           
            ScoreUpdate(0.3);
            Panjung.text = "BAD";

        }
        else if (LS[ln] == LaneState.normal) //해당 레인에서의 노트가 Normal판정 위치에 존재할 때입니다. 콤보가 초기화되고 점수 계수는 0.5입니다.
        {
            MGM.Note_v[thisNoteNum[ln]].SetActive(false);
            thisNoteNum[ln] = -1;
            LS[ln] = LaneState.idle;
            MGM.combo = 0;
            MGM.Combo.text = MGM.combo.ToString();


            LaneEffects[ln].GetComponent<Renderer>().material = MT_GBI;
            LaneEffects[ln].SetActive(false);
            LaneEffects[ln].SetActive(true);

            ScoreUpdate(0.5);
            Panjung.text ="NORMAL";
        } 
        else if (LS[ln] == LaneState.great) //해당 레인에서의 노트가 Normal판정 위치에 존재할 때입니다. 콤보가 1 증가하고 점수 계수는 0.8입니다.
        {
            MGM.Note_v[thisNoteNum[ln]].SetActive(false);
            thisNoteNum[ln] = -1;
            LS[ln] = LaneState.idle;
            MGM.combo++;
            if(MGM.combo > MGM.maxcombo)
            {
                MGM.maxcombo = MGM.combo;
            }
            MGM.Combo.text = MGM.combo.ToString();


            LaneEffects[ln].GetComponent<Renderer>().material = MT_Great;
            LaneEffects[ln].SetActive(false);
            LaneEffects[ln].SetActive(true);
            Panjung.text = "GREAT";
            ScoreUpdate(0.8);
        }
        else if (LS[ln] == LaneState.perfect) //해당 레인에서의 노트가 Perfect 판정 위치에 존재할 때입니다. 콤보가 1 증가하고 점수 계수는 1입니다.
        {
            MGM.Note_v[thisNoteNum[ln]].SetActive(false);
            thisNoteNum[ln] = -1;
            LS[ln] = LaneState.idle;
            MGM.combo++;
            if (MGM.combo > MGM.maxcombo)
            {
                MGM.maxcombo = MGM.combo;
            }
            MGM.Combo.text = MGM.combo.ToString();

            LaneEffects[ln].GetComponent<Renderer>().material = MT_Perfect;
            LaneEffects[ln].SetActive(false);
            LaneEffects[ln].SetActive(true);

            ScoreUpdate(1.0);
            Panjung.text = "PERFECT";
        }
        else //노트가 판정 범위 내에 존재하지 않을 때입니다. 그냥 레인을 클릭하였을 때의 이펙트만 재생됩니다.
        {
            LaneEffects[ln].GetComponent<Renderer>().material = MT_GBI;
            LaneEffects[ln].SetActive(false);
            LaneEffects[ln].SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            LaneButtonPressed(0); //D를 누르면 0번 레인이 활성화됩니다.
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            LaneButtonPressed(1); //F를 누르면 1번 레인이 활성화됩니다.
        }
        if (Input.GetKeyDown(KeyCode.J)){
            LaneButtonPressed(2); //J를 누르면 2번 레인이 활성화됩니다.
        }
        if (Input.GetKeyDown(KeyCode.K)){
            LaneButtonPressed(3); //K를 누르면 3번 레인이 활성화됩니다.
        }
    }
}
