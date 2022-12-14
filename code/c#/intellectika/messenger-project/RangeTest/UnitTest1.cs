using Range.Encryption;

namespace RangeTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Verify_IdenticalPassword_True()
        {
            string password = "25emaful";

            var hashedPass = SecurePasswordHasher.Hash(password, 5);

            var result = SecurePasswordHasher.Verify("w", hashedPass);

            Assert.That(result, Is.True);
        }
    }
}