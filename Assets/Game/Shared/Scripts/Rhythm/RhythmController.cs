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

    public static event EventHandler OnCorrectHit;
    public static event EventHandler OnMissHit;
    public static event EventHandler OnPlayGame;

    private float noteSpawnTime;
    private List<GameObject> currentNotes_R;
    private List<GameObject> currentNotes_L;

    private Coroutine playNotesCorroutine;


    private void OnEnable()
    {
        currentNotes_R = new List<GameObject>();
        currentNotes_L = new List<GameObject>();

        MainScreen.OnStartGame += PlayGame;
        GameOverMenu.OnAdSuccess += PlayGame;
        DinoHealth.OnPlayerDeath += stopNotes;
        DinoBehaviour.onRageFinished += startNotes;
        DinoBehaviour.onRageStarted += stopNotes;
        DinoHealth.OnPlayerDeath += SaveHighScore;
    }

    private void OnDisable()
    {
        Debug.Log("on disable", this);
        MainScreen.OnStartGame -= PlayGame;
        DinoHealth.OnPlayerDeath -= stopNotes;
        DinoBehaviour.onRageFinished -= startNotes;
        DinoBehaviour.onRageStarted -= stopNotes;
        DinoHealth.OnPlayerDeath -= SaveHighScore;
    }

    private void PlayGame(object sender, EventArgs e)
    {
        //StopAllCoroutines();
        try
        {
            canSpawnNotes = true;
            currentNotes_R = new List<GameObject>();
            currentNotes_L = new List<GameObject>();

            AudioManager.instance.Play("environmentSounds", true);

            //needleScale = needle.GFX.localScale;

            noteSpawnTime = (float)60 / Song.BeatsPerMinute;

            playNotesCorroutine = StartCoroutine(StartSpawningNotes());

            //DinoHealth.OnPlayerDeath += stopNotes;
            //DinoBehaviour.onRageFinished += startNotes;
            //DinoBehaviour.onRageStarted += stopNotes;

            //DinoHealth.OnPlayerDeath += SaveHighScore;

            OnPlayGame?.Invoke(this, null);
        }
        catch (Exception ex)
        {
            //Debug.LogError(ex.Message);
        }
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

        stopNotes(null, null);
        StopAllCoroutines();
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
        currentNotes_R.Clear();
        currentNotes_L.Clear();
        currentNote_R = null;
        currentNote_L = null;

    }


    private void Update()
    {
        if (!canSpawnNotes) return;

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
        {
            if(NeedleOnPosition)
            {
                correctHits++;

                try
                {
                    float d = Vector3.Distance(currentNote_R.transform.position, currentNote_L.transform.position);
                    if (d < 0.1)
                        Debug.Log("ÓTIMO");
                    else
                        Debug.Log("BOM");
                }
                catch (Exception) {}

                allHitsText.text = correctHits.ToString();
                NeedleOnPosition = false;
                OnCorrectHit?.Invoke(this, null);
            }
            else
            {
                if(!DinoBehaviour.isRaging)
                {
                    Debug.Log("BOM");
                    OnMissHit?.Invoke(this, null);
                }
            }
            allHits++;
            currentNotes_R.Remove(currentNote_R);
            currentNotes_L.Remove(currentNote_L);
            Destroy(currentNote_R);
            Destroy(currentNote_L);
            //needle.GFX.localScale = needleScale;
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
        OnMissHit?.Invoke(this, null);
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
