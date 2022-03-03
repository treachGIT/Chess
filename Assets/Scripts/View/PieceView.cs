using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PieceView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private Canvas canvas;

    public GameView view;

    private CanvasGroup canvasGroup;

    private RectTransform rectTransform;
    public float startX;
    public float startY;
    public Vector3 startPos;

    bool dragging = false;
    int clickTime = 0;
    public string color;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startX = rectTransform.anchoredPosition.x;
        startY = rectTransform.anchoredPosition.y;
        startPos = transform.position;

        this.transform.position = eventData.position;

        rectTransform.SetAsLastSibling();

        view.GetLegalMoves(startX, startY);

        clickTime += 1;       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
        canvasGroup.blocksRaycasts = false;
        clickTime = 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
         transform.position = Input.mousePosition;     
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RectTransform currentRectTransform = GetComponent<RectTransform>();

        //jeżeli poza plansza
        if (currentRectTransform.anchoredPosition.x < -400 || currentRectTransform.anchoredPosition.x > 400 || currentRectTransform.anchoredPosition.y < -400 || currentRectTransform.anchoredPosition.y > 400)
        {
            transform.position = startPos;
        }

        dragging = false;
        canvasGroup.blocksRaycasts = true;

        if (view.isMoveLegal(currentRectTransform.anchoredPosition.x, currentRectTransform.anchoredPosition.y) == true)
        {
            view.MakeMove(startX, startY, currentRectTransform.anchoredPosition.x, currentRectTransform.anchoredPosition.y);
        }
        else
        {
            this.transform.position = startPos;
        }

        view.DeleteHighlightMoves();

        view.moveWait = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(dragging == false)
        {
            transform.position = startPos;
        }

        if (clickTime == 2)
        {
            view.DeleteHighlightMoves();
            clickTime = 0;
        }
    }

}
