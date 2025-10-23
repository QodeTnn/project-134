// AppInvestigationSystem.cs
public class AppInvestigationSystem : MonoBehaviour
{
    [System.Serializable]
    public class AppContent
    {
        public string appName;
        public Clue[] clues;
        public ChatMessage[] xReaction; // How X reacts when you find clues
    }
    
    public AppContent[] phoneApps = new AppContent[]
    {
        new AppContent
        {
            appName = "Gallery",
            clues = new Clue[]
            {
                new Clue
                {
                    clueName = "Last Photo",
                    description = "Blurry photo of an empty park bench",
                    foundText = "You find a photo from last month. It's just an empty bench.",
                    unlocksAbility = "can_ask_about_bench"
                }
            },
            xReaction = new ChatMessage[]
            {
                new ChatMessage("X", "Why are you looking through old photos?", 3f, MessageType.Hesitant, EmotionalState.Defensive),
                new ChatMessage("X", "That was from a bad day.", 2f, MessageType.Normal, EmotionalState.Vulnerable)
            }
        },
        
        new AppContent
        {
            appName = "Notes",
            clues = new Clue[]
            {
                new Clue
                {
                    clueName = "Y's Journal",
                    description = "Entries about feeling overwhelmed",
                    foundText = "You find notes about someone feeling smothered.",
                    unlocksAbility = "understand_ys_perspective"
                }
            },
            xReaction = new ChatMessage[]
            {
                new ChatMessage("X", "Those are private!", 1f, MessageType.Urgent, EmotionalState.Angry),
                new ChatMessage("X", "You weren't supposed to see that.", 2f, MessageType.Hesitant, EmotionalState.Hurt)
            }
        }
        // ADD MORE APPS HERE
    };
}