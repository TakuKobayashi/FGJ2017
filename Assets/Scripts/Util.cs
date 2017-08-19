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
}
