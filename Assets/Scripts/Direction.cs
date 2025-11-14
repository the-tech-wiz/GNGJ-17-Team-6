using System;
using UnityEngine;
[System.Flags]
public enum Direction
{
    None = 0,
    Up = 1 << 0,   // 1
    Right = 1 << 1,   // 2
    Down = 1 << 2,   // 4
    Left = 1 << 3,   // 8
}

static class DirectionMethods
{
    public static Vector2 ToVec(this Direction self) =>
        self switch
        {
            Direction.Up => Vector2.up,
            Direction.Down => Vector2.down,
            Direction.Left => Vector2.left,
            Direction.Right => Vector2.right,
            Direction.None => Vector2.zero,
        };
    public static Direction Opposite(this Direction self) =>
                self switch
                {
                    Direction.Up => Direction.Down,
                    Direction.Down => Direction.Up,
                    Direction.Left => Direction.Right,
                    Direction.Right => Direction.Left,
                    Direction.None => Direction.None,
                    _ => throw new NotImplementedException()
                };
}


