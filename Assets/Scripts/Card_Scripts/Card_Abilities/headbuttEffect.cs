using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headbuttEffect : MonoBehaviour
{
    //Get enemy portrait for charge
    private Transform enemyPortraitPos;
    public float chargeSpeed = 50f;
    private bool canCharge = false;
    public float lifeTime;

    void Start()
    {
        enemyPortraitPos = GameObject.FindGameObjectWithTag("Player2Portrait").transform;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;

        float step = chargeSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, enemyPortraitPos.position, step);

        Vector3 targetDirection = enemyPortraitPos.position - transform.position;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);

        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
