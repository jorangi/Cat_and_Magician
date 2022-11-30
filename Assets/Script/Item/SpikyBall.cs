using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikyBall : Item
{
    private float[] val = { 10, 15, 25, 40, 60};
    private int lv;
    public int Lv
    {
        get => lv;
        set
        {
            enabled = true;
            value = Mathf.Clamp(value, 1, val.Length);
            GameManager.Inst.player.SpikeDmg += val[value-1];
            if (lv > 0)
            {
                GameManager.Inst.player.SpikeDmg -= val[lv - 1];
            }
            lv = value;
            GameManager.Inst.player.itemLevels["SpikyBall"] = value;
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
        if(itemEnabled)
        {
            GameManager.Inst.player.SpikeDmg += val[Lv-1];
        }
        itemEnabled = true;
    }
    private void OnDisable()
    {
        if (GameManager.Inst == null)
            return;
        GameManager.Inst.player.SpikeDmg -= val[Lv-1];
    }
}
