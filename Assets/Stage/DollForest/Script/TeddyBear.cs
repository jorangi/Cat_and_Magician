using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyBear : Enemy
{
    public GameObject bottleCap;
    private WaitForSeconds wait = new(3.0f);
    private WaitForSeconds delay = new(2.0f);
    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(Sway());
        StartCoroutine(Shoot());
    }
    private IEnumerator Shoot()
    {
        GameObject obj =Instantiate(bottleCap);
        obj.transform.position = transform.position;
        yield return delay;
        StartCoroutine(Shoot());
    }
    private IEnumerator Sway()
    {
        yield return wait;
        float x = Random.Range(-2.55f, 2.55f);
        while(Mathf.Abs(transform.position.x - x) > 0.01f)
        {
            transform.position = new(Mathf.Lerp(transform.position.x, x, Time.deltaTime * 5), transform.position.y);
        }
        transform.position = new(x, transform.position.y);
        StartCoroutine(Sway());
    }
}
