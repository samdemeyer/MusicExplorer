using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcBattle : MonoBehaviour {
    public GameObject inputController;
    private InputScript inputScript;
    public int health;
    public List<GameObject> healthBar = new List<GameObject>();
    private List<InputScript.noten> hits = null;
    public GameObject[] weakness;
    // Use this for initialization
    void Start () {
        hits = new List<InputScript.noten>();
        hits.Add((InputScript.noten)Random.Range(1, 7));
        hits.Add((InputScript.noten)Random.Range(1, 7));
        while (hits[0] == hits[1])
        {
            hits.Remove(hits[1]);
            hits.Add((InputScript.noten)Random.Range(1, 7));
        }
        health = hits.Count;
        Debug.Log("orc has HP: " + health.ToString());
        setWeaknessIMGs();
        Debug.Log("Kan nu aangevallen worden, speel de noten " + hits[0].ToString() + " en " + hits[1].ToString());
    }
    void Update () {
        if (this.GetComponent<OrcBehaviour>().closeEnoughToAttack)
        {
            if (inputController.GetComponent<InputScript>().getMusic() != InputScript.noten.Nothing)
            {
                hitOrc(inputController.GetComponent<InputScript>().getMusic());
            }
        }   
    }
    public void hitOrc(InputScript.noten attack)
    {
        if (inputController.GetComponent<InputScript>().getMusic() == hits[hits.Count-health])
        {
                health--;
                if (health > 0)
                {
                    Debug.Log("Orc has been hit by the player");
                    healthBar[health].SetActive(false);
                    weakness[0].SetActive(false);
                }
                else
                {
                Debug.Log("Orc has been killed");
                GameObject.Destroy(gameObject);
                } 
        }
    }
    private void setWeaknessIMGs()
    {
        for (int i = 0; i < hits.Count; i++)
        {
            weakness[i].GetComponent<SpriteRenderer>().sprite = inputController.GetComponent<InputScript>().notenList[(int) hits[i] -1];
        }

    }
}
