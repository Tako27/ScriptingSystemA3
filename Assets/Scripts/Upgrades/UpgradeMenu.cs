using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

// Code Done By: Lee Ying Jie
// ================================
// This script handles upgrades system in the game
public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] GameObject upgrade;

    [SerializeField] GameObject weaponReplacementMenu;

    [SerializeField] GameObject itemReplacementMenu;
    [SerializeField] Button weaponButton;

    [SerializeField] Button itemButton;

    [SerializeField] Button randomButton;

    [SerializeField]TextMeshProUGUI weaponDetails;
    [SerializeField] Image weaponButtonImage;
    [SerializeField] TextMeshProUGUI itemDetails;

    [SerializeField] Image itemButtonImage;
    [SerializeField] TextMeshProUGUI  randomDetails;
    [SerializeField] Image randomButtonImage;

    [SerializeField] List<TextMeshProUGUI> weaponInventoryDetails;

    [SerializeField] List<Image> weaponInventoryImages;
    [SerializeField] List<TextMeshProUGUI> itemInventoryDetails;

    [SerializeField] List<Image> itemInventoryImages;

    [SerializeField] List<Button> weaponInventorySlots;

    [SerializeField] List<Button> itemInventorySlots;

    [SerializeField] PlayerInventory playerInventory;

    [SerializeField] Player player;


    private bool replacingWeapon;

    private bool replacingFromRandomButton;

    private Weapon newWeapon;
    private Weapon newWeapon2;
    private item newItem;

    private item newItem2;
    
    private float random;

    private int notDefaultWeapon;

    private float weaponProbability;

    public bool replacingItem = false;

    void Start()
    {
        
    }
    public void OpenUpgradeMenu() //opens the upgrade interface
    {
        //everytime the upgrade interface is displayed, 3 options will be offered to the player
        //1 weapon, 1 item, 1 random (item or weapon)
        
        random = Random.value; //this is to get a random value from 0 to 1, which will be used to handle the random option later
        weaponProbability = 0.5f; //50% chance of it being a weapon
        notDefaultWeapon = 0;

        InitializeWeaponUpgradeOptions();
        InitializaItemUpgradeOptions();

        
        Time.timeScale = 0f; //pause the game when upgrade menu comes out
        upgrade.SetActive(true);
        
        SetWeaponButton();
        SetItemButton();
        SetRandomButton();
        
    }
    public void CloseUpgradeMenu() //close upgrade menu
    {
        upgrade.SetActive(false);
        Time.timeScale = 1f; //unpause game
    }

    #region weapon upgrades
    public void InitializeWeaponUpgradeOptions() //initialize weapon upgrades options
    {
        List<Weapon> weapons = Game.GetWeaponList();
        List<Weapon> genericWeapon = new List<Weapon>(); //this is to get all generic weapons in the full list of weapons

        foreach(Weapon weapon in weapons)
        {
            if(weapon.isGeneric)
            {
                genericWeapon.Add(weapon); //all generic weapons is in the list now
            }
        }
        //when playing the game, the weapon upgrades that the player can get is either the default weapon or generic weapons
        //currently the list only has the generic weapons, so now the default weapon has to be added into the list as well
        //add default weapon into inventory
        genericWeapon.Add(playerInventory.weaponInventory[0]);

        List<Weapon> WeaponsInInventory = new List<Weapon>();

        foreach(Weapon weapon in playerInventory.weaponInventory)
        {
            WeaponsInInventory.Add(weapon);
        }

        foreach(Weapon weapon in WeaponsInInventory) //if any weapon in the inventory is at max level (level 3), remove from list so that there is no chance of getting the same weapon as upgrade option
        {
            if(weapon.initialLevel==3)
            {
                genericWeapon.Remove(weapon);
            }
        }

        //now, there is equal getting any weapon in the genericWeapon list as an upgrade
        //in the case that the player is unlucky and does not get default weapon upgrade for a long time
        //notDefaultWeapon field comes into use
        //it acts like a pity system for the default weapon appearance
        //if the default weapoon did not appear as an upgrade for 3 times in a row, the next time the player level up, it is guaranteed that one of the option is the default weapon
        if(notDefaultWeapon<4) //this is when the player has not yet hit not getting the default weapon as a upgrade option 3 times in a row
            {
                newWeapon = genericWeapon[Random.Range(0, genericWeapon.Count)]; //get a random weapon from the list
                genericWeapon.Remove(newWeapon); //remove weapon from list so that the random upgrade option is not the same weapon
                newWeapon2 = genericWeapon[Random.Range(0,genericWeapon.Count)]; //this is for the random upgrade option
                notDefaultWeapon++; //notDefaultWeapon is incremented by 1
            }
        else //this is if the player did not get the default weapon as upgrade option 3 times in a row
        {
            //it is now guaranteed that the default weapon will appear
            newWeapon = genericWeapon.Find(weapon => !weapon.isGeneric); //default weapon
            genericWeapon.Remove(newWeapon); //remove default weapon from list so tha trandom upgrades is not the same thing
            newWeapon2 = genericWeapon[Random.Range(0,genericWeapon.Count)]; //his is a random weapon that is for the random upgrade
            notDefaultWeapon = 0; //reset the pity count back to zero because this time around the default weapon has in fact appeared
        }
        if(!newWeapon.isGeneric || !newWeapon2.isGeneric) //check if any of the upgrade option is the default weapon
        {
                notDefaultWeapon = 0; //if any of the upgrades is the default weapon, reset the pity count to 0
        }
        
    }

    public void ChooseWeapon() //this is to handle weapon upgrade button
    {
        if(playerInventory.weaponInventory.Count<3 || playerInventory.weaponInventory.Contains(newWeapon)) //check if inventory is not full --> add weapon to inventory, or inventory already contains the weapon --> upgrade weapon
        {
            playerInventory.AddWeaponToInventory(newWeapon);

            CloseUpgradeMenu(); //close menu after choosing
        }
        else //this is when player inventory does not contain the selected weapon, and is full
        {
            replacingFromRandomButton = false;
            OpenWeaponReplacementPrompt(); //open prompt for player to choose weapon in inventory to replace
        }
    }

    public void SetWeaponButton() //set weapon upgrade button text and image
    {
        //set weapon button image
        string spriteFilePath = newWeapon.imageFilePath;
        #if UNITY_EDITOR
        Sprite weaponImage = AssetDatabase.LoadAssetAtPath<Sprite>(spriteFilePath);
        #endif
        weaponButtonImage.sprite = weaponImage;

        if(playerInventory.GetWeaponInventory().Contains(newWeapon)) //if weapon is in inventory --> set info to next level
        {
            List<WeaponUpgrades> upgradeList = Game.GetWeaponUpgradesList().FindAll(upgrade => upgrade.refID == newWeapon.id); //this is to find all levels for this specific weapon
            WeaponUpgrades nextLevelWeapon = upgradeList.Find(upgrade => upgrade.level == newWeapon.initialLevel + 1); //get the stats of next level for this weapon
            
            weaponDetails.text = newWeapon.name + "\n" +"\nLevel: " + nextLevelWeapon.level.ToString() + "\n" + "\n" + nextLevelWeapon.upgradeDesc;

        }
        else //not in inventory --> set info to level 1
        {
            weaponDetails.text = newWeapon.name + "\n" +"\nLevel: " + newWeapon.initialLevel.ToString() + "\n" + "\n" + newWeapon.basicDesc;
            
        }
    }

    #endregion weapon upgrades

    #region item upgrades
    public void InitializaItemUpgradeOptions() //initialize item upgrade options
    {
        
        List<item> items = Game.GetItemList();
        List<item> itemOptions = new List<item>();

        foreach(item item in items)
        {
            itemOptions.Add(item);
        }
        Debug.Log(itemOptions.Count);
        Debug.Log("Game list:" + items.Count);

        List<item> itemsInInventory = new List<item>();

        foreach(item item in playerInventory.itemInventory)
        {
            itemsInInventory.Add(item);
        }

        foreach(item item in itemsInInventory)
        {
            if(item.initiallevel==3)
            {
                itemOptions.Remove(item); //if any item in the inventory is at max level (level 3), remove from list so that there is no chance of getting the same item as upgrade option
            }
        }

        newItem = itemOptions[Random.Range(0, itemOptions.Count)]; //get a random item from list
        itemOptions.Remove(newItem); //remove the item from the list so that there is no chance of the rrandom upgrade option being the same thing
        newItem2 = itemOptions[Random.Range(0, itemOptions.Count)]; //this is for the random upgrade option
        
    }

    public void ChooseItem() //handle item upgrade button
    {
        if(playerInventory.itemInventory.Count<3 || playerInventory.itemInventory.Contains(newItem)) //if inventory is not full --> add to inventory, or inventory has the item --> upgrade item
        {
            playerInventory.AddItemToInventory(newItem); 

            CloseUpgradeMenu(); //close menu after selection

        }
        else //this is when player inventory does not contain the selected item, and is full
        {
            replacingFromRandomButton = false;
            OpenItemReplacementPrompt(); //open prompt for player to choose item in inventory to replace
        }

        player.ApplyItemEffects(); //apply effects of items 

    }

    public void SetItemButton() //set item upgrade option button text and image
    {
        //set item image for item button
        string spriteFilePath = newItem.imageFilePath;
        #if UNITY_EDITOR
        Sprite itemImage = AssetDatabase.LoadAssetAtPath<Sprite>(spriteFilePath);
        #endif
        itemButtonImage.sprite = itemImage;
        
        //set item text for item button
        if(playerInventory.GetItemInventory().Contains(newItem)) //if item is in inventory, set the text of item button to the next level stats of the item
        {
            List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == newItem.id); //this is to find all levels for this specific item
            ItemUpgrades nextLevelItem = upgradeList.Find(upgrade => upgrade.level == newItem.initiallevel + 1); //get the stats of next level for this weapon
            
            itemDetails.text = newItem.name + "\n" +"\nLevel: " + nextLevelItem.level.ToString() + "\n" + "\n" + nextLevelItem.upgradeDesc;
            
        }
        else //this is if the item is not already in inventory
        {
            itemDetails.text = newItem.name + "\n" +"\nLevel: " + newItem.initiallevel.ToString() + "\n" + "\n" + newItem.basicDesc;
            

        }
    }

    #endregion item upgrades

    #region random upgrades
    public void SetRandomButton() //set random upgrade option button text and image
    {
        

        if(random <weaponProbability) //if upgrade option is a weapon
        {
            //set weapon image
            string weaponSprite = newWeapon2.imageFilePath;
            #if UNITY_EDITOR
            Sprite weaponImage = AssetDatabase.LoadAssetAtPath<Sprite>(weaponSprite);
            #endif
            randomButtonImage.sprite = weaponImage;

            if(playerInventory.GetWeaponInventory().Contains(newWeapon2)) //set weapon upgrade text --> if weapon is in invenotry, set text to next level info
            {
                List<WeaponUpgrades> upgradeList = Game.GetWeaponUpgradesList().FindAll(upgrade => upgrade.refID == newWeapon2.id); //this is to find all levels for this specific weapon
                WeaponUpgrades nextLevelWeapon = upgradeList.Find(upgrade => upgrade.level == newWeapon2.initialLevel + 1); //get the stats of next level for this weapon
                
                randomDetails.text = newWeapon2.name + "\n" +"\nLevel: " + nextLevelWeapon.level.ToString() + "\n" + "\n" + nextLevelWeapon.upgradeDesc;

            }
            else //not in innventory --> level 1 info
            {
                randomDetails.text = newWeapon2.name + "\n" +"\nLevel: " + newWeapon2.initialLevel.ToString() + "\n" + "\n" + newWeapon2.basicDesc;
                
            }
        }
        else //if upgrade option is a item
        {
            //set item image
            string itemSprite = newItem2.imageFilePath;
            #if UNITY_EDITOR
            Sprite itemImage = AssetDatabase.LoadAssetAtPath<Sprite>(itemSprite);
            #endif
            randomButtonImage.sprite = itemImage;

            if(playerInventory.GetItemInventory().Contains(newItem2)) //set item upgrade text --> next level info if item is in inventory
            {
                List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == newItem2.id); //this is to find all levels for this specific item
                ItemUpgrades nextLevelItem = upgradeList.Find(upgrade => upgrade.level == newItem2.initiallevel + 1); //get the stats of next level for this weapon
                
                randomDetails.text = newItem2.name + "\n" +"\nLevel: " + nextLevelItem.level.ToString() + "\n" + "\n" + nextLevelItem.upgradeDesc;
                
            }
            else //not in inventory --> level 1 info
            {
                randomDetails.text = newItem2.name + "\n" +"\nLevel: " + newItem2.initiallevel.ToString() + "\n" + "\n" + newItem2.basicDesc;
             
            }
        }
        
    }
    public void ChooseRandom() //handle random upgrade option button text
    {
        //random upgrade button can either be a weapon or item upgrade
        
        if(random <weaponProbability) //this is to find out if upgrade is a weapon or item --> in this case, the upgrade is a weapon
        {
            if(playerInventory.weaponInventory.Count<3  || playerInventory.weaponInventory.Contains(newWeapon2)) //if inventory is not full --> add to inventory, or inventory has the weapon --> upgrade weapon
            {
                
                playerInventory.AddWeaponToInventory(newWeapon2);
                CloseUpgradeMenu();
            }
            else
            {
                replacingFromRandomButton =true;
                OpenWeaponReplacementPrompt(); //open replacement prompt if inventory is full and does not contain the weapon
            }
        }
        else //the upgrade is an item
        {   
            if(playerInventory.itemInventory.Count<3  || playerInventory.itemInventory.Contains(newItem2)) //if inventory not full --> add to inventory, if inventory has item --> upgrade item
            {
                playerInventory.AddItemToInventory(newItem2);

                CloseUpgradeMenu();

            }
            else
            {
                replacingFromRandomButton = true;
                OpenItemReplacementPrompt(); //open item replacement prompt if inventory is full and does not contain the selected item
            }

            player.ApplyItemEffects(); //apply effect of items
        }

    }

    #endregion random upgrades

    public void OnReplacement(int index) //close replacement menu upon selection
    {
        if(replacingWeapon) //weapon has been replaced, so replacement menu has to be closed and game unpaused
        {
            ReplaceWeapon(index);
            weaponReplacementMenu.SetActive(false);
            Time.timeScale = 1f; //unpause game
        }
        else //item has been replaced, so replacement menu has to be closed and game unpaused
        {
            
            ReplaceItem(index);
            replacingItem = false;
            itemReplacementMenu.SetActive(false);
            Time.timeScale = 1f; //unpause game
        }
    }

    public void ReOpenUppgradeMenu() //this is to go back to upgrade menu from the replacement interface
    {
        itemReplacementMenu.SetActive(false);
        weaponReplacementMenu.SetActive(false);
        upgrade.SetActive(true);
    }


    #region weapon replacement
   public void OpenWeaponReplacementPrompt() //open weapon replacement prompt
   {

        upgrade.SetActive(false);
        weaponReplacementMenu.SetActive(true);
        replacingWeapon = true;
        for(int i=0; i<playerInventory.weaponInventory.Count;i++) //set weapon details text
        {
            SetWeaponReplacementButton(i);
        }
   }

      public void ReplaceWeapon(int index) //handle weapon replacement
    {
       if(!replacingFromRandomButton) //weapon is from the weapon upgrade option button
       {
            playerInventory.ReplaceWeaponInInventory(index, newWeapon);
            
       }
       else //weapon is from the radom upgrade option button
       {
            playerInventory.ReplaceWeaponInInventory(index, newWeapon2);
            
       }
        
        
    }

       public void SetWeaponReplacementButton(int index) //set text for stats of weapons in inventory, and image of weapons
    {
        Weapon weapon = playerInventory.weaponInventory[index];
        TextMeshProUGUI weaponText =  weaponInventoryDetails[index];

        //set weapon image
        string weaponSprite = weapon.imageFilePath;
        #if UNITY_EDITOR
        Sprite weaponImage = AssetDatabase.LoadAssetAtPath<Sprite>(weaponSprite);
        #endif
        weaponInventoryImages[index].sprite = weaponImage;
        
        if(weapon.initialLevel == 1) //weapon is currently level 1
        {
            weaponText.text = weapon.name + "\n" +"\nLevel: " + weapon.initialLevel.ToString() + "\n" + "\n" + weapon.basicDesc;
        }
        else //weapon is level 2 and above
        {
            List<WeaponUpgrades> upgradeList = Game.GetWeaponUpgradesList().FindAll(upgrade => upgrade.refID == newWeapon2.id); //this is to find all levels for this specific weapon
            WeaponUpgrades weaponStats = upgradeList.Find(upgrade => upgrade.level == weapon.initialLevel); //get the stats of current level for this weapon
            weaponText.text = weapon.name + "\n" +"\nLevel: " + weapon.initialLevel.ToString() + "\n" + "\n" + weaponStats.upgradeDesc;
        }

    }
    #endregion weapon replacement

    #region item replacement

   public void OpenItemReplacementPrompt() //open item replacement prompt
   {
        upgrade.SetActive(false);
        itemReplacementMenu.SetActive(true);
        replacingWeapon = false;
        for(int i =0; i<playerInventory.itemInventory.Count; i++) //set item details text
        {
            SetItemReplacementButton(i);
        }
   }

    public void ReplaceItem(int index) //handle item replacement
    {    
        replacingItem = true;
        if(!replacingFromRandomButton) //if item is from the item upgrade option button
        {
            playerInventory.ReplaceItemInInvetory(index, newItem);
            
            player.ApplyItemEffects(); //apply effect of items
            
        }
        else //item is from the random upgrade option button 
        {
            playerInventory.ReplaceItemInInvetory(index, newItem2);
            player.ApplyItemEffects(); //apply effect of items
        }
        
    }

       public void SetItemReplacementButton(int index) //set text for stats of items in inventory, and image of the items in inventory
    {
        item item = playerInventory.itemInventory[index];
        TextMeshProUGUI itemText = itemInventoryDetails[index];

        //set item image
        string itemSprite = item.imageFilePath;
        #if UNITY_EDITOR
        Sprite itemImage = AssetDatabase.LoadAssetAtPath<Sprite>(itemSprite);
        #endif
        itemInventoryImages[index].sprite = itemImage;


        if(item.initiallevel == 1) //item is currently level 1
        {
            itemText.text = item.name + "\n" +"\nLevel: " + item.initiallevel.ToString() + "\n" + "\n" + item.basicDesc;
        }
        else //item is level 2 and above
        {
            List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == item.id); //this is to find all levels for this specific item
            ItemUpgrades itemStats = upgradeList.Find(upgrade => upgrade.level == item.initiallevel); //get the stats of current level for this item
            itemText.text = item.name + "\n" +"\nLevel: " + item.initiallevel.ToString() + "\n" + "\n" + itemStats.upgradeDesc;
        }
    }

   #endregion item replacement



    
}
