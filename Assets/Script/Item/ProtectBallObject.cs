using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectBallObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }
}
