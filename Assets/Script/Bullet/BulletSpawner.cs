using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject Bullets;
    public BulletData bulletData;
    public float dmg;
    public float delay;
    public float speed;
    public int level;
    public bool revol;
    public float timer;

    protected virtual void Awake()
    {
        Bullets = GameObject.FindGameObjectWithTag("Bullets");
        timer = 0.0f;
    }
    private void Start()
    {
        dmg = bulletData.dmg[Mathf.Min(level, bulletData.dmg.Length - 1)];
        delay = 1 / bulletData.delay[Mathf.Min(level, bulletData.delay.Length - 1)];
        speed = bulletData.speed[Mathf.Min(level, bulletData.speed.Length - 1)];

        PoolingBullet();
    }
    private void PoolingBullet()
    {
        GameObject parent = new();
        parent.transform.SetParent(Bullets.transform);
        parent.name = bulletData.name;

        for(int i = 0; i<bulletData.limit; i++)
        {
            GameObject obj = Instantiate(bulletData.bullet, parent.transform);
            obj.SetActive(false);
        }
    }
    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if(timer >= delay)
        {
            timer = 0;
            ShootBullet();
        }
    }
    protected virtual void ShootBullet()
    {
        GameObject obj = Bullets.transform.Find(bulletData.name).GetChild(0).gameObject;
        obj.transform.SetParent(null);
        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.dmg = dmg;
        bullet.speed = speed;
        bullet.knockback = bulletData.knockback;
        obj.name = bulletData.name;
        obj.transform.position = transform.position;
        obj.SetActive(true);
    }
}
