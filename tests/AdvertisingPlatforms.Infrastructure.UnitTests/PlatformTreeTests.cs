using AdvertisingPlatforms.Infrastructure.PlatformsTree;

namespace AdvertisingPlatforms.Infrastructure.UnitTests
{
    public class PlatformTreeTests
    {
        private readonly PlatformTree _tree;

        public PlatformTreeTests()
        {
            _tree = new PlatformTree();
        }

        [Fact]
        public void AddElement_SinglePlatformForLocation_AddsPlatform()
        {
            // Arrange
            var platform = "Google";
            var locations = new[] { "USA/California" };

            // Act
            _tree.AddElement(platform, locations);
            var result = _tree.GetElements("USA/California");

            // Assert
            Assert.Single(result);
            Assert.Contains(platform, result);
        }

        [Fact]
        public void AddElement_MultiplePlatformsForSameLocation_AddsAllPlatforms()
        {
            // Arrange
            var platforms = new[] { "Google", "Facebook" };
            var locations = new[] { "USA/California" };

            // Act
            foreach (var platform in platforms)
            {
                _tree.AddElement(platform, locations);
            }
            var result = _tree.GetElements("USA/California");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains("Google", result);
            Assert.Contains("Facebook", result);
        }

        [Fact]
        public void AddElement_PlatformForMultipleLocations_AddsToAllLocations()
        {
            // Arrange
            var platform = "Google";
            var locations = new[] { "USA/California", "USA/NewYork" };

            // Act
            _tree.AddElement(platform, locations);
            var result1 = _tree.GetElements("USA/California");
            var result2 = _tree.GetElements("USA/NewYork");

            // Assert
            Assert.Single(result1);
            Assert.Single(result2);
            Assert.Contains(platform, result1);
            Assert.Contains(platform, result2);
        }

        [Fact]
        public void GetElements_NonExistentLocation_ReturnsEmptySet()
        {
            // Act
            var result = _tree.GetElements("NonExistentLocation");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetElements_PartialPath_ReturnsAllPlatformsAlongPath()
        {
            // Arrange
            _tree.AddElement("Google", new[] { "USA/California" });
            _tree.AddElement("Facebook", new[] { "USA/California/SanFrancisco" });

            // Act
            var result = _tree.GetElements("USA/California");

            // Assert
            Assert.Single(result);
            Assert.Contains("Google", result);
        }

        [Fact]
        public void GetElements_FullPath_ReturnsAllPlatformsInHierarchy()
        {
            // Arrange
            _tree.AddElement("Google", new[] { "USA" });
            _tree.AddElement("Facebook", new[] { "USA/California" });
            _tree.AddElement("Twitter", new[] { "USA/California/SanFrancisco" });

            // Act
            var result = _tree.GetElements("USA/California/SanFrancisco");

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Contains("Google", result);
            Assert.Contains("Facebook", result);
            Assert.Contains("Twitter", result);
        }

        [Fact]
        public void GetElements_CachesResults_ReturnsCachedValue()
        {
            // Arrange
            _tree.AddElement("Google", new[] { "USA/California" });
            var firstCall = _tree.GetElements("USA/California");

            // Act
            var secondCall = _tree.GetElements("USA/California");

            // Assert
            Assert.Same(firstCall, secondCall);
        }

        [Fact]
        public void Clear_RemovesAllData()
        {
            // Arrange
            _tree.AddElement("Google", new[] { "USA/California" });
            _tree.GetElements("USA/California"); // Заполняем кэш

            // Act
            _tree.Clear();
            var result = _tree.GetElements("USA/California");

            // Assert
            Assert.Empty(result);
            Assert.Empty(_tree.GetElements("NonExistentLocation")); // Проверяем сброс кэша
        }

        [Fact]
        public void Node_AddElement_AddsPlatformToCorrectNode()
        {
            // Arrange
            var node = new Node();
            var segments = new[] { "USA", "California", "SanFrancisco" };

            // Act
            node.AddElement("Google", segments, 0);
            var result = new HashSet<string>();
            node.GetElements(segments, result, 0);

            // Assert
            Assert.Contains("Google", result);
        }
    }
}
