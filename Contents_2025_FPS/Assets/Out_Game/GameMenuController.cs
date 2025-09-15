using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;


public enum PanelType { None, MainMenu, ControllerGuide, ChooseBackGame, ChooseBackTitle, ChooseControllerGuide }

[Serializable]
public class VideoPanel
{
    public PanelType Type;
    public VideoPlayer player;      // この動画の再生機
    public RawImage image;        // 表示先（UIパネル）
    public RenderTexture target;    // 出力先（動画ごとに別RT）
}

public class GameMenuController : MonoBehaviour
{
    [SerializeField] ScenesManagersScripts scenesManager;

    [Header("各パネル(動画ごとに1セット)")]
    public VideoPanel mainMenu;
    public VideoPanel controllerGuide;
    public VideoPanel chooseBackGame;
    public VideoPanel chooseBackTitle;
    public VideoPanel chooseControllerGuide;
    [Header("アニメーションつきオブジェクト")]
    [SerializeField] GameObject onMainMenu;
    [SerializeField] GameObject onControllerGuide;
    [SerializeField] GameObject onChooseBackGame;
    [SerializeField] GameObject onChooseBackTitle;
    [SerializeField] GameObject onChooseControllerGuide;
    [Header("メインメニューの待機用")]
    [SerializeField] GameObject imageMainMenu;
    [Header("ボタンのオブジェクト")]
    [SerializeField] GameObject backGameB;
    [SerializeField] GameObject backTitleB;
    [SerializeField] GameObject controllerGuideB;
    [Header("ボタンのスクリプト")]
    [SerializeField] ButtonCheck chooseBackGameScr;
    [SerializeField] ButtonCheck chooseBackTitleScr;
    [SerializeField] ButtonCheck chooseControllerGuideScr;

    VideoPanel currentVideoPanel;

    // 連続再生のキャンセル・識別用トークン ---
    int _seqToken = 0;
    bool isImageView = false;　// 画像を描画するためのフラグ
    bool isDisp = false;
    bool prevChooseBackGame = false; // updateの中で1回だけ実行するもの
    bool prevChooseBackTitle = false;
    bool prevChooseControllerGuide = false;

    private void Awake()
    {
        Init(mainMenu);
        Init(controllerGuide);
        Init(chooseBackGame);
        Init(chooseBackTitle);
        Init(chooseControllerGuide);
        backGameB.SetActive(false);
        backTitleB.SetActive(false);
        controllerGuideB.SetActive(false);
        imageMainMenu.SetActive(false);
    }

    private void Update()
    {

        InputDispMenu();
        ChooseButton();
        ImageView();
     
    }

    // 初期化
    void Init(VideoPanel v)
    {
        // v.Type = PanelType.None;
        v.player.playOnAwake = false;
        v.player.waitForFirstFrame = true;     // 最初のフレーム準備ができるまで再生をブロック
        v.player.sendFrameReadyEvents = true;  // frameReady を使うので必須
        v.image.enabled = false;               // 最初は見せない
        ClearRT(v.target);
    }

    void ResetPanel(VideoPanel v)
    {
        v.player.Stop();
        v.player.time = 0;
        v.player.frame = 0;
        v.image.enabled = false;
        ClearRT(v.target);

    }

