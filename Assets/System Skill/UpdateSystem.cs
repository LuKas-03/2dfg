using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpdateSystem : MonoBehaviour {

//    public float health;           // здоровье
    public float Maxhealth;        //макс здоровье
    public float physicaldamage;   // физический урон игрока
    public float magicdamage;      // магический урон игрока
    public float damageonPlayer;   // урон по игроку
    public float damageonPlayer2;  // урон по игроку
    public int protection;          // защита
    public float experience;       // опыт 
    public float scaleexp;         //шкала опыта 
    public float scaleexp2;        //шкала опыта полная
    public int point;    //очки прокачки
//    Image healthbar;

    void Start()
    {
        Kakaha();
        // healthbar =GameObject.Find("HealthBar").GetComponent<Image>();
        // health = Maxhealth;
        FunctionPoint(experience);
    }

    void Update()
    {
        // healthbar.fillAmount = health;
        FunctionPoint(experience);
        FunctionldamageFiz();
        FunctiondamegeMag();
        FunctionHels();
        FunctionDamege();
    }

    public void FunctionPoint(float experience)
    {
        scaleexp = scaleexp + experience;
        PlayerPrefs.SetInt("EndExp", Convert.ToInt32(scaleexp2 - scaleexp));
        if (scaleexp >= scaleexp2)
          {

            point = point + 5;
            PlayerPrefs.SetInt("Point", point);
            scaleexp = scaleexp-scaleexp2;
            scaleexp2 = scaleexp2 + 100;

        }
    }

    // тот урон, который наносит персонаж
    public float FunctionldamageFiz()
    {
        physicaldamage = Convert.ToSingle(PlayerPrefs.GetInt("Streng")) * 10f; // + ("sword")
        return physicaldamage;
    }

    // тот магический урон, который наносит персонаж
    void FunctiondamegeMag()
    {
        magicdamage = Convert.ToSingle(PlayerPrefs.GetInt("Intel")) * 10f;// +(посох) + (заклинание)
    }

    // максимальный уровень здоровья персонажа
    public float FunctionHels()
    {
        Maxhealth = PlayerPrefs.GetInt("Hels") * 25;
        return Maxhealth;
    }

    // урон, который получает персонаж
    public float FunctionDamege()
    {
        damageonPlayer = damageonPlayer2 - protection;
       // health = health - damageonPlayer;
        return damageonPlayer;
    }

    public int hp;
    public int strength;
    public int mana;
    public int intelligence;

    void Kakaha()
    {
        PlayerPrefs.SetInt("Point", point);
        PlayerPrefs.SetInt("Hels", hp);
        PlayerPrefs.SetInt("Streng", strength);
        PlayerPrefs.SetInt("Mana", mana);
        PlayerPrefs.SetInt("Intel", intelligence);
    }

    // защита брони
    void FunctionProtection()
    {
        //protection = шлем+нагрудник+штаны+боты+щит
    }
}
 
