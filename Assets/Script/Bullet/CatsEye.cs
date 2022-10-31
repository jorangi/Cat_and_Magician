using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatsEye : Bullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Remove"))
        {
            ReturnObject();
        }
        else if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().HP -= dmg;
            ReturnObject();
        }
    }
}
