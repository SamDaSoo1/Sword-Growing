using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldTmp;
    [SerializeField] TextMeshProUGUI jewelTmp;
    [SerializeField] List<GameObject> starUpgradeBoxList;
    [SerializeField] GameObject getJewelUI;
    [SerializeField] GameObject getJewelUIGroup;
    [SerializeField] TextMeshProUGUI maximumStarCountTMP;
    [SerializeField] GameObject battleButton;
    [SerializeField] GameObject bossButton;
    [SerializeField] List<GameObject> prefabs;
    [SerializeField] GameObject starsGroup;
    [SerializeField] TextMeshProUGUI powerText;
    [SerializeField] TextMeshProUGUI criticalChanceText;
    [SerializeField] TextMeshProUGUI criticalDamageText;
    [SerializeField] TextMeshProUGUI goldGainText;
    [SerializeField] GameObject getNewWeaponPopup;
    [SerializeField] TextMeshProUGUI getNewWeaponPopupText;
    [SerializeField] Image getNewWeaponPopupImg;
    [SerializeField] GameObject noti;
    [SerializeField] CanvasGroup notiCG;
    [SerializeField] Image notiImg;
    [SerializeField] TextMeshProUGUI notiText;

    List<string> weaponNameList;
    [SerializeField] List<Sprite> weaponImg;

    static UIDisplay instance;
    public static UIDisplay Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIDisplay>();
                if (instance == null)
                {
                    GameObject uIDisplay = new GameObject();
                    instance = uIDisplay.AddComponent<UIDisplay>();
                    uIDisplay.name = "@" + typeof(UIDisplay).Name;
                    DontDestroyOnLoad(uIDisplay);
                }
            }

            return instance;
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

        weaponNameList = new List<string>
        {
            "1. 낡은 검",
            "2. 흠집난 검",
            "3. 녹슨 검",
            "4. 튼튼한 검",
            "5. 날카로운 검",
            "6. 단단한 검",
            "7. 연마된 검",
            "8. 강철 검",
            "9. 명검",
            "10. 용사의 검",
            "11. 성검",
            "12. 영웅의 검",
            "13. 전설의 검",
            "14. 저주의 검",
            "15. 혼돈의 검",
            "16. 혈검"
        };
    }

    void Start()
    {
        InitMain();

        DataManager.Instance.GoldChanged += UpdateGoldText;
        DataManager.Instance.JewelChanged += UpdateJewelText;
        DataManager.Instance.Highest_Star_Changed += UpdateStarUpgradeBox;
        DataManager.Instance.Highest_Star_Changed += GetNewWeaponPopup;
        DataManager.Instance.TotalCountChanged += UpdateStarCountText;
        DataManager.Instance.IncreasedAttackDamageChanged += UpdatePowerText;
        DataManager.Instance.CriticalChanced += UpdateCriticalChanceText;
        DataManager.Instance.CriticalDamaged += UpdateCriticalDamageText;
        DataManager.Instance.OnGoldGained += UpdateGoldGainText;
        DataManager.Instance.OnMaximumStarCounted += UpdateMaximumStarCountText;

        SceneManager.sceneLoaded += InitSceneChanged;
    }

    void OnDestroy()
    {
        if(DataManager.Instance != null)
        {
            DataManager.Instance.GoldChanged -= UpdateGoldText;
            DataManager.Instance.JewelChanged -= UpdateJewelText;
            DataManager.Instance.Highest_Star_Changed -= UpdateStarUpgradeBox;
            DataManager.Instance.TotalCountChanged -= UpdateStarCountText;
            DataManager.Instance.IncreasedAttackDamageChanged -= UpdatePowerText;
            DataManager.Instance.CriticalChanced -= UpdateCriticalChanceText;
            DataManager.Instance.CriticalDamaged -= UpdateCriticalDamageText;
            DataManager.Instance.OnGoldGained -= UpdateGoldGainText;
            DataManager.Instance.OnMaximumStarCounted -= UpdateMaximumStarCountText;
        }

        SceneManager.sceneLoaded -= InitSceneChanged;
    }

    void InitSceneChanged(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")
            InitMain();
        else if (scene.name == "Battle")
        {
            InitGame();
        }
            
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DataManager.Instance.Gold += 1000;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            DataManager.Instance.Jewel += 1000;
        }
    }
    
    void Init()
    {
        goldTmp = GameObject.Find("Canvas").transform.Find("Top Panel").transform.Find("GoldTab").transform.Find("GoldText").GetComponent<TextMeshProUGUI>();
        jewelTmp = GameObject.Find("Canvas").transform.Find("Top Panel").transform.Find("JewelTab").transform.Find("JewelText").GetComponent<TextMeshProUGUI>();
        maximumStarCountTMP = GameObject.Find("Canvas").transform.Find("Top Panel").transform.Find("InfoTab").transform.Find("maximumStarCountText").GetComponent<TextMeshProUGUI>();
        UpdateGoldText(DataManager.Instance.Gold);
        UpdateJewelText(DataManager.Instance.Jewel);
        UpdateMaximumStarCountText();
    }

    void StarSetting()
    {
        List<int> list = DataManager.Instance.GetStarData();
        list.Reverse();

        int xSpacing = 80;
        int ySpacing = 85;
        int idx = 0;
        Vector2 pos = new Vector2(-360, -700);

        foreach (int count in list)
        {
            if (count > 0)
            {
                SceneStateMonitor.Count += count;
            }

            for (int j = 0; j < count; j++)
            {
                GameObject go = Instantiate(prefabs[idx], starsGroup.transform);
                Destroy(go.GetComponent<CombineThrowingStar>());
                go.AddComponent<ShootStar>();
                go.GetComponent<RectTransform>().anchoredPosition = pos;
                pos = new Vector2(pos.x + xSpacing, pos.y);
                if(pos.x == 440)
                {
                    pos = new Vector2(-320, -700 - ySpacing);
                }
            }
            idx++;
        }
    }

    void InitGame()
    {
        Init();
        starsGroup = GameObject.Find("Canvas").transform.Find("StarsGroup").gameObject;
        StarSetting();
    }

    void InitMain()
    {
        Init();
        
        starUpgradeBoxList = new List<GameObject>();
        GameObject go = GameObject.Find("Canvas").transform.Find("StarTab Scroll View").transform.Find("Viewport").transform.Find("Content").gameObject;
        foreach(Transform box in go.transform)
        {
            starUpgradeBoxList.Add(box.gameObject);
        }

        getJewelUI = Resources.Load<GameObject>("Prefabs/Get Jewel UI");
        getJewelUIGroup = GameObject.Find("Canvas").transform.Find("GetJewelUIGroup").gameObject;
        battleButton = GameObject.Find("Canvas").transform.Find("Button Group").transform.Find("Battle Button").transform.Find("GameButton").gameObject;
        bossButton = GameObject.Find("Canvas").transform.Find("Button Group").transform.Find("Battle Button").transform.Find("BossButton").gameObject;
        powerText = GameObject.Find("Canvas").transform.Find("Tab Bgd").transform.Find("PowerText").gameObject.GetComponent<TextMeshProUGUI>();
        criticalChanceText = GameObject.Find("Canvas").transform.Find("Tab Bgd").transform.Find("CriticalChanceText").gameObject.GetComponent<TextMeshProUGUI>();
        criticalDamageText = GameObject.Find("Canvas").transform.Find("Tab Bgd").transform.Find("CriticalDamageText").gameObject.GetComponent<TextMeshProUGUI>();
        goldGainText = GameObject.Find("Canvas").transform.Find("Tab Bgd").transform.Find("GoldGainText").gameObject.GetComponent<TextMeshProUGUI>();
        getNewWeaponPopup = GameObject.Find("Canvas2").transform.Find("GetNewWeaponPopup").gameObject;
        getNewWeaponPopupText = getNewWeaponPopup.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        getNewWeaponPopupImg = getNewWeaponPopup.transform.Find("Img").GetComponent<Image>();
        noti = GameObject.Find("Canvas").transform.Find("Noti").gameObject;
        notiCG = noti.GetComponent<CanvasGroup>();
        notiImg = noti.transform.Find("Img").GetComponent<Image>();
        notiText = noti.transform.Find("Text").GetComponent<TextMeshProUGUI>();

        battleButton.SetActive(false);
        bossButton.SetActive(false);
        UpdatePowerText();
        UpdateCriticalChanceText();
        UpdateCriticalDamageText();
        UpdateGoldGainText();
        getNewWeaponPopup.SetActive(false);
        notiCG.alpha = 0;

        int highestValue = DataManager.Instance.Highest_Star;

        List<int> starsUpgradeLevelData = DataManager.Instance.GetStarsUpgradeLevelData();
        for (int i = 0; i <= highestValue; i++)
        {
            starUpgradeBoxList[i].SetActive(true);
            starUpgradeBoxList[i].GetComponent<StarUpgradeBoxUI>().Init(i + 1, starsUpgradeLevelData[i]);
        }
    }

    void UpdateGoldText(long gold)
    {
        StringBuilder goldTextBuilder = new StringBuilder();
        long _gold = gold;

        AppendUnit(ref goldTextBuilder, ref _gold, 1000000000000, "조");
        AppendUnit(ref goldTextBuilder, ref _gold, 100000000, "억");
        AppendUnit(ref goldTextBuilder, ref _gold, 10000, "만");

        if (_gold > 0 || goldTextBuilder.Length == 0)
        {
            goldTextBuilder.Append(_gold.ToString());
        }

        goldTmp.text = goldTextBuilder.ToString();
    }

    void UpdateJewelText(long jewel)
    {
        StringBuilder jewelTextBuilder = new StringBuilder();
        long _jewel = jewel;

        AppendUnit(ref jewelTextBuilder, ref _jewel, 1000000000000, "조");
        AppendUnit(ref jewelTextBuilder, ref _jewel, 100000000, "억");
        AppendUnit(ref jewelTextBuilder, ref _jewel, 10000, "만");

        if (_jewel > 0 || jewelTextBuilder.Length == 0)
        {
            jewelTextBuilder.Append(_jewel.ToString());
        }

        jewelTmp.text = jewelTextBuilder.ToString();
    }

    void AppendUnit(ref StringBuilder builder, ref long value, long unitValue, string unitName)
    {
        if (value >= unitValue)
        {
            long unitAmount = value / unitValue;
            value -= unitAmount * unitValue;
            builder.Append(unitAmount.ToString() + unitName + " ");
        }
    }

    void UpdateStarUpgradeBox(int newHighestValue)
    {
        starUpgradeBoxList[newHighestValue].SetActive(true);
    }

    void UpdateStarCountText()
    {
        maximumStarCountTMP.text = DataManager.Instance.TotalCount.ToString() + " / " + DataManager.Instance.Maximum_Star_Count.ToString();
    }

    void UpdatePowerText()
    {
        powerText.text = $"{(DataManager.Instance.Increased_Attack_Damage - 1) * 100}%";
    }

    void UpdateCriticalChanceText()
    {
        criticalChanceText.text = $"{DataManager.Instance.Critical_Chance}%";
    }

    void UpdateCriticalDamageText()
    {
        criticalDamageText.text = $"{(DataManager.Instance.Critical_Damage - 1) * 100}%";
    }

    void UpdateGoldGainText()
    {
        goldGainText.text = $"{(DataManager.Instance.Gold_Gained - 1) * 100}%";
    }

    void UpdateMaximumStarCountText()
    {
        maximumStarCountTMP.text = DataManager.Instance.TotalCount.ToString() + " / " + DataManager.Instance.Maximum_Star_Count.ToString();
    }

    public void BattleButtonClick()
    {
        if (battleButton.activeSelf && bossButton.activeSelf)
        {
            battleButton.SetActive(false);
            bossButton.SetActive(false);
        }
        else if (!battleButton.activeSelf && !bossButton.activeSelf)
        {
            battleButton.SetActive(true);
            bossButton.SetActive(true);
        }
    }

    public void GetJewelUI(int getValue, Vector3 starPos)
    {
        GameObject ui = Instantiate(getJewelUI, getJewelUIGroup.transform);
        ui.GetComponentInChildren<TextMeshProUGUI>().text = getValue.ToString();
        ui.transform.position = new Vector3(starPos.x - 28, starPos.y + 50, starPos.z);
        ui.transform.localScale = Vector3.one * 0.7f;
        ui.transform.DOScale(Vector3.one, 0.3f);
        ui.transform.DOMoveY(ui.transform.position.y + 30, 0.7f).OnComplete(() => { Destroy(ui); });
    }

    public void GetNewWeaponPopup(int newHighestValue)
    {
        getNewWeaponPopupText.text = weaponNameList[newHighestValue];
        getNewWeaponPopupImg.sprite = weaponImg[newHighestValue];
        getNewWeaponPopup.SetActive(true);
    }

    public void NotiUI(Color imgColor, string txt)
    {
        notiCG.DOKill();
        notiImg.color = imgColor;
        notiText.text = txt;
        notiCG.alpha = 1;
        notiCG.DOFade(0, 2);
    }
}

