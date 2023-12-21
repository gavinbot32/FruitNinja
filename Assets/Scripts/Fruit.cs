using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int health;
    public int bounty;
    public Sprite[] spriteList;
    public Sprite nextSprite;
    private bool secondGen;
    [Header("Components")]
    private Rigidbody2D rig;
    public ParticleSystem ps;
    private Animator anim;
    private SpriteRenderer sr;
    [Header("Physics Vars")]
    public float minVelx;
    public float maxVelx;
    public float minVely;
    public float maxVely;



    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (secondGen) {

            minVelx = -5;
            maxVelx = 5;
            minVely = 0;
            maxVely = 5;
        
        } 
        else 
        { 
            if (transform.position.x < -2)
            {
                minVelx = 0;
                maxVelx = 10;
            }
            else if (transform.position.x > 2)
            {
                minVelx = 0;
                maxVelx = -10;
            }
            else
            {
                minVelx = -5;
                maxVelx = 5;
            }
        }
        float velX = Random.Range(minVelx, maxVelx);
        float velY = Random.Range(minVely, maxVely);
        rig.AddForce(new Vector2(velX,velY),ForceMode2D.Impulse);
        rig.angularVelocity = ((velX * velY)/2)*10;
        print((velX,velY));
    }

    private void Update()
    {
        if (transform.position.y <= -15)
        {
            Destroy(gameObject);
        }
    }

    public void initialize(Sprite sprite, Sprite thenextSprite)
    { 
        secondGen = true;
        sr.sprite = sprite;
        nextSprite = thenextSprite;
    }

    public void sliceFruit()
    {
        psBurst();
        if (!secondGen)
        {
            Instantiate(this.gameObject, transform.position, Quaternion.identity).GetComponent<Fruit>().initialize(nextSprite, spriteList[1]);
        }
        Destroy(gameObject);
    }


    public void psBurst()
    {
        ps.Stop();
        ps.Play();
    }
}
