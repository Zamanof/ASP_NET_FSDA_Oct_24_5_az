namespace ASP_NET_22._TaskFlow_CQRS.Application.Storage;

public class StoredFileInfo
{
    public string StorageKey { get; set; } = string.Empty;
    public string StoredFileName { get; set; } = string.Empty;
    public long Size { get; set; }
}
