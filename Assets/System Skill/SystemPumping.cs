using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SystemPumping : MonoBehaviour
{
    public float maxhealth;     //макс здоровье
    public float physicaldamage;   // физический урон игрока
    public float magicdamage;      // магический урон игрока
    public float damageonPlayer;   // урон по игроку
    public float damageonPlayer2;  // урон по игроку
    public int protection;          // защита
    public float scaleexp;         //шкала опыта 
    public float scaleexpfull;        //шкала опыта полная
    public int point;    //очки прокачки
    int endexp; //опыт который осталось набрать
    void Start()
    {
        FunctionPoint(0);
        PumpingSkills(0);
    }

    void Update()
    {
        FunctionHels();
    }

    public void Save()
    {
        SaveLoadManager.SavePlayer(this);
    }

    public void Load()
    {
        int[] loadedStats = SaveLoadManager.LoadPlayer();

        health = loadedStats[0];
        strength = loadedStats[1];
        mana = loadedStats[2];
        intelligence = loadedStats[3];
        point = loadedStats[4];
        PumpingSkills(0);
    }

    //проверка на получения нового уровня героя и расчет остаточного опыта
    public void FunctionPoint(float experience)
    {
        scaleexp = scaleexp + experience;
        endexp = Convert.ToInt32(scaleexpfull - scaleexp);
        FindObjectOfType<СonclusionParameters>().DataOutputExp(endexp);
        if (scaleexp >= scaleexpfull)
        {

            point = point + 5;
            scaleexp = scaleexp- scaleexpfull;
            scaleexpfull = scaleexpfull + 100;
        }
    }

    // тот урон, который наносит персонаж
    public float DamagePhysical()
    {
        physicaldamage = strength * 10;   // + ("sword")
        return physicaldamage;
    }

    // тот магический урон, который наносит персонаж
    void DamegeMagical()
    {
        magicdamage = intelligence * 10;  // +(посох) + (заклинание)
    }

    // максимальный уровень здоровья персонажа
    public float FunctionHels()
    {
        maxhealth = health * 25;
        return maxhealth;
    }

    // урон, который получает персонаж
    public float Damege()
    {
        damageonPlayer = damageonPlayer2 - protection;
        return damageonPlayer;
    }
    // защита игрока 
    void Protection()
    {
        //protection = шлем+нагрудник+штаны+боты+щит
    }

    public int health;
    public int strength;
    public int mana;
    public int intelligence;
    //увеличиваем характерисстики
    public void PumpingSkills(int ButtenNumber)
    {
        if (point > 0)
        { 
            if (ButtenNumber == 1)
            {
                health++;
                point--;
            }
            if (ButtenNumber == 2)
            {
                strength++;
                point--;
            }
            if (ButtenNumber == 3)
            {
                mana++;
                point--;
            }
            if (ButtenNumber == 4)
            {
                intelligence++;
                point--;
            }
        }
        FindObjectOfType<СonclusionParameters>().DataOutput(health, strength, mana, intelligence, point);
    }
}
 
