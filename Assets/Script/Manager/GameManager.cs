using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager inst = null;
    public EnemyManager enemyManager;
    public ConsoleManager consoleManager;
    public Player player;
    public DropsDictionary Drops = new();
    public static GameManager Inst
    {
        get
        {
            if(!inst)
            {
                inst = FindObjectOfType<GameManager>();
            }
            return inst;
        }
    }
    private void Awake()
    {
        if(inst == null)
        {
            inst = this;
        }
        else if(inst != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 120;
        enemyManager = FindObjectOfType<EnemyManager>();
        consoleManager = FindObjectOfType<ConsoleManager>();
        player = FindObjectOfType<Player>();
    }
}