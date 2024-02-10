namespace BusinessLayer.Infrastructure;

public static class AvatarHelper
{
    public static async Task<byte[]> GenerateAvatarAsync(string name)
    {
        // TO DO, pull avatar service value from app setting, this is an opportunity for improvement

        using HttpClient client = new();
        var byteArrayResponse = await client.GetByteArrayAsync(
            $"https://ui-avatars.com/api/?name={name}");

        return byteArrayResponse;
    }
}