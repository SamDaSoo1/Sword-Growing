using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    Vector2 moveDir = Vector2.right;
    [SerializeField] float speed = 100f;
    [SerializeField] RectTransform canvas;
    [SerializeField] RectTransform myRect;
    [SerializeField] int hp = 1000;

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
            SoundManager.Instance.PlaySFX(Sfx.Obstacle);
            int swordLevel = int.Parse(collision.GetComponent<Image>().sprite.name);
            Vector3 collisionPos = collision.transform.position;
           
            hp -= collision.gameObject.GetComponent<ThrowingStarData>().Power;

            Destroy(collision.gameObject);
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
