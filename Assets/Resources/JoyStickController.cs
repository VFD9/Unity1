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

    // ** Target이 움직일 방향
    private Vector2 Direction;

    // ** Target이 움직일 값
    private Vector3 Movement;

    // ** 반지름
    private float Radius;

    // ** 이동 속도
    private float Speed;

    // ** 터치 입력 여부
    private bool TouchCheck;

    public void OnDrag(PointerEventData eventData)
    {
        // ** 드래그가 시작되면 터치 입력이 활성화,
        TouchCheck = true;

        // ** 움직임 계산.
        OnTouch(eventData.position);
        //Debug.Log(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // ** 입력이 시작되면 터치 입력 활성화.
        TouchCheck = true;

        BackBoard.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // ** 터치 입력이 종료되면 비활성화로 변경
        TouchCheck = false;

        // ** Stick을 원위치 시킴
        Stick.localPosition = Vector2.zero;
    }

    private void Awake()
    {
        Target = GameObject.Find("Player");
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

        // ** 방향이 없는 상태로 초기화
        Direction = new Vector2(0.0f, 0.0f);

        // ** 이동 속도 설정
        Speed = 5.0f;

        // ** 이동값이 없는 상태로 초기화
        Movement = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        if (TouchCheck)
            Target.transform.position += Movement;
    }

    private void OnTouch(Vector2 _eventData)
    {
        //Debug.Log("OnTouch");

        // ** Stick 의 중앙으로부터 터치가 스크린을 이동한 거리를 구함.
        Stick.localPosition = new Vector2(_eventData.x - BackBoard.position.x, _eventData.y - BackBoard.position.y);

        // ** Stick 이 Radius 를 벗어나지 못하게 함.
        Stick.localPosition = Vector2.ClampMagnitude(Stick.localPosition, Radius);

        // ** 조이스틱이 움직이는 방향에 맞게 타겟을 이동시켜준다.
        Direction = Stick.localPosition.normalized;

        // ** 조이스틱이 이동가능한 최대 거리에서 실제 이동한 비율만큼 이동 속도를 적용시킴.
        float Ratio = Vector3.Distance(BackBoard.position, Stick.position) / Radius;

        // ** 조이스틱이 움직이는 있는 방향에 맞게 타겟을 이동시켜준다.
        Movement = new Vector3(
            Direction.x * (Ratio * Speed) * Time.deltaTime,
            0.0f,
            Direction.y * (Ratio * Speed) * Time.deltaTime);

        // ** 조이스틱이 바라보는 방향으로 타겟을 바라보게한다.(호도법, sin, cos, tan)
        Target.transform.eulerAngles = new Vector3(0.0f, Mathf.Atan2(Direction.x, Direction.y) * Mathf.Rad2Deg, 0.0f);
    }
}