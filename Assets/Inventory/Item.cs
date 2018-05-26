using Assets.FantasyHeroes.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Helmet, Armor, Back, OneHandedWeapon, TwoHandedWeapon, Shield, Bow, QuestItem
};

public class Item : MonoBehaviour, IComparable
{

    public ItemType type;
    public Sprite image;
    public string name;
    public float weight;
    public int price;
    public int protection;
    public int attack;
    public bool Picable;

    public int CompareTo(object obj)
    {
        int result = 0;
        if (obj is Item)
        {
            var item2 = obj as Item;
            if (item2 == null)
            {
                result = 1;
            }
            else if (this.type != item2.type)
            {
                result = price.CompareTo(item2.price);
            }
            else if (this.type == ItemType.OneHandedWeapon || this.type == ItemType.TwoHandedWeapon || this.type == ItemType.Bow)
            {
                result = attack.CompareTo(item2.attack);
            }
            else if (this.type == ItemType.Helmet || this.type == ItemType.Armor || this.type == ItemType.Back || this.type == ItemType.Shield)
            {
                result = protection.CompareTo(item2.protection);
            }
            else
            {
                result = price.CompareTo(item2.price);
            }
        }
        else
        {
            throw new ArgumentException("obj");
        }

        return result;
    }

    public void PickUp(Inventory inventory)
    {
        Item item = gameObject.GetComponent<Item>();
        if (item != null && inventory != null)
            inventory.Put(item);
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    public void PutOn(Character c)
    {
        switch (type)
        {
            case ItemType.Helmet:
                c.Helmet = image;
                c.HelmetRenderer.sprite = c.Helmet;
                break;
            case ItemType.Armor:
                //TODO
                break;
            case ItemType.Back:
                c.Back = image;
                c.BackRenderer.sprite = c.Back;
                break;
            case ItemType.OneHandedWeapon:
                c.WeaponType = WeaponType.Melee1H;
                c.PrimaryMeleeWeapon = image;
                c.PrimaryMeleeWeaponRenderer.sprite = c.PrimaryMeleeWeapon;
                break;
            case ItemType.TwoHandedWeapon:
                c.WeaponType = WeaponType.Melee2H;
                c.PrimaryMeleeWeapon = image;
                c.PrimaryMeleeWeaponRenderer.sprite = c.PrimaryMeleeWeapon;
                c.BuildWeaponTrails();
                break;
            case ItemType.Shield:
                c.Shield = image;
                c.ShieldRenderer.sprite = c.Shield;
                break;
            case ItemType.Bow:
                //c.SetBow(FindSprite(SpriteCollection.BowArrow, entry), FindSprite(SpriteCollection.BowLimb, entry), FindSprite(SpriteCollection.BowRiser, entry));
                c.WeaponType = WeaponType.Bow;
                break;
            case ItemType.QuestItem:
                //TODO
                break;
            default:
                break;
        }
        c.Initialize();
    }

    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int x = Physics2D.GetContacts(gameObject.GetComponent<Collider2D>(), new Collider2D[] { player.GetComponent<Collider2D>() });
        if ((Input.GetKeyDown(KeyCode.E)) && x > 0 && Picable)
            PickUp(player.GetComponent <Inventory>());

    }

    
}
