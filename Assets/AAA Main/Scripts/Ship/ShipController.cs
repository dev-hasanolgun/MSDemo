using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public Joystick Joystick;
    public float Speed = 6f;
    public Ship ShipPrefab;
    public List<Ship> Ships = new();
    public bool IsSnakeEnabled = false;

    [HideInInspector] public Vector3Int PosLimit;
    
    private Vector3 _target;

    public void InitializeShip()
    {
        var ship = Instantiate(ShipPrefab, transform.position, Quaternion.identity);
        Ships.Add(ship);
    }
    private void SpawnShip(Dictionary<string,object> message)
    {
        if (IsSnakeEnabled)
        {
            var lead = Ships[^1].transform;
            var ship = Instantiate(ShipPrefab, lead.position, Quaternion.identity);
            ship.LeadShip = Ships[^1];
            ship.transform.rotation = ship.LeadShip.transform.rotation;
            Ships.Add(ship);
        }
    }

    private void Move()
    {
        if (Ships.Count == 0 || Joystick.Magnitude == 0) return;

        var headShip = Ships[0].transform;
        var pos = headShip.position;
        var rot = Quaternion.LookRotation(Joystick.Direction.ToVector3XZ());
        headShip.rotation = Quaternion.Lerp(headShip.rotation, rot, Time.deltaTime * 10f);
        pos += headShip.forward * Time.deltaTime * Speed;
        pos.x = Mathf.Clamp(pos.x, -PosLimit.x/2f, PosLimit.x/2f);
        pos.y = Mathf.Clamp(pos.y, -PosLimit.y/2f, PosLimit.y/2f);
        headShip.position = pos;
        
        for (int i = 1; i < Ships.Count; i++)
        {
            Ships[i].MoveShip();
        }
    }

    public void ToggleSnake()
    {
        IsSnakeEnabled = !IsSnakeEnabled;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnEnable()
    {
        EventManager.StartListening("OnBarrelCollect", SpawnShip);
    }
    
    private void OnDisable()
    {
        EventManager.StopListening("OnBarrelCollect", SpawnShip);
    }
}