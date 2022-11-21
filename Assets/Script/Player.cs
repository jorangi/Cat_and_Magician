using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInputs input;
    private Transform healthBar;
    private float hp;
    public float HP
    {
        get => hp;
        set
        {
            hp = value;
            healthBar.localScale = new (10 * (hp/maxhp), 1);
        }
    }
    public float maxhp;
    public float recover;
    public List<BulletSpawner> spawners;
    public List<BulletData> bulletDatas;

    private void Awake()
    {
        healthBar = transform.Find("HealthBar");
        input = new PlayerInputs();
        maxhp = 100.0f;
        HP = maxhp;
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.Drag.performed += Drag;
    }
    private void OnDisable()
    {
        input.Player.Drag.performed -= Drag;
        input.Disable();
    }
    private void Drag(InputAction.CallbackContext obj)
    {
        transform.Translate(obj.ReadValue<Vector2>().x / 150.0f, 0, 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2, 2), transform.position.y, transform.position.z);
    }
    public void NewBulletSpawner(string type)
    {
        transform.Find("BulletSpawner").gameObject.AddComponent(Type.GetType(type));
        var comps = transform.Find("BulletSpawner").gameObject.GetComponents<BulletSpawner>();
        switch (type)
        {
            case "CatsEyeSpawner":
                comps[^1].bulletData = bulletDatas[0];
                break;
            case "RosrucSpawner":
                comps[^1].bulletData = bulletDatas[1];
                break;
            case "MagicBallSpawner":
                comps[^1].bulletData = bulletDatas[2];
                break;
            case "MagicArrowSpawner":
                comps[^1].bulletData = bulletDatas[3];
                break;
            case "AirStrikeSpawner":
                comps[^1].bulletData = bulletDatas[4];
                break;
        }
    }
    public void RemoveBulletSpawner(string type)
    {
        Destroy(transform.Find("BulletSpawner").gameObject.GetComponent(Type.GetType(type)));
    }
    public void SwitchBulletSpawner(string type)
    {
        BulletSpawner spawner = transform.Find("BulletSpawner").gameObject.GetComponent(Type.GetType(type)) as BulletSpawner;
        if(spawner == null)
        {
            return;
        }
        spawner.enabled = !spawner.enabled;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            HP -= collision.GetComponent<Enemy>().dmg;
        }
    }
}