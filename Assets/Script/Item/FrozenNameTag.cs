using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenNameTag : Item
{
    private float[] val = { 0.7f, 2f, 3.6f, 6.5f, 9f};
    private int lv;
    public int Lv
    {
        get => lv;
        set
        {
            enabled = true;
            value = Mathf.Clamp(value, 1, val.Length);
            GameManager.Inst.player.BulletSpeed -= val[value-1];
            if (lv > 0)
            {
                GameManager.Inst.player.BulletSpeed -= val[lv - 1];
            }
            lv = value;
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
            GameManager.Inst.player.BulletSpeed -= val[Lv-1];
        }
        itemEnabled = true;
    }
    private void OnDisable()
    {
        if (GameManager.Inst == null)
            return;
        GameManager.Inst.player.BulletSpeed += val[Lv-1];
    }
}
