using GitDataMiningTool.Commands;
using System;
using System.IO;
using GitDataMiningTool.Pipes;

namespace GitDataMiningTool
{
    public class RepositoryDestination : IEquatable<RepositoryDestination>
    {

        public RepositoryDestination(string destination)
        {
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            Destination = destination;
        }

        public string Destination { get; }

        public bool Equals(RepositoryDestination other)
            => other != null && string.Equals(Destination, other.Destination);
        public override int GetHashCode() => Destination.GetHashCode();
        public override string ToString() => Destination;
    }
}
