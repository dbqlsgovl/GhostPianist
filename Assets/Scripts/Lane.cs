using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Ű���尡 Ŭ���Ǿ��� �� ��Ʈ�� ������ ó���ϴ� ��ũ��Ʈ�Դϴ�.
//������ ���� ������ ��ġ�� �����ϴ� ��� ���� ����մϴ�.

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
    //������ �� �� �ٷ� �̷����ֽ��ϴ�. ���� ���� ������ ��ȣ�� 0, ���� ������ ������ ��ȣ�� 3�Դϴ�.
    //���� ���κ��� ���� D, F, J, K�� ������ �� ó���˴ϴ�.

    public MGManager MGM;

    public LaneState[] LS = new LaneState[4]; //�� ������ ���¸� ��Ÿ���� �迭�Դϴ�.
    public int[] thisNoteNum = new int[4]; //�� ���ο��� ���� ���� ������ �ִ� ��Ʈ�� ��ȣ�� ��Ÿ���ϴ�.

    public GameObject[] Targets; //������ �̵��� ��ġ�� �����ϴ� �����Դϴ�. ���ڰ� �������� ���� �����ϴ� ��ġ�Դϴ�.
    public Text Panjung; //������ ǥ���� �ؽ�Ʈ ������Ʈ�Դϴ�.

    public GameObject[] LaneEffects = new GameObject[4]; //�� ���ο��� ����� ȿ�����Դϴ�.

    public Material MT_Perfect; //Perfect ���� ���� ǥ�õ� ����Ʈ�� ���׸����Դϴ�.
    public Material MT_Great; //Great ���� ���� ���׸����Դϴ�.
    public Material MT_GBI; //Good, Bad, Idle ���� ���� ���׸����Դϴ�.

    public int NumOfNotes;
    // Start is called before the first frame update

    void ScoreUpdate(double p) //������ ������Ʈ�ϰ� �̿� ���� ������ �̵���Ű�� �Լ��Դϴ�. p�� �� ������ ���� ����Դϴ�.
    {
        MGM.Score += (100000 / NumOfNotes) * p; //������ ��� 10������ �����Դϴ�. ���� ��� ��Ʈ�� perfect�� ó������ ��, ������ 10������ �˴ϴ�.
        MGM.ScoreText.text = ((int)MGM.Score).ToString();
        if(MGM.Score >= 20000 && MGM.Audiences_Chr[MGM.Audiences_Arr[1]].activeSelf == false) //������ 2������ �Ǿ��� ��, �� ��° ������ ��Ÿ���ϴ�.
        {
            MGM.Audiences_Chr[MGM.Audiences_Arr[1]].GetComponent<AudienceMove>().Target = Targets[1];
            MGM.Audiences_Chr[MGM.Audiences_Arr[1]].SetActive(true);
        }
        else if (MGM.Score >= 50000 && MGM.Audiences_Chr[MGM.Audiences_Arr[2]].activeSelf == false) //������ 5������ �Ǿ��� ��, �� ��° ������ ��Ÿ���ϴ�.
        {
            MGM.Audiences_Chr[MGM.Audiences_Arr[2]].GetComponent<AudienceMove>().Target = Targets[2];
            MGM.Audiences_Chr[MGM.Audiences_Arr[2]].SetActive(true);
        }
        else if (MGM.Score >= 70000 && MGM.Audiences_Chr[MGM.Audiences_Arr[3]].activeSelf == false) //������ 7������ �Ǿ��� ��, ������ ������ ��Ÿ���ϴ�.
        {
            MGM.Audiences_Chr[MGM.Audiences_Arr[3]].GetComponent<AudienceMove>().Target = Targets[3];
            MGM.Audiences_Chr[MGM.Audiences_Arr[3]].SetActive(true);
        }
    }

    public void LaneButtonPressed(int ln) //D, F, J, K�� ������ �� ����Ǵ� �Լ��Դϴ�. ���� 0, 1, 2, 3��° �����Դϴ�. �� �� ��Ʈ�� ��ġ�� ���� ������ �ٸ��� ó���˴ϴ�.
    {
        if (LS[ln] == LaneState.bad) //�ش� ���ο����� ��Ʈ�� Bad���� ��ġ�� ������ ���Դϴ�. �޺��� �ʱ�ȭ�ǰ� ���� ����� 0.3�Դϴ�.
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
        else if (LS[ln] == LaneState.normal) //�ش� ���ο����� ��Ʈ�� Normal���� ��ġ�� ������ ���Դϴ�. �޺��� �ʱ�ȭ�ǰ� ���� ����� 0.5�Դϴ�.
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
        else if (LS[ln] == LaneState.great) //�ش� ���ο����� ��Ʈ�� Normal���� ��ġ�� ������ ���Դϴ�. �޺��� 1 �����ϰ� ���� ����� 0.8�Դϴ�.
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
        else if (LS[ln] == LaneState.perfect) //�ش� ���ο����� ��Ʈ�� Perfect ���� ��ġ�� ������ ���Դϴ�. �޺��� 1 �����ϰ� ���� ����� 1�Դϴ�.
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
        else //��Ʈ�� ���� ���� ���� �������� ���� ���Դϴ�. �׳� ������ Ŭ���Ͽ��� ���� ����Ʈ�� ����˴ϴ�.
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
            LaneButtonPressed(0); //D�� ������ 0�� ������ Ȱ��ȭ�˴ϴ�.
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            LaneButtonPressed(1); //F�� ������ 1�� ������ Ȱ��ȭ�˴ϴ�.
        }
        if (Input.GetKeyDown(KeyCode.J)){
            LaneButtonPressed(2); //J�� ������ 2�� ������ Ȱ��ȭ�˴ϴ�.
        }
        if (Input.GetKeyDown(KeyCode.K)){
            LaneButtonPressed(3); //K�� ������ 3�� ������ Ȱ��ȭ�˴ϴ�.
        }
    }
}
