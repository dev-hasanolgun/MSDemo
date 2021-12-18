using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GridManager GridManager;
    public ShipController ShipController;
    public BarrelSpawner BarrelSpawner;

    private void Start()
    {
        GridManager.CreateGridMap();
        BarrelSpawner.GenerateBarrels(GridManager.Grid);
        ShipController.InitializeShip();
        ShipController.PosLimit = GridManager.GridSize.ToVector3Int();
    }
}