using Assets.FantasyHeroes.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public GameObject imageButton;
    public Button removeButton;
    private GameObject player;
    Item item;
    Inventory inventory;

    public void SetItem(Item item)
    {
        this.item = item;
        try
        {
            if (item != null)
            {
                imageButton.GetComponent<Image>().enabled = true;
                imageButton.GetComponent<Image>().sprite = item.image;
                if (removeButton != null)
                {
                    removeButton.interactable = true;
                }
                imageButton.SetActive(true);
            }
        }
        catch { }
    }

    public void OnUse()
    {
        var hero = player.GetComponent<Character>();
        item.PutOn(hero);
    }    

    public void OnRemove()
    {
        inventory.Drop(item.type);
        imageButton.GetComponent<Image>().sprite = null;
        imageButton.GetComponent<Image>().enabled = false;
        removeButton.interactable = false;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
    }
}
