namespace Airport.BL.Dto.Crew
{
    public class EditableCrewFields
    {
        public string Name { get; set; }
        public int PilotId { get; set; }
        public int[] StewardessIds { get; set; }
    }
}