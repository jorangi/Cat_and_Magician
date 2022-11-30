using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName ="Scriptable Object/ItemData", order = int.MaxValue)]
public class ItemData:ScriptableObject
{
    public string id;
    public Sprite icon;
    public string title;
    public string[] itemUp = { };
    public string Upgraded;
}
