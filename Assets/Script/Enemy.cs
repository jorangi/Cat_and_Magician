using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float dmg = 10;
    private Transform healthBar;
    public float maxhp;
    private float hp;
    private float spd = 1.0f;
    public float HP
    {
        get => hp;
        set
        {
            value = Mathf.Clamp(value, 0, maxhp);
            hp = value;
            healthBar.localScale = new(10*(hp/maxhp), 1);
            if(hp == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Awake()
    {
        healthBar = transform.Find("HealthBar").GetComponent<Transform>();
        maxhp = 100;
        HP = 100;
    }
    private void FixedUpdate()
    {
        transform.Translate(spd * Time.fixedDeltaTime * Vector2.down);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Remove"))
        {
            Destroy(gameObject);
        }
    }
}
