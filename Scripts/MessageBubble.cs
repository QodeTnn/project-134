using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageBubble : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public Image bubbleBackground;
    public CanvasGroup canvasGroup;
    
    [Header("Colors")]
    public Color xMessageColor = new Color(0.9f, 0.9f, 0.9f, 1f);
    public Color playerMessageColor = new Color(0.2f, 0.6f, 1f, 1f);
    
    public void SetupMessage(ChatMessage message)
    {
        messageText.text = message.text;
        
        // Set bubble color based on sender
        if (message.sender == "X")
        {
            bubbleBackground.color = xMessageColor;
            // Optional: align left for X messages
        }
        else
        {
            bubbleBackground.color = playerMessageColor;
            // Optional: align right for player messages
        }
        
        // Handle different message types
        if (message.type == MessageType.Hesitant)
        {
            StartCoroutine(ShowHesitantMessage());
        }
        else
        {
            canvasGroup.alpha = 1f;
        }
    }
    
    private System.Collections.IEnumerator ShowHesitantMessage()
    {
        canvasGroup.alpha = 0f;
        
        // Show typing indicator
        messageText.text = "...";
        canvasGroup.alpha = 1f;
        
        yield return new WaitForSeconds(1.5f);
        
        // Show actual message
        canvasGroup.alpha = 0f;
        yield return new WaitForSeconds(0.3f);
        canvasGroup.alpha = 1f;
    }
}