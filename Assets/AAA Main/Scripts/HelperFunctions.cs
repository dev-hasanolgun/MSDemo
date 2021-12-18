using System.Collections.Generic;
using UnityEngine;

public static class HelperFunctions
{
    public static Vector3 ToVector3XZ(this Vector2 value)
    {
        return new Vector3(value.x, 0f, value.y);
    }
    
    public static Vector3Int ToVector3Int(this Vector2Int value)
    {
        return new Vector3Int(value.x, 0, value.y);
    }

    public static T GetRandom2DArrayElement<T>(this T[,] array)
    {
        var randomX = Random.Range(0,array.GetLength(0));
        var randomY = Random.Range(0,array.GetLength(1));

        return array[randomX, randomY];
    }

    public static List<T> Array2DIntoList<T>(this T[,] array)
    {
        var list = new List<T>();
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                list.Add(array[i,j]);
            }
        }

        return list;
    }
    public static List<T> ShuffleList<T>(this List<T> list)
    {
        var shuffledCoords = new List<T>();
        var iterationCount = list.Count;
        for (int i = 0; i < iterationCount; i++)
        {
            var randomIndex = Random.Range(0, list.Count);
            shuffledCoords.Add(list[randomIndex]);
            list.RemoveAt(randomIndex);
        }
        return shuffledCoords;
    }
}
