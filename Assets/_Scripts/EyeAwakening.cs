using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeAwakening : MonoBehaviour
{
    [SerializeField]
    private GameObject MainCanvas;

	public AnimationCurve curve;

	public Transform lidUp;
	public Transform lidDown;

	public float speed;
	public float MaxPos;

	private float curveTime;
	private float curveAmount;

	void Start()
    {
		curveTime = 0;
		curveAmount = curve.Evaluate(curveTime);
	}

    void Update()
    {
        if (curveAmount < 1.0f)
        {
            curveTime += Time.deltaTime * speed;
            curveAmount = curve.Evaluate(curveTime);
            lidUp.transform.localPosition = new Vector3(0, MaxPos * curveAmount, 0);
            lidDown.transform.localPosition = new Vector3(0, MaxPos * (-curveAmount), 0);
        }
        else
        {
            MainCanvas.SetActive(true);
            Destroy(this.gameObject, 0.5f);
        }
    }
}