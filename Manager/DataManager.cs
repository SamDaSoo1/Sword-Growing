using System;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HaveStarData
{
    public List<int> starsCount;
}

public class GameData
{
    public string nickName;
    public long gold;                       // 보유골드량
    public long jewel;                      // 보유보석량
    public int highest_Star;                // 최고표창등급
    public int increased_Attack_Damage;     // 공격력증가량
    public int critical_Chance;             // 치명타 확률
    public int critical_Damage;             // 치명타 데미지
    public int gold_Gained;                 // 골드획득량
    public int maximum_Star_Count;          // 표창최대개수
    public int star_Make_CoolTime;          // 표창제작시간
    public bool magnet;            // 자석
    public bool auto_Star_Make;    // 자동제작

    public List<int> starsUpgradeLevel;
    public List<bool> upGradeState;
    public List<int> itemUpgradeLevel;
    public List<bool> itemUpgradeState;
}

public class DataManager : MonoBehaviour
{
    static bool isDestroyed = false;
    static DataManager instance;

    public static DataManager Instance
    {
        get
        {
            if (instance == null && !isDestroyed)
            {
                instance = FindObjectOfType<DataManager>();
                if(instance == null)
                {
                    GameObject dataManager = new GameObject();
                    instance = dataManager.AddComponent<DataManager>();
                    dataManager.name = "@" + typeof(DataManager).Name;
                    DontDestroyOnLoad(dataManager);
                }
            }

            return instance;
        }
    }

    HaveStarData haveStarData;
    GameData gameData;

    public event Action<long> GoldChanged;
    public event Action<long> JewelChanged;
    public event Action<int> Highest_Star_Changed;
    public event Action TotalCountChanged;
    public event Action IncreasedAttackDamageChanged;
    public event Action CriticalChanced;
    public event Action CriticalDamaged;
    public event Action OnGoldGained;
    public event Action OnMaximumStarCounted;
    public event Action OnStarMakeCoolTimed;

    public string NickName 
    { 
        get { return gameData.nickName; } 
        set
        {
            gameData.nickName = value;
        }
    }
    public long Gold 
    {
        get { return gameData.gold; }
        set
        {
            if (gameData.gold != value)
            {
                if (value < 0)
                {
                    return;
                }
                gameData.gold = value;
                GoldChanged?.Invoke(gameData.gold);
            }
        }
    }
    public long Jewel
    {
        get { return gameData.jewel; }
        set
        {
            if (gameData.jewel != value)
            {
                if (value < 0)
                {
                    return;
                }
                gameData.jewel = value;
                JewelChanged?.Invoke(gameData.jewel);
            }
        }
    }
    public int Highest_Star 
    { 
        get { return gameData.highest_Star; }
        set
        {
            if (gameData.highest_Star < value)
            {
                gameData.highest_Star = value;
                Highest_Star_Changed?.Invoke(gameData.highest_Star);
            }
        }
    }
    public int Increased_Attack_Damage 
    {
        get { return gameData.increased_Attack_Damage; }
        set
        {
            if(gameData.increased_Attack_Damage != value)
            {
                if (value < 0)
                {
                    return;
                }
                gameData.increased_Attack_Damage = value;
                IncreasedAttackDamageChanged?.Invoke();
            }
        }
    }
    public int Critical_Chance 
    { 
        get { return gameData.critical_Chance; }
        set
        {
            if (gameData.critical_Chance != value)
            {
                if (value < 0)
                {
                    return;
                }
                gameData.critical_Chance = value;
                CriticalChanced?.Invoke();
            }
        }
    }
    public int Critical_Damage 
    {
        get { return gameData.critical_Damage; } 
        set
        {
            if (gameData.critical_Chance != value)
            {
                if (value < 0)
                {
                    return;
                }
                gameData.critical_Damage = value;
                CriticalDamaged?.Invoke();
            }
        }
    }
    public int Gold_Gained 
    { 
        get { return gameData.gold_Gained; }
        set
        {
            if (gameData.gold_Gained != value)
            {
                if (value < 0)
                {
                    return;
                }
                gameData.gold_Gained = value;
                OnGoldGained?.Invoke();
            }
        }
    }
    public int Maximum_Star_Count 
    {
        get { return gameData.maximum_Star_Count; }
        set
        {
            if(gameData.maximum_Star_Count != value)
            {
                if (value < 0)
                {
                    return;
                }
                gameData.maximum_Star_Count = value;
                OnMaximumStarCounted?.Invoke();
            }
        }
    }
    public int Star_Make_CoolTime 
    {
        get { return gameData.star_Make_CoolTime; }
        set
        {
            if (gameData.star_Make_CoolTime != value)
            {
                // 제작 시간은 1초마다 작아질 수 없다(기획을 그렇게 함)
                if (value < 1)
                {
                    return;
                }
                gameData.star_Make_CoolTime = value;
                OnStarMakeCoolTimed?.Invoke();
            }
        }
    }
    public bool Magnet 
    {
        get { return gameData.magnet; } 
        set
        {
            if (gameData.magnet != value)
            {
                gameData.magnet = value;
            }
        }
    }
    public bool Auto_Star_Make 
    { 
        get { return gameData.auto_Star_Make; }
        set
        {
            if (gameData.auto_Star_Make != value)
            {
                gameData.auto_Star_Make = value;
            }
        }
    }

