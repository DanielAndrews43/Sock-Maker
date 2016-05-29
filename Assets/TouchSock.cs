using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class TouchSock : MonoBehaviour {

	//Sock + score
	float score = 0;
	public Text scoreText;
	float time;
	bool clicked;
	public Collider2D boxCollider;

	//Plus One Spawns
	public GameObject plus1;
	public float plus1Chance;
	ArrayList plusOnes;
	ArrayList plusOnesTime;
	public float plus1Time;

	//Upgrade!
	public Text scoreMultText;
	float scoreMult = 1;
	public Text upgradeButtonText;
	int upgradeCost = 10;

	// Use this for initialization
	void Start () {
		time = 0;
		plusOnes = new ArrayList();
		plusOnesTime = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (Input.GetMouseButtonDown(0)) {
			Vector3 clickPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			if (boxCollider.OverlapPoint (clickPos)) {
				clicked = true;
				score += scoreMult;
			} else {
				clicked = false;
			}
		}

		scoreText.text = "" + Mathf.Round(score * 100) / 100;
		upgradeButtonText.text = "Upgrade: " + upgradeCost;
		scoreMultText.text = "Click Multiplier: " + Mathf.Round(scoreMult * 100) / 100;
	}


	void FixedUpdate(){
		if (clicked && Random.value <= plus1Chance) {
			Vector3 pos = Camera.main.ViewportToWorldPoint (new Vector3(Random.value*0.5f + 0.25f, 0.25f + Random.value*0.5f, 10));

			plusOnes.Add((GameObject)Instantiate (plus1, pos, Quaternion.identity));
			plusOnesTime.Add(time);

			clicked = false;
		}

		for (int i = 0; i < plusOnes.Count; i++) {
			if (plusOnes[i] != null && time > (float) plusOnesTime [i] + plus1Time) {
				Destroy ((GameObject) plusOnes [i]);
			}
		}
	}


	public void TryToUpgrade(){
		if (score > upgradeCost) {
			score -= upgradeCost;
			upgradeCost = (int)(upgradeCost * 2f);
			scoreMult *= 1.3f;
		}
	}
}
