using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knopka : MonoBehaviour {
    public int hp;
    public int strength;
    public int mana;
    public int intelligence;
    int point;

    public void OnMouseUpAsButton()
    {
        point = point + PlayerPrefs.GetInt("Point");
    }

    public void HP()
    {
        if (PlayerPrefs.GetInt("Point") > 0)
        {
            hp = PlayerPrefs.GetInt("Hels") + 1;
            point = PlayerPrefs.GetInt("Point") - 1;
            PlayerPrefs.SetInt("Point", point);
            PlayerPrefs.SetInt("Hels", hp);
        }      
    }

    public void Strength()
    {
        if (PlayerPrefs.GetInt("Point") > 0)
        {
            strength = PlayerPrefs.GetInt("Streng") + 1;
            point = PlayerPrefs.GetInt("Point") - 1;
            PlayerPrefs.SetInt("Point", point);
            PlayerPrefs.SetInt("Streng", strength);
        }  
    }

    public void Magic()
    {
        if (PlayerPrefs.GetInt("Point") > 0)
        {
            mana = PlayerPrefs.GetInt("Mana") + 1;
            point = PlayerPrefs.GetInt("Point") - 1;
            PlayerPrefs.SetInt("Point", point);
            PlayerPrefs.SetInt("Mana", mana);
        }   
    }

    public void Intellegnce()
    {
        if (PlayerPrefs.GetInt("Point") > 0)
        {
            intelligence = PlayerPrefs.GetInt("Intel") + 1;
            point = PlayerPrefs.GetInt("Point") - 1;
            PlayerPrefs.SetInt("Point", point);
            PlayerPrefs.SetInt("Intel", intelligence);
        }
        
    }

    public void Exit()
    {
        FindObjectOfType<OknoSkills>().EndSkills();
    }
}