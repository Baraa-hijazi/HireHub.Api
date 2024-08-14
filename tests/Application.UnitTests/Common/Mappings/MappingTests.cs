using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using HireHub.Api.Application.Candidates.Queries.DTOs;
using HireHub.Api.Application.Common.Interfaces;
using HireHub.Api.Domain.Entities;
using NUnit.Framework;

namespace HireHub.Api.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IMapper _mapper;

    public MappingTests()
    {
        MapperConfiguration configuration = new(config =>
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

        _mapper = configuration.CreateMapper();
    }

    [Test]
    [TestCase(typeof(Candidate), typeof(CandidateDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private static object GetInstanceOf(Type type)
    {
        return type.GetConstructor(Type.EmptyTypes) != null
            ? Activator.CreateInstance(type)!
            : RuntimeHelpers.GetUninitializedObject(type);
    }
}
