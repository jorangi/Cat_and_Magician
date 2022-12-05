using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirstrikeSpawner : BulletSpawner
{
    public GameObject AirstrikeRange;
    public Sprite AirstrikeRangeRed;

    protected override void PoolingBullet()
    {
        GameObject parent = new();
        parent.transform.SetParent(Bullets.transform);
        parent.name = "AirstrikeRange";

        for (int i = 0; i < bulletData.limit; i++)
        {
            GameObject obj = Instantiate(AirstrikeRange, parent.transform);
            obj.name = "AirstrikeRange";
            obj.SetActive(false);
        }

        base.PoolingBullet(); 
    }
    protected override void ShootBullet()
    {
        GameObject obj = Bullets.transform.Find("AirstrikeRange").GetChild(0).gameObject;
        obj.transform.SetParent(null);
        AirstrikeRange bullet = obj.GetComponent<AirstrikeRange>();
        bullet.spawner = this;
        bullet.dmg = dmg + GameManager.Inst.player.BulletDmg;
        bullet.speed = speed + GameManager.Inst.player.BulletSpeed;
        bullet.knockback = bulletData.knockback + GameManager.Inst.player.Knockback;
        obj.transform.localScale = new(GameManager.Inst.player.BulletSize, GameManager.Inst.player.BulletSize);
        obj.name = bulletData.name;
        if(FindObjectOfType<Enemy>() == null)
        {
            obj.transform.position = new(transform.position.x, 3);
        }
        else
        {
            obj.transform.position = FindObjectOfType<Enemy>().transform.position;
        }
        obj.SetActive(true);
    }
}
