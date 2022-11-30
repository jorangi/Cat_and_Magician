using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class LevelupMenu : MonoBehaviour
{
    private int min = 4;
    private int max = 5;
    public GameObject levelupItemPrefab;
    public Transform list;


    private void OnEnable()
    {
        GameManager.Inst.player.levelupCount--;
        Time.timeScale = 0.0f;
        GameManager.Inst.player.input.Disable();

        List<ItemData> items = GameManager.Inst.player.itemDatas.ToList();
        for(int i = items.Count-1; i>0; i--)
        {
            if (GameManager.Inst.player.itemLevels[items[i].id] == items[i].itemUp.Length)
            {
                items.RemoveAt(i);
            }
        }
        if(items.Count == 0)
        {
            GameObject obj = Instantiate(levelupItemPrefab, list);
            ItemData data = GameManager.Inst.player.StardustData;
            LevelupItem tempData = obj.GetComponent<LevelupItem>();
            tempData.iconImage.sprite = data.icon;
            tempData.titleText.text = data.title;
            tempData.descText.text = data.itemUp[0];
            tempData.GetComponent<Button>().onClick.AddListener(() => { GameManager.Inst.player.StarDust += 50; gameObject.SetActive(false); });
        }
        else
        {
            for(int i = 0; i < Random.Range(min, max); i++)
            {
                GameObject obj = Instantiate(levelupItemPrefab, list);
                int index = Random.Range(0, items.Count);
                ItemData data = items[index];
                LevelupItem tempData = obj.GetComponent<LevelupItem>();
                tempData.iconImage.sprite = data.icon;
                switch(GameManager.Inst.player.itemLevels[data.id])
                {
                    case 0:
                        tempData.iconImage.GetComponentInChildren<TextMeshProUGUI>().text = "¥°";
                        break;
                    case 1:
                        tempData.iconImage.GetComponentInChildren<TextMeshProUGUI>().text = "¥±";
                        break;
                    case 2:
                        tempData.iconImage.GetComponentInChildren<TextMeshProUGUI>().text = "¥²";
                        break;
                    case 3:
                        tempData.iconImage.GetComponentInChildren<TextMeshProUGUI>().text = "¥³";
                        break;
                    case 4:
                        tempData.iconImage.GetComponentInChildren<TextMeshProUGUI>().text = "¥´";
                        break;
                }
                tempData.titleText.text = data.title;
                tempData.descText.text = data.itemUp[GameManager.Inst.player.itemLevels[data.id]];
                tempData.GetComponent<Button>().onClick.AddListener(() => { GameManager.Inst.player.AddItem(data.id); gameObject.SetActive(false); });

                items.RemoveAt(index);
            }
        }
    }
    private void OnDisable()
    {
        Time.timeScale = 1.0f;
        GameManager.Inst.player.input.Enable();
        foreach (Transform item in list)
        {
            Destroy(item.gameObject);
        }
    }
}
