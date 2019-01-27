﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tile))]
public class LevelEndTrigger : MonoBehaviour
{
    private AudioSource audioSource; // Remember to add

    private Tile tile;
    private bool finished = false;

    void Awake() {
        tile = GetComponent<Tile>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if(tile.moldType != MoldType.None && !finished) {
            GameManager.instance.FinishLevel();
            finished = true;
        }
    }

}
