using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PunchedObject : MonoBehaviour {
    public Action<Collision, Vector3> OnHit;

    void OnCollisionEnter(Collision collision){
        if(OnHit != null){
			OnHit(collision, new Vector3(g_fTobuSokudo_x, g_fTobuSokudo_y, g_fTobuSokudo_z));
        }
	}

	// 手の座標
	Vector3 g_pos_prev;   // 前の位置座標
	Vector3 g_pos_cur;   // 前の位置座標

	// 跳ね返った時の飛ぶ速度
	float g_fTobuSokudo_x = 0.0f;
	float g_fTobuSokudo_y = 0.0f;
	float g_fTobuSokudo_z = 0.0f;


	// 前回のUpdateからの経過時間（ミリ秒）
	float g_fKeikazikan_ms = 0.0f;

	// 殴るモードか飛ぶモードか
	const int MODO_NAGURU = 0;
	const int MODO_TOBU = 1;
	int gamemodo = MODO_NAGURU; // 現在のモード

	//debug用
	float g_fKeikaTime = 0.0f;

	// Use this for initialization
	void Start()
	{
		g_pos_prev = this.gameObject.transform.localPosition;
		//mText.text = "aaaa";
	}


	// Update is called once per frame
	void Update()
	{
		// 前回Updateからの経過時間取得
		g_fKeikazikan_ms = Time.deltaTime;

		switch (gamemodo)
		{
			case MODO_NAGURU:
				gamemodo = naguruModo();
				break;
			case MODO_TOBU:
				gamemodo = tobuModo();
				break;
		}
	}

	// 衝突前（殴り待ちモード）
	int naguruModo()
	{

		// debug用
		testMove(); // テスト用。手の移動

		// 反発係数
		float fHanpatuKeisuu;

		// 現在の位置情報取得
		g_pos_cur = this.gameObject.transform.localPosition;

		// 衝突部位判定
		switch (chkOnaka())
		{
			// 当たった部位ごとに反発係数を取得
			case 0: // 当たっていない場合
				break;
			case 1: // 部位が●●の場合
				fHanpatuKeisuu = 0.1f;     // 反発係数
				setTobusokudo(fHanpatuKeisuu);
				return MODO_TOBU;
				break;
			case 2: // 部位が●●の場合
				fHanpatuKeisuu = 0.5f;     // 反発係数
				setTobusokudo(fHanpatuKeisuu);
				return MODO_TOBU;

		}
		this.gameObject.transform.localPosition = g_pos_cur;
		g_pos_prev = g_pos_cur;

		return MODO_NAGURU;
	}


	//    void setTobusokudo(float fHanpatuKeisuu, float fNaguruSokudo_x, float fNaguruSokudo_y, float fNaguruSokudo_z)
	void setTobusokudo(float fHanpatuKeisuu)
	{
		// 殴る手の速度計算（単位：座標数値/ms）
		float fNaguruSokudo_x = (g_pos_cur.x - g_pos_prev.x) / g_fKeikazikan_ms;
		float fNaguruSokudo_y = (g_pos_cur.y - g_pos_prev.y) / g_fKeikazikan_ms;
		float fNaguruSokudo_z = (g_pos_cur.z - g_pos_prev.z) / g_fKeikazikan_ms;

		// 跳ね返った時の飛ぶ速度計算（単位：座標数値/ms）
		g_fTobuSokudo_x = -1 * fHanpatuKeisuu * fNaguruSokudo_x;
		g_fTobuSokudo_y = -1 * fHanpatuKeisuu * fNaguruSokudo_y;
		g_fTobuSokudo_z = -1 * fHanpatuKeisuu * fNaguruSokudo_z;
	}


	int tobuModo()
	{
		g_pos_cur = this.gameObject.transform.localPosition;
		g_pos_cur.x += g_fTobuSokudo_x * Time.deltaTime;
		g_pos_cur.y += g_fTobuSokudo_y * Time.deltaTime;
		g_pos_cur.z += g_fTobuSokudo_z * Time.deltaTime;
		this.gameObject.transform.localPosition = g_pos_cur;
		return MODO_TOBU;
	}


	int chkOnaka()
	{
		//        g_fKeikaTime += Time.deltaTime;
		if (g_pos_cur.z >= -0.25f)
		{
			/*
             *Vector3 scale = this.gameObject.transform.localScale;
                        scale.x += scale.x;
                        this.gameObject.transform.localScale = scale;
            */
			return 1;
		}
		return 0;

	}

	// テスト用。手の移動
	private void testMove()
	{

		Vector3 pos = this.gameObject.transform.localPosition;
		if (pos.z <= 5)
		{
			// g_fKeikazikan_ms
			pos.z += 0.2f;
			pos.x += 0.5f;
			pos.y -= 0.02f;
			this.gameObject.transform.localPosition = pos;
		}
	}
}
