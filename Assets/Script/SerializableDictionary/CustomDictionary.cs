using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ConsoleDictionary : SerializableDictionary <string, string>{ }

[System.Serializable]
public class DropsDictionary : SerializableDictionary <string, GameObject>{ }

[System.Serializable]
public class EnemyDictionary : SerializableDictionary<string, GameObject> { }

[System.Serializable]
public class BulletSpawnerDictionary : SerializableDictionary <string, BulletSpawner>{ }
[System.Serializable]
public class BulletDataDictionary : SerializableDictionary <string, BulletData>{ }