    int _totalCount;
    public int TotalCount                               // 현재 보유하고 있는 표창갯수
    { 
        get { return _totalCount; }
        set
        {
            if (_totalCount != value)
            {
                _totalCount = value;
                TotalCountChanged?.Invoke();
            }
        }
    }                 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        
        Init();
    }

    void Start()
    {
        SceneManager.sceneLoaded += JsonFileUpdate;
    }

    private void OnDestroy()
    {
        if(instance == this)
        {
            isDestroyed = true;
            instance = null;
        }
    }

    public void UpdateStarDataList()
    {
        haveStarData.starsCount[0]++;
    }

    void OnApplicationQuit()
    {
        JsonFileUpdate(default, default);
    }

    public void Combine(int createdStar)
    {
        if (createdStar > Highest_Star)
            Highest_Star = createdStar;

        haveStarData.starsCount[createdStar]++;
        haveStarData.starsCount[createdStar - 1] -= 2;
        Jewel += createdStar;
    }

    public void LevelUp(int createdStar)
    {
        if (createdStar > Highest_Star)
            Highest_Star = createdStar;

        haveStarData.starsCount[createdStar]++;
        haveStarData.starsCount[createdStar - 1] -= 1;
        Jewel += createdStar;
    }

    void JsonFileUpdate(Scene scene, LoadSceneMode mode)
    {
        if (instance != null)
        {
            JsonFileManager<HaveStarData>.Instance.Write(haveStarData, "HaveStarData");
            JsonFileManager<GameData>.Instance.Write(gameData, "GameData");
        }
    }

    void Init()
    {
        haveStarData = JsonFileManager<HaveStarData>.Instance.Read("HaveStarData");
        if (haveStarData == default)
        {
            haveStarData = new HaveStarData();
            haveStarData.starsCount = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            TotalCount = 0;
        }
        else
        {
            foreach(int count in haveStarData.starsCount)
            {
                TotalCount += count;
            }
        }

        gameData = JsonFileManager<GameData>.Instance.Read("GameData");
        if (gameData == default)
        {
            gameData = new GameData();
            gameData.nickName = PlayerPrefs.GetString("NickName");
            gameData.gold = 0;
            gameData.jewel = 0;
            gameData.highest_Star = 0;
            gameData.increased_Attack_Damage = 1;
            gameData.critical_Chance = 0;
            gameData.critical_Damage = 1;
            gameData.gold_Gained = 1;
            gameData.maximum_Star_Count = 10;
            gameData.star_Make_CoolTime = 3;
            gameData.magnet = false;
            gameData.auto_Star_Make = false;
            gameData.starsUpgradeLevel = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            gameData.upGradeState = new List<bool> { false, false, false, false, false, false, false, false, false };
            gameData.itemUpgradeLevel = new List<int> { 0, 0 };
            gameData.itemUpgradeState = new List<bool> { false, false };
        }

        Gold = gameData.gold;
        Jewel = gameData.jewel;
        Highest_Star = gameData.highest_Star;
        Increased_Attack_Damage = gameData.increased_Attack_Damage;
        Critical_Chance = gameData.critical_Chance;
        Critical_Damage = gameData.critical_Damage;
        Gold_Gained = gameData.gold_Gained;
        Maximum_Star_Count = gameData.maximum_Star_Count;
        Star_Make_CoolTime = gameData.star_Make_CoolTime;
        Magnet = gameData.magnet;
        Auto_Star_Make = gameData.auto_Star_Make;
    }

    public List<int> GetStarData()
    {
        return new List<int>(haveStarData.starsCount);
    }

    public List<int> GetStarsUpgradeLevelData()
    {
        return gameData.starsUpgradeLevel;
    }

    public int GetStarsUpgradeLevelData(int idx)
    {
        return gameData.starsUpgradeLevel[idx];
    }

    public void StarUpgrade(int starGrade)
    {
        if(gameData.starsUpgradeLevel[starGrade - 1] < 5)
            gameData.starsUpgradeLevel[starGrade - 1]++;
    }

    public void UpgradeComplete(int idx)
    {
        if(idx >= gameData.upGradeState.Count)
        {
            return;
        }
        gameData.upGradeState[idx] = true;
    }

    public bool GetUpgradeState(int idx)
    {
        return gameData.upGradeState[idx];
    }

    public int GetItemUpgradeLevelData(int idx)
    {
        return gameData.itemUpgradeLevel[idx];
    }

    public void AddItemUpgradeLevelData(int idx)
    {
        gameData.itemUpgradeLevel[idx]++;
    }

    public bool GetItemUpgradeStateData(int idx)
    {
        return gameData.itemUpgradeState[idx];
    }

    public void SetItemUpgradeStateData(int idx)
    {
        gameData.itemUpgradeState[idx] = true;
    }

    public bool GetCriticalChance()
    {
        int num = UnityEngine.Random.Range(1, 101);

        if(num <= Critical_Chance)
            return true;
        else
            return false;
    }
}
