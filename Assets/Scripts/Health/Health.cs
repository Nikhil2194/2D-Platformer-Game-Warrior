using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOflashes;
    [SerializeField] SpriteRenderer spriteRend;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerablity());
        }
        else
        {
            if (!dead)
            { 
            anim.SetTrigger("die");


                // Player 
                if(GetComponent<PlayerMovemnet>() != null)
                  GetComponent<PlayerMovemnet>().enabled = false;


                //Enemy
                if (GetComponentInParent<EnemyPatrol>() != null)
                GetComponentInParent<EnemyPatrol>().enabled = false;

                if(GetComponent<MeleeEnemy>() != null)
                GetComponent<MeleeEnemy>().enabled = false;

                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
        }
    }

       public void AddHealth(float _value)
        {
            currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
        }

    private IEnumerator Invunerablity()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOflashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOflashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOflashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    
}
