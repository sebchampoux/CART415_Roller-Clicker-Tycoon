using UnityEngine;
using UnityEngine.UI;

public abstract class Modal : MonoBehaviour
{
    [SerializeField] protected Park _park;
    [SerializeField] protected VerticalLayoutGroup _scrollViewContent;
    [SerializeField] private Transform _modalsPosition;

    public virtual void Awake()
    {
        SubscribeToParkEvents();
        gameObject.SetActive(false);
        transform.position = _modalsPosition.position;
    }

    public void CloseModal()
    {
        gameObject.SetActive(false);
    }

    public void OpenModal()
    {
        gameObject.SetActive(true);
    }

    protected abstract void SubscribeToParkEvents();
}