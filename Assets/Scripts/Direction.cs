using System;
using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
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
            _ => throw new NotImplementedException()
        };
    public static Direction Opposite(this Direction self) =>
                self switch
                {
                    Direction.Up => Direction.Down,
                    Direction.Down => Direction.Up,
                    Direction.Left => Direction.Right,
                    Direction.Right => Direction.Left,
                    _ => throw new NotImplementedException()
                };
}


