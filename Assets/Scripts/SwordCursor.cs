using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCursor : MonoBehaviour
{

    [Header("Components")]
    public GameObject particleObject;
    public ParticleSystem hitParticles;
    public Collider2D gameCollider;
    public GameManager gameManager;
    protected private AudioSource audioSrc;

    [Header("Game Vars")]
    public bool mouseDown;
    public float cooldownDur;
    float cooldown;
    public AudioClip[] swooshSounds;

    [Header("Combo Vars")]
    public int comboPoints;
    public int comboMultiplier;

    private float comboCooldown;
    public float comboDur;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSrc = GetComponent<AudioSource>();

    }

    public void newGame()
    {
        comboPoints = 0;
        comboMultiplier = 1;
        comboCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x,mousePos.y,0);

        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if( mouseDown)
        {
            selfActive(true);
        }
        else
        {
            selfActive(false);
        }

        if(comboCooldown > 0)
        {
            comboCooldown -= Time.deltaTime;
        }

        if (comboCooldown <= 0)
        {
            comboPoints = 0;
            comboMultiplier = 1;
            gameManager.uiUpdate();

        }

    }

    private void selfActive(bool active)
    {
        gameCollider.enabled = active;
        particleObject.SetActive(active);
    }

    private void OnMouseDown()
    {
        mouseDown = true;
        comboMultiplier = 1;
        gameManager.uiUpdate();
    }
    private void OnMouseUp()
    {
        mouseDown = false;
        comboPoints = 0;
        gameManager.uiUpdate();
    }

    IEnumerator particleOff()
    {
        particleObject.transform.parent = null;
        yield return new WaitForSeconds(0.2f);
        particleObject.SetActive(false);
        particleObject.transform.SetParent(transform);
        particleObject.transform.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (mouseDown)
        {
            
            if (collision.CompareTag("Fruit"))
            {
                if (cooldown <= 0)
                {
                    

                    cooldown = cooldownDur;

                    if (collision.GetComponent<Fruit>().sliced == false)
                    {
                        AudioClip clip = swooshSounds[randomChoice(swooshSounds.Length)];
                        audioSrc.PlayOneShot(clip);
                    }
                    collision.GetComponent<Fruit>().damageFruit();


                }
            }
            if (collision.CompareTag("Bomb"))
            {
                collision.GetComponent<Bomb>().sliceBomb();
            }
        }
    }

    public void comboHandler()
    {
        comboCooldown = comboDur;
        comboPoints++;
        gameManager.uiUpdate();

        if (comboPoints >= 3)
        {
            gameManager.comboObject.GetComponent<Animator>().SetTrigger("ComboHit");
            comboMultiplier += 1;
        }
    }
    public int randomChoice(int length)
    {
        return Random.Range(0, length);
    }
    public void psBurst()
    {
        ParticleSystem ps = Instantiate(hitParticles,transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        ps.Stop();
        ps.Play();
        Destroy(ps.gameObject, 1.5f);
    }
}
