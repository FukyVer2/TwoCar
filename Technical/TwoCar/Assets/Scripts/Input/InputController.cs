using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IPointerDownHandler
{
    public Car car;

    public void OnPointerDown(PointerEventData eventData)
    {
        car.Turn();
        Vector3 pos = eventData.position;
    }
}
