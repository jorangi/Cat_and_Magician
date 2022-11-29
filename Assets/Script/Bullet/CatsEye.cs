using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CatsEye : Bullet
{
    protected override void Awake()
    {
        base.Awake();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Remove"))
        {
            ReturnObject();
        }
        else if (collision.CompareTag("Enemy"))
        {
            collision.transform.Translate(Vector2.up * knockback);
            collision.GetComponent<Enemy>().HP -= dmg;
            ReturnObject();
        }
    }
}