    void InputDispMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isDisp = !isDisp; // トグル式
            if (isDisp && scenesManager.currentScene == ScenesManagersScripts.Scene.GAME)
            {
                scenesManager.MenuSceneTransition();
                onMainMenu.SetActive(true);
                onChooseBackGame.SetActive(true);
                onChooseBackTitle.SetActive(true);
                onChooseControllerGuide.SetActive(true);
                //backGameB.SetActive(true);
                //backTitleB.SetActive(true);
                //controllerGuideB.SetActive(true);

                PlayWithCallback(mainMenu, afterFirstDone: () =>
                {
                    backGameB.SetActive(true);
                    backTitleB.SetActive(true);
                    controllerGuideB.SetActive(true);

                    // ★前回ホバーの記憶を消す
                    prevChooseBackGame = prevChooseBackTitle = prevChooseControllerGuide = false;
                    chooseBackGameScr?.ForceExit();
                    chooseBackTitleScr?.ForceExit();
                    chooseControllerGuideScr?.ForceExit();

                    // （任意）初期カーソルが中央＝戻るボタン上にある前提なら、明示的に切替えてしまう
                    // SwitchTo(chooseBackGame);
                }
                );
                
            }
            else
            {
                if (scenesManager.currentScene == ScenesManagersScripts.Scene.MENU)
                {
                    if (currentVideoPanel == null) return;

                    // 操作説明からのescはゲームに戻さずメインメニューのアニメーションをする
                    if (currentVideoPanel.Type == PanelType.ControllerGuide)
                    {
                        isDisp = true; // メニューは開いたまま
                        onMainMenu.SetActive(true);

                        _seqToken++; // 古い再生をキャンセルする
                        ResetPanel(mainMenu); // 0から再生するためリセット

                        PlayWithCallback(mainMenu , afterFirstDone: () =>
                        {
                            // アニメーション終了後にボタンを出す
                            backGameB.SetActive(true);
                            backTitleB.SetActive(true);
                            controllerGuideB.SetActive(true);

                            // ホバー状態のリセット
                            prevChooseBackGame = prevChooseBackTitle = prevChooseControllerGuide = false;
                            chooseBackGameScr?.ForceExit();
                            chooseBackTitleScr?.ForceExit();
                            chooseControllerGuideScr?.ForceExit();
                        });

                        return; // ここで終了しゲームシーンにはいかない
                    }

                    // それ以外の場合はescで閉じるようにする
                    CloseMenuAndReset();
                    scenesManager.GameSceneTransition();
                }
            }
        }
    }

    // ボタンにカーソルが重なった時の関数
    void ChooseButton()
    {
        bool hG = chooseBackGameScr.isHovering;
        bool hT = chooseBackTitleScr.isHovering;
        bool hC = chooseControllerGuideScr.isHovering;

        // ★メニューが開いていなければ一切何もしない
        if (!isDisp || scenesManager.currentScene != ScenesManagersScripts.Scene.MENU)
        {
            prevChooseBackGame = hG;
            prevChooseBackTitle = hT;
            prevChooseControllerGuide = hC;
            return;
        }

        if (currentVideoPanel != controllerGuide)
        {
            if (hG && !prevChooseBackGame) { SwitchTo(chooseBackGame); }
            else if (!hG && prevChooseBackGame) { isImageView = true; ResetPanel(chooseBackGame); currentVideoPanel = mainMenu; }

            if (hT && !prevChooseBackTitle) { SwitchTo(chooseBackTitle); }
            else if (!hT && prevChooseBackTitle) { isImageView = true; ResetPanel(chooseBackTitle); currentVideoPanel = mainMenu; }

            if (hC && !prevChooseControllerGuide) { SwitchTo(chooseControllerGuide); }
            else if (!hC && prevChooseControllerGuide) { isImageView = true; ResetPanel(chooseControllerGuide); currentVideoPanel = mainMenu; }
        }

        prevChooseBackGame = hG;
        prevChooseBackTitle = hT;
        prevChooseControllerGuide = hC;
    }

    void ImageView()
    {
        if (isImageView)
        {
            imageMainMenu.SetActive(true);
        }
        else
        {
            imageMainMenu.SetActive(false);
        }
    }


    // ゲームに戻るボタンを押したとき
    public void OnclickBackGameScene()
    {
        CloseMenuAndReset();

        scenesManager.GameSceneTransition();
    }
    //　タイトルに戻るボタンを押したとき
    public void OnClickBackTitle()
    {
        onMainMenu.SetActive(false);
        onControllerGuide.SetActive(false);
        onChooseBackGame.SetActive(false);
        onChooseBackTitle.SetActive(false);
        onChooseControllerGuide.SetActive(false);
        backGameB.SetActive(false);
        backTitleB.SetActive(false);
        controllerGuideB.SetActive(false);
        isImageView = false;
    }
    //　操作説明ボタンを押したとき
    public void OnclickTakeControllerGuide()
    {
        TakeControllerGuide();
    }
    void TakeControllerGuide()
    {
        if (currentVideoPanel != controllerGuide)
        {
            onControllerGuide.SetActive(true);
            SwitchTo(controllerGuide);
        }
    }




    // ここを呼ぶだけで「チラつかずに」切り替えられる(同じ動画でも再生しなおせるようにする)
    public void SwitchTo(VideoPanel next)
    {
        if (next == currentVideoPanel) return;

        // 旧は表示を維持したまま、新を準備（穴を作らない）
        PrepareAndShowWhenFirstFrame(next, onShown: () =>
        {
            // 新が映った“直後”に旧を隠す
            if (currentVideoPanel != null) currentVideoPanel.image.enabled = false;
            currentVideoPanel = next;
        });
    }

    void PrepareAndShowWhenFirstFrame(VideoPanel v, Action onShown)
    {
        // 新側をいったん完全初期化
        v.image.enabled = false;
        ClearRT(v.target);

        // 最初のフレームが届いた、その瞬間だけ見せる（1回限りで解除）
        void OnFirstFrame(VideoPlayer src, long frameIdx)
        {
            v.image.enabled = true;
            src.frameReady -= OnFirstFrame; // 1回でOK（解除忘れ注意）
            onShown?.Invoke();
        }
        v.player.frameReady += OnFirstFrame;

        // 準備できたら再生開始
        void OnPrepared(VideoPlayer src)
        {
            src.prepareCompleted -= OnPrepared;
            src.Play();
        }
        v.player.prepareCompleted += OnPrepared;

        // 0秒からキッチリ用意
        v.player.Stop();
        v.player.time = 0; v.player.frame = 0;
        v.player.Prepare();
    }

    // 1本目 → 終了時に afterFirstDone 実行 →（必要なら）2本目再生
    // shouldPlaySecond が null の場合は second があるだけで自動再生
    public void PlayWithCallback(
        VideoPanel first,
        Action afterFirstDone,
        VideoPanel second = null,
        Func<bool> shouldPlaySecond = null)
    {
        if (first == null || first.player == null) return;

        int token = ++_seqToken;

        // 1本目を“チラつき無し”で表示＆再生開始
        PrepareAndShowWhenFirstFrame(first, onShown: () =>
        {
            if (token != _seqToken) return;

            if (currentVideoPanel != null && currentVideoPanel != first)
                currentVideoPanel.image.enabled = false;
            currentVideoPanel = first;

            first.player.isLooping = false;

            void OnFirstEnd(VideoPlayer src)
            {
                src.loopPointReached -= OnFirstEnd;
                if (token != _seqToken) return;

                // まず、1本目の終了時コールバックを実行
                try { afterFirstDone?.Invoke(); } catch (Exception e) { Debug.LogException(e); }

                // 2本目を再生するかを判定
                bool wantSecond =
                    (second != null && second.player != null) &&
                    (shouldPlaySecond == null || shouldPlaySecond());

                if (wantSecond)
                {
                    // 2本目へ（既存の“切替えはチラつかない”ポリシーを踏襲）
                    SwitchTo(second);
                }
                // else: 再生せず終了（見た目の後処理は afterFirstDone 側で好きにやる）
            }

            first.player.loopPointReached += OnFirstEnd;
        });

        // 0秒から準備
        first.player.Stop();
        first.player.time = 0;
        first.player.frame = 0;
        first.player.Prepare();
    }


    void CloseMenuAndReset()
    {
        _seqToken++;                 // 遅延イベント無効化（シーケンスキャンセル）
        isDisp = false;
        isImageView = false;
        imageMainMenu.SetActive(false);

        // ホバー状態クリア
        prevChooseBackGame = prevChooseBackTitle = prevChooseControllerGuide = false;
        chooseBackGameScr?.ForceExit();
        chooseBackTitleScr?.ForceExit();
        chooseControllerGuideScr?.ForceExit();

        // ボタン/装飾を閉じる
        backGameB.SetActive(false);
        backTitleB.SetActive(false);
        controllerGuideB.SetActive(false);
        onMainMenu.SetActive(false);
        onChooseBackGame.SetActive(false);
        onChooseBackTitle.SetActive(false);
        onChooseControllerGuide.SetActive(false);
        onControllerGuide.SetActive(false);

        // ★動画・RawImageを確実に落とす
        ResetPanel(mainMenu);
        ResetPanel(controllerGuide);
        ResetPanel(chooseBackGame);
        ResetPanel(chooseBackTitle);          // ← ここが効く
        ResetPanel(chooseControllerGuide);

        currentVideoPanel = null;
    }

    void ClearRT(RenderTexture rt)
    {
        var prev = RenderTexture.active;
        RenderTexture.active = rt;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = prev;
    }
}
