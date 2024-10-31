using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{
	[SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _tmp;
    [SerializeField] private TextListSO _list;
    private int count = 0;
    public bool isTextTrigger;

    private void Start()
    {
        _image.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isTextTrigger)
        {
            _image.gameObject.SetActive(true);
            DOText.DOTexting(_list._textList[count].Text, _tmp, 2,DG.Tweening.Ease.Linear, ()=>count++);
        }
        else if (!isTextTrigger)
        {
            _image.gameObject.SetActive(false);        }
    }
}
