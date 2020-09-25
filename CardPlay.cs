using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlay : MonoBehaviour
{
    private Vector3 Detector;

    [SerializeField]
    public GameObject Other;

    [SerializeField]
    public GameObject Hand;

    public bool Dragable = false;

    // Update is called once per frame
    void Update()
    {
        Detector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D ray = Physics2D.Raycast(new Vector2(Detector.x, Detector.y), Vector2.zero, 0);
        {
            if (ray && Input.GetMouseButtonDown(0))
            {
                Other = ray.transform.gameObject;
                Dragable = true;                
            }
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            Dragable = false;
        }
        if (Other.gameObject.tag == "PlayerCard")
        {
            if (Dragable == true)
            {
                Other.transform.position = Detector;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Dragable == false)
        {
            if (collision.tag == "Own Hand")
            {
                    Other.transform.position = collision.transform.position;               
            }           
            else if (collision.tag == "InPlay")
            {
                if (ManaSpend() == true)
                {
                    Other.transform.position = collision.transform.position;
                }
                else if (ManaSpend() == false)
                {
                    Other.transform.position = Hand.transform.position;
                }
            }
            else if (collision.tag == null)
            {
                Other.transform.position = Hand.transform.position;
            }
        }
    }

    bool ManaSpend()
    {
        if (playerManager.Mana >= CardManager.cardCost)
        {
            playerManager.Mana -= CardManager.cardCost;
            return true;
        }
        else
        {
            return false;
        }
    }
}
