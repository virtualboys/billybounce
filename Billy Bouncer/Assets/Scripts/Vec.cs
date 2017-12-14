
using System;
using UnityEngine;

public static class Vec
{
	/// <summary>
	/// returns true if magnitude of vec is < range
	/// </summary>
	/// <returns><c>true</c> if this instance is mag less than the specified vec range; otherwise, <c>false</c>.</returns>
	/// <param name="vec">Vec.</param>
	/// <param name="range">Range.</param>
	public static bool IsMagLessThan(Vector3 vec, float range) {
		return vec.sqrMagnitude < range * range;
	}

	/// <summary>
	/// Returns true if magnitude of vec1 is less than magnitude of vec2
	/// </summary>
	/// <returns><c>true</c> if is mag less than the specified vec1 vec2; otherwise, <c>false</c>.</returns>
	/// <param name="vec1">Vec1.</param>
	/// <param name="vec2">Vec2.</param>
	public static bool IsMagLessThan(Vector3 vec1, Vector3 vec2) {
		return vec1.sqrMagnitude < vec2.sqrMagnitude;
	}

	/// <summary>
	/// Determines if target is within <range> of source, ignoring y components
	/// </summary>
	/// <returns><c>true</c> if is in range ignore y the specified target source range; otherwise, <c>false</c>.</returns>
	/// <param name="target">Target.</param>
	/// <param name="source">Source.</param>
	/// <param name="range">Range.</param>
	public static bool IsInRangeIgnoreY(Vector3 target, Vector3 source, float range) {
		target.y = 0;
		source.y = 0;
		return (target - source).sqrMagnitude < range;
	}

	public static Vector3 SetX(Vector3 vec, float x) {
		return new Vector3 (x, vec.y, vec.z);
	}

	public static Vector3 SetY(Vector3 vec, float y) {
		return new Vector3 (vec.x, y, vec.z);
	}

	public static Vector3 SetZ(Vector3 vec, float z) {
		return new Vector3 (vec.x, vec.y, z);
	}
}

