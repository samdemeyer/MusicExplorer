using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour {
    public List<GameObject> healthBar = new List<GameObject>();
    public GameObject[] weaponsToHitYou;
    public GameObject spawnPoint;
    private int health;
    public GameObject diedPanel;
    public bool firstHit = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (float i = 0; i < 1; i+=0.1f)
        {

        }
       if (collision.name == GameObject.Find("orc_weapon").GetComponent<Collider2D>().name)
         {
            if (firstHit)
            {
                health--;
                if (health > 0)
                {
                    Debug.Log("You've been hit by the orc");
                    healthBar[health].SetActive(false);
                }
                else
                {
                    Die();
                }
            }
            firstHit = true;
        }  
    }
    private void Update()
    {
        if (transform.position.y < -8)
        {
            Die();
        }
    }
    private void Start()
    {
        health = healthBar.Count;
        Respawn();
    }
    public void Respawn() {
        transform.position = new Vector2(spawnPoint.transform.position.x, spawnPoint.transform.position.y);
    }
    private void Die()
    {
        for (int i = 0; i < healthBar.Count; i++)
        {
            healthBar[i].SetActive(true);
        }
        diedPanel.SetActive(true);
        health = healthBar.Count;
    }
}
