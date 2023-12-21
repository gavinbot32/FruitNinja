using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCursor : MonoBehaviour
{

    public bool mouseDown;
    public GameObject particleObject;
    public Collider2D gameCollider;


    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x,mousePos.y,0);
        if( mouseDown)
        {
            selfActive(true);
        }
        else
        {
            selfActive(false);
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
    }
    private void OnMouseUp()
    {
        mouseDown = false;
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
        if (collision.CompareTag("Fruit"))
        {
            collision.GetComponent<Fruit>().sliceFruit();
        }
    }

}
