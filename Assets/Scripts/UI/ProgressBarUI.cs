using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField]
    private Image _barImage;

    [SerializeField]
    private GameObject _hasProgressGameObject;

    private IHasProgress _hasProgress;

    private void Start()
    {
        _hasProgress = _hasProgressGameObject.GetComponent<IHasProgress>();
        _hasProgress.OnProgressChanged += OnProgressChanged;
        _barImage.fillAmount = 0;

        Hide();
    }
    private void OnProgressChanged(object sender, IHasProgress.ProgressChangedArgs e)
    {
        _barImage.fillAmount = e.ProgressNormalized;

        if (e.ProgressNormalized is 0f or >= 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
