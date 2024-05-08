using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace SharpChat.Logic;

public class Hash()
{
    private readonly int _degreeOfParallelism = 4;
    private readonly int _numberOfIterations = 4;
    private readonly int _memorySize = 16 * 1024;

    public string CreateSalt()
    {
        byte[] salt = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);

        return Convert.ToBase64String(salt);
    }

    public string GetHash(string password, string salt)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            DegreeOfParallelism = _degreeOfParallelism,
            Iterations = _numberOfIterations,
            MemorySize = _memorySize,
            Salt = Convert.FromBase64String(salt)
        };

        return Convert.ToBase64String(argon2.GetBytes(64));
    }

    public bool VerifyHash(string hash, string password, string salt)
    {
        return hash == GetHash(password, salt);
    }
}
