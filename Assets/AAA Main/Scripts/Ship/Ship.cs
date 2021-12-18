using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Ship LeadShip;
    public List<Trace> Waypoints = new List<Trace>();
    public float Size = 2.5f;
    public Explosion Particle;

    private Trace _previousTrace;
    private Vector3 _tempPos;
    private float _totalDistance;

    public void MoveShip()
    {
        var pos = transform.position;
        var lead = LeadShip.transform;
        var trace = new Trace(lead.position, lead.rotation);
        
        if (_previousTrace.Position != trace.Position)
        {
            Waypoints.Add(trace);
        }
        _previousTrace = trace;
        
        _totalDistance += Vector3.Distance(_tempPos, Waypoints[^1].Position);
        _tempPos = Waypoints[^1].Position;
        
        if (_totalDistance >= Size)
        {
            _totalDistance -= Vector3.Distance(pos, Waypoints[0].Position);

            transform.position = Waypoints[0].Position;
            transform.rotation = Waypoints[0].Rotation;

            Waypoints.RemoveAt(0);
        }
    }

    private void Start()
    {
        _tempPos = transform.position;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrel"))
        {
            var particle = PoolManager.Instance.GetObjectFromPool("explosionParticle", Particle);
            particle.transform.position = other.transform.position;
            EventManager.TriggerEvent("OnBarrelCollect", new Dictionary<string, object>{{"oldBarrelPos", other.transform.position}});
            other.gameObject.SetActive(false);
        }
    }
}