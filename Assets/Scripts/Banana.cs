using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : Fruit
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
            Instantiate(this.gameObject, transform.position, Quaternion.identity).GetComponent<Banana>().initialize(genIndex);
            Instantiate(this.gameObject, transform.position, Quaternion.identity).GetComponent<Banana>().initialize(genIndex);
            Instantiate(this.gameObject, transform.position, Quaternion.identity).GetComponent<Banana>().initialize(genIndex);
            manager.addPoints(bounty);
            player.comboHandler();
            Destroy(sr);
            if (genIndex != 0)
            {
                Destroy(gameObject);
            }
        }
        Collider2D thisCol = GetComponent<Collider2D>();
        Destroy(thisCol);

    }



    public void ability()
    {
        manager.dblPointsObj.SetActive(true);
        manager.dblPointsObj.GetComponent<Animator>().SetTrigger("Start");
        StartCoroutine(doublePoints());
    }

    IEnumerator doublePoints()
    {

        manager.pointMult = 2f;
        manager.dblPointsUI.SetActive(true);
        yield return new WaitForSeconds(abilitySeconds);
        manager.pointMult = 1f;
        manager.dblPointsUI.SetActive(false);
        Destroy(gameObject);
    }
}
