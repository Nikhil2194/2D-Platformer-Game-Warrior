using UnityEngine;
using System.Collections;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;   //when the traps gets triggered
    private bool active;   //when the traps is active and can hurt the player

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
            {
                StartCoroutine(ActivateFireTrap());
            }

            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
        
    }

    private IEnumerator ActivateFireTrap()
    {
        //turen the sprite red to notify the player
        triggered = true;
        spriteRend.color = Color.red;  

        //wait for delay, activate trap, turn on animation, return color back to normal
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white;   //turen the sprite back to initial color
        active = true;
        anim.SetBool("activated", true);

        //wait unitl X seconds, deactivate trap and rest all variables and animator
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
