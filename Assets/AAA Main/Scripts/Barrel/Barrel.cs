using UnityEngine;

public class Barrel : MonoBehaviour
{
    private void OnDisable()
    {
        PoolManager.Instance.PoolObject("barrelPool", this);
    }
}