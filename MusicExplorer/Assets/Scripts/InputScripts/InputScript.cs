using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour {
    public enum noten
    {
        Nothing,
        Do,
        Re,
        Mi,
        Fa,
        Sol,
        La,
        Si,
    }
    public List<Sprite> notenList = new List<Sprite>();
    // Use this for initialization
    public noten getMusic()
    {
        noten attack;
        if (Input.GetKey(KeyCode.Alpha1))
        {
            attack = noten.Do;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            attack = noten.Re;
        }
        else if(Input.GetKey(KeyCode.Alpha3))
        {
            attack = noten.Mi;
        }
        else if(Input.GetKey(KeyCode.Alpha4))
        {
            attack = noten.Fa;
        }
        else if(Input.GetKey(KeyCode.Alpha5))
        {
            attack = noten.Sol;
        }
        else if(Input.GetKey(KeyCode.Alpha6))
        {
            attack = noten.La;
        }
        else if(Input.GetKey(KeyCode.Alpha7))
        {
            attack = noten.Si;
        }
        else
        {
            attack = noten.Nothing;
        }
        return attack;
    }
}
