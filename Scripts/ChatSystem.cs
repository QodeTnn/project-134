// ChatSystem.cs
[System.Serializable]
public class ChatMessage
{
    public string sender; // "X" or "Player"
    public string text;
    public float delayBeforeShow; // How long before this message appears
    public MessageType type;
    public EmotionalState emotion;
    public string specialEvent; // Optional: triggers game events
    
    public ChatMessage(string sender, string text, float delay = 0.5f, MessageType type = MessageType.Normal, EmotionalState emotion = EmotionalState.Neutral, string specialEvent = "")
    {
        this.sender = sender;
        this.text = text;
        this.delayBeforeShow = delay;
        this.type = type;
        this.emotion = emotion;
        this.specialEvent = specialEvent;
    }
}

[System.Serializable]
public class PlayerOption
{
    public string choiceText; // What player sees
    public string choiceId; // Internal ID for tracking
    public string[] emotionalEffects; // How this affects X
    
    public PlayerOption(string text, string id, string[] effects)
    {
        choiceText = text;
        choiceId = id;
        emotionalEffects = effects;
    }
}

public enum MessageType
{
    Normal,
    Hesitant,    // ... appears before message
    Urgent,      // Quick successive
    Deleted,     // "This message was unsent"
    Withheld,    // Takes very long to arrive
    Typing       // Just typing indicators
}

public enum EmotionalState
{
    Neutral,
    Happy,
    Anxious,
    Lonely,
    Needy,
    Defensive,
    Angry,
    Hurt,
    Vulnerable,
    Relieved,
    Withdrawn,
    Confused
}