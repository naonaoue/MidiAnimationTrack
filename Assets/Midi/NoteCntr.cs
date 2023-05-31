using Klak.Timeline.Midi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCntr : MonoBehaviour
{
    [SerializeField] MidiAnimationAsset track;
    [SerializeField] GameObject note;
    [SerializeField] Transform notes;

    float realTime=0f;
    public float RealTimeInTicks;
    List<GameObject> noteList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        var midiEventSet = track.template.events;

        foreach (MidiEvent midiEvent in midiEventSet)
        {
            Debug.Log(midiEvent.time + "," + midiEvent.status + "," + midiEvent.data1 + "," + midiEvent.data2);

            //if (midiEvent.data1 != 36)
            //    continue;
            if (midiEvent.IsNoteOn)
            {
                noteList.Add(Instantiate<GameObject>(note, new Vector3((float)((midiEvent.data1-36)-5)/12f, 0f, midiEvent.time / 1000f), Quaternion.identity, notes));
                var noteObj = noteList[noteList.Count - 1].GetComponent<Note>();
                noteObj.Tick = midiEvent.time;
                noteObj._NoteCntr = this;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        realTime += Time.deltaTime;
        RealTimeInTicks = realTime * track.template.ticksPerQuarterNote * track.template.tempo / 60f;

        notes.position = new Vector3(0f,0f, 6f-RealTimeInTicks/1000f);
        /*
        foreach (GameObject _note in noteList)
        {
            if (_note == null)
                continue;
            if (_note.transform.position.z < 0f)
            {
                Destroy(_note);
            }
        }*/
#if false
        var midiEventSet = track.template.events;
        foreach (MidiEvent midiEvent in midiEventSet)
        {
            if (midiEvent.data1 != 36)
                continue;
            if (midiEvent.IsNoteOn && midiEvent.time < realTimeInTicks)
            {
                Debug.Log("Z");
            }
        }
#endif
    }
}
