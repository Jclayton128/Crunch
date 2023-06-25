using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : Health
{
    [SerializeField] SpriteRenderer _backSprite = null;
    SpriteRenderer _frontSprite;

    //settings
    int chanceToBeDamaged = 1;

    //state
    Material _backSpriteMat;

    protected override void Start()
    {
        base.Start();
        _frontSprite = GetComponent<SpriteRenderer>();
        _backSprite.sprite = _frontSprite.sprite;
        _backSpriteMat = _backSprite.material;

    }

    public override void ReceiveDamage(float incomingDamage)
    {
        base.ReceiveDamage(incomingDamage);
        //UpdateSprite();
    }

    private void UpdateSprite()
    {
        Sprite newSprite = Sprite.Create(_frontSprite.sprite.texture,
    _frontSprite.sprite.textureRect,
    _frontSprite.sprite.pivot);
        Texture2D newtex = newSprite.texture;
        int randX = Mathf.RoundToInt(UnityEngine.Random.Range(-newSprite.textureRect.width, newSprite.textureRect.width));
        int randY = Mathf.RoundToInt(UnityEngine.Random.Range(-newSprite.textureRect.height, newSprite.textureRect.height));
        Debug.Log($"width: {newSprite.textureRect.width}, height: {newSprite.textureRect.height}");
        newtex.SetPixel(randX, randY, Color.blue);
        newtex.Apply();
        newSprite = Sprite.Create(newtex,_frontSprite.sprite.textureRect,_frontSprite.sprite.pivot);
        _frontSprite.sprite = newSprite;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer dd;
        if (collision.TryGetComponent<DamageDealer>(out dd))
        {
            int rand = UnityEngine.Random.Range(0, 11);
            if (rand <= chanceToBeDamaged)
            {
                ReceiveDamage(dd.Damage);
                dd.ImpactTarget();
            }
        }
    }


}
