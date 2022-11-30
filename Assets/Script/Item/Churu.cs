using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Churu : Item
{
    private float[] val = { 0.1f, 0.2f, 0.35f, 0.5f, 0.8f};
    private int lv;
    public int Lv
    {
        get => lv;
        set
        {
            enabled = true;
            value = Mathf.Clamp(value, 1, val.Length);
            GameManager.Inst.player.recover += val[value-1];
            if (lv > 0)
            {
                GameManager.Inst.player.recover -= val[lv - 1];
            }
            lv = value;
            GameManager.Inst.player.itemLevels["Churu"] = value;
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
            GameManager.Inst.player.recover += val[Lv-1];
        }
        itemEnabled = true;
    }
    private void OnDisable()
    {
        if (GameManager.Inst == null)
            return;
        GameManager.Inst.player.recover -= val[Lv-1];
    }
}
