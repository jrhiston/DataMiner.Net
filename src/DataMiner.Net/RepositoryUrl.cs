using System;

namespace GitDataMiningTool
{
    /// <summary>
    /// Represents a url for a repository.
    /// </summary>
    public class RepositoryUrl : IEquatable<RepositoryUrl>
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="url">The url to encapsulate.</param>
        public RepositoryUrl(string url)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            Url = url;
        }

        /// <summary>
        /// Get the url this class encapsulates.
        /// </summary>
        public string Url { get; }

        public bool Equals(RepositoryUrl other) => other != null && other.Url == Url;
        public override string ToString() => Url;
        public override int GetHashCode() => Url.GetHashCode();
    }
}
