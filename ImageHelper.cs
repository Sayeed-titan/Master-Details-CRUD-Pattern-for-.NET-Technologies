public static class ImageHelper
{
    // Convert IFormFile to byte array
    public static byte[] ConvertToBytes(IFormFile file)
    {
        using var ms = new MemoryStream();
        file.CopyTo(ms);
        return ms.ToArray();
    }

    // Convert byte array to Base64
    public static string ToBase64String(byte[] imageData)
    {
        return imageData != null ? 
            $"data:image/jpeg;base64,{Convert.ToBase64String(imageData)}" : 
            string.Empty;
    }
    
    // MAUI-specific implementation
    public static async Task<byte[]> ConvertMauiImageToBytes(FileResult file)
    {
        using var stream = await file.OpenReadAsync();
        using var ms = new MemoryStream();
        await stream.CopyToAsync(ms);
        return ms.ToArray();
    }
}
