using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    private Transform _currentSpawner;

    //Singleton
    private static GameManager instance;
    public static GameManager GetGemeManager()
    {
        return instance;
    }

    void Awake()
    {
        if(instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetSpawner(Transform _newSpawner) { _currentSpawner = _newSpawner; }
    public Vector3 GetSpawnerPosition() { return _currentSpawner.position; }
}
