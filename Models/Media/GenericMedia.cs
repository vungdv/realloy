namespace realloy.Models.Media;

[ContentType(GUID = "378ce813-3d08-450e-95ef-55a7d9eac65d")]
public class GenericMedia : MediaData
{
    public virtual string Description { get; set; } = null!;
}