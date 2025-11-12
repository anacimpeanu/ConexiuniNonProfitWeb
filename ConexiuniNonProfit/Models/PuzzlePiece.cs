public class PuzzlePiece
{
    public int Id { get; set; }
    public string VolunteerNeed { get; set; }     // ex: "Vreau să ajut copiii"
    public string AssociationMatch { get; set; }   // ex: "Asociație educațională"
    public string Category { get; set; }           // ex: "Educație"
    public string ActionType { get; set; }         // ex: "Mentorat"
    public int Points { get; set; }
}

