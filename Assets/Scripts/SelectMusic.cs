using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMusic : MonoBehaviour
{
    //음악의 선곡 기능을 담당합니다.

    public GameObject Piano;
    public GameObject Player;
    public Image CoverImage; //음악의 커버 이미지입니다.
    public Text MusicName;
    public Text Score;
    public Text MaxCombo;
    public Text Level;
    public GameObject Locked; //아직 NPC로부터 음악을 얻지 못했을 때, 음악이 비활성화되었음을 표시하는 UI 오브젝트입니다.

    // Start is called before the first frame update
    public void MusicSelect(int MN)
    {
        GManager.SelectedMusic = MN; //GManager의 SelectedMusic을 현재 선곡 중인 음악 번호로 변경합니다.
        if (GManager.MI[MN - 1].Unlocked == true)//음악 보유 시
        {
            CoverImage.color = new Color32(255, 255, 255, 255);
            Locked.SetActive(false);
        }
        else //음악 미보유
        {
            CoverImage.color = new Color32(72, 72, 72, 255);
            Locked.SetActive(true);
        }
        CoverImage.sprite = Resources.Load("Images/Cover/Cover" + MN.ToString(), typeof(Sprite)) as Sprite;
        MusicName.text = GManager.MI[MN - 1].Name;
        MaxCombo.text = "Max Combo : " + GManager.MI[MN - 1].MaxCombo.ToString();
        Score.text = "Score : " + GManager.MI[MN - 1].Score.ToString();
        Level.text = "Level" + GManager.MI[MN - 1].Level.ToString();
        this.GetComponent<AudioSource>().clip = Resources.Load("Audio/Music/Music" + MN.ToString(), typeof(AudioClip)) as AudioClip;
        this.GetComponent<AudioSource>().Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) //이전 음악 선택
        {
            if(GManager.SelectedMusic == 1)
            {
                MusicSelect(4);
            }
            else
            {
                MusicSelect(GManager.SelectedMusic - 1);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.K)) //다음 음악 선택
        {
            if (GManager.SelectedMusic == 4)
            {
                MusicSelect(1);
            }
            else
            {
                MusicSelect(GManager.SelectedMusic + 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.V)) //음악 선곡 완료. MusicGame 씬으로 이동되며, 이 때 현재 선곡중인 음악 정보도 GManager와 함께 같이 넘어갑니다.
        {
            if(GManager.MI[GManager.SelectedMusic - 1].Unlocked == true)
            {
                this.GetComponent<SceneMove>().MoveScene();
            }
        }
        if(Vector3.Distance(Piano.transform.position, Player.transform.position) > 5 || Input.GetKeyDown(KeyCode.X))
        {
            //피아노와의 거리가 멀어졌을 때, SelectedMusic 오브젝트를 비활성화합니다.
            Piano.GetComponent<Piano>().DeactivateSM();
        }
    }
}
