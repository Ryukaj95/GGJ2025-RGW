using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] public Collider2D playerArena;

    [SerializeField] public Collider2D bulletArena;

    [SerializeField] public BigInteger score;

    [SerializeField] public Points points;

    public void Lose()
    {

    }

    public void DropPoints(UnityEngine.Vector2 pos)
    {
        Instantiate(points, pos, UnityEngine.Quaternion.identity);
    }

    public void AddPoints(int points)
    {
        score += points;
    }

}
