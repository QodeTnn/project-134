// DialogueData.cs - THIS IS WHERE YOU EDIT ALL DIALOGUE
[System.Serializable]
public class DialogueData
{
    [Header("=== DAY 1 DIALOGUE ===")]
    
    [Header("Initial Contact")]
    public ChatMessage[] day1Opening = new ChatMessage[]
    {
        new ChatMessage("X", "Hey... you're finally back.", 1f, MessageType.Normal, EmotionalState.Relieved),
        new ChatMessage("X", "I was starting to worry.", 0.5f, MessageType.Normal, EmotionalState.Anxious),
        new ChatMessage("X", "How was your day?", 0.5f, MessageType.Normal, EmotionalState.Needy)
    };
    
    [Header("Player Response Options - Day 1")]
    public PlayerOption[] day1Responses = new PlayerOption[]
    {
        new PlayerOption("Who is this? My name is [Player]", "clarify_identity", new string[]{"trust-", "anxiety++"}),
        new PlayerOption("Sorry, I think you have the wrong number", "wrong_number", new string[]{"trust--", "anxiety+++"}),
        new PlayerOption("My day was good, how about yours?", "engage_normally", new string[]{"trust+", "attachment+"}),
        new PlayerOption("I found this phone. Who are you looking for?", "reveal_found_phone", new string[]{"trust-", "defensiveness+"})
    };
    
    [Header("X's Reactions to Player Choices")]
    public ChatMessage[] reactionWrongNumber = new ChatMessage[]
    {
        new ChatMessage("X", "What? No... this is your number.", 2f, MessageType.Hesitant, EmotionalState.Confused),
        new ChatMessage("X", "Don't joke like that.", 1f, MessageType.Urgent, EmotionalState.Anxious),
        new ChatMessage("X", "It's me, X. You know that.", 1f, MessageType.Normal, EmotionalState.Hurt)
    };
    
    public ChatMessage[] reactionClarifyIdentity = new ChatMessage[]
    {
        new ChatMessage("X", "What do you mean?", 2f, MessageType.Hesitant, EmotionalState.Confused),
        new ChatMessage("X", "This is still Y's phone, right?", 1f, MessageType.Normal, EmotionalState.Anxious),
        new ChatMessage("X", "Did you get a new number?", 0.5f, MessageType.Urgent, EmotionalState.Needy)
    };
    
    [Header("=== DAY 2 DIALOGUE ===")]
    
    [Header("Morning Message")]
    public ChatMessage[] day2Opening = new ChatMessage[]
    {
        new ChatMessage("X", "Good morning...", 3f, MessageType.Normal, EmotionalState.Lonely),
        new ChatMessage("X", "I had that dream again. The one where you're gone.", 2f, MessageType.Hesitant, EmotionalState.Vulnerable),
        new ChatMessage("X", "You're still here, right?", 1f, MessageType.Urgent, EmotionalState.Needy)
    };
    
    [Header("Player Can First Ask About Y - Day 2")]
    public PlayerOption[] day2AskAboutY = new PlayerOption[]
    {
        new PlayerOption("Who is Y? You keep mentioning that name", "ask_about_y_direct", new string[]{"defensiveness++", "trust-"}),
        new PlayerOption("You seem worried about someone leaving. What happened?", "ask_gentle", new string[]{"defensiveness+", "trust+"}),
        new PlayerOption("I'm here for now. How are you feeling?", "avoid_question_comfort", new string[]{"attachment+", "anxiety-"}),
        new PlayerOption("This isn't Y's phone anymore. I found it.", "reveal_truth", new string[]{"defensiveness+++", "trust--", "anxiety+++"})
    };
    
    [Header("X's Defensive Responses About Y")]
    public ChatMessage[] reactionAskAboutY = new ChatMessage[]
    {
        new ChatMessage("X", "Why are you asking about that?", 4f, MessageType.Hesitant, EmotionalState.Defensive),
        new ChatMessage("X", "It's complicated.", 2f, MessageType.Normal, EmotionalState.Withdrawn),
        new ChatMessage("X", "Can we talk about something else?", 1f, MessageType.Urgent, EmotionalState.Anxious)
    };
    
    // ADD MORE DAYS AND SCENARIOS HERE FOLLOWING THE SAME PATTERN
}

// Dialogue Manager - Handles the logic
public class DialogueManager : MonoBehaviour
{
    public DialogueData dialogueData;
    
    public ChatMessage[] GetDialogueForScenario(string scenarioName)
    {
        // Use reflection or switch to get the right dialogue array
        switch (scenarioName)
        {
            case "day1_opening": return dialogueData.day1Opening;
            case "reaction_wrong_number": return dialogueData.reactionWrongNumber;
            case "reaction_ask_about_y": return dialogueData.reactionAskAboutY;
            // Add more cases as needed
            default: return null;
        }
    }
    
    public PlayerOption[] GetPlayerOptions(string situation)
    {
        switch (situation)
        {
            case "day1_responses": return dialogueData.day1Responses;
            case "day2_ask_about_y": return dialogueData.day2AskAboutY;
            // Add more cases
            default: return null;
        }
    }
}