using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Button botan;
    public Text _punchPowerText;
    public Image punchPowerImage;
    public GameObject powerGuide;

    public int PunchPower { get; private set; }
    private int PowerBarCount;
    private int count;
    private bool countingUp = true;
    private bool powerBarFixed = false; // パワーバーカウントを固定するかどうかのフラグ

    public Text powerBarCountView;

    void Start()
    {
        if (botan != null)
        {
            botan.onClick.AddListener(OnClickButton);
        }
        else
        {
            Debug.LogError("Botan object is missing!");
        }

        GameObject punchPowerObject = GameObject.Find("PunchPowerText");
        if (punchPowerObject != null)
        {
            _punchPowerText = punchPowerObject.GetComponent<Text>();
            if (_punchPowerText == null)
            {
                Debug.LogError("PunchPowerText component is missing!");
            }
        }
        else
        {
            Debug.LogError("PunchPowerText object is missing!");
        }

        GameObject punchPowerImageObject = GameObject.Find("PunchPowerImage");
        if (punchPowerImageObject != null)
        {
            punchPowerImage = punchPowerImageObject.GetComponent<Image>();
            if (punchPowerImage == null)
            {
                Debug.LogError("PunchPowerImage component is missing!");
            }
        }
        else
        {
            Debug.LogError("PunchPowerImage object is missing!");
        }

        // PowerGuideのGameObjectを取得
        powerGuide = GameObject.Find("PowerGuide");
        if (powerGuide == null)
        {
            Debug.LogError("PowerGuide object is missing!");
        }
        else
        {
            // PowerGuideの初期位置のY座標を調整
            powerGuide.transform.position = new Vector3(powerGuide.transform.position.x, 210f, powerGuide.transform.position.z);
        }

         // powerBarCountViewの初期化
         powerBarCountView = GameObject.Find("powerBarCountView").GetComponent<Text>();
         if (powerBarCountView == null)
         {
             Debug.LogError("powerBarCountView component is missing!");
         }
      }

    void OnClickButton()
    {
        // ボタンが押されたらパワーバーカウントを固定する
        powerBarFixed = true;

        // デバッグログを追加
        Debug.LogError("powerBarFixed: " + powerBarFixed);

        PunchPower = PowerBarCount;
        SetPunchPowerMessage(PunchPower);
        SetpunchPowerImage(PunchPower);

        // ゲームの終了処理を遅延実行
        Invoke("EndGame", 3f); // 3秒後にEndGameメソッドを呼び出す
    }

    // ゲームの終了処理
    void EndGame()
    {
        // ゲーム終了メッセージを表示
        Debug.Log("ゲーム終了処理を実行します。");
        
        // ゲーム終了時の画面表示などがあればここに追加する
    
        // ゲームの終了
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    void SetPunchPowerMessage(int punchPower)
    {
        string message;
        if (punchPower <= 69)
        {
            message = "ごくろう様でした";
        }
        else if (punchPower <= 79)
        {
            message = "もう一回チャレンジ！";
        }
        else if (punchPower <= 95)
        {
            message = "メンツは保った";
        }
        else if (punchPower <= 109)
        {
            message = "強い！これはてごわい";
        }
        else
        {
            message = "地上最強の男！";
        }

        _punchPowerText.text = "PunchPower: " + punchPower.ToString("f2") + "\n" + message;
    }

    void SetpunchPowerImage(int punchPower)
    {
        Sprite imageSprite = null;

        if (punchPower <= 69)
        {
            imageSprite = Resources.Load<Sprite>("Images/GokurousamaImage");
        }
        else if (punchPower <= 79)
        {
            imageSprite = Resources.Load<Sprite>("Images/RetryImage");
        }
        else if (punchPower <= 95)
        {
            imageSprite = Resources.Load<Sprite>("Images/MenTsuImage");
        }
        else if (punchPower <= 109)
        {
            imageSprite = Resources.Load<Sprite>("Images/StrongImage");
        }
        else
        {
            imageSprite = Resources.Load<Sprite>("Images/StrongestImage");
        }

        if (punchPowerImage != null && imageSprite != null)
        {
            punchPowerImage.sprite = imageSprite;
        }
        else
        {
            Debug.LogError("punchPowerImage component or Image sprite is missing!");
        }
    }

    void Update()
    {
        // デバッグログを追加
        Debug.Log("powerBarFixed: " + powerBarFixed);

        // PowerBarCountの値をテキストオブジェクトに表示
        powerBarCountView.text = "PowerBarCount: " + PowerBarCount.ToString("f2");

        // PowerGuideの移動処理
        float offsetY = Mathf.Lerp(210f, 330f, (float)PowerBarCount / 120f);
        powerGuide.transform.position = new Vector3(powerGuide.transform.position.x, offsetY, powerGuide.transform.position.z);

        // パワーバーカウントが固定されていない場合のみ更新
        if (!powerBarFixed)
        {
            count += 1;
    
            if (count % 1 == 0)
            {
                if (countingUp)
                {
                    PowerBarCount += 1;
                    if (PowerBarCount >= 120)
                    {
                        countingUp = false;
                    }
                }
                else
                {
                    PowerBarCount -= 1;
                    if (PowerBarCount <= 0)
                    {
                        countingUp = true;
                    }
                }
            }
        }

        // デバッグログを追加
        Debug.Log("powerBarFixed: " + powerBarFixed);

    }
}
