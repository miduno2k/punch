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
            powerGuide.transform.position = new Vector3(powerGuide.transform.position.x, 30f, powerGuide.transform.position.z);
        }
    }

    void OnClickButton()
    {
        // ボタンが押されたらパワーバーカウントを固定する
        powerBarFixed = true;

        PunchPower = PowerBarCount;
        SetPunchPowerMessage(PunchPower);
        SetpunchPowerImage(PunchPower);
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
        // PowerBarCountの値をテキストオブジェクトに表示
        if (powerBarCountView != null)
        {
            powerBarCountView.text = "PowerBarCount: " + PowerBarCount.ToString("f2");
        }
        else
        {
            Debug.LogError("powerBarCountView is not assigned!");
        }
    
        // パワーバーカウントが固定されていない場合のみ移動処理を実行
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
    
            // PowerGuideの移動処理
            float offsetY = Mathf.Lerp(210f, 330f, (float)PowerBarCount / 120f);
            powerGuide.transform.position = new Vector3(powerGuide.transform.position.x, offsetY, powerGuide.transform.position.z);
        }
    
        // デバッグログを追加
        Debug.Log("powerBarFixed: " + powerBarFixed);
    }
}
