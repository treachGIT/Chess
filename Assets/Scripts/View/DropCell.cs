using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropCell : MonoBehaviour, IDropHandler
{
    public GameObject currentPiece;
    public GameObject previousPiece;
    public List<GameObject> whitePieces;
    public List<GameObject> blackPieces;

    public void OnDrop(PointerEventData eventData)
    {

        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<Transform>().position = GetComponent<Transform>().position;


            if (currentPiece != null)
            {
                if(eventData.pointerDrag.GetComponent<Transform>().gameObject != currentPiece)
                {
                    GameObject pieceToDelete = currentPiece;
                    currentPiece = eventData.pointerDrag.GetComponent<Transform>().gameObject;

                    previousPiece = pieceToDelete;
                    pieceToDelete.SetActive(false);
                }
              
            }
            else if(currentPiece == null)
            {
                currentPiece = eventData.pointerDrag.GetComponent<Transform>().gameObject;
                previousPiece = null;
            }
            
        }    
    }
}
