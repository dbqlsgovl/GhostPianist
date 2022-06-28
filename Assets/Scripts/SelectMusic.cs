using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMusic : MonoBehaviour
{
    //������ ���� ����� ����մϴ�.

    public GameObject Piano;
    public GameObject Player;
    public Image CoverImage; //������ Ŀ�� �̹����Դϴ�.
    public Text MusicName;
    public Text Score;
    public Text MaxCombo;
    public Text Level;
    public GameObject Locked; //���� NPC�κ��� ������ ���� ������ ��, ������ ��Ȱ��ȭ�Ǿ����� ǥ���ϴ� UI ������Ʈ�Դϴ�.

    // Start is called before the first frame update
    public void MusicSelect(int MN)
    {
        GManager.SelectedMusic = MN; //GManager�� SelectedMusic�� ���� ���� ���� ���� ��ȣ�� �����մϴ�.
        if (GManager.MI[MN - 1].Unlocked == true)//���� ���� ��
        {
            CoverImage.color = new Color32(255, 255, 255, 255);
            Locked.SetActive(false);
        }
        else //���� �̺���
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
        if (Input.GetKeyDown(KeyCode.J)) //���� ���� ����
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
        if (Input.GetKeyDown(KeyCode.K)) //���� ���� ����
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
        if (Input.GetKeyDown(KeyCode.V)) //���� ���� �Ϸ�. MusicGame ������ �̵��Ǹ�, �� �� ���� �������� ���� ������ GManager�� �Բ� ���� �Ѿ�ϴ�.
        {
            if(GManager.MI[GManager.SelectedMusic - 1].Unlocked == true)
            {
                this.GetComponent<SceneMove>().MoveScene();
            }
        }
        if(Vector3.Distance(Piano.transform.position, Player.transform.position) > 5 || Input.GetKeyDown(KeyCode.X))
        {
            //�ǾƳ���� �Ÿ��� �־����� ��, SelectedMusic ������Ʈ�� ��Ȱ��ȭ�մϴ�.
            Piano.GetComponent<Piano>().DeactivateSM();
        }
    }
}
