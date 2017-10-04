using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject[] startPlatforms;
    public GameObject[] levelTwoPlatforms;

    public GameObject startBG, levelTwoBG;

    public GameObject platformSpawnPosition;
    public GameObject musicBox;
    public GameObject[] bgInGame;

    private float platformEndPosition;
    private List<GameObject> currentPlatforms = new List<GameObject>();
    private bool specialIsPossible = true;
    public GameObject orcToSpawn;
    private int orcSpawnChance, chestSpawnChance;
    // Use this for initialization

    void Start()
    {

        bgInGame[1].SetActive(false);
        bgInGame[0].SetActive(true);

        orcSpawnChance = 8;
        chestSpawnChance = 2;
        GameObject obj = Instantiate(startPlatforms[0]) as GameObject;
        obj.transform.position = this.transform.position;

        obj.name = "Ground";
        

        obj.AddComponent<BoxCollider2D>();
        currentPlatforms.Add(obj);
    }

    // Update is called once per frame
    void Update()
    {
        int RandomPlatformNumber = Random.Range(0, 2);
        //Debug.Log("platform nr " + RandomPlatformNumber);
        if (transform.position.x > 150)
        {
            orcSpawnChance--; //7
            chestSpawnChance++; //3
        }
        else if (transform.position.x > 250)
        {
            orcSpawnChance--; //6
            chestSpawnChance++; //4
        }
        else if (transform.position.x > 500)
        {
            orcSpawnChance--; //5
        }

        if (platformSpawnPosition.transform.position.x > transform.position.x-5)
        {
            GameObject obj=null;
            if (currentPlatforms.Count >= 3)
            {
                if (currentPlatforms[currentPlatforms.Count - 1].name == "SpecialGround" && currentPlatforms[currentPlatforms.Count - 2].name == "SpecialGround")
                {
                    specialIsPossible = false;
                }
                else
                {
                    specialIsPossible = true;
                }
            }
            if (transform.position.x < 600)//moet 600
            {
                
                if (RandomPlatformNumber == 0)
                {
                    obj = Instantiate(startPlatforms[0]) as GameObject;
                    obj.name = "NormalGround";
                }
                else if (RandomPlatformNumber == 1 && specialIsPossible == true)
                {
                    obj = Instantiate(startPlatforms[1]) as GameObject;
                    obj.name = "SpecialGround";
                }
                else
                {
                    obj = Instantiate(startPlatforms[0]) as GameObject;
                    obj.name = "NormalGround";
                }   
            }
            else/* if(transform.position.x < 1000)*/ //moet 1000 
            {
                bgInGame[1].SetActive(true);
                bgInGame[0].SetActive(false);
                if (RandomPlatformNumber == 0)
                {
                    obj = Instantiate(levelTwoPlatforms[0]) as GameObject;
                    obj.name = "NormalGround";
                }
                else if (RandomPlatformNumber == 1 && specialIsPossible == true)
                {
                    obj = Instantiate(levelTwoPlatforms[1]) as GameObject;
                    obj.name = "SpecialGround";
                }
                else
                {
                    obj = Instantiate(levelTwoPlatforms[0]) as GameObject;
                    obj.name = "NormalGround";
                }
            }            
//            obj.AddComponent<BoxCollider2D>();
            transform.position = new Vector2(transform.position.x + 17, transform.position.y);
            obj.transform.position = transform.position;
            currentPlatforms.Add(obj);
            if (currentPlatforms.Count > 5)
            {
                extrasOnMap(currentPlatforms[currentPlatforms.Count-1]);
            }
        }
    }
    
    private void extrasOnMap(GameObject _platformToSpawnOn)
    {
        int random = Random.Range(0 , 10);
        if (_platformToSpawnOn.name == "NormalGround" && random >= orcSpawnChance )
        {
            GameObject orc = Instantiate(orcToSpawn) as GameObject;
            orc.name = "Orc";
            orc.transform.position = new Vector2(_platformToSpawnOn.transform.position.x, _platformToSpawnOn.transform.position.y+4);

        }
        else if (_platformToSpawnOn.name == "NormalGround" && random <= chestSpawnChance )
        {
            GameObject crate = Instantiate(musicBox) as GameObject;
            crate.name = "MusicCrate";
            crate.transform.position = new Vector2(_platformToSpawnOn.transform.position.x, _platformToSpawnOn.transform.position.y+4);
        }
        else if (_platformToSpawnOn.name != "NormalGround")
        {
            Debug.Log("The next ground is " + _platformToSpawnOn.name);
        }
    }
}
