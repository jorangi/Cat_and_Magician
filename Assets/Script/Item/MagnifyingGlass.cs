using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingGlass : Item
{
    private float[] val = { 0.2f, 0.4f, 0.6f, 0.8f, 1f};
    private int lv;
    public int Lv
    {
        get => lv;
        set
        {
            enabled = true;
            value = Mathf.Clamp(value, 1, val.Length);
            GameManager.Inst.player.BulletSize += val[value-1];
            if (lv > 0)
            {
                GameManager.Inst.player.BulletSize -= val[lv - 1];
            }
            lv = value;
            GameManager.Inst.player.itemLevels["MagnifyingGlass"] = value;
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
            GameManager.Inst.player.BulletSize += val[Lv-1];
        }
        itemEnabled = true;
    }
    private void OnDisable()
    {
        if (GameManager.Inst == null)
            return;
        GameManager.Inst.player.BulletSize -= val[Lv-1];
    }
}
