using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using TMPro;
using UnityEngine.UI;

//MusicGame �� ������ �����ϴ� ��ũ��Ʈ�Դϴ�.
//���� ���� �о����, ��Ʈ ����, ��Ʈ �̵�, ����, �޺� ������ ����� ����մϴ�.

public class Note //�� ��Ʈ�� ������ ���� Ŭ�����Դϴ�.
{
    public int lane; //��Ʈ�� ���� ������ ��ȣ�Դϴ�.
    public int type; //��Ʈ�� Ÿ���Դϴ�. type=1�̸� �⺻ ��Ʈ�Դϴ�. �Ŀ� Ű���带 ������ �־�� ó���Ǵ� �� ��Ʈ�� ������ �����Դϴ�.
    public float beat; //��Ʈ�� ������ �����Դϴ�.
    public Note(int type, int lane, float beat)
    {
        this.lane = lane;
        this.type = type;
        this.beat = beat;
    }

}

public class MGManager : MonoBehaviour
{
    //UI�� ǥ�õ� ������Ʈ���Դϴ�.
    public Text Combo;
    public Text LifeText;
    public Text Panjung;
    public Text ScoreText;

    public GameObject Screen;
    public GameObject Screen_v;
    
    public GameObject notePrefab;
    public GameObject[] Note_v; //������ ���ӻ󿡼� ���� ��Ʈ ������Ʈ�� �迭�Դϴ�.
    public GameObject[] Audiences_Chr;

    public GameObject MGR; //���� ���� ���� �� ����� ǥ���� ������Ʈ�Դϴ�.

    public Note[] Notes; //��Ʈ���� ������ ���� �迭�Դϴ�.
    JsonData NoteData;
    string NoteString;
    float lanescale = 0.4f;

    public int[] Audiences_Arr; //���� ���� ���� ������ ���ߵ��� ������ ��Ÿ���� �迭�Դϴ�.
    
    float bpm;
    float songLength;
    float noteSpeed;
    
    public int musicNumber;
    public int combo; //���� �޺� ���Դϴ�.
    public int maxcombo = 0; //�ִ� �޺��Դϴ�.
    public Lane Lane;
    public double Score = 0;


    public float[] sync; //��Ʈ�� �������� Ÿ�̹��� ���߱� ���� float �迭�Դϴ�.
    
    float time;//������ ����Ǿ������� �Ǻ��ϱ� ���� ��� �ð��� �����ϴ� �����Դϴ�.
    


    // Start is called before the first frame update
    void Start()
    {
        combo = 0;
        Score = 0;
        noteSpeed = 2.5f;
        
        musicNumber = GManager.SelectedMusic; //GManager�κ��� ���� ����� ���� ��ȣ�� �ҷ��ɴϴ�.

        //���� ������ ����� Json������ �Ľ��ؼ� �����鿡 ������ ������ �����մϴ�.
        TextAsset NoteAsset = Resources.Load("SongJsons/Song" + musicNumber.ToString()) as TextAsset;
        NoteString = NoteAsset.text;
        NoteData = JsonMapper.ToObject(NoteString);
        
        bpm = float.Parse(NoteData[0]["bpm"].ToString());
        songLength = float.Parse(NoteData[0]["length"].ToString());
        for (int i = 0; i < 4; i++)
        {
            //���Ǹ��� ������ ��Ÿ�� ������ �ٸ��ϴ�. �ش� ������ Audiences_Arr �迭�� �����մϴ�. 
            Audiences_Arr[i] = (int)NoteData[0]["Audiences"][i];
        }

        //��Ʈ�� �������� Notes�� �����մϴ�.
        Notes = new Note[NoteData.Count];
        for (int i = 1; i < NoteData.Count; i++)
        {
            Notes[i] = new Note(int.Parse(NoteData[i]["type"].ToString()), int.Parse(NoteData[i]["lane"].ToString()), float.Parse(NoteData[i]["beat"].ToString()));
        }

        //���� ��Ʈ�� �����մϴ�.
        Note_v = new GameObject[NoteData.Count];
        Lane.NumOfNotes = NoteData.Count - 1;
       
        SetScreen(); //��Ʈ�� ��Ÿ�� ��ũ���� �����մϴ�.
        SetNotePos(); //��ũ������ ��Ʈ�� ��Ÿ�� ��ġ�� �����մϴ�.

        Audiences_Chr[Audiences_Arr[0]].GetComponent<AudienceMove>().Target = Lane.Targets[0]; //ù ��° ������ �̵��� ��ġ�� �����մϴ�.
        Audiences_Chr[Audiences_Arr[0]].SetActive(true); //ù ��° ������ ���� ���۰� ���ÿ� ��Ÿ���ϴ�.
        this.GetComponent<AudioSource>().clip = Resources.Load("Audio/Music/Music" + musicNumber.ToString(), typeof(AudioClip)) as AudioClip; //������ �ҷ��ɴϴ�.
        this.GetComponent<AudioSource>().Play(); //������ ����մϴ�.
        
    }

    void SetNotePos() //��ũ�� �� ��Ÿ�� ��Ʈ�� ��ġ�� �����ϴ� �Լ��Դϴ�.
    {
        for (int i = 1; i < NoteData.Count; i++)
        {
            if (Notes[i].type == 1) Note_v[i] = Instantiate(notePrefab, transform.position, transform.rotation) as GameObject; //��Ʈ�� �������� �ҷ��ɴϴ�.
            
            Note_v[i].transform.position = new Vector3(9.0f, Note_v[i].transform.position.y, 11.0f);
            Note_v[i].transform.SetParent(Screen.transform, false);
            
            Note_v[i].transform.localPosition = new Vector3((Notes[i].lane - 1.5f) * lanescale, Note_v[i].transform.localPosition.y, 0.015f);
            Note_v[i].transform.position = new Vector3(Note_v[i].transform.position.x, Screen.transform.localScale.y / songLength * (60 / bpm) * Notes[i].beat + 11.834f + sync[musicNumber - 1], Note_v[i].transform.position.z);
            
            Note_v[i].transform.localScale = new Vector3(0.011f, 0.01f / Screen.transform.localScale.y, 1.0f);
            
            Note_v[i].GetComponent<Note_v>().noteNum = i;
            Note_v[i].GetComponent<Note_v>().noteType = Notes[i].type;
            Note_v[i].GetComponent<Note_v>().laneNum = Notes[i].lane;
            Note_v[i].GetComponent<Note_v>().MGM = this;
            Note_v[i].GetComponent<Note_v>().Lane = Lane;
        }
    }
    void SetScreen() //��ũ���� ũ��� ��ġ�� �����մϴ�.
    {
        Screen.transform.localScale = new Vector3(Screen.transform.localScale.x, noteSpeed * songLength, Screen.transform.localScale.z);
        Screen.transform.position = new Vector3(Screen.transform.position.x, noteSpeed * songLength / 2 + 11.834f, Screen.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //�ð��� ���� ��ũ���� �Ʒ��� �̵��մϴ�. �̿� ���� ��Ʈ�鵵 �ٰ��� �Ʒ��� �̵��մϴ�.
        Screen.transform.Translate(Vector3.down * noteSpeed * Time.deltaTime);
        time += Time.deltaTime; //��� �ð��� �����մϴ�.
        if(time > songLength + 3) //���� ������ �������� ������ ���� ���̺��� 3�� �� ������ ��, ������ ����� ȭ�鿡 ǥ���մϴ�.
        {
            MGR.SetActive(true);
        }
    }
}
