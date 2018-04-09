﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOverlay : MonoBehaviour {

    [SerializeField]
    private Sprite mov, atk;

    [SerializeField]
    private GameObject cursorObject;
    private SelectionCursor cursor;
    private GameObject selectedUnit = null;

    private bool selected = false;

    private TileStatus[] tileStatus;


    // Use this for initialization
    void Start() {
        cursor = cursorObject.GetComponent<SelectionCursor>();
        tileStatus = GetComponentsInChildren<TileStatus>();
    }

    // Update is called once per frame
    void Update() {
        UpdateUnit();
        DisplaySelectionGrid();
    }

    void UpdateUnit() {
        if (cursor.GetSelectedUnit() != null && !selected) {
            selectedUnit = cursor.GetSelectedUnit();
            selected = true;
            transform.position = selectedUnit.transform.position;
        }
        else if (cursor.GetSelectedUnit() == null && selected) {
            selected = false;
        }
    }

    private void DisplaySelectionGrid() {
        if (selected) {
            foreach (var tile in tileStatus) {
                float localX = tile.transform.localPosition.x, localY = tile.transform.localPosition.y;
                int movStat = selectedUnit.GetComponent<FEFriendlyUnit>().mov;
                int range = selectedUnit.GetComponent<FEFriendlyUnit>().range;

                float movTileLimit = (float)movStat / 2;
                float atkTileLimit = (float)(movStat + range) / 2;

                if (Mathf.Abs(localX) + Mathf.Abs(localY) <= movTileLimit) {
                    tile.type = 0;
                    tile.active = true;
                }

                if (Mathf.Abs(localX) + Mathf.Abs(localY) > movTileLimit && Mathf.Abs(localX) + Mathf.Abs(localY) <= atkTileLimit) {
                    tile.type = 1;
                    tile.active = true;
                }
            }
        }

        else {
            foreach (var tile in tileStatus) {
                tile.active = false;
            }
        }
    }
}