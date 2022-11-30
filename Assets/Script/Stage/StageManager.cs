using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Stage[] Allstages;
    public Stage stage;
    public float stageTime;
    public float currentTime;
    public List<int> waveTime;
    public List<int> namedTime;
    public List<Coroutine> nonWaveTimes = new();
    float sec;
    int min;

    private void Awake()
    {
        StageInit();
    }
    private void StageInit()
    {
        waveTime = new();
        namedTime = new();
        foreach(var w in stage.wave.Keys)
        {
            waveTime.Add(w);
        }
        foreach(var w in stage.namedEnemies.Keys)
        {
            namedTime.Add(w);
        }
        for(int i = 0; i<stage.NonWaveMulitply; i++)
        {
            nonWaveTimes.Add(StartCoroutine(NonWaveTime()));
        }
    }
    private IEnumerator NonWaveTime()
    {
        yield return new WaitForSeconds(Random.Range(stage.NonWaveDelayMin, stage.NonWaveDelayMax + 1));
        while (currentTime<=stageTime)
        {
            GameManager.Inst.enemyManager.EnemySpawn(stage.appearEnemies[Random.Range(0, stage.appearEnemies.Length)]);
            yield return new WaitForSeconds(Random.Range(stage.NonWaveDelayMin, stage.NonWaveDelayMax + 1));
        }
    }
    private IEnumerator SpawnWave()
    {
        Queue<string> wave = new();
        List<string> tempWave = new();
        foreach(KeyValuePair<string, int> mon in stage.wave[waveTime[0]].monsters)
        {
            for(int i = 0; i<mon.Value; i++)
            {
                tempWave.Add(mon.Key);
            }
        }
        for(int i = 0; i<tempWave.Count; i++)
        {
            (tempWave[i], tempWave[Random.Range(0, tempWave.Count)]) = (tempWave[Random.Range(0, tempWave.Count)], tempWave[i]);
        }
        foreach (string monster in tempWave)
        {
            wave.Enqueue(monster);
        }
        while(wave.Count > 0)
        {
            GameManager.Inst.enemyManager.EnemySpawn(wave.Peek());
            wave.Dequeue();
            yield return new WaitForSeconds(Random.Range(0f, 1f));
        }
    }
    private void Update()
    {
        if(currentTime >= stageTime)
        {
            for (int i = 0; i < nonWaveTimes.Count; i++)
            {
                StopCoroutine(nonWaveTimes[i]);
            }
        }
        else
        {
            currentTime += Time.deltaTime;
            sec += Time.deltaTime;
            GameManager.Inst.player.stageBar.fillAmount = currentTime / stageTime;

            if (waveTime.Count>0 && waveTime[0] == (int)currentTime)
            {
                StartCoroutine(SpawnWave());
                waveTime.RemoveAt(0);
            }
        }
        GameManager.Inst.player.Timer.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);
        if((int)sec > 59)
        {
            sec = 0;
            min++;
        }
    }
}
