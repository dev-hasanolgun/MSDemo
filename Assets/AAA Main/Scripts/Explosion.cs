using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float _timer;
    
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 1f)
        {
            _timer = 0f;
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        PoolManager.Instance.PoolObject("explosionParticle", this);
    }
}
