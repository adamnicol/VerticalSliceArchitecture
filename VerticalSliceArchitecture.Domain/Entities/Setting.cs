namespace VerticalSliceArchitecture.Domain.Entities
{
    [Keyless]
    public class Setting
    {
        public required string SettingName { get; set; }
        public string? SettingValue { get; set; }
    }
}
