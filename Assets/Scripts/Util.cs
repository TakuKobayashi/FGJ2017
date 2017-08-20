using UnityEngine;
using System.Collections;

public class Util
{
	public static float GetInitialVelocity(float gravity, float resistance, float mass, float distance, float theta, float offsetY)
	{
		var k = resistance;
		var g = gravity;
		var m = mass;
		var y0 = offsetY;
		var r = theta / 180.0f * Mathf.PI;
		var L = distance;

		var A = -m / k * g;
		var B = -k / m;
		var C = -g;
		var D = k / m * y0 + Mathf.Tan(r) * k / m * L + m / k * g;

		var T = -D / C - 1.0f / B * GetLambertW(A * B / C * Mathf.Exp(-B * D / C));
		return k * L / m / (1.0f - Mathf.Exp(-k / m * T)) / Mathf.Cos(r);
	}

	public static float GetLambertW(float x)
	{
		var res = PrecLambertW(x, DesyLambertW(x));
		return res;
	}

	static float PrecLambertW(float x, float initial = 0, float prec = 0.00000001f, int iteration = 100)
	{
		var w = initial;
		var i = 0;
		for (i = 0; i < iteration; i++)
		{
			var wTimesExpW = w * Mathf.Exp(w);
			var wPlusOneTimesExpW = (w + 1) * Mathf.Exp(w);
			if (prec > Mathf.Abs((x - wTimesExpW) / wPlusOneTimesExpW))
			{
				break;
			}
			w = w - (wTimesExpW - x) / (
				wPlusOneTimesExpW - (w + 2) * (wTimesExpW - x) / (2 * w + 2));
		}
		return w;
	}

	// ランベルトのW関数
	static float DesyLambertW(float x)
	{
		float lx1;
        //漸化式を用いて近似解を求めているらしい
		if (x <= 500.0)
		{
			lx1 = Mathf.Log(x + 1.0f);
			return 0.665f * (1.0f + 0.0195f * lx1) * lx1 + 0.04f;
		}
		var res = Mathf.Log(x - 4.0f) - (1.0f - 1.0f / Mathf.Log(x)) * Mathf.Log(Mathf.Log(x));
		return res;
	}




    public static Vector3 ShootVectorFromSpeed(Vector3 startPosition, Vector3 targetPosition, float speed)
	{
		if (speed <= 0.0f)
		{
			// その位置に着地させることは不可能のようだ！
			Debug.LogWarning("!!");
            return Vector3.zero;
		}

		// xz平面の距離を計算。
		Vector2 startPos = new Vector2(startPosition.x, startPosition.z);
		Vector2 targetPos = new Vector2(targetPosition.x, targetPosition.z);
		float distance = Vector2.Distance(targetPos, startPos);

		float time = distance / speed;

		return ShootFixedTime(startPosition, targetPosition, time);
	}

	public static Vector3 ShootFixedTime(Vector3 startPosition, Vector3 i_targetPosition, float i_time)
	{
		float speedVec = ComputeVectorFromTime(startPosition, i_targetPosition, i_time);
		float angle = ComputeAngleFromTime(startPosition, i_targetPosition, i_time);

		if (speedVec <= 0.0f)
		{
			// その位置に着地させることは不可能のようだ！
			Debug.LogWarning("!!");
			return Vector3.zero;
		}

		return ConvertVectorToVector3(startPosition, speedVec, angle, i_targetPosition);
	}

	private static float ComputeVectorFromTime(Vector3 startPosition, Vector3 i_targetPosition, float i_time)
	{
		Vector2 vec = ComputeVectorXYFromTime(startPosition, i_targetPosition, i_time);

		float v_x = vec.x;
		float v_y = vec.y;

		float v0Square = v_x * v_x + v_y * v_y;
		// 負数を平方根計算すると虚数になってしまう。
		// 虚数はfloatでは表現できない。
		// こういう場合はこれ以上の計算は打ち切ろう。
		if (v0Square <= 0.0f)
		{
			return 0.0f;
		}

		float v0 = Mathf.Sqrt(v0Square);

		return v0;
	}

	private static float ComputeAngleFromTime(Vector3 startPosition, Vector3 i_targetPosition, float i_time)
	{
		Vector2 vec = ComputeVectorXYFromTime(startPosition, i_targetPosition, i_time);

		float v_x = vec.x;
		float v_y = vec.y;

		float rad = Mathf.Atan2(v_y, v_x);
		float angle = rad * Mathf.Rad2Deg;

		return angle;
	}

	private static Vector3 ConvertVectorToVector3(Vector3 startPos, float i_v0, float i_angle, Vector3 i_targetPosition)
	{
		Vector3 targetPos = i_targetPosition;
		startPos.y = 0.0f;
		targetPos.y = 0.0f;

		Vector3 dir = (targetPos - startPos).normalized;
		Quaternion yawRot = Quaternion.FromToRotation(Vector3.right, dir);
		Vector3 vec = i_v0 * Vector3.right;

		vec = yawRot * Quaternion.AngleAxis(i_angle, Vector3.forward) * vec;

		return vec;
	}

	private static Vector2 ComputeVectorXYFromTime(Vector3 startPosition, Vector3 i_targetPosition, float i_time)
	{
		// 瞬間移動はちょっと……。
		if (i_time <= 0.0f)
		{
			return Vector2.zero;
		}


		// xz平面の距離を計算。
		Vector2 startPos = new Vector2(startPosition.x, startPosition.z);
		Vector2 targetPos = new Vector2(i_targetPosition.x, i_targetPosition.z);
		float distance = Vector2.Distance(targetPos, startPos);

		float x = distance;
		// な、なぜ重力を反転せねばならないのだ...
		float g = -Physics.gravity.y;
		float y0 = startPosition.y;
		float y = i_targetPosition.y;
		float t = i_time;

		float v_x = x / t;
		float v_y = (y - y0) / t + (g * t) / 2;

		return new Vector2(v_x, v_y);
	}
}
