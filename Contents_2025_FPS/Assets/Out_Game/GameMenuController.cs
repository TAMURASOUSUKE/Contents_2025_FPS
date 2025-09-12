using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;


public enum PanelType { MainMenu, ControllerGuide, ChooseBackGame, ChooseBackTitle, ChooseControllerGuide }

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
                ShowImmediately(mainMenu);
                backGameB.SetActive(true);
                backTitleB.SetActive(true);
                controllerGuideB.SetActive(true);
            }
            else
            {
                if (scenesManager.currentScene == ScenesManagersScripts.Scene.MENU)
                {
                    scenesManager.GameSceneTransition();
                    if (currentVideoPanel == null) return;
                    switch (currentVideoPanel.Type)
                    {
                        case PanelType.MainMenu:
                            isImageView = false;
                            backGameB.SetActive(false);
                            backTitleB.SetActive(false);
                            controllerGuideB.SetActive(false);
                            onMainMenu.SetActive(false);
                            break;
                        case PanelType.ControllerGuide:
                            scenesManager.MenuSceneTransition();
                            isDisp = true;
                            SwitchTo(mainMenu);
                            break;
                        case PanelType.ChooseBackGame:
                            isImageView = false;
                            backGameB.SetActive(false);
                            backTitleB.SetActive(false);
                            controllerGuideB.SetActive(false);
                            onChooseBackGame.SetActive(false);
                            onMainMenu.SetActive(false);
                            break;
                        case PanelType.ChooseBackTitle:
                            isImageView = false;
                            backGameB.SetActive(false);
                            backTitleB.SetActive(false);
                            controllerGuideB.SetActive(false);
                            onMainMenu.SetActive(false);
                            onChooseBackTitle.SetActive(false);
                            break;
                        case PanelType.ChooseControllerGuide:
                            isImageView = false;
                            backGameB.SetActive(false);
                            backTitleB.SetActive(false);
                            controllerGuideB.SetActive(false);
                            onMainMenu.SetActive(false);
                            onChooseControllerGuide.SetActive(false);
                            break;
                        default:
                            isImageView = false;
                            backGameB.SetActive(false);
                            backTitleB.SetActive(false);
                            controllerGuideB.SetActive(false);
                            break;
                    }

                }
            }
        }
    }

    // ボタンにカーソルが重なった時の関数
    void ChooseButton()
    {
        // 現在のホバー状態を先に読む
        bool hG = chooseBackGameScr.isHovering;
        bool hT = chooseBackTitleScr.isHovering;
        bool hC = chooseControllerGuideScr.isHovering;

        // 実際の動作はガード
        if (isDisp && currentVideoPanel != controllerGuide)
        {
            if (hG && !prevChooseBackGame) { /* isImageView=false; */ SwitchTo(chooseBackGame); }
            else if (!hG && prevChooseBackGame) { isImageView = true; ResetPanel(chooseBackGame); currentVideoPanel = mainMenu; }

            if (hT && !prevChooseBackTitle) { /* isImageView=false; */ SwitchTo(chooseBackTitle); }
            else if (!hT && prevChooseBackTitle) { isImageView = true; ResetPanel(chooseBackTitle); currentVideoPanel = mainMenu; }

            if (hC && !prevChooseControllerGuide) { /* isImageView=false; */ SwitchTo(chooseControllerGuide); }
            else if (!hC && prevChooseControllerGuide) { isImageView = true; ResetPanel(chooseControllerGuide); currentVideoPanel = mainMenu; }
        }

        // ★ ガードの外で“必ず”前回値を更新
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
        backGameB.SetActive(false);
        backTitleB.SetActive(false);
        controllerGuideB.SetActive(false);
        isImageView = false;
        onChooseBackGame.SetActive(false); // ゲームに戻る
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






    // すぐ見せたいとき（起動直後など）
    void ShowImmediately(VideoPanel v)
    {
        ClearRT(v.target);
        v.image.enabled = true;
        v.player.Stop();
        v.player.time = 0; v.player.frame = 0;

        currentVideoPanel = v;

        v.player.Prepare();
        v.player.prepareCompleted += OnPreparedThenPlayOnce;
        void OnPreparedThenPlayOnce(VideoPlayer src)
        {
            src.prepareCompleted -= OnPreparedThenPlayOnce;
            src.Play();
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

    void ClearRT(RenderTexture rt)
    {
        var prev = RenderTexture.active;
        RenderTexture.active = rt;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = prev;
    }
}
