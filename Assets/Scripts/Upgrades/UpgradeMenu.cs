using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] GameObject upgrade;

    [SerializeField] GameObject weaponReplacementMenu;

    [SerializeField] GameObject itemReplacementMenu;
    [SerializeField] Button weaponButton;

    [SerializeField] Button itemButton;

    [SerializeField] Button randomButton;

    [SerializeField]TextMeshProUGUI weaponDetails;
    [SerializeField] TextMeshProUGUI itemDetails;
    [SerializeField] TextMeshProUGUI  randomDetails;

    [SerializeField] List<TextMeshProUGUI> weaponInventoryDetails;
    [SerializeField] List<TextMeshProUGUI> itemInventoryDetails;

    [SerializeField] List<Button> weaponInventorySlots;

    [SerializeField] List<Button> itemInventorySlots;

    [SerializeField] PlayerInventory playerInventory;

    private bool replacingWeapon;

    private bool replacingFromRandomButton;

    private Weapon newWeapon;
    private Weapon newWeapon2;
    private item newItem;

    private item newItem2;
    
    private float random;

    private int notDefaultWeapon;

    private float weaponProbability;
    public void OpenUpgradeMenu()
    {
        random = Random.value;
        weaponProbability = 0.5f; //50% chance of it being a weapon
        notDefaultWeapon = 0;

        InitializeWeaponUpgradeOptions();
        InitializaItemUpgradeOptions();
        
        
        Time.timeScale = 0f; //pause the game when upgrade menu comes out
        upgrade.SetActive(true);
        
        SetWeaponButtonText();
        SetItemButtonText();
        SetRandomButtonText();
        
    }

    public void InitializeWeaponUpgradeOptions()
    {
        List<Weapon> weapons = Game.GetWeaponList();
        List<Weapon> genericWeapon = new List<Weapon>(); //this is to get all generic weapons in the full list of weapons

        foreach(Weapon weapon in weapons)
        {
            if(weapon.isGeneric)
            {
                genericWeapon.Add(weapon);
            }
        }

        //add default weapon into inventory
        genericWeapon.Add(playerInventory.weaponInventory[0]);
        
        if(newWeapon.initialLevel<=3 || newWeapon2.initialLevel<=3 || newWeapon==null || newWeapon2==null)
        {
            if(notDefaultWeapon<4)
            {
                newWeapon = genericWeapon[Random.Range(0, genericWeapon.Count)]; 
                genericWeapon.Remove(newWeapon);
                newWeapon2 = genericWeapon[Random.Range(0,genericWeapon.Count)];
                notDefaultWeapon++;
            }
            else
            {
                newWeapon = genericWeapon.Find(weapon => !weapon.isGeneric);
                genericWeapon.Remove(newWeapon);
                newWeapon2 = genericWeapon[Random.Range(0,genericWeapon.Count)];
                notDefaultWeapon = 0;
            }
            if(!newWeapon.isGeneric || !newWeapon2.isGeneric)
            {
                notDefaultWeapon = 0;
            }
        }
        else if(newWeapon.initialLevel == 3)
        {
            genericWeapon.Remove(newWeapon);
            if(notDefaultWeapon<4)
            {
                newWeapon = genericWeapon[Random.Range(0, genericWeapon.Count)]; 
                genericWeapon.Remove(newWeapon);
                newWeapon2 = genericWeapon[Random.Range(0,genericWeapon.Count)];
                notDefaultWeapon++;
            }
            else
            {
                newWeapon = genericWeapon.Find(weapon => !weapon.isGeneric);
                genericWeapon.Remove(newWeapon);
                newWeapon2 = genericWeapon[Random.Range(0,genericWeapon.Count)];
                notDefaultWeapon = 0;
            }
            if(!newWeapon.isGeneric || !newWeapon2.isGeneric)
            {
                notDefaultWeapon = 0;
            }
        }
        else if(newWeapon2.initialLevel == 3)
        {
            genericWeapon.Remove(newWeapon2);
            if(notDefaultWeapon<4)
            {
                newWeapon = genericWeapon[Random.Range(0, genericWeapon.Count)]; 
                genericWeapon.Remove(newWeapon);
                newWeapon2 = genericWeapon[Random.Range(0,genericWeapon.Count)];
                notDefaultWeapon++;
            }
            else
            {
                newWeapon = genericWeapon.Find(weapon => !weapon.isGeneric);
                genericWeapon.Remove(newWeapon);
                newWeapon2 = genericWeapon[Random.Range(0,genericWeapon.Count)];
                notDefaultWeapon = 0;
            }
            if(!newWeapon.isGeneric || !newWeapon2.isGeneric)
            {
                notDefaultWeapon = 0;
            }
        }
        else if(newWeapon.initialLevel==3 && newWeapon2.initialLevel==3)
        {
            genericWeapon.Remove(newWeapon);
            genericWeapon.Remove(newWeapon2);
            if(notDefaultWeapon<4)
            {
                newWeapon = genericWeapon[Random.Range(0, genericWeapon.Count)]; 
                genericWeapon.Remove(newWeapon);
                newWeapon2 = genericWeapon[Random.Range(0,genericWeapon.Count)];
                notDefaultWeapon++;
            }
            else
            {
                newWeapon = genericWeapon.Find(weapon => !weapon.isGeneric);
                genericWeapon.Remove(newWeapon);
                newWeapon2 = genericWeapon[Random.Range(0,genericWeapon.Count)];
                notDefaultWeapon = 0;
            }
            if(!newWeapon.isGeneric || !newWeapon2.isGeneric)
            {
                notDefaultWeapon = 0;
            }
        }
        
    }

    public void InitializaItemUpgradeOptions()
    {
        
        List<item> items = Game.GetItemList();

        newItem = items[Random.Range(0, items.Count)];
        items.Remove(newItem);
        newItem2 = items[Random.Range(0, items.Count)];

        if(newItem.initiallevel<=3 || newItem2.initiallevel<=3 || newItem==null || newItem2==null)
        {
            newItem = items[Random.Range(0, items.Count)];
            items.Remove(newItem);
            newItem2 = items[Random.Range(0, items.Count)];
        }
        else if(newItem.initiallevel == 3)
        {
            items.Remove(newItem);
            newItem = items[Random.Range(0, items.Count)];
            items.Remove(newItem);
            newItem2 = items[Random.Range(0, items.Count)];
        }
        else if(newItem2.initiallevel == 3)
        {
            items.Remove(newItem2);
            newItem = items[Random.Range(0, items.Count)];
            items.Remove(newItem);
            newItem2 = items[Random.Range(0, items.Count)];
        }
        else if(newWeapon.initialLevel==3 && newWeapon2.initialLevel==3)
        {
            items.Remove(newItem);
            items.Remove(newItem2);
            newItem = items[Random.Range(0, items.Count)];
            items.Remove(newItem);
            newItem2 = items[Random.Range(0, items.Count)];
        }
    }

    public void CloseUpgradeMenu()
    {
        upgrade.SetActive(false);
        Time.timeScale = 1f; //unpause after upgrading
    }

    public void ChooseWeapon()
    {
        if(playerInventory.weaponInventory.Count<3 || playerInventory.weaponInventory.Contains(newWeapon))
        {
            
            playerInventory.AddWeaponToInventory(newWeapon);

            CloseUpgradeMenu();
        }
        else
        {
            replacingFromRandomButton = false;
            OpenWeaponReplacementPrompt();
        }
        //Add to inventory
    }

    public void SetWeaponButtonText()
    {
        if(playerInventory.GetWeaponInventory().Contains(newWeapon))
        {
            List<WeaponUpgrades> upgradeList = Game.GetWeaponUpgradesList().FindAll(upgrade => upgrade.refID == newWeapon.id); //this is to find all levels for this specific weapon
            WeaponUpgrades nextLevelWeapon = upgradeList.Find(upgrade => upgrade.level == newWeapon.initialLevel + 1); //get the stats of next level for this weapon
            
            weaponDetails.text = newWeapon.name + "\n" +"\nLevel: " + nextLevelWeapon.level.ToString() + "\n" + "\n" + nextLevelWeapon.upgradeDesc;

        }
        else
        {
            weaponDetails.text = newWeapon.name + "\n" +"\nLevel: " + newWeapon.initialLevel.ToString() + "\n" + "\n" + newWeapon.basicDesc;
            
        }
    }

    public void ChooseItem()
    {
        if(playerInventory.itemInventory.Count<3 || playerInventory.itemInventory.Contains(newItem))
        {
            playerInventory.AddItemToInventory(newItem);

            CloseUpgradeMenu();

        }
        else
        {
            replacingFromRandomButton = false;
            OpenItemReplacementPrompt();
        }
        //Add to inventory
    }

    public void SetItemButtonText()
    {
        if(playerInventory.GetItemInventory().Contains(newItem))
        {
            List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == newItem.id); //this is to find all levels for this specific item
            ItemUpgrades nextLevelItem = upgradeList.Find(upgrade => upgrade.level == newItem.initiallevel + 1); //get the stats of next level for this weapon
            
            itemDetails.text = newItem.name + "\n" +"\nLevel: " + nextLevelItem.level.ToString() + "\n" + "\n" + nextLevelItem.upgradeDesc;
            
        }
        else
        {
            itemDetails.text = newItem.name + "\n" +"\nLevel: " + newItem.initiallevel.ToString() + "\n" + "\n" + newItem.basicDesc;
            

        }
    }

    public void SetRandomButtonText()
    {
        

        if(random <weaponProbability)
        {
            if(playerInventory.GetWeaponInventory().Contains(newWeapon2))
            {
                List<WeaponUpgrades> upgradeList = Game.GetWeaponUpgradesList().FindAll(upgrade => upgrade.refID == newWeapon2.id); //this is to find all levels for this specific weapon
                WeaponUpgrades nextLevelWeapon = upgradeList.Find(upgrade => upgrade.level == newWeapon2.initialLevel + 1); //get the stats of next level for this weapon
                
                randomDetails.text = newWeapon2.name + "\n" +"\nLevel: " + nextLevelWeapon.level.ToString() + "\n" + "\n" + nextLevelWeapon.upgradeDesc;

            }
            else
            {
                randomDetails.text = newWeapon2.name + "\n" +"\nLevel: " + newWeapon2.initialLevel.ToString() + "\n" + "\n" + newWeapon2.basicDesc;
                
            }
        }
        else
        {
            if(playerInventory.GetItemInventory().Contains(newItem2))
            {
                List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == newItem2.id); //this is to find all levels for this specific item
                ItemUpgrades nextLevelItem = upgradeList.Find(upgrade => upgrade.level == newItem2.initiallevel + 1); //get the stats of next level for this weapon
                
                randomDetails.text = newItem2.name + "\n" +"\nLevel: " + nextLevelItem.level.ToString() + "\n" + "\n" + nextLevelItem.upgradeDesc;
                
            }
            else
            {
                randomDetails.text = newItem2.name + "\n" +"\nLevel: " + newItem2.initiallevel.ToString() + "\n" + "\n" + newItem2.basicDesc;
             
            }
        }
        
    }
    public void ChooseRandom()
    {
        
        if(random <weaponProbability)
        {
            if(playerInventory.weaponInventory.Count<3  || playerInventory.weaponInventory.Contains(newWeapon2))
            {
                
                playerInventory.AddWeaponToInventory(newWeapon2);
                CloseUpgradeMenu();
            }
            else
            {
                replacingFromRandomButton =true;
                OpenWeaponReplacementPrompt();
            }
        }
        else
        {   
            if(playerInventory.itemInventory.Count<3  || playerInventory.itemInventory.Contains(newItem2))
            {
                playerInventory.AddItemToInventory(newItem2);

                CloseUpgradeMenu();

            }
            else
            {
                replacingFromRandomButton = true;
                OpenItemReplacementPrompt();
            }
        }

    }

   public void OpenWeaponReplacementPrompt()
   {

        upgrade.SetActive(false);
        weaponReplacementMenu.SetActive(true);
        replacingWeapon = true;
        for(int i=0; i<playerInventory.weaponInventory.Count;i++)
        {
            SetWeaponReplacementText(i);
            Debug.Log(playerInventory.weaponInventory[i].name);
        }

   }

   public void OpenItemReplacementPrompt()
   {
        upgrade.SetActive(false);
        Time.timeScale = 0f; //pause the game again
        itemReplacementMenu.SetActive(true);
        replacingWeapon = false;
        for(int i =0; i<playerInventory.itemInventory.Count; i++)
        {
            SetItemReplacementText(i);
        }
   }

   public void ReplaceWeapon(int index)
   {
       if(!replacingFromRandomButton)
       {
            playerInventory.ReplaceWeaponInInventory(index, newWeapon);
            
       }
       else
       {
            playerInventory.ReplaceWeaponInInventory(index, newWeapon2);
            
       }
        
        
   }

   public void SetWeaponReplacementText(int index)
   {
        Weapon weapon = playerInventory.weaponInventory[index];
        TextMeshProUGUI weaponText =  weaponInventoryDetails[index];
        
        if(weapon.initialLevel == 1)
        {
            weaponText.text = weapon.name + "\n" +"\nLevel: " + weapon.initialLevel.ToString() + "\n" + "\n" + weapon.basicDesc;
        }
        else
        {
            List<WeaponUpgrades> upgradeList = Game.GetWeaponUpgradesList().FindAll(upgrade => upgrade.refID == newWeapon2.id); //this is to find all levels for this specific weapon
            WeaponUpgrades weaponStats = upgradeList.Find(upgrade => upgrade.level == weapon.initialLevel); //get the stats of current level for this weapon
            weaponText.text = weapon.name + "\n" +"\nLevel: " + weapon.initialLevel.ToString() + "\n" + "\n" + weaponStats.upgradeDesc;
        }

   }

   public void SetItemReplacementText(int index)
   {
        item item = playerInventory.itemInventory[index];
        foreach(var e in itemInventoryDetails)
        {
            if(item.initiallevel == 1)
            {
                e.text = item.name + "\n" +"\nLevel: " + item.initiallevel.ToString() + "\n" + "\n" + item.basicDesc;
            }
            else
            {
                List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == item.id); //this is to find all levels for this specific item
                ItemUpgrades itemStats = upgradeList.Find(upgrade => upgrade.level == item.initiallevel); //get the stats of current level for this item
                e.text = item.name + "\n" +"\nLevel: " + item.initiallevel.ToString() + "\n" + "\n" + itemStats.upgradeDesc;
            }
            
        }
   }

   public void ReplaceItem(int index)
   {    
        if(!replacingFromRandomButton)
        {
            playerInventory.ReplaceItemInInvetory(index, newItem);
            
        }
        else
        {
            playerInventory.ReplaceItemInInvetory(index, newItem2);
            
        }
        
        
   }

    public void OnReplacement(int index)
    {
        if(replacingWeapon)
        {
            ReplaceWeapon(index);
            weaponReplacementMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            ReplaceItem(index);
            itemReplacementMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void ReOpenUppgradeMenu()
    {
        itemReplacementMenu.SetActive(false);
        weaponReplacementMenu.SetActive(false);
        upgrade.SetActive(true);
    }
    
}
