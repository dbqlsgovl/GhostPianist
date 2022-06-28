using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
������ ������ �����ϴ� ��ũ��Ʈ�Դϴ�. MainWorld ������ ��ü�� ó�� �����Ǹ�, GameManager ������Ʈ�� �����˴ϴ�.

DontDestroyOnLoad�� ���� ���� ���� MusicGame���� �Ѿ�� �������� �ʰ�, ���� ���� ���� �����˴ϴ�.

���ӿ� ���Ǵ� ���ǵ��� �������� ����Ǹ�, MusicGame(���� ���� ��) ������ �Ѿ ���� ���Ͽ� ���� ����� ���� ������ �����մϴ�.
*/

public class MusicInfo //������ ������ ����Ǵ� Ŭ�����Դϴ�.
{
    public string Name; //���� �̸�
    public string Owner; //���� �۰
    public int Number; //���� ���� ��ȣ
    public int Level; //���� ���̵�
    public bool Unlocked; //���� �ر� ����
    public int Score; //�÷��̾��� �ش� ���� ���� ���
    public int MaxCombo; //�÷��̾��� �ش� ���� �޺� ���

    public MusicInfo(string name, string owner, int number, int level, bool unlocked, int score, int MaxCombo) //MusicInfo�� �������Դϴ�.
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
    public static MusicInfo[] MI; //MusicInfo�� �迭�Դϴ�. �ٸ� ��ũ��Ʈ������ �����ϱ� ������ static���� �����Ͽ����ϴ�.
    public static int SelectedMusic = 1; //���� ����� ������ ��ȣ�Դϴ�. ù ���� �� 1�� �����˴ϴ�.
    public static GManager Instance; //GManager �ν��Ͻ��� �����Ǿ������� Ȯ���ϱ� ���� �����Դϴ�.

    // Start is called before the first frame update
    void Start()
    {
        MI = new MusicInfo[4];
        MIInit();
    }

    void MIInit()
    {
        //MusicInfo�� �ʱ�ȭ�ϴ� �Լ��Դϴ�. ������ ���� ������ ����, ������ ���� �������� �Ʒ��� �ٷ� �ۼ��Ǿ����ϴ�.
        //�Ŀ� Ȯ���Ѵٸ�, ���� ���� �������� ��� Json������ �����, �װ��� �Ľ��ؿ��� �Լ��� ������ ���Դϴ�.
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
        //���� �ٲ� ��ü�� ��� ������ �� �ֵ��� �մϴ�.
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
