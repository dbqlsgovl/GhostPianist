using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MGResult : MonoBehaviour
{
    //������ ��� ȭ���� ����ϴ� Ŭ�����Դϴ�.

    public GameObject ResultUI; //��� ȭ���� ǥ�õ� UI�Դϴ�.
    public MGManager MGM;
    public Text Score;
    public Text Combo;
    public GameObject NR; //���ο� ����� ������ �� ǥ�õǴ� UI ������Ʈ�Դϴ�.
    // Start is called before the first frame update
    void Start()
    {
        ResultUI.SetActive(true);
        Score.text = "Score : " + ((int)MGM.Score).ToString();
        Combo.text = "Combo : " + MGM.maxcombo.ToString();

        if((int)MGM.Score > GManager.MI[GManager.SelectedMusic - 1].Score) //������ ���� ��ϵ� �������� ���� �� �ְ� ������ ����
        {
            GManager.MI[GManager.SelectedMusic - 1].Score = (int)MGM.Score;
            NR.SetActive(true);
        }
        if(MGM.maxcombo > GManager.MI[GManager.SelectedMusic - 1].MaxCombo) //�޺��� ���� ��ϵ� �޺� ������ ���� �� �ִ� �޺��� ����
        {
            GManager.MI[GManager.SelectedMusic - 1].MaxCombo = MGM.maxcombo;
        }
        Invoke("ResultEnd", 3); //��� ȭ���� 3�� ��� �� ���� �̵��մϴ�.
        
    }

    void ResultEnd()
    {
        this.GetComponent<SceneMove>().MoveScene();
    }
}
