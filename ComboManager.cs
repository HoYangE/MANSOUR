using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    public GameObject piece;
    public GameObject relic;
    public RelicManager relicManager;
    public TileSet tileSetManager;

    public void OnPieceButton()
    {
        piece.SetActive(true);
        tileSetManager.DelRelic();
        tileSetManager.InitRelic();
        relic.SetActive(false);
    }

    public void OnRelicButton()
    {
        piece.SetActive(false);
        relic.SetActive(true);
        relicManager.DelRelic();
        relicManager.InitRelic();
    }
}
