using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
게임의 전반을 관리하는 스크립트입니다. MainWorld 씬에서 객체가 처음 생성되며, GameManager 오브젝트에 부착됩니다.

DontDestroyOnLoad로 인해 다음 씬인 MusicGame으로 넘어가도 삭제되지 않고, 게임 실행 내내 유지됩니다.

게임에 사용되는 음악들의 정보들이 저장되며, MusicGame(리듬 게임 씬) 씬으로 넘어갈 때를 위하여 현재 선곡된 곡의 정보도 저장합니다.
*/

public class MusicInfo //음악의 정보가 저장되는 클래스입니다.
{
    public string Name; //음악 이름
    public string Owner; //음악 작곡가
    public int Number; //음악 고유 번호
    public int Level; //음악 난이도
    public bool Unlocked; //음악 해금 여부
    public int Score; //플레이어의 해당 음악 점수 기록
    public int MaxCombo; //플레이어의 해당 음악 콤보 기록

    public MusicInfo(string name, string owner, int number, int level, bool unlocked, int score, int MaxCombo) //MusicInfo의 생성자입니다.
    {
        this.Name = name;
        this.Owner = owner;
        this.Number = number;
        this.Level = level;
        this.Unlocked = unlocked;
        this.Score = score;
        this.MaxCombo = MaxCombo;
    }
}


public class GManager : MonoBehaviour
{
    public static MusicInfo[] MI; //MusicInfo의 배열입니다. 다른 스크립트에서도 참조하기 때문에 static으로 선언하였습니다.
    public static int SelectedMusic = 1; //현재 선곡된 음악의 번호입니다. 첫 시행 시 1로 지정됩니다.
    public static GManager Instance; //GManager 인스턴스가 생성되었는지를 확인하기 위한 변수입니다.

    // Start is called before the first frame update
    void Start()
    {
        MI = new MusicInfo[4];
        MIInit();
    }

    void MIInit()
    {
        //MusicInfo를 초기화하는 함수입니다. 구현된 곡의 개수가 적어, 각각의 곡의 정보들이 아래에 바로 작성되었습니다.
        //후에 확장한다면, 따로 곡의 정보만이 담긴 Json파일을 만들고, 그것을 파싱해오는 함수로 변형할 것입니다.
        MI[0] = new MusicInfo("Minuet in G Major", "Bach", 1, 1, false, 0, 0);
        MI[1] = new MusicInfo("Catching the moonlight", "Bobo Productions", 2, 2, false, 0, 0);
        MI[2] = new MusicInfo("Waltz of chicks", "Tido Kang", 3, 3, false, 0, 0);
        MI[3] = new MusicInfo("Keys of Moon", "The Epic Hero", 4, 4, false, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        //씬이 바뀌어도 객체가 계속 유지될 수 있도록 합니다.
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
