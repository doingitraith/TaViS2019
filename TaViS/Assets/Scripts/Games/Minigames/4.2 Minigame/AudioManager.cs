using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        //make to have just one AudioManager in scene
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad if you want it to stay between scenes 
        //DontDestroyOnLoad(gameObject);

        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }


    public void Play (string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        //if array dont contain or doesnt find soundname
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
