using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    public Barrel BarrelPrefab;
    public int InitialSpawnAmount;
    
    private List<Vector3> _emptyPosList;
    
    public void GenerateBarrels(Vector3[,] grid)
    {
        var posList = grid.Array2DIntoList().ShuffleList();
        for (int i = 0; i < InitialSpawnAmount; i++)
        {
            if (i >= posList.Count) break;
            var barrel = PoolManager.Instance.GetObjectFromPool("barrelPool", BarrelPrefab);
            barrel.transform.position = posList[0]+Vector3.up*0.2f;
            
            posList.RemoveAt(0);
        }
        _emptyPosList = new List<Vector3>(posList);
    }
    
    private void SpawnBarrel(Dictionary<string, object> message)
    {
        if (_emptyPosList.Count <= 0) return;

        var randomIndex = Random.Range(0, _emptyPosList.Count);
        var barrel = PoolManager.Instance.GetObjectFromPool("barrelPool", BarrelPrefab);
        barrel.transform.position = _emptyPosList[randomIndex]+Vector3.up*0.2f;
        _emptyPosList.RemoveAt(randomIndex);

        var oldBarrelPos = (Vector3) message["oldBarrelPos"];
        _emptyPosList.Add(oldBarrelPos);
    }

    private void OnEnable()
    {
        EventManager.StartListening("OnBarrelCollect", SpawnBarrel);
    }
    
    private void OnDisable()
    {
        EventManager.StopListening("OnBarrelCollect", SpawnBarrel);
    }
}
