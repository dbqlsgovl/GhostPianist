using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using TMPro;
using UnityEngine.UI;

//MusicGame 씬 전반을 관리하는 스크립트입니다.
//음악 정보 읽어오기, 노트 생성, 노트 이동, 점수, 콤보 갱신의 기능을 담당합니다.

public class Note //각 노트의 정보를 담은 클래스입니다.
{
    public int lane; //노트가 속한 레인의 번호입니다.
    public int type; //노트의 타입입니다. type=1이면 기본 노트입니다. 후에 키보드를 누르고 있어야 처리되는 롱 노트를 구현할 예정입니다.
    public float beat; //노트가 내려올 박자입니다.
    public Note(int type, int lane, float beat)
    {
        this.lane = lane;
        this.type = type;
        this.beat = beat;
    }

}

public class MGManager : MonoBehaviour
{
    //UI에 표시될 오브젝트들입니다.
    public Text Combo;
    public Text LifeText;
    public Text Panjung;
    public Text ScoreText;

    public GameObject Screen;
    public GameObject Screen_v;
    
    public GameObject notePrefab;
    public GameObject[] Note_v; //실제로 게임상에서 보일 노트 오브젝트의 배열입니다.
    public GameObject[] Audiences_Chr;

    public GameObject MGR; //리듬 게임 종료 후 결과를 표시할 오브젝트입니다.

    public Note[] Notes; //노트들의 정보를 담은 배열입니다.
    JsonData NoteData;
    string NoteString;
    float lanescale = 0.4f;

    public int[] Audiences_Arr; //리듬 게임 도중 등장할 관중들의 순서를 나타내는 배열입니다.
    
    float bpm;
    float songLength;
    float noteSpeed;
    
    public int musicNumber;
    public int combo; //현재 콤보 수입니다.
    public int maxcombo = 0; //최대 콤보입니다.
    public Lane Lane;
    public double Score = 0;


    public float[] sync; //노트가 내려오는 타이밍을 맞추기 위한 float 배열입니다.
    
    float time;//음악이 종료되었는지를 판별하기 위해 경과 시간을 저장하는 변수입니다.
    


    // Start is called before the first frame update
    void Start()
    {
        combo = 0;
        Score = 0;
        noteSpeed = 2.5f;
        
        musicNumber = GManager.SelectedMusic; //GManager로부터 현재 선곡된 음악 번호를 불러옵니다.

        //음악 정보가 저장된 Json파일을 파싱해서 변수들에 음악의 정보를 저장합니다.
        TextAsset NoteAsset = Resources.Load("SongJsons/Song" + musicNumber.ToString()) as TextAsset;
        NoteString = NoteAsset.text;
        NoteData = JsonMapper.ToObject(NoteString);
        
        bpm = float.Parse(NoteData[0]["bpm"].ToString());
        songLength = float.Parse(NoteData[0]["length"].ToString());
        for (int i = 0; i < 4; i++)
        {
            //음악마다 관중이 나타날 순서가 다릅니다. 해당 순서를 Audiences_Arr 배열에 저장합니다. 
            Audiences_Arr[i] = (int)NoteData[0]["Audiences"][i];
        }

        //노트의 정보들을 Notes에 저장합니다.
        Notes = new Note[NoteData.Count];
        for (int i = 1; i < NoteData.Count; i++)
        {
            Notes[i] = new Note(int.Parse(NoteData[i]["type"].ToString()), int.Parse(NoteData[i]["lane"].ToString()), float.Parse(NoteData[i]["beat"].ToString()));
        }

        //실제 노트를 생성합니다.
        Note_v = new GameObject[NoteData.Count];
        Lane.NumOfNotes = NoteData.Count - 1;
       
        SetScreen(); //노트가 나타날 스크린을 세팅합니다.
        SetNotePos(); //스크린에서 노트가 나타날 위치를 지정합니다.

        Audiences_Chr[Audiences_Arr[0]].GetComponent<AudienceMove>().Target = Lane.Targets[0]; //첫 번째 관중이 이동할 위치를 지정합니다.
        Audiences_Chr[Audiences_Arr[0]].SetActive(true); //첫 번째 관중은 음악 시작과 동시에 나타납니다.
        this.GetComponent<AudioSource>().clip = Resources.Load("Audio/Music/Music" + musicNumber.ToString(), typeof(AudioClip)) as AudioClip; //음악을 불러옵니다.
        this.GetComponent<AudioSource>().Play(); //음악을 재생합니다.
        
    }

    void SetNotePos() //스크린 상에 나타날 노트의 위치를 지정하는 함수입니다.
    {
        for (int i = 1; i < NoteData.Count; i++)
        {
            if (Notes[i].type == 1) Note_v[i] = Instantiate(notePrefab, transform.position, transform.rotation) as GameObject; //노트의 프리팹을 불러옵니다.
            
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
    void SetScreen() //스크린의 크기와 위치를 지정합니다.
    {
        Screen.transform.localScale = new Vector3(Screen.transform.localScale.x, noteSpeed * songLength, Screen.transform.localScale.z);
        Screen.transform.position = new Vector3(Screen.transform.position.x, noteSpeed * songLength / 2 + 11.834f, Screen.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //시간에 따라 스크린이 아래로 이동합니다. 이에 따라 노트들도 다같이 아래로 이동합니다.
        Screen.transform.Translate(Vector3.down * noteSpeed * Time.deltaTime);
        time += Time.deltaTime; //경과 시간을 저장합니다.
        if(time > songLength + 3) //리듬 게임이 시작한지 지정된 곡의 길이보다 3초 더 지났을 때, 게임의 결과를 화면에 표시합니다.
        {
            MGR.SetActive(true);
        }
    }
}
