using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IPointerDownHandler
{
    public Car car;

    public void OnPointerDown(PointerEventData eventData)
    {
        car.Turn();
    }
}
