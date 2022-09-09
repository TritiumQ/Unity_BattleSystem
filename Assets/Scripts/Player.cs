using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
[SerializeField]

public class Player
{
    //����ģʽ
    private Player()
    {
        
    }
    private static Player instance;//ȫ��Ψһʵ��
    public static Player Instance //��ȡʵ��������
    {
        get
        {
            if (instance == null)
                instance = new Player();
            return instance;
        }
    }

    public string Name { get; private set; }
    public int MaxHP { get; private set; }
    public int CurrentHP { get; private set; }
    public int Mithrils { get; private set; }//����
    public int Tears { get; private set; }//���
    public int InitTears { get; private set; }//��ʼ���
    public List<int> cardSet  { get; private set; } //����
    //0~199�����  200~399������
    public bool IsAllUnlocked { get; private set; }
    public bool[] Unlocked { get; private set; } //��¼�ѽ����Ŀ���
    /// <summary>
    ///  ��ʼ����Ϣ, ����ArchiveManager�����봢����Ϣ��, ����ֱ�ӵ���
    /// </summary>
    /// <returns></returns>
    public void Initialized(SerializablePlayerData _info)
	{
        Name = _info.Name;
        MaxHP = _info.MaxHP;
        CurrentHP = _info.CurrentHP;
        Tears = _info.Tears;
        Mithrils = _info.Mithrils;
        InitTears = _info.InitTears;

        cardSet = new List<int>(_info.CardSet);
        IsAllUnlocked = _info.IsAllUnlocked;
        Unlocked = new bool[400];
        if(IsAllUnlocked)
		{
            Array.Fill(Unlocked, true);
        }
        else if(_info.IsStarter)
		{
            Array.Fill(Unlocked, false);
            for (int i=1; i < Unlocked.Length; i++)
			{
                if (i > 0 && i < 25)
				{
                    Unlocked[i] = true;
				}
                if (i > 250 && i <= 257)
				{
                    Unlocked[i] = true;
				}
			}
		}
		else
		{
            Array.Fill(Unlocked, false);
        }
        foreach (var id in _info.UnlockCard)
        {
            Unlocked[id] = true;
        }
    }

    #region �����޸Ľӿ�
    public void SetData(string _name, int _maxHP, int _currentHp, int _mithrils, int _tears,int _initTears)
    {
        Name = _name;
        MaxHP = _maxHP;
        CurrentHP = _currentHp;
        Mithrils = _mithrils;
        Tears = _tears;
        InitTears = _initTears;
    }
    public void SetCurrentHP(int _value)
    {
        CurrentHP = _value;
        if (CurrentHP >= MaxHP)
            CurrentHP = MaxHP;
    }
    public void SetMaxHP(int _value)
    {
        MaxHP = _value;
    }
    public void AddCurrentHp(int _value)
    {
        CurrentHP += _value;
        if (CurrentHP >= MaxHP)
            CurrentHP = MaxHP;
        else if (CurrentHP < 0)
            PlayerDataTF.EventEnd();
    }
    public void AddMaxHp(int _value)
    {
        CurrentHP += _value;
        MaxHP += _value;
    }
    public void SetMoney(int _mithrils, int _tears)
    {
        Mithrils = _mithrils;
        Tears = _tears;
    }
    public void AddMoney(int _mithrils, int _tears)
    {
        Mithrils += _mithrils;
        Tears += _tears;
    }

    public void SetTears(int _tears)
	{
        Tears = _tears;
	}
    public void SetMithrils(int _mithrils)
	{
        Mithrils = _mithrils;
	}

    public void AddInitTears(int _initTears)
    {
        InitTears += _initTears;
    }
    public void ReSet()
    {
        CurrentHP = MaxHP;
        Tears = InitTears;
        while(cardSet.Count>12)
        {
            cardSet.RemoveAt(cardSet.Count - 1);
        }
    }
	#endregion

	#region �����޸Ľӿ�
	/// <summary>
	/// ˢ�¿��ƽ������
	/// </summary>
	public void CheckUnlockedCard()
    {
        for (int i = 0; i < cardSet.Count; i++)
        {
            if (Unlocked[cardSet[i]] == false)
                Unlocked[cardSet[i]] = true;
        }
    }

    /// <summary>
    /// �����޸�, �÷����Ḳ��ԭ�еĿ���
    /// </summary>
    /// <param name="_list">�޸ĺ�Ŀ���</param>
    public void SetCardSet(List<int> _list)
	{
        cardSet.Clear();
        cardSet = _list;
        CheckUnlockedCard();
	}

    /// <summary>
    /// �����Լ������б��޸ģ��÷����Ḳ��ԭ�еĿ���ͽ����б�
    /// </summary>
    /// <param name="_list">�޸ĺ�Ŀ���</param>
    /// <param name="_unlock">�޸ĺ�Ľ����б�</param>
    public void SetCardSet(List<int> _list, bool[] _unlock)
    {
        cardSet.Clear();
        cardSet = _list;
        Unlocked = _unlock;
        CheckUnlockedCard();
    }

    /// <summary>
    /// �����ſ���
    /// </summary>
    /// <param name="_cradID">������Ŀ���ID</param>
    public void AddCard(int _cradID)
	{
        cardSet.Add(_cradID);
        CheckUnlockedCard();
	}

    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="cardList">������Ŀ���ID�б�</param>
    public void AddCard(List<int> cardList)
	{
        foreach(var card in cardList)
		{
            cardSet.Add(card);
		}
        CheckUnlockedCard();
	}

    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="cardList">������Ŀ���ID�б�</param>
    public void AddCard(int[] cardList)
    {
        foreach (var card in cardList)
        {
            cardSet.Add(card);
        }
        CheckUnlockedCard();
    }

    /// <summary>
    /// ɾ�����ſ���
    /// </summary>
    /// <param name="_cardID">��ɾ������ID</param>
    public void DeleteCard(int _cardID)
	{
        cardSet.Remove(_cardID);
	}

	#endregion

}

