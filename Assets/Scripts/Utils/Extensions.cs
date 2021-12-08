using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Extensions holds a set of utility functions.
/// </summary>
public static class Extensions {

    /// <summary>
    /// Returns Vector2 based on x and y values of vector.
    /// </summary>
    public static Vector2 ToVec2(this Vector3 vector) {
        return new Vector2(vector.x, vector.y);
    }

    /// <summary>
    /// Returns Vector3 based on x and y values of vector.
    /// Z value is set to 0
    /// </summary>
    public static Vector3 ToVec3(this Vector2 vector) {
        return new Vector3(vector.x, vector.y, 0f);
    }

    /// <summary>
    /// Returns a Vector2 where x and y values 
    /// are clamped in respect to min and max provided.
    /// </summary>
    public static Vector2 Clamp(this Vector2 vector, Vector2 min, Vector2 max) {
        vector.x = Mathf.Clamp(vector.x, min.x, max.x);
        vector.y = Mathf.Clamp(vector.y, min.y, max.y);

        return vector;
    }

    /// <summary>
    /// Returns true if x and y values are within 
    /// the min and max range provided. 
    /// </summary>
    public static bool WithinRange(this Vector2 vector, Vector2 min, Vector2 max) {
        return vector.x <= max.x
                && vector.x >= min.x
                && vector.y <= max.y
                && vector.y >= min.y;
    }

    /// <summary>
    /// Checks if a position is within distance of a target position.
    /// Has better performance than using Vector3.Distance because it
    /// avoid using square root :)
    /// </summary>
    public static bool IsWithinDistanceOf(this Vector3 position, Vector3 targetPosition, float distance) {
        return (targetPosition - position).sqrMagnitude <= distance * distance;
    }

    /// <summary>
    /// 2D version of:
    /// <see cref="IsWithinDistanceOf(Vector3, Vector3, float)"/>
    /// </summary>
    public static bool IsWithinDistanceOf(this Vector2 position, Vector2 targetPosition, float distance) {
        return (targetPosition - position).sqrMagnitude <= distance * distance;
    }

}
