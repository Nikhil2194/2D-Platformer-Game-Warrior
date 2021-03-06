using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("Spikehead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private float checkTimer;
    private Vector3 destination;    //when spike detect the position  of player it will store here

    private bool attacking;

    private Vector3[]  directions = new Vector3[4];

    private void OnEnable()  //gets called every as object is activated
    {
        Stop();
    }


    private void Update()
    {

        //Move spikehead to destination only if attacking
        if (attacking)
        transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();

        //check if spikehead sees player in all 4 directions
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);//detecting the player

            if(hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }

        }
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range;   //right  direction
        directions[1] = -transform.right * range;   // left direction
        directions[2] = transform.up * range;   //upward  direction
        directions[3] = -transform.up * range;   //downward direction
    }

    private void Stop()
    {
        destination = transform.position;  //set destination as current position so its doesnt move
        attacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Stop(); //stop spikehead once it hits something

    }
}
