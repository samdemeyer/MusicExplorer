using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformToSpawn1 = null, platformToSpawn2 = null;
    public GameObject backGround = null;
    public GameObject platformSpawnPosition;
    public GameObject musicBox;
    private float platformEndPosition;
    private List<GameObject> currentPlatforms = new List<GameObject>();
    private bool specialIsPossible = true;
    public GameObject orcToSpawn;
    // Use this for initialization

    void Start()
    {
        GameObject obj = Instantiate(platformToSpawn1) as GameObject;
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

            if (RandomPlatformNumber == 0)
            {
                obj = Instantiate(platformToSpawn1) as GameObject;
                obj.name = "NormalGround";
            }
            else if (RandomPlatformNumber == 1 && specialIsPossible == true)
            {
                obj = Instantiate(platformToSpawn2) as GameObject;
                obj.name = "SpecialGround";
            }
            else
            {
                obj = Instantiate(platformToSpawn1) as GameObject;
                obj.name = "NormalGround";
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
        int random = Random.Range(0 , 7);
        if (_platformToSpawnOn.name == "NormalGround" && random > 4 )
        {
            GameObject orc = Instantiate(orcToSpawn) as GameObject;
            orc.name = "Orc";
            orc.transform.position = new Vector2(_platformToSpawnOn.transform.position.x, _platformToSpawnOn.transform.position.y+4);

        }
        else if (_platformToSpawnOn.name == "NormalGround" && random <2 )
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
