﻿using GitDataMiningTool;
using GitDataMiningTool.Commands;
using Moq;
using System;
using Xunit;
using GitDataMiningTool.Pipes;
using GitDataMiningTool.Tests.Builders;

namespace GitDataMiningTool.Tests
{

    public class RepositoryAnalyserTests
    {
        [Fact]
        public void Should_ThrowArgumentNullException_When_PipelineFactoryIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(
                () => new RepositoryAnalyser(null));
        }

        [Fact]
        public void Should_ExposePipelineFactory_When_GivenPipelineFactory()
        {
            var pipelineFactoryMock = Mock.Of<IPipelineFactory>();

            var sut = new RepositoryAnalyser(pipelineFactoryMock);

            Assert.Same(pipelineFactoryMock, sut.PipelineFactory);
        }

        [Fact]
        public void Should_CreateDataAnalysisPipelineWithParameters_When_GivenParameters()
        {
            var builder = new PipelineFactoryBuilder()
                .SetRepositoryUrl("url")
                .SetRepositoryDestination("destination");

            var mockPipelineFactory = builder.Build();

            var sut = new RepositoryAnalyser(mockPipelineFactory);

            var result = sut.Analyse(
                builder.RepositoryUrl, 
                builder.RepositoryDestination);

            Mock.Get(mockPipelineFactory)
                .Verify(
                    pl => pl.CreateDataAnalysisPipeline(builder.RepositoryUrl, builder.RepositoryDestination), 
                    Times.Once, 
                    "Must call pipe line create at least once");
        }

        [Fact]
        public void Should_CreatePipeline_When_GivenPipelineFactory()
        {
            var builder = new PipelineFactoryBuilder()
                .SetRepositoryUrl("url")
                .SetRepositoryDestination("destination");

            var mockPipelineFactory = builder.Build();

            var sut = new RepositoryAnalyser(mockPipelineFactory);

            var result = sut.Analyse(
                builder.RepositoryUrl, 
                builder.RepositoryDestination);

            Mock.Get(builder.Pipeline)
                .Verify(
                    pl => pl.Create(), 
                    Times.Once, 
                    "Must call pipe line create at least once");
        }

        [Fact]
        public void Should_CallPipe_When_PipelineIsCreated()
        {
            var mockPipe = new CompositePipe<CommandResults>();
            var mockPipeline = Mock.Of<IPipeline<CommandResults>>(
                pl => pl.Create() == mockPipe
            );

            var builder = new PipelineFactoryBuilder()
                .SetPipeline(mockPipeline);

            var mockPipelineFactory = builder.Build();

            var sut = new RepositoryAnalyser(mockPipelineFactory);

            var result = sut.Analyse(builder.RepositoryUrl, builder.RepositoryDestination);

            var expected = mockPipe.Pipe(new CommandResults());

            Assert.Equal(expected, result);
        }
    }
}
