using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SampleApi.Data.Entities;
using SampleApi.RestAPI.AutoMapperProfiles;
using SampleApi.RestAPI.Controllers;
using SampleApi.RestAPI.Models;
using System;
using System.Security.Claims;

namespace SampleApi.RestAPI.Tests
{
    [Category("UnitTests")]
    public class ApartmentsControllerTests
    {
        ApartmentsController _controller;
        FakeApartmentsRepository _apartmentsRepository;
        FakeLocationsRepository _locationsRepository;


        public void InitializeTest()
        {
            _apartmentsRepository = new FakeApartmentsRepository();
            _locationsRepository = new FakeLocationsRepository();
            var apartmentsAutoMapperProfile = new ApartmentProfile();
            var locationsAutoMapperProfile = new LocationProfile();
            var mapperConfigs = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(apartmentsAutoMapperProfile);
                cfg.AddProfile(locationsAutoMapperProfile);
            });
            var mapper = new Mapper(mapperConfigs);

            //Func<AuthorizationResult> authResult = authSucceeds ? 
            //    (Func<AuthorizationResult>)AuthorizationResult.Success : AuthorizationResult.Failed;

            var iLoggerMock = new Mock<ILogger<ApartmentsController>>();

            _controller = new ApartmentsController(iLoggerMock.Object, _apartmentsRepository, _locationsRepository, mapper);
        }

        [Test]
        public void PostApartment_WithValidDto_ReturnsCreatedResponse()
        {
            // Setup
            InitializeTest();
            _locationsRepository.AddAsync(new Location
            {
                Id = 1,
                Building = "X",
                Flat = "x",
                StreetName = "X"
            });

            var createAptDto = new CreateApartmentDto
            {
                NumberOfBeds = 1,
                RentalPrice = 9,
                SquareMeters = 10,
                LocationId = 1
            };

            //Act 
            var response = _controller.Post(createAptDto);

            //Assert
            response.Result.Should().BeOfType<CreatedAtActionResult>("valid CreateApartmentDto provided");
        }

        [Test]
        public void PostApartment_WithNotExistingLocation_ReturnsNotFound()
        {
            // Setup
            InitializeTest();

            var createAptDto = new CreateApartmentDto
            {
                NumberOfBeds = 1,
                RentalPrice = 9,
                SquareMeters = 10,
                LocationId = 111
            };

            //Act 
            var response = _controller.Post(createAptDto);

            //Assert
            response.Result.Should().BeOfType<ObjectResult>("non existing locationId provided")
                .Which.StatusCode.Value.Equals(422);

        }
    }
}