using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    Vector2 moveDir = Vector2.right;
    [SerializeField] float speed = 100f;
    [SerializeField] RectTransform canvas;
    [SerializeField] RectTransform myRect;
    [SerializeField] Slider hpBar;
    [SerializeField] Image img;
    [SerializeField] BattleResult battleResult;

    private void Awake()
    {
        hpBar.value = 1;
    }

    void Update()
    {
        if (myRect.anchoredPosition.x < (-canvas.rect.width / 2) + (myRect.rect.width / 2))
        {
            moveDir = Vector2.right;
        }
        else if (myRect.anchoredPosition.x > (canvas.rect.width / 2) - (myRect.rect.width / 2))
        {
            moveDir = Vector2.left;
        }

        myRect.anchoredPosition += speed * Time.deltaTime * moveDir;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ThrowingStar"))
        {
            int num = Random.Range(2, 4);
            if (num == 2)
                SoundManager.Instance.PlaySFX(Sfx.Hit1);
            else
                SoundManager.Instance.PlaySFX(Sfx.Hit2);

            int swordLevel = int.Parse(collision.GetComponent<Image>().sprite.name);
            Vector3 collisionPos = collision.transform.position;

            hpBar.value -= 0.2f; 
            Destroy(collision.gameObject);
            StartCoroutine(BlinkImage());
            if (hpBar.value < 0.01f)
            {
                battleResult.ShowPopUp(Result.Victory);
                Destroy(gameObject);
            }  
        }
    }

    IEnumerator BlinkImage()
    {
        for(int i = 0; i < 3; i++)
        {
            img.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.05f);
            img.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
