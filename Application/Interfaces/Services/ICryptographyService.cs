namespace Application.Interfaces.Services
{
    public interface ICryptographyService
    {
        string Name { get; }

        byte[] Encrypt(byte[] data, byte[] key, byte[] iv);
        byte[] Decrypt(byte[] data, byte[] key, byte[] iv);
    }
}
