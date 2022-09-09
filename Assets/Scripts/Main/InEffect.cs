using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InEffect : MonoBehaviour
{
	[Header("淡出淡入速度")]
    public float alpha = 1f;
    public Image mask;

    IEnumerator EnterScene()
    {
        float alpha_tmp = alpha;
        while (alpha_tmp > 0)
        {
            alpha_tmp -= Time.deltaTime;
            mask.color = new Color(0, 0, 0, alpha_tmp);
            yield return null;
        }
    }

    public IEnumerator QuitScene()
    {
        float alpha_tmp = 0;
        while (alpha_tmp < alpha)
        {
            alpha_tmp += Time.deltaTime;
            mask.color = new Color(0, 0, 0, alpha_tmp);
            yield return null;
        }
    }

    void Start()
	{
		StartCoroutine(EnterScene());
	}
}

