using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum ZombieState
{
    Patrol,
    Chase,
    Attack, 
}

//public class zhauntai   
//{
//    public static bool iskuangbao = false; 
//}

public class MY_enemg : MonoBehaviour {

    public ZombieState entityState = ZombieState.Patrol;  // default state is patrol

    Quaternion b = new Quaternion(0, 0, 0, 0);

    public Animator entityAnimator;

    NavMeshAgent entity;

    public GameObject[] point;
    bool isAttacking=true;

    public float hp = 0.34f;
    public GameObject hit;
    public AudioSource Hit_source;
    public Image healthbarImage;
    private bool isaud = false;

    private GameObject playerEntity;

    // Use this for initialization
    void Start () {
        entity = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerEntity = GameObject.FindGameObjectWithTag("Player");
    }

    public void Shoot()
    {
        GameObject.Find("PlayerManager").GetComponent<PlayerAction>().TakeDamage(10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "bullet")
        {
            GameObject Hit = GameObject.Instantiate(hit, other.gameObject.transform.position, b) as GameObject;
            Hit.SetActive(true);
            Hit.transform.parent = gameObject.transform;
            Destroy(hit.gameObject, 1f);
            Damage();
        }
    }

    // Update is called once per frame
    void Update () {
        playerEntity = GameObject.FindGameObjectWithTag("Player");

        switch (entityState)
        {
            case ZombieState.Patrol:
                entityAnimator.SetBool("run", false);

                entityAnimator.SetBool("attack", false);

                if (Vector3.Distance(transform.position, playerEntity.transform.position) < 20f)
                {
                    entityState = ZombieState.Chase; // change to chase state
                    break;
                }

                break;
            
            case ZombieState.Chase: // chase
                if(isAttacking==false)
                {
                    isAttacking = true;
                    CancelInvoke("Shoot");
                }

                if(!isaud)
                {
                    isaud = true;
                    gameObject.GetComponent<AudioSource>().Play();
                }

                entityAnimator.SetBool("run", true);
                entityAnimator.SetBool("attack", false);
                entity.SetDestination(playerEntity.transform.position);

                if (Vector3.Distance(transform.position, playerEntity.transform.position) <2)
                {
                   
                    entityState = ZombieState.Attack;  // change to attack state
                    entity.ResetPath();
                }

                break;

            case ZombieState.Attack: // attack
                if(isAttacking)
                {
                    isAttacking = false;
                    InvokeRepeating("Shoot", 1.2f, 3);
                }
                entity.ResetPath();
                entityAnimator.SetBool("attack", true);
                RotateTo();

                if (Vector3.Distance(transform.position, playerEntity.transform.position) >300)
                {
                    entityState = ZombieState.Patrol; // change to patrol state
                }
                if (Vector3.Distance(transform.position, playerEntity.transform.position) >2)
                {
                    entityState = ZombieState.Chase; // change to chase state
                }

                break;
        }
    }

    void RotateTo() // rotate to the player
    {
        Vector3 targetdir = playerEntity.transform.position - transform.position;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetdir, 5 * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDir);
    }

    public void Damage() // damage the enemy    (when the player shoots the enemy)    
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        if (healthbarImage.fillAmount <= 0)
        {
            return;
        }
        GameObject Hit = GameObject.Instantiate(hit, hit.gameObject.transform.position, b) as GameObject;
        Hit.SetActive(true);
        Hit.transform.parent =null;
        Destroy(Hit.gameObject, 1f);

        healthbarImage.fillAmount -= hp;
        if (healthbarImage.fillAmount <= 0)
        {
            Destroy(gameObject);
        }

    }
}
