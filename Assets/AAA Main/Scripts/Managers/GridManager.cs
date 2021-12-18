using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector3[,] Grid;
    public Vector2Int GridSize;
    public float Radius;

    private float _diamater;
    private int _gridSizeX;
    private int _gridSizeY;

    public void CreateGridMap()
    {
        _diamater = Radius * 2f;
        _gridSizeX = Mathf.RoundToInt(GridSize.x / _diamater);
        _gridSizeY = Mathf.RoundToInt(GridSize.y / _diamater);
        
        Grid = new Vector3[_gridSizeX, _gridSizeY];
        
        var snapPos = transform.position - Vector3.right * GridSize.x / 2f - Vector3.forward * GridSize.y / 2f;
        
        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                var pos = snapPos + Vector3.right * (x * _diamater + Radius) + Vector3.forward * (y * _diamater + Radius);
                Grid[x, y] = pos;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        var diamater = Radius * 2f;
        var gridSizeX = Mathf.RoundToInt(GridSize.x / diamater);
        var gridSizeY = Mathf.RoundToInt(GridSize.y / diamater);
        
        var snapPos = transform.position - Vector3.right * GridSize.x / 2f - Vector3.forward * GridSize.y / 2f;
        
        Gizmos.DrawWireCube(transform.position, new Vector3(GridSize.x, 1f, GridSize.y));
        
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                var pos = snapPos + Vector3.right * (x * diamater + Radius) + Vector3.forward * (y * diamater + Radius);
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(pos, Vector3.one * (diamater - 0.1f));
            }
        }
    }
}
