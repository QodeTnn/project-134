using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    
    [Header("UI References")]
    public Transform chatContainer;
    public GameObject messageBubblePrefab;
    public GameObject playerChoicePanel;
    public GameObject choiceButtonPrefab;
    public ScrollRect chatScrollRect;
    
    [Header("Dialogue Data")]
    public DialogueData dialogueData;
    
    private List<ChatMessage> currentConversation = new List<ChatMessage>();
    private bool isDisplayingMessages = false;
    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        // Start the game with Day 1 opening
        StartConversation("day1_opening");
    }
    
    public void StartConversation(string conversationKey)
    {
        ChatMessage[] messages = GetDialogueForScenario(conversationKey);
        if (messages != null)
        {
            currentConversation.Clear();
            currentConversation.AddRange(messages);
            StartCoroutine(DisplayConversation());
        }
    }
    
    public void ShowPlayerOptions(string situationKey)
    {
        PlayerOption[] options = GetPlayerOptions(situationKey);
        if (options != null)
        {
            ClearPlayerChoices();
            
            foreach (PlayerOption option in options)
            {
                GameObject choiceButton = Instantiate(choiceButtonPrefab, playerChoicePanel.transform);
                Button button = choiceButton.GetComponent<Button>();
                Text buttonText = choiceButton.GetComponentInChildren<Text>();
                
                buttonText.text = option.choiceText;
                
                button.onClick.AddListener(() => OnPlayerChoiceSelected(option));
            }
            
            playerChoicePanel.SetActive(true);
        }
    }
    
    private void OnPlayerChoiceSelected(PlayerOption chosenOption)
    {
        playerChoicePanel.SetActive(false);
        ClearPlayerChoices();
        
        // Apply emotional effects
        GameManager.Instance.UpdateXEmotions(chosenOption.emotionalEffects);
        
        // Record player choice
        GameManager.Instance.playerChoices.majorDecisions.Add(chosenOption.choiceId);
        
        // Show X's reaction based on choice
        string reactionKey = "reaction_" + chosenOption.choiceId;
        StartConversation(reactionKey);
    }
    
    private IEnumerator DisplayConversation()
    {
        isDisplayingMessages = true;
        
        foreach (ChatMessage message in currentConversation)
        {
            yield return new WaitForSeconds(message.delayBeforeShow);
            
            GameObject newBubble = Instantiate(messageBubblePrefab, chatContainer);
            MessageBubble bubbleComponent = newBubble.GetComponent<MessageBubble>();
            
            bubbleComponent.SetupMessage(message);
            
            // Auto-scroll to bottom
            Canvas.ForceUpdateCanvases();
            chatScrollRect.verticalNormalizedPosition = 0f;
            
            // Wait a bit between messages
            yield return new WaitForSeconds(0.8f);
        }
        
        isDisplayingMessages = false;
        
        // After conversation ends, show player choices if it's their turn
        if (currentConversation.Count > 0 && currentConversation[currentConversation.Count - 1].sender == "X")
        {
            ShowAppropriatePlayerOptions();
        }
    }
    
    private void ShowAppropriatePlayerOptions()
    {
        if (GameManager.Instance.day == 1)
        {
            ShowPlayerOptions("day1_responses");
        }
        else if (GameManager.Instance.day == 2)
        {
            ShowPlayerOptions("day2_ask_about_y");
        }
        // Add more day conditions as needed
    }
    
    private void ClearPlayerChoices()
    {
        foreach (Transform child in playerChoicePanel.transform)
        {
            Destroy(child.gameObject);
        }
    }
    
    public ChatMessage[] GetDialogueForScenario(string scenarioName)
    {
        switch (scenarioName)
        {
            case "day1_opening": return dialogueData.day1Opening;
            case "reaction_wrong_number": return dialogueData.reactionWrongNumber;
            case "reaction_clarify_identity": return dialogueData.reactionClarifyIdentity;
            case "reaction_engage_normally": return dialogueData.reactionEngageNormally;
            case "day2_opening": return dialogueData.day2Opening;
            case "reaction_ask_about_y": return dialogueData.reactionAskAboutY;
            default: return null;
        }
    }
    
    public PlayerOption[] GetPlayerOptions(string situation)
    {
        switch (situation)
        {
            case "day1_responses": return dialogueData.day1Responses;
            case "day2_ask_about_y": return dialogueData.day2AskAboutY;
            default: return null;
        }
    }
}