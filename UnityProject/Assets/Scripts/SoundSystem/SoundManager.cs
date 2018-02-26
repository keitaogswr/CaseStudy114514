using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVolume
{
    public float BGM = 1.0f;
    public float SE = 1.0f;
    public float Voice = 1.0f;
    public bool Mute = false;

    public void Init()
    {
        BGM = 1.0f;
        SE = 1.0f;
        Voice = 1.0f;
        Mute = false;
    }
}

//サウンドマネージャクラス。これを使うためにはどこかでInstance関数を呼ぶ。
//効果音・ボイス再生関数の戻り値であるインデックスは呼び出し元でとっておく。そうしないとループしている効果音・ボイスを止めることが出来ない。

public class SoundManager : MonoBehaviour{
    private static SoundManager instance;

    //オーディオソース

    //BGM
    private static AudioSource BGMSource;
    //効果音
    private static AudioSource[] SESources = new AudioSource[16];
    //声
    private static AudioSource[] VoiceSources = new AudioSource[16];

    //オーディオクリップ

    //BGM
    static AudioClip[] BGM;
    //効果音
    static AudioClip[] SE;
    //声
    static AudioClip[] Voice;

    static Dictionary<string, int> BGMData;
    static Dictionary<string, int> SEData;
    static Dictionary<string, int> VoiceData;

    //音量
    public static SoundVolume volume = new SoundVolume();

    float OldVolume = 1.0f;

    private SoundManager()
    {
        Debug.Log("Create SoundManager instance");
    }

