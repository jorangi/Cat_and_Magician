using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConsoleManager : MonoBehaviour
{
    private float logTimer = 0;
    public GameObject ConsoleUI;
    public GameObject ConsoleButton;
    public GameObject InstanceConsole;
    public GameObject Preview;
    public GameObject PreviewButton;
    public TMP_InputField input;
    public TextMeshProUGUI Log;
    public StrStrDictionary Consoles = new();
    public StrStrDictionary ConsolesOrigin = new();
    public bool Autoclose = false;

    public void SwitchAutoclose()
    {
        Autoclose = !Autoclose;
    }
    private void Awake()
    {
        ConsoleInitialize();
        Preview = ConsoleUI.transform.Find("Preview").gameObject;
        Preview.SetActive(false);
    }
    private void Update()
    {
        if((Input.GetKey(KeyCode.L) && Input.GetKeyDown(KeyCode.LeftControl)) || (Input.GetKeyDown(KeyCode.L) && Input.GetKey(KeyCode.LeftControl)))
        {
            VisibleConsole();
        }
        if(logTimer > 0)
        {
            logTimer -= Time.unscaledDeltaTime;
        }
        else
        {
            logTimer = 0;
            Log.gameObject.SetActive(false);
        }
    }
    private void VisibleConsole()
    {
        if (ConsoleUI.activeSelf)
        {
            input.text = "";
            Time.timeScale = 1;
            GameManager.Inst.player.input.Enable();
        }
        else
        {
            Time.timeScale = 0;
            GameManager.Inst.player.input.Disable();
        }
        ConsoleUI.SetActive(!ConsoleUI.activeSelf);
    }
    private void HideConsole()
    {
        Time.timeScale = 1;
        GameManager.Inst.player.input.Enable();
        ConsoleUI.SetActive(false);
    }
    private void ConsoleInitialize()
    {
        string[] itemNamekor = { "Ĺ������", "�ν���ũ", "������", "�����ַο�", "���Ʈ����ũ", "����", "������", "������", "������", "�������̸�ǥ", "������Ż", "������Ʈ��", "�׻���", "�����", "Ǫ��������" };
        string[] itemName = { "catseye", "rosruc", "magicball", "magicarrow", "airstrike", "ribbon", "magichat", "magnifyingglass", "magicstone", "frozennametag", "accelerateportal", "protectball", "churu", "spikyball", "fragmentbluestar" };
        string[] itemNameOrigin = { "CatsEye", "Rosruc", "MagicBall", "MagicArrow", "AirStrike", "Ribbon", "MagicHat", "MagnifyingGlass", "MagicStone", "FfrozenNameTag", "AcceleratePortal", "ProtectBall", "Churu", "SpikyBall", "FragmentBlueStar" };
        string[] mobNamekor = { "�����", "Ǫ����", "�ʷϺ�", "������", "�ϾẰ" };
        string[] mobName = { "yellowexpstar", "blueexpstar", "greenexpstar", "redexpstar", "whiteexpstar" };
        string[] mobNameOrigin = { "YellowExpStar", "BlueExpStar", "GreenExpStar", "RedExpStar", "WhiteExpStar" };
        string[] monsterNamekor = { "�׽�Ʈ����" };
        string[] monsterName = { "testenemy" };
        string[] monsterNameOrigin = { "TestEnemy" };

        for (int i = 0; i < itemName.Length; i++)
        {
            string lastChar = (itemNamekor[i][^1] - 0XAC00) % 28 > 0 ? "��" : "��";
            Consoles.Add($"increase{itemName[i]}", $"{itemNamekor[i]}�� ������ ��½�ŵ�ϴ�");
            Consoles.Add($"decrease{itemName[i]}", $"{itemNamekor[i]}�� ������ �϶���ŵ�ϴ�");
            Consoles.Add($"enable{itemName[i]}", $"{itemNamekor[i]}{lastChar} Ȱ��ȭ�մϴ�");
            Consoles.Add($"disable{itemName[i]}", $"{itemNamekor[i]}{lastChar} ��Ȱ��ȭ�մϴ�");

            Consoles.Add($"setitemlv{itemName[i]}", $"{itemNamekor[i]}�� ������ �����մϴ�");

            //Origin Code
            ConsolesOrigin.Add($"increase{itemName[i]}", $"Increase {itemNameOrigin[i]}");
            ConsolesOrigin.Add($"decrease{itemName[i]}", $"Decrease {itemNameOrigin[i]}");
            ConsolesOrigin.Add($"enable{itemName[i]}", $"Enable {itemNameOrigin[i]}");
            ConsolesOrigin.Add($"disable{itemName[i]}", $"Disable {itemNameOrigin[i]}");

            ConsolesOrigin.Add($"setitemlv{itemName[i]}", $"Set ItemLv {itemNameOrigin[i]}");

            if (i < 5)
            {
                Consoles.Add($"evolution{itemName[i]}", $"{itemNamekor[i]}{lastChar} ��ȭ��ŵ�ϴ�");
                Consoles.Add($"devolution{itemName[i]}", $"{itemNamekor[i]}{lastChar} ��ȭ��ŵ�ϴ�");

                ConsolesOrigin.Add($"evolution{itemName[i]}", $"Evolution {itemNameOrigin[i]}");
                ConsolesOrigin.Add($"devolution{itemName[i]}", $"Devolution {itemNameOrigin[i]}");
            }
        }
        for (int i = 0; i < mobName.Length; i++)
        {
            string lastChar = mobNamekor[i][mobNamekor[i].Length - 1] - 0XAC00 % 28 > 0 ? "��" : "��";
            Consoles.Add($"spawn{mobName[i]}", $"{mobNamekor}{lastChar} ��ȯ�մϴ�");
            Consoles.Add($"remove{mobName[i]}", $"{mobNamekor}{lastChar} ��� �����մϴ�");

            //Origin Code
            ConsolesOrigin.Add($"spawn{mobName[i]}", $"Spawn {mobNameOrigin[i]}");
            ConsolesOrigin.Add($"remove{mobName[i]}", $"Remove {mobNameOrigin[i]}");
        }
        for (int i = 0; i < monsterName.Length; i++)
        {
            string lastChar = monsterNamekor[i][monsterNamekor[i].Length - 1] - 0XAC00 % 28 > 0 ? "��" : "��";
            Consoles.Add($"spawn{monsterName[i]}", $"{monsterNamekor}{lastChar} ��ȯ�մϴ�");
            Consoles.Add($"kill{monsterName[i]}", $"{monsterNamekor}{lastChar} ��� óġ�մϴ�");
            Consoles.Add($"remove{monsterName[i]}", $"{monsterNamekor}{lastChar} ��� �����մϴ�");

            //Origin Code
            ConsolesOrigin.Add($"spawn{monsterName[i]}", $"Spawn {monsterNameOrigin[i]}");
            ConsolesOrigin.Add($"kill{monsterName[i]}", $"Kill {monsterNameOrigin[i]}");
            ConsolesOrigin.Add($"remove{monsterName[i]}", $"Remove {monsterNameOrigin[i]}");
        }

        Consoles.Add("clearall", "ȭ�� ���� �÷��̾ ������ ��� ������Ʈ�� �����մϴ�");
        ConsolesOrigin.Add("clearall", "Clear All");

        Consoles.Add("clearbullet", "����ü�� ��� �����մϴ�");
        ConsolesOrigin.Add("clearbullet", "Clear Bullet");

        Consoles.Add("addexp", "����ġ�� ȹ���մϴ�");
        ConsolesOrigin.Add("addexp", "Add Exp");

        Consoles.Add("subexp", "����ġ�� ���ҽ�ŵ�ϴ�");
        ConsolesOrigin.Add("subexp", "Sub Exp");

        Consoles.Add("resetlv", "������ �ʱ�ȭ�մϴ�");
        ConsolesOrigin.Add("resetlv", "Reset Lv");

        Consoles.Add("addlv", "������ ��½�ŵ�ϴ�");
        ConsolesOrigin.Add("addlv", "Add Lv");

        Consoles.Add("sublv", "������ �϶���ŵ�ϴ�");
        ConsolesOrigin.Add("sublv", "Sub Lv");

        Consoles.Add("collisionon", "�÷��̾��� �浹�� Ȱ��ȭ�մϴ�");
        ConsolesOrigin.Add("collisionon", "Collision On");

        Consoles.Add("collisionoff", "�÷��̾��� �浹�� ��Ȱ��ȭ�մϴ�");
        ConsolesOrigin.Add("collisionoff", "Collision Off");

        Consoles.Add("setbulletspeed", "�÷��̾��� ����ü�� �ӵ��� �����մϴ�");
        ConsolesOrigin.Add("setbulletspeed", "Set BulletSpeed");

        Consoles.Add("setbulletdmg", "�÷��̾��� ����ü�� �������� �����մϴ�");
        ConsolesOrigin.Add("setbulletdmg", "Set Bullet Dmg");

        Consoles.Add("setbulletsize", "�÷��̾��� ����ü�� ũ�⸦ �����մϴ�");
        ConsolesOrigin.Add("setbulletsize", "Set Bullet Size");

        Consoles.Add("setdelay", "�÷��̾��� ���ݼӵ��� �����մϴ�");
        ConsolesOrigin.Add("setdelay", "Set Delay");

        Consoles.Add("sethp", "ü���� �����մϴ�");
        ConsolesOrigin.Add("sethp", "Set HP");

        Consoles.Add("setmaxhp", "�ִ� ü���� �����մϴ�");
        ConsolesOrigin.Add("setmaxhp", "Set MaxHP");

        Consoles.Add("setrecover", "ü������� �����մϴ�");
        ConsolesOrigin.Add("setrecover", "Set Recover");

        Consoles.Add("setspikedmg", "�浹�������� �����մϴ�");
        ConsolesOrigin.Add("setspikedmg", "Set SpikeDmg");

        Consoles.Add("setknockback", "��� ����ü�� �˹��� �����մϴ�");
        ConsolesOrigin.Add("setknockback", "Set Knockback");

        Consoles.Add("resetknockback", "��� ����ü�� �˹��� �ʱ�ȭ�մϴ�");
        ConsolesOrigin.Add("resetknockback", "Reset Knockback");

        Consoles.Add("setmagnet", "�ڷ��� �����մϴ�");
        ConsolesOrigin.Add("setmagnet", "Set Magnet");

        Consoles.Add("resetmagnet", "�ڷ��� �ʱ�ȭ�մϴ�");
        ConsolesOrigin.Add("resetmagnet", "Reset Magnet");

        Consoles.Add("createbutton", "�ֹܼ�ư�� �����մϴ�");
        ConsolesOrigin.Add("createbutton", "Create Button");

        Consoles.Add("removebuttons", "�ֹܼ�ư�� ��� �����մϴ�");
        ConsolesOrigin.Add("removebuttons", "Remove Buttons");

        foreach(KeyValuePair<string, string> cs in Consoles)
        {
            GameObject obj = Instantiate(PreviewButton, Preview.transform);
            obj.name = cs.Key;
            obj.GetComponentInChildren<TextMeshProUGUI>().text = $"{ConsolesOrigin[cs.Key]} - {cs.Value}";
            obj.GetComponent<Button>().onClick.AddListener(() => { input.text = obj.name; });
        }
    }
    public void ConsolePreview()
    {
        if (string.IsNullOrEmpty(input.text))
        {
            Preview.SetActive(false);
        }
        else
        {
            foreach (Transform child in Preview.transform)
            {
                if (child.name.IndexOf(input.text.ToLower().Replace(" ", "")) > -1 && child.name != input.text.ToLower().Replace(" ", ""))
                {
                    child.gameObject.SetActive(true);
                    Preview.SetActive(true);
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }
    public void InputConsole()
    {
        EnterConsole(input.text.ToLower().Replace(" ", ""));
    }
    public void EnterConsole(string text)
    {
        input.text = "";
        if(text.IndexOf("|")>-1)
        {
            string[] consoles = text.Split('|');
            foreach(string console in consoles)
            {
                EnterConsole(console);
            }
            return;
        }
        string[] weaponName = { "CatsEye", "Rosruc", "MagicBall", "MagicArrow", "AirStrike"};
        string[] itemNameOrigin = { "CatsEye", "Rosruc", "MagicBall", "MagicArrow", "AirStrike", "Ribbon", "MagicHat", "MagnifyingGlass", "MagicStone", "FfrozenNameTag", "AcceleratePortal", "ProtectBall", "Churu", "SpikyBall", "FragmentBlueStar" };
        string[] mobName = { "yellowexpstar", "blueexpstar", "greenexpstar", "redexpstar", "whiteexpstar" };
        string[] monsterName = { "testenemy" };

        string[] numeralKeyword = { "setiemlv", "setknockback", "addexp", "subexp", "addlv", "sublv", "setbulletspeed", "setbulletdmg", "setbulletsize", "setdelay", "sethp", "setmaxhp", "setrecover", "setspikedmg", "setmagnet" };

        foreach (KeyValuePair<string, string> cs in Consoles)
        {
            if (text.IndexOf(cs.Key) > -1)
            {
                if(text.IndexOf("createbutton") > -1)
                {
                    GameObject obj = Instantiate(ConsoleButton, InstanceConsole.transform);
                    obj.name = "instancebutton";
                    if (text.IndexOf('"')>-1)
                    {
                        string tempName = text.Split('"')[1];
                        text = text.Split('"')[0] + text.Split('"')[2];
                        obj.GetComponent<Button>().onClick.AddListener(() => { EnterConsole(text.Replace("createbutton", "")); });
                        obj.GetComponentInChildren<TextMeshProUGUI>().text = $"{tempName}";
                    }
                    else
                    {
                        obj.GetComponent<Button>().onClick.AddListener(() => { EnterConsole(text.Replace("createbutton", "")); });
                        obj.GetComponentInChildren<TextMeshProUGUI>().text = $"{text.Replace("createbutton", "")}";
                    }
                    if(Autoclose)HideConsole();
                    return;
                }
                else if(text.IndexOf("removebuttons") > -1)
                {
                    foreach(Transform buttons in InstanceConsole.transform)
                    {
                        Destroy(buttons.gameObject);
                    }
                    if(Autoclose)HideConsole();
                    return;
                }
                foreach (string nk in numeralKeyword)
                {
                    if (text.IndexOf(nk) > -1)
                    {
                        string replaceText = text.Replace(nk, "");
                        int index = -1;
                        for (int i = 0; i < replaceText.Length; i++)
                        {
                            if (int.TryParse(replaceText[i].ToString(), out int dump))
                            {
                                index = i;
                                break;
                            }
                        }
                        if (index == -1)
                        {
                            SetLog("���� �������� �ʽ��ϴ�");
                            return;
                        }
                        string keyword = "";
                        if (!int.TryParse(replaceText, out int setvalue))
                        {
                            keyword = replaceText[..index];
                            setvalue = System.Convert.ToInt32(replaceText.Replace(keyword, ""));
                        }

                        switch (nk)
                        {
                            case "setitemlv":

                                foreach (string originKeyword in itemNameOrigin)
                                {
                                    if (originKeyword.ToLower() == keyword)
                                    {
                                        keyword = originKeyword;
                                        if (System.Array.IndexOf(weaponName, keyword) > -1)
                                        {
                                            keyword = $"{keyword}Spawner";
                                        }
                                    }
                                }
                                if (GameManager.Inst.player.GetComponentInChildren(System.Type.GetType($"{keyword}")) != null)
                                {
                                    if(keyword.IndexOf("Spawner")>-1)
                                    {
                                        BulletSpawner item = GameManager.Inst.player.GetComponentInChildren(System.Type.GetType($"{keyword}")) as BulletSpawner;
                                        item.Lv = setvalue;
                                        if (Autoclose) HideConsole();
                                        return;
                                    }
                                    else
                                    {
                                        Item item = GameManager.Inst.player.transform.Find("Items").GetComponent(System.Type.GetType(keyword)) as Item;
                                        item.SetLv(setvalue);
                                        if (Autoclose) HideConsole();
                                        return;
                                    }
                                }
                                else
                                {
                                    SetLog("�ش� �������� �������� �ʽ��ϴ�");
                                    return;
                                }
                            case "setknockback":
                                GameManager.Inst.player.Knockback = setvalue;
                                if(Autoclose)HideConsole();
                                return;
                            case "addexp":
                                GameManager.Inst.player.Exp += setvalue;
                                if(Autoclose)HideConsole();
                                return;
                            case "subexp":
                                GameManager.Inst.player.Exp -= setvalue;
                                if(Autoclose)HideConsole();
                                return;
                            case "addlv":
                                for (int i = 0; i < setvalue; i++)
                                {
                                    GameManager.Inst.player.Exp += GameManager.Inst.player.maxExp;
                                }
                                if(Autoclose)HideConsole();
                                return;
                            case "sublv":
                                GameManager.Inst.player.Lv -= setvalue;
                                if(Autoclose)HideConsole();
                                return;
                            case "setbulletspeed":
                                GameManager.Inst.player.BulletSpeed -= setvalue;
                                if(Autoclose)HideConsole();
                                return;
                            case "setbulletdmg":
                                GameManager.Inst.player.BulletDmg -= setvalue;
                                if(Autoclose)HideConsole();
                                return;
                            case "setbulletsize":
                                GameManager.Inst.player.BulletSize = setvalue;
                                if(Autoclose)HideConsole();
                                return;
                            case "setdelay":
                                GameManager.Inst.player.Delay -= setvalue;
                                if(Autoclose)HideConsole();
                                return;
                            case "sethp":
                                GameManager.Inst.player.HP = setvalue;
                                if(Autoclose)HideConsole();
                                return;
                            case "setmaxhp":
                                GameManager.Inst.player.maxhp = setvalue;
                                if(Autoclose)HideConsole();
                                return;
                            case "setrecover":
                                GameManager.Inst.player.recover = setvalue;
                                if(Autoclose)HideConsole();
                                return;
                            case "setspikedmg":
                                GameManager.Inst.player.SpikeDmg = setvalue;
                                if(Autoclose)HideConsole();
                                return;
                            case "setmagnet":
                                GameManager.Inst.player.GetComponentInChildren<FragmentBlueStar>().magnet.radius = setvalue;
                                if(Autoclose)HideConsole();
                                return;

                        }
                    }
                }
                if (text.IndexOf("increase") > -1)
                {
                    string keyword = text.Replace("increase", "");
                    foreach (string originKeyword in itemNameOrigin)
                    {
                        if (originKeyword.ToLower() == keyword)
                        {
                            keyword = originKeyword;
                            if (System.Array.IndexOf(weaponName, keyword) > -1)
                            {
                                keyword = $"{keyword}Spawner";
                            }
                        }
                    }
                    if (GameManager.Inst.player.GetComponentInChildren(System.Type.GetType($"{keyword}")) != null)
                    {
                        if (keyword.IndexOf("Spawner") > -1)
                        {
                            BulletSpawner item = GameManager.Inst.player.GetComponentInChildren(System.Type.GetType($"{keyword}")) as BulletSpawner;
                            item.Lv++;
                            if (Autoclose) HideConsole();
                            return;
                        }
                        else
                        {
                            Item item = GameManager.Inst.player.transform.Find("Items").GetComponent(System.Type.GetType(keyword)) as Item;
                            item.AddLv();
                            if (Autoclose) HideConsole();
                            return;
                        }
                    }
                    else
                    {
                        SetLog("�ش� �������� �������� �ʽ��ϴ�");
                        return;
                    }
                }
                if (text.IndexOf("decrease") > -1)
                {
                    string keyword = text.Replace("decrease", "");
                    foreach (string originKeyword in itemNameOrigin)
                    {
                        if (originKeyword.ToLower() == keyword)
                        {
                            keyword = originKeyword;
                            if (System.Array.IndexOf(weaponName, keyword) > -1)
                            {
                                keyword = $"{keyword}Spawner";
                            }
                        }
                    }
                    if (GameManager.Inst.player.GetComponentInChildren(System.Type.GetType($"{keyword}")) != null)
                    {
                        if (GameManager.Inst.player.GetComponentInChildren(System.Type.GetType($"{keyword}")) != null)
                        {
                            if (keyword.IndexOf("Spawner") > -1)
                            {
                                BulletSpawner item = GameManager.Inst.player.GetComponentInChildren(System.Type.GetType($"{keyword}")) as BulletSpawner;
                                item.Lv--;
                                if (Autoclose) HideConsole();
                                return;
                            }
                            else
                            {
                                Item item = GameManager.Inst.player.transform.Find("Items").GetComponent(System.Type.GetType(keyword)) as Item;
                                item.SubLv();
                                if (Autoclose) HideConsole();
                                return;
                            }
                        }
                    }
                    else
                    {
                        SetLog("�ش� �������� �������� �ʽ��ϴ�");
                        return;
                    }
                }
                if (text.IndexOf("enable") > -1)
                {
                    string keyword = text.Replace("enable", "");
                    foreach (string originKeyword in itemNameOrigin)
                    {
                        if (originKeyword.ToLower() == keyword)
                        {
                            keyword = originKeyword;
                            if (System.Array.IndexOf(weaponName, keyword) > -1)
                            {
                                keyword = $"{keyword}Spawner";
                            }
                        }
                    }

                    if (GameManager.Inst.player.GetComponentInChildren(System.Type.GetType($"{keyword}")) != null)
                    {
                        if (keyword.IndexOf("Spawner") > -1)
                        {
                            BulletSpawner item = GameManager.Inst.player.GetComponentInChildren(System.Type.GetType($"{keyword}")) as BulletSpawner;
                            item.enabled = true;
                            if (Autoclose) HideConsole();
                            return;
                        }
                        else
                        {
                            Item item = GameManager.Inst.player.transform.Find("Items").GetComponent(System.Type.GetType(keyword)) as Item;
                            item.enabled = true;
                            if (Autoclose) HideConsole();
                            return;
                        }
                    }
                    else
                    {
                        SetLog("�ش� �������� �������� �ʽ��ϴ�");
                        return;
                    }
                }
                if (text.IndexOf("disable") > -1)
                {
                    string keyword = text.Replace("disable", "");
                    foreach (string originKeyword in itemNameOrigin)
                    {
                        if (originKeyword.ToLower() == keyword)
                        {
                            keyword = originKeyword;
                            if (System.Array.IndexOf(weaponName, keyword) > -1)
                            {
                                keyword = $"{keyword}Spawner";
                            }
                        }
                    }
                    if (GameManager.Inst.player.GetComponentInChildren(System.Type.GetType($"{keyword}")) != null)
                    {
                        if(keyword.IndexOf("Spawner")>-1)
                        {
                            BulletSpawner item = GameManager.Inst.player.GetComponentInChildren(System.Type.GetType($"{keyword}")) as BulletSpawner;
                            item.enabled = false;
                            if (Autoclose) HideConsole();
                            return;
                        }
                        else
                        {
                            Item item = GameManager.Inst.player.transform.Find("Items").GetComponent(System.Type.GetType(keyword)) as Item;
                            item.enabled = false;
                            if (Autoclose) HideConsole();
                            return;
                        }
                    }
                    else
                    {
                        SetLog("�ش� �������� �������� �ʽ��ϴ�");
                        return;
                    }
                }
                if (text.IndexOf("evolution") > -1)
                {
                    string keyword = text.Replace("evolution", "");
                    foreach (string originKeyword in itemNameOrigin)
                    {
                        if (originKeyword.ToLower() == keyword)
                        {
                            keyword = originKeyword;
                            if (System.Array.IndexOf(weaponName, keyword) > -1)
                            {
                                keyword = $"{keyword}Spawner";
                            }
                        }
                    }
                    if (GameManager.Inst.player.GetComponentInChildren(System.Type.GetType($"{keyword}")) != null)
                    {
                        if (keyword.IndexOf("Spawner") > -1)
                        {
                            BulletSpawner item = GameManager.Inst.player.GetComponentInChildren(System.Type.GetType($"{keyword}")) as BulletSpawner;
                            item.Evo = true;
                            if (Autoclose) HideConsole();
                            return;
                        }
                        else
                        {
                            SetLog("�ش� �������� ��ȭ�� �� �����ϴ�");
                            return;
                        }
                    }
                    else
                    {
                        SetLog("�ش� �������� �������� �ʽ��ϴ�");
                        return;
                    }
                }
                if (text.IndexOf("devolution") > -1)
                {
                    string keyword = text.Replace("devolution", "");
                    foreach (string originKeyword in itemNameOrigin)
                    {
                        if (originKeyword.ToLower() == keyword)
                        {
                            keyword = originKeyword;
                            if (System.Array.IndexOf(weaponName, keyword) > -1)
                            {
                                keyword = $"{keyword}Spawner";
                            }
                        }
                    }
                    if (GameManager.Inst.player.GetComponentInChildren(System.Type.GetType($"{keyword}")) != null)
                    {
                        if (keyword.IndexOf("Spawner") > -1)
                        {
                            BulletSpawner item = GameManager.Inst.player.GetComponentInChildren(System.Type.GetType($"{keyword}")) as BulletSpawner;
                            item.Evo = false;
                            if (Autoclose) HideConsole();
                            return;
                        }
                        else
                        {
                            SetLog("�ش� �������� ��ȭ�� �� �����ϴ�");
                            return;
                        }
                    }
                    else
                    {
                        SetLog("�ش� �������� �������� �ʽ��ϴ�");
                        return;
                    }
                }
                if (text.IndexOf("spawn") > -1)
                {
                    string keyword = text.Replace("spawn", "");
                    if (System.Array.IndexOf(mobName, keyword) > -1)
                    {
                        GameObject obj = Instantiate(GameManager.Inst.Drops[keyword]);
                        obj.transform.position = new(Random.Range(-2, 3), 4, 0);
                        obj.name = keyword;
                        if(Autoclose)HideConsole();
                    }
                    else if (System.Array.IndexOf(monsterName, keyword) > -1)
                    {
                        GameManager.Inst.enemyManager.EnemySpawn(keyword);
                        if(Autoclose)HideConsole();
                    }
                    else
                    {
                        SetLog("�ش� ������Ʈ�� �������� �ʽ��ϴ�");
                        return;
                    }
                }
                if (text.IndexOf("kill") > -1)
                {
                    string keyword = text.Replace("kill", "");
                    if (System.Array.IndexOf(monsterName, keyword) > -1)
                    {
                        GameManager.Inst.enemyManager.KillEnemies(keyword);
                        if(Autoclose)HideConsole();
                    }
                    else
                    {
                        SetLog("�ش� ����� �������� �ʽ��ϴ�");
                        return;
                    }
                }
                if (text.IndexOf("remove") > -1)
                {
                    string keyword = text.Replace("remove", "");
                    if (System.Array.IndexOf(mobName, keyword) > -1)
                    {
                        Item[] drops = GetComponents<Item>();
                        foreach (Item drop in drops)
                        {
                            if (drop.transform.parent == null)
                            {
                                Destroy(drop);
                            }
                        }
                        if(Autoclose)HideConsole();
                        return;
                    }
                    else if (System.Array.IndexOf(monsterName, keyword) > -1)
                    {
                        GameManager.Inst.enemyManager.RemoveEnemies(keyword);
                        if(Autoclose)HideConsole();
                        return;
                    }
                    else
                    {
                        SetLog("�ش� ������Ʈ�� �������� �ʽ��ϴ�");
                        return;
                    }
                }
                if (text.IndexOf("collisionoff") > -1)
                {
                    GameManager.Inst.player.GetComponent<BoxCollider2D>().enabled = false;
                    if(Autoclose)HideConsole();
                    return;
                }
                if (text.IndexOf("collisionon") > -1)
                {
                    GameManager.Inst.player.GetComponent<BoxCollider2D>().enabled = true;
                    if(Autoclose)HideConsole();
                    return;
                }
                if (text.IndexOf("resetknockback") > -1)
                {
                    GameManager.Inst.player.Knockback = 0;
                    if(Autoclose)HideConsole();
                    return;
                }
                if (text.IndexOf("clearall") > -1)
                {
                    Bullet[] bullets = FindObjectsOfType<Bullet>();
                    Enemy[] enemies = FindObjectsOfType<Enemy>();
                    Item[] drops = FindObjectsOfType<Item>();

                    foreach (Bullet bullet in bullets)
                    {
                        bullet.ReturnObject();
                    }
                    foreach (Enemy enemy in enemies)
                    {
                        Destroy(enemy.gameObject);
                    }
                    foreach (Item drop in drops)
                    {
                        if (drop.transform.parent == null)
                        {
                            Destroy(drop.gameObject);
                        }
                    }
                    if(Autoclose)HideConsole();
                    return;
                }
                if (text.IndexOf("clearbullet") > -1)
                {
                    Bullet[] bullets = FindObjectsOfType<Bullet>();
                    foreach (Bullet bullet in bullets)
                    {
                        bullet.ReturnObject();
                    }
                    if(Autoclose)HideConsole();
                    return;
                }
                if (text.IndexOf("resetmagnet") > -1)
                {
                    GameManager.Inst.player.GetComponentInChildren<FragmentBlueStar>().magnet.radius = 0.6f;
                    GameManager.Inst.player.GetComponentInChildren<FragmentBlueStar>().Lv = GameManager.Inst.player.GetComponentInChildren<FragmentBlueStar>().Lv;
                }
            }
        }
        SetLog("�ش� �ڵ尡 �������� �ʽ��ϴ�");
    }
    private void SetLog(string text)
    {
        Log.gameObject.SetActive(true);
        Log.text = text;
        logTimer = 1.5f;
    }
}
