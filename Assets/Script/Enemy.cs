using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float hp;
    public float HP
    {
        get => hp;
        set
        {
            value = Mathf.Clamp(value, 0, maxhp);
            hp = value;
            if(hp == 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public float maxhp;

    private void Awake()
    {
        maxhp = 100;
        HP = 100;
    }
}