    public static SoundManager Instance
    {
        get{
            if(instance == null)
            {
                GameObject obj = new GameObject("SoundManager");
                DontDestroyOnLoad(obj);
                instance = obj.AddComponent<SoundManager>();
            }

            SetSound();

            return instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        //BGMのオーディオソースの生成
        BGMSource = gameObject.AddComponent<AudioSource>();
        //全てのBGMのループ設定を有効にする
        BGMSource.loop = true;

        //効果音の生成
        for (int i = 0; i < SESources.Length; i++)
        {
            SESources[i] = gameObject.AddComponent<AudioSource>();
        }

        //声の生成
        for (int i = 0; i < VoiceSources.Length; i++)
        {
            VoiceSources[i] = gameObject.AddComponent<AudioSource>();
        }

        SetSound();
    }

    void Update()
    {
    }
    //BGM再生
    public static void PlayBGM(int index)
    {
        //指定したインデックスが範囲外の場合は何もしない
        if(index < 0 || index >= BGM.Length)
        {
            return;
        }

        //同じBGMの場合は何もしない
        if(BGMSource.clip == BGM[index])
        {
            return;
        }

        BGMSource.Stop();
        BGMSource.clip = BGM[index];
        BGMSource.Play();
    }

    public static void PlayBGM(string fileName,bool loop = true)
    {
        if(BGMSource == null)
        {
            SoundManager s = Instance;
        }

        //存在しないファイル名を指定した場合は何もしない
        if(BGMData.ContainsKey(fileName) == false)
        {
            return;
        }

        int index = BGMData[fileName];

        //同じBGMを使用している場合は何もしない
        if(BGMSource.clip == BGM[index])
        {
            return;
        }

        BGMSource.Stop();
        BGMSource.clip = BGM[index];
        BGMSource.Play();
        BGMSource.loop = loop;
    }

    public static void PauseBGM()
    {
        if(BGMSource.isPlaying && BGMSource != null)
        {
            BGMSource.Pause();
        }
        else if(!BGMSource.isPlaying && BGMSource != null)
        {
            BGMSource.UnPause();
        }
    }

    //BGM停止
    public static void StopBGM()
    {
        BGMSource.Stop();
        BGMSource.clip = null;
    }

    //効果音再生
    public static void PlaySE(int index, bool isLoop = false)
    {
        //指定したインデックスが範囲外の場合は何もしない
        if (index < 0 || index >= SE.Length)
        {
            return;
        }

        //再生中でないオーディオソースを検索し使用する
        foreach(AudioSource source in SESources)
        {
            if(!source.isPlaying)
            {
                source.clip = SE[index];               
                source.Play();
                return;
            }
        }
    }

    //効果音を鳴らす関数。1つ目の引数は鳴らしたい音声ファイル名、2つ目はループするかどうか。デフォルトでは無効になっている
    public static int PlaySE(string fileName,bool isLoop = false)
    {
        if (SESources.Length == 0)
        {
            SoundManager s = Instance;
        }

        //存在しないファイル名を指定した場合は何もしない
        if (SEData.ContainsKey(fileName) == false)
        {
            return -1;
        }

        int index = SEData[fileName];

        int use = 0;
        //再生中でないオーディオソースを検索し使用する
        foreach (AudioSource source in SESources)
        {
            if (!source.isPlaying)
            {
                source.clip = SE[index];
                source.loop = isLoop;
                source.Play();
                return use;
            }
            use++;
        }

        return -1;
    }

    //全ての効果音を停止する
    public static void StopSEAll()
    {
        foreach (AudioSource source in SESources)
        {
            source.Stop();
            source.clip = null;
        }
    }

    public static void PauseSEAll()
    {
        foreach (AudioSource source in SESources)
        {
            if(source.isPlaying && source != null)
            {
                source.Pause();
            }
            else if(!source.isPlaying && source != null)
            {
                source.UnPause();
            }
        }
    }

    //効果音を止める関数。引数は止めたい効果音のインデックスを指定する
    public static void StopSE(int index)
    {
        if(index < 0)
        {
            return;
        }

        if(!SESources[index].isPlaying)
        {
            return;
        }

        SESources[index].Stop();
        SESources[index].clip = null;
    }

    //BGMが止まっている状態かを確認する
    public static bool GetBGMIsStop()
    {
        if(BGMSource.clip == null)
        {
            return true;
        }

        if(!BGMSource.isPlaying)
        {
            return true;
        }

        return false;
    }

    //効果音が今どのような状態かを見る。存在していないものを指定した場合はエラーを返す
    public static bool GetSEIsStop(int index)
    {
        if(index < 0)
        {
            return false;
        }

        if(SESources[index].time == SESources[index].clip.length)
        {
            return true;
        }

        return false;
    }

    //声の再生
    public static void PlayVoice(int index,bool isLoop = false)
    {
        //指定したインデックスが範囲外の場合は何もしない
        if (index < 0 || index >= Voice.Length)
        {
            return;
        }

        //再生中でないオーディオソースを検索し使用する
        foreach (AudioSource source in VoiceSources)
        {
            if (!source.isPlaying)
            {
                source.clip = Voice[index];
                source.Play();
                return;
            }
        }
    }

    public static void PlayVoice(string fileName, bool isLoop = false)
    {
        //存在しないファイル名を指定した場合は何もしない
        if (VoiceData.ContainsKey(fileName) == false)
        {
            return;
        }

        int index = VoiceData[fileName];

        //再生中でないオーディオソースを検索し使用する
        foreach (AudioSource source in VoiceSources)
        {
            if (!source.isPlaying)
            {
                source.clip = Voice[index];
                source.loop = isLoop;
                source.Play();
                return;
            }
        }
    }

    //全ての声を停止する
    public static void StopVoiceAll()
    {
        foreach (AudioSource source in VoiceSources)
        {
            source.Stop();
            source.clip = null;
        }
    }

    public static void StopVoice(int index)
    {
        if (index < 0)
        {
            return;
        }

        VoiceSources[index].Stop();
        VoiceSources[index].clip = null;
    }

    public static bool GetVoiceIsStop(int index)
    {
        if (index < 0)
        {
            return false;
        }

        if (VoiceSources[index].time == VoiceSources[index].clip.length)
        {
            return true;
        }

        return false;
    }

    //
    static void SetSound()
    {
        BGM = Resources.LoadAll<AudioClip>("Sounds/BGM/");
        SE = Resources.LoadAll<AudioClip>("Sounds/SE/");
        Voice = Resources.LoadAll<AudioClip>("Sounds/VOICE/");
        GetFileName();
    }

    static void GetFileName()
    {
        string path = "";

        BGMData = new Dictionary<string, int>();
        SEData = new Dictionary<string, int>();
        VoiceData = new Dictionary<string, int>();

        //int count = 0;

        path = Application.dataPath + "/Resources/Sounds/";

        for(int i = 0; i < BGM.Length; i++)
        {
            BGMData.Add(BGM[i].name,i);
        }

        for (int i = 0; i < SE.Length; i++)
        {
            SEData.Add(SE[i].name, i);
        }

        for (int i = 0; i < Voice.Length; i++)
        {
            VoiceData.Add(Voice[i].name, i);
        }

        //filePath = System.IO.Directory.GetFiles(path + "BGM/", "*.wav");
        //for (int i = 0; i < filePath.Length; i++)
        //{
        //    string str = filePath[i].Replace(path + "BGM/", "");
        //    BGMData.Add(str, i);
        //    count++;
        //}

        //filePath = System.IO.Directory.GetFiles(path + "BGM/", "*.mp3");
        //for (int i = 0; i < filePath.Length; i++)
        //{
        //    string str = filePath[i].Replace(path + "BGM/", "");
        //    BGMData[str] = i + count;
        //}

        //count = 0;
        //filePath = System.IO.Directory.GetFiles(path + "SE/", "*.wav");
        //for (int i = 0; i < filePath.Length; i++)
        //{
        //    string str = filePath[i].Replace(path + "SE/", "");
        //    SEData[str] = i;
        //    count++;
        //}

        //filePath = System.IO.Directory.GetFiles(path + "SE/", "*.mp3");
        //for (int i = 0; i < filePath.Length; i++)
        //{
        //    string str = filePath[i].Replace(path + "SE/", "");
        //    SEData[str] = i + count;
        //}

        //count = 0;
        //filePath = System.IO.Directory.GetFiles(path + "Voice/", "*.wav");
        //for (int i = 0; i < filePath.Length; i++)
        //{
        //    string str = filePath[i].Replace(path + "Voice/", "");
        //    BGMData[str] = i;
        //    count++;
        //}

        //filePath = System.IO.Directory.GetFiles(path + "Voice/", "*.mp3");
        //for (int i = 0; i < filePath.Length; i++)
        //{
        //    string str = filePath[i].Replace(path + "Voice/", "");
        //    BGMData[str] = i + count;
        //}
    }

    public static void SetBGMVolume(float volume)
    {
        BGMSource.volume = volume;
    }

    public static void SetSEVolume(float volume)
    {
        foreach (AudioSource source in SESources)
        {
            source.volume = volume;
        }
    }

    public static void SetVoiceVolume(float volume)
    {
        foreach (AudioSource source in VoiceSources)
        {
            source.volume = volume;
        }
    }
}
