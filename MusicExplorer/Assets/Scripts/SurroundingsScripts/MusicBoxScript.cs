using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoxScript : MonoBehaviour {
    public Sprite[] musicBox;
    private InputScript.noten thisBox;
    public GameObject inputController;
    private bool deletable = false;
    private GameObject playersMusicMemory;
    private void Start()
    {
       thisBox = (InputScript.noten)Random.Range(1, 7);
        Debug.Log("Deze kist is de noot " + thisBox.ToString());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playersMusicMemory = collision.gameObject;
            Debug.Log("De speler hit een muziekdoos");
            gameObject.GetComponent<SpriteRenderer>().sprite = musicBox[(int)thisBox - 1];
            deletable = true;
        }
    }
    private void Update()
    {
        if (deletable)
        {
            if (inputController.GetComponent<InputScript>().getMusic() == thisBox)
            {
                playersMusicMemory.GetComponent<MusicBoxSaveScript>().playedMusic.Add(thisBox);
                Destroy(gameObject);
            }
        }
    }

}
