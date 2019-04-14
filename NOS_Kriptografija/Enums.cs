namespace NOS_Kriptografija
{
    public enum SymetricAlgorithm
    {
        AES,
        THREE_DES
    }

    public enum KeySize
    {
        _128 = 128,
        _192 = 192,
        _256 = 256
    }

    public enum EncryptionMode
    {
        ECB,
        CBC,
        CFB,
        OFB,
        CTR
    }

    public enum HashingMode
    {
        SHA_1,
        SHA_2_256,
        SHA_2_512,
        SHA_3_256,
        SHA_3_512
    }
}
