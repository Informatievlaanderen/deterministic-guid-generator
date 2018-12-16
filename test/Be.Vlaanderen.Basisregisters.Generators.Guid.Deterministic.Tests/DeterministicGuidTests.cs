namespace Be.Vlaanderen.Basisregisters.Generators.Guid.DeterministicTests
{
    using System;
    using Shouldly;
    using Xunit;

    public class DeterministicGuidTests
    {
        [Theory]
        // https://www.ietf.org/rfc/rfc4122.txt
        [InlineData("6ba7b810-9dad-11d1-80b4-00c04fd430c8", "www.widgets.com", 3, "3d813cbb-47fb-32ba-91df-831e1593ac29")]

        // https://docs.python.org/2/library/uuid.html
        [InlineData("6ba7b810-9dad-11d1-80b4-00c04fd430c8", "python.org", 3, "6fa459ea-ee8a-3ca4-894e-db77e160355e")]
        [InlineData("6ba7b810-9dad-11d1-80b4-00c04fd430c8", "python.org", 5, "886313e1-3b8a-5372-9b90-0c9aee199e5d")]

        // https://www.npmjs.com/package/uuid
        [InlineData("6ba7b810-9dad-11d1-80b4-00c04fd430c8", "hello.example.com", 5, "fdda765f-fc57-5604-a269-52a7df8164ec")]
        [InlineData("6ba7b811-9dad-11d1-80b4-00c04fd430c8", "http://example.com/hello", 5, "3bbcee75-cecc-5b56-8031-b6641c1ed1f1")]
        public void When_generating_a_deterministic_guid(
            string namespaceGuidString,
            string value,
            int version,
            string resultingGuidString)
        {
            var namespaceGuid = new Guid(namespaceGuidString);
            var resultingGuid = new Guid(resultingGuidString);

            Deterministic.Create(namespaceGuid, value, version).ShouldBe(resultingGuid);
        }
    }
}
