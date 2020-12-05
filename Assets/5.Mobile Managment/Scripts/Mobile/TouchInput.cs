using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] private Vector2 _touchDist;

    private int _pointedIndex;
    private bool _pressed;

    private Vector2 _pointerOld;

    public float Horizontal => _touchDist.x;
    public float Vertical => _touchDist.y;


    private void Update()
	{
		if (_pressed)
		{
			if (_pointedIndex >= 0 && _pointedIndex < Input.touches.Length)
			{
				_touchDist = Input.touches[_pointedIndex].position - _pointerOld;
				_pointerOld = Input.touches[_pointedIndex].position;
			}
			else
			{
				_touchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - _pointerOld;
				_pointerOld = Input.mousePosition;
			}
		}
		else
		{
			_touchDist = Vector2.zero;
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_pressed = true;
		_pointedIndex = eventData.pointerId;
		_pointerOld = eventData.position;
	}


	public void OnPointerUp(PointerEventData eventData)
	{
		_pressed = false;
	}

}