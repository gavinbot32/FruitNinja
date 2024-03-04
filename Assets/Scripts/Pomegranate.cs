using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pomegranate : Fruit
{
    public float abilitySeconds;

    public GameObject shineObj;


    public override void sliceFruit()
    {
        sliced = true;
        audioSrc.transform.SetParent(null);
        audioSrc.PlayOneShot(sliceSounds[randomChoice(sliceSounds.Length)]);
        Destroy(audioSrc.gameObject, 2f);
        if (genIndex == 0)
        {
            ability();

        }

        if (genIndex < genLength)
        {
            shineObj.transform.SetParent(null);

            Destroy(shineObj);
            psBurst();
            Instantiate(this.gameObject, transform.position, Quaternion.identity).GetComponent<Pomegranate>().initialize(genIndex);
            Instantiate(this.gameObject, transform.position, Quaternion.identity).GetComponent<Pomegranate>().initialize(genIndex);
            manager.addPoints(bounty);
            player.comboHandler();
            Destroy(sr);
            if(genIndex != 0)
            {
                Destroy(gameObject);
            }
        }
        Collider2D thisCol = GetComponent<Collider2D>();
        Destroy(thisCol);

    }



    public void ability()
    {
        manager.stopwatch.SetActive(true);
        manager.stopwatch.GetComponent<Animator>().SetTrigger("Start");
        StartCoroutine(timeWarp());
    }

    IEnumerator timeWarp()
    {
        print("timewarp");
        Vector2 defaultMinMax = manager.spawnMinMax;
        manager.stopwatchUI.SetActive(true);
        manager.spawnMinMax = new Vector2(0.5f, 1f);
        yield return new WaitForSeconds(abilitySeconds);
        manager.spawnMinMax = defaultMinMax;
        manager.stopwatchUI.SetActive(false);
        Destroy(gameObject);
    }

}
