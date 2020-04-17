using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundBGM
{
    rd_bgm_result_loop,
}

public enum SoundFX
{
    //Common 0~ 1000
    NONE = -1,

    rd_sfx_click = 1,
    rd_sfx_cancel,
    rd_sfx_popup,

    rd_sfx_enemy_hit = 101,
    rd_sfx_coin_get_loop,
    rd_sfx_coin_get_end,
    rd_sfx_card_support,
    rd_sfx_card_equip,
    rd_sfx_card_levelup,
    rd_sfx_card_combine,
    rd_sfx_fail,
    rd_sfx_life_decrease,
    rd_sfx_life_decreasebyboss,
    rd_sfx_get_star,
    rd_sfx_card_draw,

    //Card 1001~
    rd_sfx_card_attack = 1001,

    // rd_sfx_card_fire,
    // rd_sfx_card_lightning,
    // rd_sfx_card_wind,
    // rd_sfx_card_poison,
    // rd_sfx_card_ice,
    // rd_sfx_card_hammer,
    // rd_sfx_card_eyepatch,
    // rd_sfx_card_random,
    // rd_sfx_card_paralysis,

    //Card skill
    rd_sfx_card_lock = 2001,
    rd_sfx_card_seaurchin = 2002,
    rd_sfx_card_death = 2003,
    rd_sfx_card_teleport = 2004,
    rd_sfx_card_infection = 2005,
}

public class SoundManager : GameObjectSingleton<SoundManager>
{

    /////////////////////////////////////////////////////////////
    // public

    public ClockStone.AudioObject PlayBGM(SoundBGM bgm)
    {
        return this.PlayBGM(bgm.ToString());
    }

    public ClockStone.AudioObject PlaySFX(SoundFX sfx)
    {
        return this.PlaySFX(sfx.ToString());
    }

    public bool StopSFX(SoundFX sfx)
    {
        return this.StopSFX(sfx.ToString());
    }

    public bool StopBGM()
    {
        return AudioController.StopMusic();
    }

    public void PauseBGM()
    {
        AudioController.PauseMusic();
    }
    public bool UnPauseBGM()
    {
        return AudioController.UnpauseMusic();
    }

    /////////////////////////////////////////////////////////////
    // Common Use

    public void PlayButtonClick()
    {
        this.PlaySFX(SoundFX.rd_sfx_click);
    }

    public void PlayCancel()
    {
        this.PlaySFX(SoundFX.rd_sfx_cancel);
    }

    /////////////////////////////////////////////////////////////
    // private

    private ClockStone.AudioObject PlayBGM(string audioID)
    {
        if (!Preference.LoadPreference(Pref.BGM, true))
            return null;

        ClockStone.AudioObject currentAudioObj = AudioController.GetCurrentMusic();
        if (currentAudioObj != null && currentAudioObj.audioID.Equals(audioID))
            return null;

        AudioController.StopMusic();

        return AudioController.PlayMusic(audioID);
    }

    private ClockStone.AudioObject PlaySFX(string audioID)
    {
        if (!Preference.LoadPreference(Pref.SFX, true))
            return null;

        return AudioController.Play(audioID);
    }

    private ClockStone.AudioObject PlayWalkSFX(string audioID)
    {
        return AudioController.Play(audioID);
    }

    private bool StopSFX(string audioID)
    {
        return AudioController.Stop(audioID);
    }
}
