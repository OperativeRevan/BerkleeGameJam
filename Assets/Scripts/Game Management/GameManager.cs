﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    // global marks (I don't want to do it this way but Unity really has bad nativ support for this functionality
    public bool interactionOn = true;

    // progress trackers /////////////////////////////////////////////////////
    // TODO: rune progress
    public Dictionary<string, bool> runes;

    // object containers /////////////////////////////////////////////////////
    public GameObject mainDialogueBox;
    public GameObject mainJournal;
    public Dictionary<string, GameObject> soundEfx;


    // system messages ///////////////////////////////////////////////////////
    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this);

        runes = new Dictionary<string, bool>();
        soundEfx = new Dictionary<string, GameObject>();
    }


    // scene loading messages ///////////////////////////////////////////////
    public void StartGame() {
        // load the first scene
        SceneLoader.instance.StartGame();
    }


    // story trigger handles ////////////////////////////////////////////////
    public void RegisterNewTrigger(string dialogue, string journal) { // called whenever a trigger is triggered
        // show dialogue
        mainDialogueBox.GetComponent<DialogueBox>().ShowDialogue(dialogue);
        // add journal entry
        mainJournal.GetComponent<JournalUI>().AddJournalEntry(journal);
    }

    
    // rune system //////////////////////////////////////////////////////////
    public void RegisterRune(string key, bool initial = false) {
        runes.Add(key, initial);
    }

    public void UnlockRune(string key) {
        if (runes.ContainsKey(key)) {
            runes[key] = true;
        }
    }

    public bool CheckRune(string key) {
        return (runes.ContainsKey(key) && runes[key] == true);
    }


    // sound effect system //////////////////////////////////////////////////
    public void RegisterSoundMixer(string key, GameObject other) {
        soundEfx.Add(key, other);
    }

    public void PlayerSound(string key, AudioClip sound) {
        if (soundEfx.ContainsKey(key)) {
            GameObject sp = soundEfx[key];
            sp.GetComponent<AudioSource>().clip = sound;
            sp.GetComponent<AudioSource>().Play();
        }
    }
}
