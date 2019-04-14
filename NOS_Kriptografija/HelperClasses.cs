namespace NOS_Kriptografija
{
    public static class HelperClasses
    {
        public class RSAKey
        {
            public string Modulus;
            public string Exponent;
        }

        public class Envelope
        {
            public string Data;
            public string Key;
        }
    }
}
