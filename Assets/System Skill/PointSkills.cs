using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSkills : MonoBehaviour {

    void Update()
    {
        GameObject.Find("HP").GetComponent<Text>().text = PlayerPrefs.GetInt("Hels").ToString();
        GameObject.Find("Strength").GetComponent<Text>().text = PlayerPrefs.GetInt("Streng").ToString();
        GameObject.Find("Magic").GetComponent<Text>().text = PlayerPrefs.GetInt("Mana").ToString();
        GameObject.Find("Intellegence").GetComponent<Text>().text = PlayerPrefs.GetInt("Intel").ToString();
        GameObject.Find("Points_quantity").GetComponent<Text>().text = PlayerPrefs.GetInt("Point").ToString();
        GameObject.Find("Exp").GetComponent<Text>().text = PlayerPrefs.GetInt("EndExp").ToString();
    }

}
