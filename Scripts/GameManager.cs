// GameManager.cs
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("Game State")]
    public int day = 1;
    public int cluesFound = 0;
    public RelationshipStatus relationshipWithX;
    public StoryEnding currentEndingPath;
    
    [Header("Character Knowledge")]
    public bool knowsAboutYsJob;
    public bool knowsAboutXsAttachment;
    public bool knowsTheTruth;
    
    [System.Serializable]
    public class PlayerChoices
    {
        public List<string> majorDecisions = new List<string>();
        public List<string> comfortOffered = new List<string>();
        public List<string> questionsAsked = new List<string>();
    }
    public PlayerChoices playerChoices;
    
    void Awake()
    {
        Instance = this;
    }
}

public enum RelationshipStatus
{
    Stranger,
    Acquaintance,
    Friend,
    CloseFriend,
    Therapist,
    Enabler
}

public enum StoryEnding
{
    NotSet,
    XHeals,           // X accepts help and grows
    XDependent,       // Player replaces Y as X's crutch
    XAbandoned,       // Player leaves like Y did
    TruthRevealed,    // Player discovers what really happened to Y
    CycleRepeats      // X finds someone new to depend on
}