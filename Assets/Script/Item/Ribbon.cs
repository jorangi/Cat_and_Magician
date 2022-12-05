using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ribbon : Item
{
    private float[] val = { 10, 20, 30, 40, 50};
    private int lv;
    public int Lv
    {
        get => lv;
        set
        {
            enabled = true;
            value = Mathf.Clamp(value, 1, val.Length);
            GameManager.Inst.player.maxhp += val[value-1];
            GameManager.Inst.player.HP += val[value-1];
            if (lv > 0)
            {
                GameManager.Inst.player.maxhp -= val[lv - 1];
            }
            lv = value;
            GameManager.Inst.player.itemLevels["Ribbon"] = value;
        }
    }
    public override void SetLv(int lv)
    {
        Lv = lv;
    }
    public override void AddLv()
    {
        Lv++;
    }
    public override void SubLv()
    {
        Lv--;
    }
    private void OnEnable()
    {
        if (itemEnabled)
        {
            GameManager.Inst.player.maxhp += val[Lv-1];
            GameManager.Inst.player.HP += val[Lv-1];
        }
        itemEnabled = true;
    }
    private void OnDisable()
    {
        if (GameManager.Inst == null)
            return;
        GameManager.Inst.player.maxhp -= val[Lv - 1];
    }
}
