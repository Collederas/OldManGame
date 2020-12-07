using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyEnemyController : EnemyController
{
    public Bullet bullet;
    public float shootingInterval;
    public float shootingSpeed;

    private GameObject player;
    private bool bCanShoot;

    protected override void Start()
    {
        currentState = State.Attack;
        bCanShoot = true;
        player = GameObject.FindGameObjectWithTag("Player");
        ChangeState(currentState);
    }
    protected override void AttackState()
    {
        if (bCanShoot)
        {   
            StartCoroutine(ResetCanShoot());
            var obj = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            obj.Target = player.transform.transform.position;
            obj.Speed = shootingSpeed;
        }
    }

    IEnumerator ResetCanShoot()
    {
        var time = 0f;

        while (time < shootingInterval)
        {
            time += Time.fixedDeltaTime;
            bCanShoot = false;
            yield return null;
        }
        bCanShoot = true;
    }
}
