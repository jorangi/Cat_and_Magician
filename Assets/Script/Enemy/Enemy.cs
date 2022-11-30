using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    public float dmg;
    private Transform healthBar;
    public float maxhp;
    private float hp;
    private float spd;
    public float HP
    {
        get => hp;
        set
        {
            if(hp == 0)
            {
                return;
            }
            value = Mathf.Clamp(value, 0, maxhp);
            hp = value;
            healthBar.localScale = new(10*(hp/maxhp), 1);
            if(hp == 0)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GameObject item = Instantiate(GameManager.Inst.Drops["yellowexpstar"]);
                item.name = "YellowExpStar";
                item.transform.position = transform.position;
                Destroy(gameObject);
            }
        }
    }

    protected virtual void Awake()
    {
        healthBar = transform.Find("HealthBar").GetComponent<Transform>();
        maxhp = data.hp;
        hp = data.hp;
        HP = data.hp;
        spd = data.spd;
    }
    protected virtual void FixedUpdate()
    {
        transform.Translate(spd * Time.fixedDeltaTime * Vector2.down);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Remove"))
        {
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Player"))
        {
            gameObject.layer = 10;
            HP -= GameManager.Inst.player.SpikeDmg;
            if (collision.name == "ProtectBall")
            {
                collision.gameObject.SetActive(false);
            }
            else
            {
                GameManager.Inst.player.HP -= dmg;
            }
        }
    }
}
