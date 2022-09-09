using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Player player;       //����Ϊ����ģʽ

    //TODO �ṹ�Ż�
    public int step;     //�ؿ�����
    public int[] GameEventCount; //ÿ��ؿ�����
    public int level;     //����
    public int result=-1;    //��Ϸ���
    public List<int> GameEvent; //��Ϸ�¼�
    public static bool isFistOpen = true;

    public GameObject eventPrefab;
    public GameObject levelUI;
    public GameObject LoadManager;

    public List<GameObject> eventUnit; //�¼���������
    private void Awake()
    {
        InitGameManager();
    }
    void Start()
    {
        //InitGameManager();
        

        //������
        //step = 4; level = 2;
        //PlayerDataTF.EventEnd();
        //InitGameManager();
        //InitGameEvent(2);

        //��������
        //player = Player.Instance;
        //ArchiveManager.LoadPlayerData(1);
        //GameEventCount = GameConst.GameEventCount;//��ʼ��ÿ��ؿ�����
        //if (isFistOpen)
        //{
        //    InitGameEvent(2);
        //    GameProcessSave.GameSaveSet(1, this, true);
        //    isFistOpen = false;
        //}
    }

    void Update()
    {
        RefreshData(); //���ݸ���
    }
    void InitGameManager() //��ʼ����Ϸ�����߶���
    {
        player = Player.Instance;
        GameEventCount = GameConst.GameEventCount;//��ʼ��ÿ��ؿ�����
        if (isFistOpen)
        {
            Debug.Log("�״ν����������");
            ArchiveManager.LoadPlayerData(1);
            GameProcessSave.GameProcessDataLoad(this);
            isFistOpen = false;
        }
    }
    public void InitGameEvent(int _level = 1)   //��ʼ��/���� ��Ϸ�¼�
    {
        level = _level; //��ʼ�����ڲ���
        step = 0;
        GameEvent = new List<int>();

        //ˢ�²���UI
        levelUI.GetComponent<LevelUI>().SetLevel(level);
        
        //����ԭ���¼�UI����
        for (int i = eventUnit.Count - 1; i >= 0; i--)
        {
            Destroy(eventUnit[i]);
        }
        eventUnit.Clear();
        
        //α��������¼�������
        GetRandom.GetRandomEvent(GameEvent,level);
        
        //��ʼ���¼�Ԥ�����������
        for (int i = 0; i < GameEventCount[level]; i++)//��ʼ���¼�Ԥ�����������
        {
            GameObject newEvent = Instantiate(eventPrefab, transform); //�����¼�Ԥ�Ƽ�
            newEvent.GetComponent<Transform>().position = new Vector3(SetPosition(i, 375, 1725), 500, 0);
            if (newEvent != null)
                newEvent.GetComponent<EventUI>().SetEventSign(GameEvent[i]); //�����¼����������
            eventUnit.Add(newEvent);
        }

        //������һ�س�������
        //Debug.Log(GameEvent[0]);
        LoadManager.GetComponent<LoadManager>().NextScene(GameEvent[0]);
    }

    public void InitGameEvent(SerializableGP gp)
    {
        level = gp.level; //��ʼ�����ڲ���
        //Debug.Log(level);
        step = gp.step;
        GameEvent = new List<int>();

        levelUI.GetComponent<LevelUI>().SetLevel(level);

        for (int i = eventUnit.Count - 1; i >= 0; i--)
        {
            Destroy(eventUnit[i]);
        }
        eventUnit.Clear();
        GameEvent.Clear();

        foreach (var e in gp.gameEvent)
        {
            GameEvent.Add(e.eventSign);
        }

        for (int i = 0; i < GameEventCount[level]; i++)//��ʼ���¼�Ԥ�����������
        {
            GameObject newEvent = Instantiate(eventPrefab, transform); //�����¼�Ԥ�Ƽ�
            newEvent.GetComponent<Transform>().position = new Vector3(SetPosition(i, 375, 1725), 500, 0);
            if (newEvent != null)
            {
                newEvent.GetComponent<EventUI>().SetEventSign(GameEvent[i]); //�����¼����������
                if (gp.gameEvent[i].isPass)
                {
                    newEvent.GetComponent<EventUI>().SetPass();
                }
            }
            eventUnit.Add(newEvent);
        }

        LoadManager.GetComponent<LoadManager>().NextScene(GameEvent[step]);
    }
    void RefreshData() //������Ϸ��������
    {
        //������Ϸ���ݸ���
        int judge = PlayerDataTF.GetResult();
        if (judge == 1)//ͨ����ǰ�¼�
        {
            AddStep();
        }
        else if (judge == -1 || player.CurrentHP <= 0)
        {
            Gameover(0);//��Ϸʧ��
        }
    }
    public void Gameover(int _result) //������Ϸ����
    {
        result = _result;
        GetReward();
        isFistOpen = true;
        if (player != null)
        {
            player.ReSet();
            ArchiveManager.SavePlayerData(1);
            //InitGameEvent();
            //GameProcessSave.GameSaveSet(1, this, true);
        }
        //_result���ƽ������,�ݶ� 0��ʧ�ܣ�1��ʤ��......
        //�л�End����
        SceneManager.LoadScene("EndScene");
    }

    
    #region ��Ϊ/���߷���

    void AddStep()
    {
        Debug.Log(eventUnit.Count);
        eventUnit[step].GetComponent<EventUI>().SetPass();//�¼�ͼ�����
        step++;
        if (step < GameEventCount[level])
        {
            LoadManager.GetComponent<LoadManager>().NextScene(GameEvent[step]);//������һ�س�������
        }
        else//ͨ����ǰ����
        {
            AddLevel();
        }
        //���ݱ���
        if (player != null)
        {
            GameProcessSave.GameSaveSet(1, this, true);
        }
    }
    void AddLevel()
    {
        level++;
        step = 0;//���ò���
        if (level<GameConst.GameEventCount.Length)
        {
            InitGameEvent(level); //ˢ���¼�
        }
        else//��Ϸͨ��
        {
            Gameover(1); //�������ҳ��
        }
    }
    float SetPosition(int i,float low,float up)//����x��λ��
    {
        float len,x;
        len = (up - low) / GameEventCount[level];
        x = i * len + len / 2 + low;
        return x;
    }

    void GetReward()
    {
        int value=0;
        for(int i=1;i<level;i++)
        {
            value += GameEventCount[i];
        }
        value += step;
        value *= 2;
        player.AddMoney(value, 0);
    }

}
#endregion
