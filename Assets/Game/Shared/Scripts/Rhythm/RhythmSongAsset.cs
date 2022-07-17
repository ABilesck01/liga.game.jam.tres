using UnityEngine;

[CreateAssetMenu(menuName = "Asset/New song")]
public class RhythmSongAsset : ScriptableObject
{
    public int BeatsPerMinute = 120;
    public float NoteSpeed = 2;
    public AudioClip Beat;
}
