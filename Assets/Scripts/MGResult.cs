using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MGResult : MonoBehaviour
{
    //음악의 결과 화면을 담당하는 클래스입니다.

    public GameObject ResultUI; //결과 화면이 표시될 UI입니다.
    public MGManager MGM;
    public Text Score;
    public Text Combo;
    public GameObject NR; //새로운 기록을 세웠을 때 표시되는 UI 오브젝트입니다.
    // Start is called before the first frame update
    void Start()
    {
        ResultUI.SetActive(true);
        Score.text = "Score : " + ((int)MGM.Score).ToString();
        Combo.text = "Combo : " + MGM.maxcombo.ToString();

        if((int)MGM.Score > GManager.MI[GManager.SelectedMusic - 1].Score) //점수가 현재 기록된 점수보다 높을 때 최고 점수를 갱신
        {
            GManager.MI[GManager.SelectedMusic - 1].Score = (int)MGM.Score;
            NR.SetActive(true);
        }
        if(MGM.maxcombo > GManager.MI[GManager.SelectedMusic - 1].MaxCombo) //콤보가 현재 기록된 콤보 수보다 높을 때 최대 콤보를 갱신
        {
            GManager.MI[GManager.SelectedMusic - 1].MaxCombo = MGM.maxcombo;
        }
        Invoke("ResultEnd", 3); //결과 화면을 3초 띄운 후 씬을 이동합니다.
        
    }

    void ResultEnd()
    {
        this.GetComponent<SceneMove>().MoveScene();
    }
}
