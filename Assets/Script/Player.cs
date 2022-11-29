using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Player : MonoBehaviour
{
    //컴포넌트 관련
    public PauseMenu pauseMenu;
    public PlayerInputs input;
    private Transform healthBar;
    private TextMeshProUGUI levelText;
    private Image expBar;
    public BulletSpawnerDictionary spawners;
    public BulletDataDictionary bulletDatas;


    //플레이어 투사체 넉백 관련
    private float knockback;
    public float Knockback
    {
        get => knockback;
        set
        {
            knockback = value;
        }
    }

    //플레이어 투사체 크기 관련
    private float bulletSize = 1.0f;
    public float BulletSize
    {
        get => bulletSize;
        set
        {
            bulletSize = value;
        }
    }

    //플레이어 피격 관련
    private float spikeDmg;
    public float SpikeDmg
    {
        get => spikeDmg;
        set
        {
            spikeDmg = value;
        }
    }

    //플레이어 공격속도
    private float delay = 0;
    public float Delay
    {
        get => delay;
        set
        {
            delay = value;
        }
    }

    //플레이어 투사체 속도
    private float bulletSpeed = 0;
    public float BulletSpeed
    {
        get => bulletSpeed;
        set
        {
            bulletSpeed = value;
        }
    }

    //플레이어 투사체 데미지
    private float bulletDmg = 0;
    public float BulletDmg
    {
        get => bulletDmg;
        set
        {
            bulletDmg = value;
        }
    }

    //플레이어 체력관련
    private float hp;
    public float maxhp;
    public float recover;
    public float HP
    {
        get => hp;
        set
        {
            hp = value;
            healthBar.localScale = new (10 * (hp/maxhp), 1);
        }
    }

    //플레이어 경험치 관련
    private int[] ExpIncrease = { 2, 4, 7, 12, 18};
    private int lv;
    public int Lv
    {
        get => lv;
        set
        {
            lv = value;
            levelText.text = $"LV. {value}";
            if (value == 1)
                return;
            maxExp += ExpIncrease[value/20];
        }
    }
    private int maxExp;
    private int exp;
    public int Exp
    {
        get => exp;
        set
        {
            while (value >= maxExp)
            {
                value -= maxExp;  
                Lv++;
            }
            expBar.fillAmount = (float)value / maxExp;
            exp = value;
        }
    }

    private void Awake()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        healthBar = transform.Find("HealthBar");
        expBar = canvas.transform.Find("exp").GetComponent<Image>();
        levelText = canvas.transform.Find("level").GetComponent<TextMeshProUGUI>();
        input = new PlayerInputs();
    }
    private void Start()
    {
        maxhp = 100.0f;
        HP = maxhp;
        maxExp = 5;
        exp = 0;
        Exp = 0;
        Lv = 1;
        levelText.text = $"LV. {Lv}";
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
    private void Update()
    {
        HP += recover*Time.deltaTime;
    }
    private void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            pauseMenu.gameObject.SetActive(true);
        }
    }
    public void Drag(InputAction.CallbackContext obj)
    {
        transform.Translate(obj.ReadValue<Vector2>().x / 150.0f, 0, 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2, 2), transform.position.y, transform.position.z);
    }
    public void AddItem(string type)
    {
        if(GetComponentInChildren(Type.GetType($"{type}Spawner"))!=null)
        {
            var item = GetComponent(Type.GetType($"{type}Spawner")) as BulletSpawner;
            item.enabled = true;
            item.Lv++;
        }
        else if(transform.Find("Items").GetComponent(Type.GetType(type)) as Item)
        {
            var item = transform.Find("Items").GetComponent(Type.GetType(type)) as Item;
            item.enabled = true;
            item.AddLv();
        }
    }
    public void SetLevelItem(string type, int level)
    {
        if(transform.Find("BulletSpawner").gameObject.GetComponentInChildren(Type.GetType($"{type}Spawner")) != null)
        {
            BulletSpawner item = transform.Find("BulletSpawner").gameObject.GetComponentInChildren(Type.GetType($"{type}Spawner")) as BulletSpawner;
            item.Lv = level;
        }
        else
        {
            Item item = transform.Find("Items").GetComponent(Type.GetType(type)) as Item;
            item.SetLv(level);
        }
    }
}