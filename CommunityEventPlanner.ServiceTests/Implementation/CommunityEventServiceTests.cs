using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using CommunityEventPlanner.Data.Models;
using CommunityEventPlanner.Data.Data;
using AutoMapper;
using CommunityEventPlanner.Shared.Contract;
using CommunityEventPlanner.Service.MapperProfile;

namespace CommunityEventPlanner.Service.Implementation.Tests
{
    [TestClass()]
    public class CommunityEventServiceTests
    {
        private CommunityEventService _communityEventService;
        private CommunityEventPlannerDbContext _context;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CommunityEventPlannerDbContext>()
                .UseInMemoryDatabase(databaseName: "CommunityEventPlannerTest")
                .Options;
            _context = new CommunityEventPlannerDbContext(options);

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CommunityEventProfile());
            }).CreateMapper();

            _communityEventService = new CommunityEventService(_context, _mapper);
        }

        [TestMethod()]
        public async Task CreateTest_ShouldCreateCommunityEventAsync()
        {
            var communityEvent = new CommunityEventCreationRequest() {Name = "Test", StartDate = DateTime.Now.Date };

            var result = await _communityEventService.CreateAsync(communityEvent);

            var insertedRecord = await _context.CommunityEvents.FirstOrDefaultAsync();

            insertedRecord.Should().NotBeNull();
            insertedRecord.Name.Should().Be(communityEvent.Name);
            insertedRecord.StartDate.Should().Be(communityEvent.StartDate);
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Event Created.");
            await _context.Database.EnsureDeletedAsync();
        }

        [TestMethod()]
        public async Task createTest_ShouldNotCreateCommunityEventWhenNameIsEmptyStringAsync()
        {
            var communityEvent = new CommunityEventCreationRequest() { Name = "", StartDate = DateTime.Now.Date };

            var result = await _communityEventService.CreateAsync(communityEvent);

            var insertedRecord = await _context.CommunityEvents.FirstOrDefaultAsync();

            insertedRecord.Should().BeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Name is required for an event.");
            await _context.Database.EnsureDeletedAsync();
        }

        [TestMethod()]
        public async Task CreateTest_ShouldNotCreateCommunityEventWhenStartDateIsAfterEndDateAsync()
        {
            var communityEvent = new CommunityEventCreationRequest() { Name = "", StartDate = DateTime.Now.Date, EndDate = DateTime.Now.Date.AddDays(-1) };

            var result = await _communityEventService.CreateAsync(communityEvent);

            var insertedRecord = await _context.CommunityEvents.FirstOrDefaultAsync();

            insertedRecord.Should().BeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Start Date must be before End Date.");

            await _context.Database.EnsureDeletedAsync();
        }


        [TestMethod()]
        public async Task GetUpcomingTest_ShouldOnlyGetCommunityEventWithStartDateInFuture()
        {
            var communityEvents = new List<CommunityEvent>() 
                { 
                    new() { Name = "Test", StartDate = DateTime.Now.Date.AddDays(1) },
                    new() { Name = "Test Two", StartDate = DateTime.Now.Date.AddDays(-1) }
                };

            await SeedCommunityEventInMemoryDb(communityEvents);

            var result = await _communityEventService.GetUpcomingAsync();

            result.Count().Should().Be(1);
            result.First().Name.Should().Be("Test");
            result.First().StartDate.Should().Be(DateTime.Now.Date.AddDays(1));

            await _context.Database.EnsureDeletedAsync();
        }

        private async Task SeedCommunityEventInMemoryDb(List<CommunityEvent> communityEvents)
        {
            _context.CommunityEvents.AddRange(communityEvents);
            await _context.SaveChangesAsync();
        }
    }
}