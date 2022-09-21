using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("Move Target")]
    [Tooltip("type : GameObject   조이스틱을 사용하여 움직일 대상을 정함.")]
    [SerializeField] private GameObject Target;

    [Header("Joy Stick Controller")]
    [Tooltip("type : RectTransform    실제로 움직일 버튼")]
    [SerializeField] private RectTransform Stick;
    [Tooltip("type : RectTransform    JoyStick Out Line")]
    [SerializeField] private RectTransform BackBoard;

    private float Radius;

    private bool TouchCheck;

	public void OnDrag(PointerEventData eventData)
	{
        TouchCheck = true;
        OnTouch(eventData.position);
    }

	public void OnPointerDown(PointerEventData eventData)
	{
        TouchCheck = true;
    }

	public void OnPointerUp(PointerEventData eventData)
	{
        TouchCheck = false;
        Stick.localPosition = Vector2.zero;
    }

	private void Awake()
	{
        Target = GameObject.Find("Tank").gameObject;
        Stick = GameObject.Find("FilledCircle").GetComponent<RectTransform>();
        BackBoard = GameObject.Find("OutLineCircle").GetComponent<RectTransform>();
    }

	void Start()
    {
        // ** Out Line 의 반지름을 구함
        Radius = BackBoard.rect.width * 0.5f;

        // ** 길이를 반지름의 절반만큼 더 길게 잡아준다.
        // ** 이유 : Stick이 Out Line 을 살짝 넘어갈 수 있게 하기 위함.
        Radius += Radius * 0.5f;

        // ** 스크린에 터치가 되었는지 확인.
        TouchCheck = false;
    }

    void Update()
    {
        //if (TouchCheck)
    }

    private void OnTouch(Vector2 _eventData)
	{
        // ** Stick 의 중앙으로부터 터치가 스크린을 이동한 거리를 구함.
        Stick.localPosition = new Vector2(_eventData.x - BackBoard.position.x, _eventData.y - BackBoard.position.y);

        // ** Stick 이 Radius 를 벗어나지 못하게 함.
        Stick.localPosition = Vector2.ClampMagnitude(Stick.localPosition, Radius);

        // ** 1. 조이스틱이 움직이는 방향에 맞게 타겟을 이동시켜준다.
        // ** 2. 조이스틱이 바라보는 방향으로 타겟을 바라보게한다.
        // ** 3. 조이스틱이 이동가능한 최대 거리에서 실제 이동한 비율만큼 이동 속도를 적용시킴.
    }
}
