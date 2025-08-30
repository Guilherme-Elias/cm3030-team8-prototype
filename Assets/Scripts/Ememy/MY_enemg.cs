using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public enum mstate
    {
        XunLuo,  // patrol
        ZhuiZhu, // chase
        GongJi, // attack

    }

public class zhauntai   
{
    public static bool iskuangbao = false; 
}

public class MY_enemg : MonoBehaviour {



    public mstate my_state = mstate.XunLuo;  // default state is patrol

    Quaternion b = new Quaternion(0, 0, 0, 0);

    public Animator m_ani;

    NavMeshAgent m_agent;

    public GameObject m_player;

    float m_movSpeed = 1.5f;

    public GameObject[] point;
    bool IsShoot=true;

    int index = 0;
    public float hp = 0.34f;
    public GameObject hit;
    public AudioSource Hit_source;
    public Image hpima;
    private bool isaud = false;
    // Use this for initialization
    void Start () {


        m_agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
      //  m_agent.speed = m_movSpeed;

        m_player = GameObject.FindGameObjectWithTag("Player");
        
    }
    public void Shoot()
    {
        GameObject.Find("PlayerManager").GetComponent<PlayerAction>().TakeDamage(10);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="bullet")
        {
           // Hit_source.Play();

           

            GameObject Hit = GameObject.Instantiate(hit, other.gameObject.transform.position, b) as GameObject;
            Hit.SetActive(true);
            Hit.transform.parent = gameObject.transform;
            Destroy(hit.gameObject, 1f);
            Damage();
        }
    }
    // Update is called once per frame
    void Update () {
        m_player = GameObject.FindGameObjectWithTag("Player");
        switch (my_state)
        {

            case mstate.XunLuo:
                m_ani.SetBool("run", false);

                m_ani.SetBool("attack", false);
              //  m_agent.SetDestination(point[index].transform.position);

                if (Vector3.Distance(transform.position, m_player.transform.position) < 20f)
                {
                    my_state = mstate.ZhuiZhu; // change to chase state
                    break;
                }

                /*
                if (Vector3.Distance(transform.position, point[index].transform.position) <1)
                {
                    index++;

                    if (index == point.Length)
                    {
                        index = 0;
                    }
                    m_agent.SetDestination(point[index].transform.position);

                }
                */

                break;
            case mstate.ZhuiZhu: // chase
                if(IsShoot==false)
                {
                    IsShoot = true;
                    CancelInvoke("Shoot");
                }
                if(!isaud)
                {
                    isaud = true;
                    gameObject.GetComponent<AudioSource>().Play();
                }
                m_ani.SetBool("run", true);
                m_ani.SetBool("attack", false);
                m_agent.SetDestination(m_player.transform.position);



                if (Vector3.Distance(transform.position, m_player.transform.position) <2)
                {
                   
                    my_state = mstate.GongJi;  // change to attack state
                    m_agent.ResetPath();
                }

                break;
            case mstate.GongJi: // attack
                if(IsShoot)
                {
                    IsShoot = false;
                    InvokeRepeating("Shoot", 1.2f, 3);
                }
                m_agent.ResetPath();
                m_ani.SetBool("attack", true);
                RotateTo();

                if (Vector3.Distance(transform.position, m_player.transform.position) >300)
                {
                    my_state = mstate.XunLuo; // change to patrol state
                }
                if (Vector3.Distance(transform.position, m_player.transform.position) >2)
                {
                    my_state = mstate.ZhuiZhu; // change to chase state
                }

                break;

        }
    }


    void RotateTo() // rotate to the player
    {
        Vector3 targetdir = m_player.transform.position - transform.position;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetdir, 5 * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDir);
    }


    public void Damage() // damage the enemy    (when the player shoots the enemy)    
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        if (hpima.fillAmount <= 0)
        {
            return;
        }
        GameObject Hit = GameObject.Instantiate(hit, hit.gameObject.transform.position, b) as GameObject;
        Hit.SetActive(true);
        Hit.transform.parent =null;
        Destroy(Hit.gameObject, 1f);

        hpima.fillAmount -= hp;
        if (hpima.fillAmount <= 0)
        {
            Destroy(gameObject);
        }

      
    }
}
