using System.Data;
using Discord;
using Unity.VisualScripting;
using UnityEngine;

public class Discord_Controller : MonoBehaviour
{
    public long applicationID;
    [Space]
    public string details = "Gardening";
    [Space]
    public string largeImage = "icon_nobg";
    public string largeText = "flowerz";
    
    private long _time;

    private static bool _instanceExists;
    private Discord.Discord _discord;

    private void Awake()
    {
        if(!_instanceExists)
        {
            _instanceExists = true;
            DontDestroyOnLoad(gameObject);
        }
        else if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);    
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        _discord = new Discord.Discord(applicationID, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);

        _time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();

        UpdateStatus();
    }

    // Update is called once per frame
    private void Update()
    {
        try
        {
            _discord.RunCallbacks();
        }
        catch
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        try
        {
            var activityManager = _discord.GetActivityManager();
            var activity = new Discord.Activity
            {
                Details = details,
                Assets =
                {
                    LargeImage = largeImage,
                    LargeText = largeText
                },
                Timestamps =
                {
                    Start = _time
                }
            };
            
            activityManager.UpdateActivity(activity, (res) =>
            {
                if(res != Discord.Result.Ok) Debug.LogWarning("Failed connecting to Discord");
                
            });
        }
        catch
        {
            Destroy(gameObject);
        }
    }
}
