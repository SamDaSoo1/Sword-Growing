using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Threading;

public class Target : MonoBehaviour
{
    [SerializeField] GameObject goldGainPopUp;
    [SerializeField] TargetPointer tp;
    int getGold = 1;

    public int HitCount { get; private set; }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ThrowingStar"))
        {
            int num = Random.Range(2, 4);
            if(num == 2)
                SoundManager.Instance.PlaySFX(Sfx.Hit1);
            else
                SoundManager.Instance.PlaySFX(Sfx.Hit2);

            SoundManager.Instance.BGMSoundPitchUp();
            int swordLevel = int.Parse(collision.GetComponent<Image>().sprite.name) - 1;
            Vector3 collisionPos = collision.transform.position;
            EffectPoolManager.Instance.PlayEffect(swordLevel, collisionPos);
            Destroy(collision.gameObject);

            GameObject go = Instantiate(goldGainPopUp, transform.parent.parent);
            DataManager.Instance.Gold += getGold * DataManager.Instance.Gold_Gained;
            go.GetComponentInChildren<TextMeshProUGUI>().text = $"+{getGold * DataManager.Instance.Gold_Gained}";
            getGold *= 2;
            go.transform.position = transform.position + Vector3.down * 155;
            go.transform.DOMove(go.transform.position + Vector3.up * 30, 0.5f).OnComplete(() => Destroy(go));
            tp.SpeedUp();
            transform.localScale *= 0.9f;
            HitCount += 1;
        }
    }

    private void OnDestroy()
    {
        SoundManager.Instance.BGMSoundPitchReset();
        transform.DOKill();
    }
}
