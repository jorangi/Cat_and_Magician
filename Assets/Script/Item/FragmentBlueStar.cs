using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentBlueStar : Item
{
    public CircleCollider2D magnet;
    private float[] val = { 0.2f, 0.4f, 0.6f, 0.8f, 1f };
    private int lv;
    public int Lv
    {
        get => lv;
        set
        {
            enabled = true;
            value = Mathf.Clamp(value, 1, val.Length);
            magnet.radius += val[value-1];
            if (lv > 0)
            {
                magnet.radius -= val[lv - 1];
            }
            lv = value;
            GameManager.Inst.player.itemLevels["FragmentBlueStar"] = value;
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
        magnet.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        if (GameManager.Inst == null)
            return;
        magnet.gameObject.SetActive(false);
    }
}
