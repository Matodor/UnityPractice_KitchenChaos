using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField]
    private BaseCounter _counter;

    [SerializeField]
    private GameObject[] _visualGameObjectArray;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void OnDestroy()
    {
        Player.Instance.OnSelectedCounterChanged -= Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.SelectedCounterChangedEventArgs e)
    {
        if (e.Selected == _counter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (var go in _visualGameObjectArray)
            go.SetActive(true);
    }

    private void Hide()
    {
        foreach (var go in _visualGameObjectArray)
            go.SetActive(false);
    }
}
