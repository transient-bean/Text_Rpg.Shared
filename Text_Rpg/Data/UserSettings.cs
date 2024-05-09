namespace Text_Rpg.Data
{
    public class UserSettings
    {
        public int MusicVolume { get; set; }
        public bool ShowHints { get; set; }
        public string? Theme { get; set; } // Nullable property
        public bool Fullscreen { get; set; }
        public int GuiScale { get; set; }
        public string? Difficulty { get; set; } // Nullable property
    }
}
