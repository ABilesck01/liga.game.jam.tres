using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmController : MonoBehaviour
{
    [Header("Status")]
    public int correctHits = 0;
    public int allHits = 0;
    [Header("Flags")]
    public bool NeedleOnPosition = false;

    public bool canSpawnNotes;
    [Header("Notes settings")]
    public GameObject note;
    public RectTransform noteSpawner;
    public float songBPM;
    public float noteSpawnTime;
    public float noteSpeed;
    [Space]
    public RectTransform UI_Parent;
    public RhythmNeedle needle;
    [HideInInspector]
    public GameObject currentNote = null;

    private Vector3 needleScale;

    public static event EventHandler onCorrectHit;
    public static event EventHandler onMissHit;
    public static event EventHandler onStartGame;

    private List<GameObject> currentNotes;

    private void Start()
    {
        MainScreen.onStartGame += PlayGame;
        PlayGame(this, null);

    }

    private void PlayGame(object sender, EventArgs e)
    {
        canSpawnNotes = true;
        currentNotes = new List<GameObject>();

        AudioManager.instance.Play("environmentSounds");

        needleScale = needle.GFX.localScale;

        noteSpawnTime = 60 / songBPM;

        StartCoroutine(StartSpawningNotes());

        DinoHealth.onPlayerDeath += stopNotes;
        DinoBehaviour.onRageFinished += startNotes;
        DinoBehaviour.onRageStarted += stopNotes;

        DinoHealth.onPlayerDeath += delegate
        {
            if (PlayerPrefs.HasKey("highscore"))
            {
                int highscore = PlayerPrefs.GetInt("highscore");
                if (highscore > correctHits)
                    PlayerPrefs.SetInt("highscore", correctHits);
            }
            else
                PlayerPrefs.SetInt("highscore", correctHits);
        };

        onStartGame?.Invoke(this, null);
    }

    private void destroyAllNotes()
    {
        foreach (GameObject item in currentNotes)
        {
            Destroy(item);
        }
        currentNote = null;
        currentNotes.Clear();

    }


    private void Update()
    {
        if (!canSpawnNotes) return;

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if(NeedleOnPosition)
            {
                correctHits++;
                onCorrectHit?.Invoke(this, null);
                NeedleOnPosition = false;
            }
            else
            {
                if(!DinoBehaviour.isRaging)
                    onMissHit?.Invoke(this, null);
            }
            allHits++;
            currentNotes.Remove(currentNote);
            Destroy(currentNote);
            needle.GFX.localScale = needleScale;
        }
    }
    
    IEnumerator StartSpawningNotes()
    {
        WaitForSeconds s = new WaitForSeconds(noteSpawnTime);
        WaitForSeconds half = new WaitForSeconds(noteSpawnTime / 2);
        int d = UnityEngine.Random.Range(1, 3);
        AudioManager.instance.Play($"drums{d}");
        while(canSpawnNotes)
        {
            bool halfBong = (UnityEngine.Random.value < 0.25f);

            SpawnNote();
            if(halfBong)
                yield return half;
            else
                yield return s;
        }
    }

    private void SpawnNote()
    {
        GameObject noteGO = Instantiate(note, noteSpawner.anchoredPosition, Quaternion.identity);
        currentNotes.Add(noteGO);
        RhythymNoteController noteController = noteGO.GetComponent<RhythymNoteController>();
        noteController.speed = noteSpeed;
        noteGO.transform.SetParent(UI_Parent, false);
        //AudioManager.instance.Play("singleDrum");

    }

    internal void missHit()
    {
        if (DinoBehaviour.isRaging) return;
        allHits++;
        onMissHit?.Invoke(this, null);
    }

    private void stopNotes(object sender, EventArgs e)
    {
        canSpawnNotes = false;
        destroyAllNotes();
    }

    private void startNotes(object sender, EventArgs e)
    {
        if (canSpawnNotes) return;

        canSpawnNotes = true;
        StartCoroutine(StartSpawningNotes());
    }

}
