using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class BlinkText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _blinkText;
    private bool isBlink = false;

    private void Start()
    {
        StartCoroutine(BlinkCourount());
    }

    private IEnumerator BlinkCourount()
    {
        while (true)
        {
            if (!isBlink)
                isBlink = true;
            else
                isBlink = false;

            yield return new WaitForSeconds(0.3F);

            _blinkText.gameObject.SetActive(isBlink);

            yield return new WaitForSeconds(0.3f);
        }
    }

    public void Onclik()
    {
        SceneManager.LoadScene(1);
    }
}
