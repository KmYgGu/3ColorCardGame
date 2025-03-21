using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    [SerializeField] private GameObject playerObj;

    private void Awake()
    {
        //playerObj.TryGetComponent<IWhosHandCards>(out IWhosHandCards whocards);
        //whocards.CheckHandCards(playerObj, 20);

        //playerObj.TryGetComponent<IWhosDeck>(out IWhosDeck component);
        //component.CheckDeck(playerObj, 20);
    }
}
