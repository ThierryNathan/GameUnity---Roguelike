using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    public Sprite dmgSprite;
    public int hp = 4;

    private SpriteRenderer spriteRenderer;

    private AudioClip chopSound1;
    private AudioClip chopSound2;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DamageWall(int loss)
    {
        SoundManager.instance.RandomSfx(chopSound1, chopSound2);

        spriteRenderer.sprite = dmgSprite;
        hp -= 1;

        if (hp <= 0)
        {
            gameObject.SetActive(false);

        }
    }
}