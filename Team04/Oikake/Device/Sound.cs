using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;

namespace Oikake.Device
{
    class Sound
    {
        #region フィールドとコンストラクタ
        //コンテンツ管理者
        private ContentManager contentManager;
        //MP3管理用
        private Dictionary<string, Song> bgms;
        //WAV管理用
        private Dictionary<string, SoundEffect> soundEffects;
        //WAVインスタンス管理用
        private Dictionary<string, SoundEffectInstance> seInstances;
        //WAVインスタンスの再生管理用ディクショナリ
        private Dictionary<string, SoundEffectInstance> sePlayDict;
        //現在再生中のMP3のアセット名
        private string currentBGM;

        public Sound(ContentManager content)
        {
            //Game1クラスのコンテンツ管理者と紐付け
            contentManager = content;
            //BGMは繰り返し再生
            MediaPlayer.IsRepeating = true;

            //各Dictionaryの実態生成
            bgms = new Dictionary<string, Song>();
            soundEffects = new Dictionary<string, SoundEffect>();
            seInstances = new Dictionary<string, SoundEffectInstance>();
            //再生Listの実態生成
            sePlayDict = new Dictionary<string, SoundEffectInstance>();

            //何も再生していないのでnullで初期化
            currentBGM = null;
        }

        public void Unload()
        {
            //ディクショナリをクリア
            bgms.Clear();
            soundEffects.Clear();
            seInstances.Clear();
            sePlayDict.Clear();
        }

        #endregion フィールドとコンストラクタ
        private string ErrorMessage(string name)
        {
            return "再生する音データのアセット名(" + name + ")がありません" +
                "アセット名の確認,Dicionaryに登録しているか確認してください";
        }

        #region BGM(MP3:MediaPlayer)関連

        public void LoadBGM(string name, string filepath = "./")
        {
            //すでに登録されているか？
            if (bgms.ContainsKey(name))
            {
                return;
            }
            //MP3の読み込みとDictionaryへ登録
            bgms.Add(name, contentManager.Load<Song>(filepath + name));
        }

        public bool IsStoppedBGM()
        {
            return (MediaPlayer.State == MediaState.Stopped);
        }

        public bool IsPlayingBGM()
        {
            return (MediaPlayer.State == MediaState.Playing);
        }

        public bool IsPausedBGM()
        {
            return (MediaPlayer.State == MediaState.Paused);
        }

        public void StopBGM()
        {
            MediaPlayer.Stop();
            currentBGM = null;
        }

        public void PlayBGM(string name)
        {
            //アセット名がディクショナリに登録されているか？
            Debug.Assert(bgms.ContainsKey(name), ErrorMessage(name));

            //同じ曲か？
            if (currentBGM == name)
            {
                //同じ曲だったら何もしない
                return;
            }

            //BGM再生中か？
            if (IsPlayingBGM())
            {
                //再生中なら、停止処理
                StopBGM();
            }

            //ボリューム設定(BGMはＳＥに比べて音量半分が普通）
            MediaPlayer.Volume = 0.5f;

            //現在のBGM名を設定
            currentBGM = name;

            //再生開始
            MediaPlayer.Play(bgms[currentBGM]);
        }

        public void PauseBGM()
        {
            if (IsPlayingBGM())
            {
                MediaPlayer.Pause();
            }
        }

        public void ResumeBGM()
        {
            if (IsPausedBGM())
            {
                MediaPlayer.Resume();
            }
        }

        public void ChangeBGMLoopFlag(bool loopFlag)
        {
            MediaPlayer.IsRepeating = loopFlag;
        }
        #endregion BGM(BGM3:MediaPlayer)

        #region WAV(SE:SoundEffect)関連

        public void LoadSE(string name, string filepath = "./")
        {
            //すでに登録されていれば何もしない
            if (soundEffects.ContainsKey(name))
            {
                return;
            }
            soundEffects.Add(name, contentManager.Load<SoundEffect>(filepath + name));
        }

        public void PlaySE(string name)
        {
            //アセット名が登録されているか？
            Debug.Assert(soundEffects.ContainsKey(name), ErrorMessage(name));

            //再生
            soundEffects[name].Play();
        }
        #endregion //WAV(SE:SoundEffect)関連

        #region WAVインスタンス関連

        public void CreateSEInstance(string name)
        {
            //すでに登録されていたら何もしない
            if (seInstances.ContainsKey(name))
            {
                return;
            }

            //WAV用ディクショナリに登録されていないと無理
            Debug.Assert(
                soundEffects.ContainsKey(name),
                "先に" + name + "の読み込み処理を行ってください" 
                );
            //WAVデータのインスタンスを生成し、登録
            seInstances.Add(name, soundEffects[name].CreateInstance());
        }

        public void PlaySEInstances(string name, int no, bool loopFLag = false)
        {
            Debug.Assert(seInstances.ContainsKey(name), ErrorMessage(name));

            //再生管理用ディクショナリ登録されていたら何もしない
            if (sePlayDict.ContainsKey(name + no))
            {
                return;
            }


            var data = seInstances[name];
            data.IsLooped = loopFLag;
            data.Play();
            sePlayDict.Add(name + no, data);
        }

        public void StoppedSE(string name, int no)
        {
            //再生管理用ディクショナリになれば何もしない
            if (sePlayDict.ContainsKey(name + no) == false)
            {
                return;
            }
            if (sePlayDict[name + no].State == SoundState.Playing)
            {
                sePlayDict[name + no].Stop();
            }
        }

        public void StopppedSE()
        {
            foreach (var se in sePlayDict)
            {
                if (se.Value.State == SoundState.Playing)
                {
                    se.Value.Stop();
                }
            }
        }

        public void RemoveSE(string name, int no)
        {
            //再生管理用ディクショナリになければ何もしない
            if (sePlayDict.ContainsKey(name + no) == false)
            {
                return;
            }
            sePlayDict.Remove(name + no);
        }

        public void RemoveSE()
        {
            sePlayDict.Clear();
        }

        public void PauseSE(string name, int no)
        {
            //再生管理用ディクショナリになければ何もしない
            if(sePlayDict.ContainsKey(name + no) == false)
            {
                return;
            }
            //再生中なら一時停止
            if(sePlayDict[name + no].State == SoundState.Playing)
            {
                sePlayDict[name + no].Pause();
            }
        }

        public void PauseSE()
        {
            foreach(var se in sePlayDict)
            {
                if(se.Value.State == SoundState.Playing)
                {
                    se.Value.Pause();
                }
            }
        }

        public void ResumeSE(string name,int no)
        {
            //再生管理用ディクショナリに何もなければ何もしない
            if(sePlayDict.ContainsKey(name + no) == false)
            {
                return;
            }
            if(sePlayDict[name + no].State == SoundState.Paused)
            {
                sePlayDict[name + no].Resume();
            }
        }

        public void ResumeSE()
        {
            foreach(var se in sePlayDict)
            {
                if(se.Value.State == SoundState.Paused)
                {
                    se.Value.Resume();
                }
            }
        }
        public bool IsPlayingSEInstance(string name,int no)
        {
            return sePlayDict[name + no].State == SoundState.Paused;
        }

        public bool IsStoppedSEInstnce(string name,int no)
        {
            return sePlayDict[name + no].State == SoundState.Stopped;
        }

        public bool IsPausedSEInstance(string name,int no)
        {
            return sePlayDict[name + no].State == SoundState.Paused;
        }
        #endregion WAVインスタンス関連


    }
}
