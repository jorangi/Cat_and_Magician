using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Player : MonoBehaviour
{
    //������Ʈ ����
    public PauseMenu pauseMenu;
    public LevelupMenu levelupMenu;
    public PlayerInputs input;
    private Transform healthBar;
    private TextMeshProUGUI levelText;
    private Image expBar;
    public Image stageBar;
    public TextMeshProUGUI stardustUI;
    public Transform itemList;
    public GameObject itemSlot;
    public StrBspawnerDictionary spawners = new();
    public StrBdataDictionary bulletDatas = new();
    public StrIntDictionary itemLevels = new();
    public TextMeshProUGUI Timer;

    //������ ������
    public ItemData[] itemDatas;
    public ItemData StardustData;
    public int levelupCount;

    //�÷��̾� ����ü �˹� ����
    private float knockback;
    public float Knockback
    {
        get => knockback;
        set
        {
            knockback = value;
        }
    }

    //�÷��̾� ����ü ũ�� ����
    private float bulletSize = 1.0f;
    public float BulletSize
    {
        get => bulletSize;
        set
        {
            bulletSize = value;
        }
    }

    //�÷��̾� �ǰ� ����
    private float spikeDmg;
    public float SpikeDmg
    {
        get => spikeDmg;
        set
        {
            spikeDmg = value;
        }
    }

    //�÷��̾� ���ݼӵ�
    private float delay = 0;
    public float Delay
    {
        get => delay;
        set
        {
            delay = value;
        }
    }

    //�÷��̾� ����ü �ӵ�
    private float bulletSpeed = 0;
    public float BulletSpeed
    {
        get => bulletSpeed;
        set
        {
            bulletSpeed = value;
        }
    }

    //�÷��̾� ����ü ������
    private float bulletDmg = 0;
    public float BulletDmg
    {
        get => bulletDmg;
        set
        {
            bulletDmg = value;
        }
    }

    //�÷��̾� ü�°���
    private float hp;
    public float maxhp;
    public float recover;
    public float HP
    {
        get => hp;
        set
        {
            value = Mathf.Clamp(value, 0.0f, maxhp);
            hp = value;
            healthBar.localScale = new (10 * (hp/maxhp), 1);
        }
    }

    //�÷��̾� ����ġ ����
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
    public int maxExp;
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
                levelupCount++;
            }
            expBar.fillAmount = (float)value / maxExp;
            exp = value;
        }
    }
    private int stardust;
    public int StarDust
    {
        get => stardust;
        set
        {
            stardust = value;
            stardustUI.text = value.ToString();
        }
    }

    private void Awake()
    {
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        healthBar = transform.Find("HealthBar");
        expBar = canvas.transform.Find("exp").GetComponent<Image>();
        levelText = canvas.transform.Find("level").GetComponent<TextMeshProUGUI>();
        itemList = canvas.transform.Find("ItemList").transform.Find("list");
        stageBar = canvas.transform.Find("stageBar").GetComponent<Image>();
        Timer = stageBar.GetComponentInChildren<TextMeshProUGUI>();
        input = new PlayerInputs();
        AddItem("CatsEye");
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
        if(levelupCount>0)
        {
            levelupMenu.gameObject.SetActive(true);
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if(!focus && !levelupMenu.gameObject.activeSelf)
        {
            pauseMenu.gameObject.SetActive(true);
        }
    }
    public void Drag(InputAction.CallbackContext obj)
    {
        transform.Translate(obj.ReadValue<Vector2>().x / 150.0f, 0, 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.55f, 2.55f), transform.position.y, transform.position.z);
    }
    public void AddItem(string type)
    {
        string[] weapon = { "CatsEye", "Rosruc", "MagicBall", "MagicArrow", "Airstrike"};
        if (Array.IndexOf(weapon, type)>-1)
        {
            var item = GetComponentInChildren(Type.GetType($"{type}Spawner")) as BulletSpawner;
            item.enabled = true;
            item.Lv++;
        }
        else
        {
            var item = transform.Find("Items").GetComponent(Type.GetType(type)) as Item;

            if(item.itemSlot==null)
            {
                item.itemSlot = Instantiate(GameManager.Inst.player.itemSlot, GameManager.Inst.player.itemList); 
                foreach (ItemData i in itemDatas)
                {
                    if (i.id == type)
                    {
                        item.itemSlot.GetComponent<Image>().sprite = i.icon;
                    }
                }
            }
            
            item.itemSlot.name = type;
            switch (itemLevels[type])
            {
                case 1:
                    item.itemSlot.GetComponentInChildren<TextMeshProUGUI>().text = "��";
                    break;
                case 2:
                    item.itemSlot.GetComponentInChildren<TextMeshProUGUI>().text = "��";
                    break;
                case 3:
                    item.itemSlot.GetComponentInChildren<TextMeshProUGUI>().text = "��";
                    break;
                case 4:
                    item.itemSlot.GetComponentInChildren<TextMeshProUGUI>().text = "��";
                    break;
                case 5:
                    item.itemSlot.GetComponentInChildren<TextMeshProUGUI>().text = "��";
                    break;
            }

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