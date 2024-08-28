using TMPro;
using UnityEngine;
using DG.Tweening;

public enum Result
{
    AllStarsThrown,
    Victory
}

public class BattleResult : MonoBehaviour
{
    [SerializeField] RectTransform popUp;
    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField] Target target;
    public bool GameMode { get; set; } = false; 

    void Awake()
    {
        gameObject.SetActive(false);
        popUp.localScale = Vector3.zero;
    }

    public void ShowPopUp(Result result)
    {
        if (gameObject.activeSelf)
            return;

        if(result == Result.AllStarsThrown && GameMode)
            tmp.text = $"{target.HitCount}�� �����Ͽ����ϴ�.";
        else if (result == Result.AllStarsThrown && !GameMode)
            tmp.text = "�� �������� ���ƿ�����.";
        else if (result == Result.Victory && !GameMode)
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseSuccessed);
            tmp.text = "5�� ��带 ȹ���Ͽ����ϴ�.";
            DataManager.Instance.Gold += 50000;
        }
            

        gameObject.SetActive(true);
        popUp.DOScale(Vector3.one, 1f);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
