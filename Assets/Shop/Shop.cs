using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Shop : MonoBehaviour
{
    class Category
    {
        List<Item> items;
        int current = -1;

        public Item CurrentItem
        {
            get
            {
                return current == -1 ? null : items[current];
            }
        }

        public void Next()
        {
            current++;
            if (items == null || items.Count == 0)
            {
                current = -1;
            }
            else if (current >= items.Count)
            {
                current = 0;
            }
        }

        public void Remove(Item i)
        {
            var succsess = items.Remove(i);
            if (succsess)
            {
                current--;
                Next();
            }
        }


        public Category(params Item[] items)
        {
            this.items = new List<Item>(items);
            Next();
        }
    }

    Dictionary<ItemType, Category> categories;
    Inventory customer;
    Item selected;
    GameObject itemPanel;

    public GameObject shopPanel;
    public GameObject hero;

    //шаблон объеката. Нужен для порождения игровых предметов в магазине.
    //Unity запрещает порождать игровые объекты при помощи new
    public Item itemTemplate;

    // Use this for initialization
    void Start()
    {
        //shopPanel = this.gameObject;
        itemPanel = shopPanel.transform.Find("ItemPanel").gameObject;
        customer = hero.GetComponent<Inventory>();
        UpdateMoney();

        Category helmets = new Category(
            NewItem(type: ItemType.Helmet, name: "Helmet1", price: 10, protection: 1, image: Resources.Load<Sprite>("Helmets/VikingLeatherHelm")),
            NewItem(type: ItemType.Helmet, name: "Helmet2", price: 20, protection: 5, image: Resources.Load<Sprite>("Helmets/MercenaryHelm2")),
            NewItem(type: ItemType.Helmet, name: "Helmet3", price: 30, protection: 7, image: Resources.Load<Sprite>("Helmets/BerserkHelm")),
            NewItem(type: ItemType.Helmet, name: "Helmet4", price: 50, protection: 10, image: Resources.Load<Sprite>("Helmets/WarriorHelm")),
            NewItem(type: ItemType.Helmet, name: "Helmet5", price: 10, protection: 1, image: Resources.Load<Sprite>("Helmets/InquisitorHat")));
        Category armors = new Category(
            NewItem(type: ItemType.Armor, name: "Armor1", price: 10, protection: 1, image: Resources.Load<Sprite>("Armor/Armor")),
            NewItem(type: ItemType.Armor, name: "Armor2", price: 50, protection: 5, image: Resources.Load<Sprite>("Armor/VikingRoughArmor2")),
            NewItem(type: ItemType.Armor, name: "Armor3", price: 100, protection: 10, image: Resources.Load<Sprite>("Armor/SamuraiLight3")));
        Category backs = new Category(
            NewItem(type: ItemType.Back, name: "Back1", price: 5, protection: 1, image: Resources.Load<Sprite>("Back/WhiteCloak")),
            NewItem(type: ItemType.Back, name: "Back2", price: 15, protection: 2, image: Resources.Load<Sprite>("Back/RedCloak")),
            NewItem(type: ItemType.Back, name: "Back3", price: 25, protection: 3, image: Resources.Load<Sprite>("Back/BatmanCloak")));
        Category oneHandedeWeapons = new Category(
            NewItem(type: ItemType.OneHandedWeapon, name: "OneHanded1", price: 5, attack: 3, image: Resources.Load<Sprite>("OneHanded/AssassinDagger")),
            NewItem(type: ItemType.OneHandedWeapon, name: "OneHanded2", price: 20, attack: 6, image: Resources.Load<Sprite>("OneHanded/FireWarriorSword")),
            NewItem(type: ItemType.OneHandedWeapon, name: "OneHanded3", price: 70, attack: 8, image: Resources.Load<Sprite>("OneHanded/VikingAxe3")),
            NewItem(type: ItemType.OneHandedWeapon, name: "OneHanded4", price: 90, attack: 10, image: Resources.Load<Sprite>("OneHanded/VikingSword3")));
        Category twoHandedeWeapons = new Category(
            NewItem(type: ItemType.TwoHandedWeapon, name: "TwoHanded1", price: 5, attack: 10, image: Resources.Load<Sprite>("TwoHanded/HeavySword")),
            NewItem(type: ItemType.TwoHandedWeapon, name: "TwoHanded2", price: 20, attack: 15, image: Resources.Load<Sprite>("TwoHanded/SamuraiSword3")),
            NewItem(type: ItemType.TwoHandedWeapon, name: "TwoHanded3", price: 40, attack: 25, image: Resources.Load<Sprite>("TwoHanded/VikingAxe2")),
            NewItem(type: ItemType.TwoHandedWeapon, name: "TwoHanded4", price: 80, attack: 35, image: Resources.Load<Sprite>("TwoHanded/VikingSword1")));
        Category shields = new Category(
             NewItem(type: ItemType.Shield, name: "Shield1", price: 10, protection: 2, image: Resources.Load<Sprite>("Shield/CrusaderShield")),
             NewItem(type: ItemType.Shield, name: "Shield2", price: 20, protection: 17, image: Resources.Load<Sprite>("Shield/IronShield3")),
             NewItem(type: ItemType.Shield, name: "Shield3", price: 30, protection: 27, image: Resources.Load<Sprite>("Shield/KnightShield")));
        Category bows = new Category(
            NewItem(type: ItemType.Bow, name: "Bow1", price: 30, attack: 10, image: Resources.Load<Sprite>("Bow/HunterBow")),
            NewItem(type: ItemType.Bow, name: "Bow2", price: 40, attack: 30, image: Resources.Load<Sprite>("Bow/HunterBow2")),
            NewItem(type: ItemType.Bow, name: "Bow3", price: 50, attack: 45, image: Resources.Load<Sprite>("Bow/VikingShortBow")));
        Category quests = new Category(
            NewItem(type: ItemType.QuestItem, name: "QuestItem1", price: 100, image: Resources.Load<Sprite>("Quest/condom")),
            NewItem(type: ItemType.QuestItem, name: "QuestItem2", price: 500, image: Resources.Load<Sprite>("Quest/bot")),
            NewItem(type: ItemType.QuestItem, name: "QuestItem3", price: 150, image: Resources.Load<Sprite>("Quest/pick")),
            NewItem(type: ItemType.QuestItem, name: "QuestItem4", price: 2500, image: Resources.Load<Sprite>("Quest/pot")));

        categories = new Dictionary<ItemType, Category>();
        categories.Add(ItemType.Helmet, helmets);
        categories.Add(ItemType.Armor, armors);
        categories.Add(ItemType.Back, backs);
        categories.Add(ItemType.OneHandedWeapon, oneHandedeWeapons);
        categories.Add(ItemType.TwoHandedWeapon, twoHandedeWeapons);
        categories.Add(ItemType.Shield, shields);
        categories.Add(ItemType.Bow, bows);
        categories.Add(ItemType.QuestItem, quests);
    }


    Item NewItem(ItemType type, string name, int price, int protection = 0, int attack = 0, Sprite image = null)
    {
        var good1 = Instantiate(itemTemplate);
        good1.type = type;
        good1.name = name;
        good1.price = price;
        good1.protection = protection;
        good1.attack = attack;
        good1.image = image;
        return good1;
    }

    public void Buy()
    {
        Buy(selected);
        UpdateMoney();
        UpdateSelected();
    }

    public void Next(string t)
    {
        var key = (ItemType)Enum.Parse(typeof(ItemType), t);
        categories[key].Next();
        selected = categories[key].CurrentItem;
        UpdateSelected();
    }

    void DeleteItem(Item item)
    {
        categories[item.type].Remove(item);
    }

    void Buy(Item item)
    {
        if (item != null && customer != null && customer.money >= item.price)
        {
            customer.Put(item);
            customer.money -= item.price;
            DeleteItem(item);
            selected = categories[item.type].CurrentItem;
        }
    }

    void SetInterfaceName(string uiElement, string value)
    {
        var x = itemPanel.transform.Find(uiElement).GetChild(0);
        var y = x.gameObject.GetComponent<Text>();
        y.text = value;
    }

    void UpdateMoney()
    {
        if (customer != null)
        {
            SetInterfaceName("Money", customer.money.ToString());
        }

    }

    void UpdateSelected()
    {
        SetInterfaceName("Info", selected != null ? selected.name : "");
        SetInterfaceName("Protection", selected != null ? selected.protection.ToString() : "0");
        SetInterfaceName("Attack", selected != null ? selected.attack.ToString() : "0");
        SetInterfaceName("Price", selected != null ? selected.price.ToString() : "0");

        var x = itemPanel.transform.Find("ItemImage").GetChild(0);
        var y = x.gameObject.GetComponent<Image>();
        y.sprite = selected != null ? selected.image : null;
        y.gameObject.SetActive(selected != null);
    }

    public void OnPointerClick(string name)
    {
        ItemType t = (ItemType)Enum.Parse(typeof(ItemType), name);
        selected = categories[t].CurrentItem;
        UpdateSelected();

    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int x = Physics2D.GetContacts(gameObject.GetComponent<Collider2D>(), new Collider2D[] { player.GetComponent<Collider2D>() });

        if (Input.GetKeyUp(KeyCode.E) && x > 0)
            switchVisible();
    }

    private void switchVisible()
    {
        if (!shopPanel.activeSelf)
            shopPanel.SetActive(true);
    }
}
