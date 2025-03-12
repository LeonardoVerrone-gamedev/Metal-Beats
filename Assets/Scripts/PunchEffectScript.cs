using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchEffectScript : MonoBehaviour
{
    [SerializeField] float lifeTime;
    [SerializeField] SpriteRenderer sprRender;

    public Sprite[] icons;

    void Start()
    {
        sprRender.sprite = icons[Random.Range(0, icons.Length)];
        Invoke("SelfDestruction", lifeTime);
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * 1.5f, Time.deltaTime * 3f);
    }

    void SelfDestruction(){
        Destroy(this.gameObject);
    }
}
