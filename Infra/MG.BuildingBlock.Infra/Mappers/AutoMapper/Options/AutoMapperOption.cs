namespace MG.BuildingBlock.Infra.Mappers.AutoMapper.Options;

public class AutoMapperOption
{
    /// <summary>
    /// Gets or sets comma separated assembly names in string format like AssemblyOne,AssemblyTwo and so on;.
    /// </summary>
    public string AssmblyNamesForLoadProfiles { get; set; } = string.Empty;
}