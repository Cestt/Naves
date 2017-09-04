using UnityEngine;
using System.Collections;

public static class Utils {
	/// <summary>
	/// LookAt for 2D
	/// </summary>
	/// <param name="source">Source.</param>
	/// <param name="target">Target.</param>
	/// <param name="offset">Offset.</param>
	public static void LookAt2D(Transform source, Transform target, float offset = -90){
		Vector3 dir = target.position - source.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		angle += offset;
		source.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	/// <summary>
	/// LookAt for 2D
	/// </summary>
	/// <param name="source">Source.</param>
	/// <param name="target">Target.</param>
	/// <param name="offset">Offset.</param>
	public static void LookAt2D(Transform source, Vector3 target, float offset = -90){
		Vector3 dir = target - source.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		angle += offset;
		source.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	/// <summary>
	/// LookAt for 2D
	/// </summary>
	/// <param name="rotationSpeed">Rotation speed.</param>
	/// <param name="source">Source.</param>
	/// <param name="target">Target.</param>
	/// <param name="offset">Offset.</param>
	public static void LookAt2D(float rotationSpeed , Transform source, Vector3 target, float offset = -90){
		Vector3 dir = target - source.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		angle += offset;
		source.rotation =  Quaternion.Slerp (source.rotation, Quaternion.Euler (0, 0, angle), rotationSpeed * Time.deltaTime);
	}

	/// <summary>
	/// LookAt for 2D
	/// </summary>
	/// <param name="source">Source.</param>
	/// <param name="target">Target.</param>
	/// <param name="min">Minimum.</param>
	/// <param name="max">Max.</param>
	/// <param name="offset">Offset.</param>
	public static void LookAt2D(Transform source, Vector3 target,float min, float max, float offset = -90){
		Vector3 dir = target - source.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		angle += offset;
		angle = Mathf.Clamp (angle, min, max);
		source.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
