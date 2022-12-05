using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RhythmController : MonoBehaviour
{
    [Header("Status")]
    public int correctHits = 0;
    public int allHits = 0;
    [Space]
    [SerializeField] private TextMeshProUGUI allHitsText;
    [Header("Flags")]
    public bool NeedleOnPosition = false;

    public bool canSpawnNotes;
    [Header("Notes Spawn settings")]
    public GameObject note_right;
    public RectTransform noteSpawner_right;
    public RectTransform noteSpawner_left;
    [Header("Notes settings")]
    //public float songBPM;
    //public float noteSpeed;
    [SerializeField] private RhythmSongAsset Song;
    [Space]
    public RectTransform UI_Parent;
    public RhythmNeedle needle;
    [HideInInspector]
    public GameObject currentNote_R = null;
    [HideInInspector]
    public GameObject currentNote_L = null;

    private Vector3 needleScale;

    public static event EventHandler onCorrectHit;
    public static event EventHandler onMissHit;
    public static event EventHandler onStartGame;

    private float noteSpawnTime;
    private List<GameObject> currentNotes_R;
    private List<GameObject> currentNotes_L;

    private Coroutine playNotesCorroutine;

    private void Start()
    {
        MainScreen.onStartGame += PlayGame;
        PlayGame(this, null);

    }

    private void OnDisable()
    {
        MainScreen.onStartGame -= PlayGame;
        DinoHealth.onPlayerDeath -= stopNotes;
        DinoBehaviour.onRageFinished -= startNotes;
        DinoBehaviour.onRageStarted -= stopNotes;
        DinoHealth.onPlayerDeath -= SaveHighScore;
    }

    private void PlayGame(object sender, EventArgs e)
    {
        canSpawnNotes = true;
        currentNotes_R = new List<GameObject>();
        currentNotes_L = new List<GameObject>();

        AudioManager.instance.Play("environmentSounds");

        needleScale = needle.GFX.localScale;

        noteSpawnTime = (float)60 / Song.BeatsPerMinute;
        
        playNotesCorroutine = StartCoroutine(StartSpawningNotes());

        DinoHealth.onPlayerDeath += stopNotes;
        DinoBehaviour.onRageFinished += startNotes;
        DinoBehaviour.onRageStarted += stopNotes;

        DinoHealth.onPlayerDeath += SaveHighScore;

        onStartGame?.Invoke(this, null);
    }

    private void SaveHighScore(object sender, EventArgs e)
    {
        if (PlayerPrefs.HasKey("highscore"))
        {
            int highscore = PlayerPrefs.GetInt("highscore");
            if (highscore < correctHits)
            {
                PlayerPrefs.SetInt("highscore", correctHits);
                Debug.Log("highscore upgrated");
            }
        }
        else
        {
            PlayerPrefs.SetInt("highscore", correctHits);
        }

        PlayerPrefs.SetInt("lastGameHits", correctHits);
    }

    private void destroyAllNotes()
    {
        foreach (GameObject item in currentNotes_R)
        {
            Destroy(item);
        }

        foreach (GameObject item in currentNotes_L)
        {
            Destroy(item);
        }
        currentNote_R = null;
        currentNote_L = null;
        currentNotes_R.Clear();
        currentNotes_L.Clear();

    }


    private void Update()
    {
        if (!canSpawnNotes) return;

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
        {
            if(NeedleOnPosition)
            {
                correctHits++;
                float d = Vector3.Distance(currentNote_R.transform.position, currentNote_L.transform.position);
                if (d < 0.1)
                    Debug.Log("ÓTIMO");
                else
                    Debug.Log("BOM");

                allHitsText.text = correctHits.ToString();
                NeedleOnPosition = false;
                onCorrectHit?.Invoke(this, null);
            }
            else
            {
                if(!DinoBehaviour.isRaging)
                {
                    Debug.Log("BOM");
                    onMissHit?.Invoke(this, null);
                }
            }
            allHits++;
            currentNotes_R.Remove(currentNote_R);
            currentNotes_L.Remove(currentNote_L);
            Destroy(currentNote_R);
            Destroy(currentNote_L);
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
            SpawnNote();
            yield return s;
            
            // bool halfBong = (UnityEngine.Random.value < 0.125f);
            //
            // SpawnNote();
            // if(halfBong)
            //     yield return half;
            // else
            //     yield return s;
        }
    }

    private void SpawnNote()
    {
        GameObject noteGO_R = Instantiate(note_right, noteSpawner_right.anchoredPosition, Quaternion.identity);
        currentNotes_R.Add(noteGO_R);
        RhythymNoteController noteController_R = noteGO_R.GetComponent<RhythymNoteController>();
        noteController_R.speed = Song.NoteSpeed;
        noteGO_R.name += "_R";
        noteGO_R.tag = "note_R";
        noteGO_R.transform.SetParent(UI_Parent, false);

        GameObject noteGO_L = Instantiate(note_right, noteSpawner_left.anchoredPosition, Quaternion.identity);
        currentNotes_L.Add(noteGO_L);
        RhythymNoteController noteController_L = noteGO_L.GetComponent<RhythymNoteController>();
        noteController_L.speed = -Song.NoteSpeed;
        noteGO_L.name += "_L";
        noteGO_L.tag = "note_L";
        noteGO_L.transform.SetParent(UI_Parent, false);

    }

    public void missHit()
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
        playNotesCorroutine = StartCoroutine(StartSpawningNotes());
    }

}
