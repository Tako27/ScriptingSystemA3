using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Game
{
    private static Character chara;
    private static Weapon weapon;
    private static item item;
    private static List<Character> charList;
    private static List<Weapon> weaponList;
    private static List<item> itemList;


    public static Character GetChar()
    {
        return chara;
    }
    public static void SetChar(Character achar)
    {
        chara = achar;
    }

    public static Character GetCharByID(string id)
    {
        return charList.Find(x => x.id == id);;
    }
    
    public static void SetCharList(List<Character> aList)
    {
        charList = aList;
    }
    public static List<Character> GetCharList()
    {
        return charList;
    }

    public static Weapon GetWeapon()
    {
        return weapon;
    }
    private static void SetWeapon(Weapon aweapon)
    {
        weapon = aweapon;
    }

    public static Weapon GetWeaponByID(string id)
    {
        return weaponList.Find(x=>x.id == id);
    }

    public static void SetWeaponList(List<Weapon> aList)
    {
        weaponList = aList;
    }

    public static List<Weapon> GetWeaponList()
    {
        return weaponList;
    }

    public static item GetItem()
    {
        return item;
    }
    private static void SetItem(item aitem)
    {
        item = aitem;
    }

    public static item GetItemByID(string id)
    {
        return itemList.Find(x=>x.id == id);
    }

    public static void SetItemList(List<item> aList)
    {
        itemList = aList;
    }

    public static List<item> GetItemList()
    {
        return itemList;
    }


}
