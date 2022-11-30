using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStone : Item
{
    private float[] val = { 0.3f, 0.6f, 0.9f, 1.2f, 1.5f};
    private int lv;
    public int Lv
    {
        get => lv;
        set
        {
            enabled = true;
            value = Mathf.Clamp(value, 1, val.Length);
            GameManager.Inst.player.Delay += val[value-1];
            if (lv > 0)
            {
                GameManager.Inst.player.Delay -= val[lv - 1];
            }
            lv = value;
            GameManager.Inst.player.itemLevels["MagicStone"] = value;
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
            GameManager.Inst.player.Delay += val[Lv-1];
        }
        itemEnabled = true;
    }
    private void OnDisable()
    {
        if (GameManager.Inst == null)
            return;
        GameManager.Inst.player.Delay -= val[Lv-1];
    }
}
