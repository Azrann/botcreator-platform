using PubnubAdmin;
using Xunit;

namespace Mentor.telegram.shared.test
{
    public class PubnubWrapperTest
    {
        [Fact]
        public void ICanLogIn()
        {
            PubnubWrapper client = new PubnubWrapper();
            var resul  = client.Login("user@example.invalid", "%Qw5od%Ew%8h");
        }

        [Fact]
        public void ICanGetBlocks()
        {
            PubnubWrapper client = new PubnubWrapper();
            var token = client.Login("user@example.invalid", "%Qw5od%Ew%8h");
            var result = client.GetState("540819", token);
        }

        [Fact]
        public void ICanStop()
        {
            PubnubWrapper client = new PubnubWrapper();
            var token = client.Login("user@example.invalid", "%Qw5od%Ew%8h");
            var result = client.Stop("540819", "44763", token);
        }

        [Fact]
        public void ICanStart()
        {
            PubnubWrapper client = new PubnubWrapper();
            var token = client.Login("user@example.invalid", "%Qw5od%Ew%8h");
            var result = client.Start("540819", "44763", token);
        }
    }
}
