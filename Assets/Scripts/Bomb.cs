using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float lifeTime;

    [Header("Components")]
    private Rigidbody2D rig;
    public ParticleSystem ps;
    public ParticleSystem ex_ps;
    private Animator anim;
    private SpriteRenderer sr;
    private GameManager manager;
    private AudioSource audioSrc;
    public AudioClip explosionSound;
    [Header("Physics Vars")]
    public float minVelx;
    public float maxVelx;
    public float minVely;
    public float maxVely;



    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        manager = FindObjectOfType<GameManager>();
        ex_ps.gameObject.SetActive(false); 
        audioSrc = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        float velY = Random.Range(minVely, maxVely);

       
        if (transform.position.x < -2)
        {
            minVelx = 5;
            maxVelx = 8;
        }
        else if (transform.position.x > 2)
        {
            minVelx = -8;
            maxVelx = -5;
        }
        else
        {
            minVelx = -7;
            maxVelx = 7;
        }
        
        float velX = Random.Range(minVelx, maxVelx);
        rig.AddForce(new Vector2(velX, velY), ForceMode2D.Impulse);
        rig.angularVelocity = ((velX * velY) / 2) * 10;
     
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -15)
        {
            Destroy(gameObject);
        }
    }
    public void sliceBomb()
    {
        audioSrc.PlayOneShot(explosionSound);
        manager.gameOver();
        Collider2D thisCol = GetComponent<Collider2D>();
        Destroy(thisCol);
        Destroy(sr);
        psBurst();
        Destroy(gameObject,2f);
    }


    public void psBurst()
    {
        rig.angularVelocity = 0f;
        rig.velocity = Vector2.zero;
        ex_ps.gameObject.SetActive(true);
        ex_ps.Stop();
        ex_ps.Play();
    }
    public int randomChoice(int length)
    {
        return Random.Range(0, length);
    }
}
