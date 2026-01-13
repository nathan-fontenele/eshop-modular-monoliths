using Microsoft.EntityFrameworkCore;
using Shared.Data.Interceptors;
using Shared.DDD;

namespace Catalog.Tests;

public class AuditableEntityInterceptorTest
{
    public class TestEntity : IEntity
    {
        public Guid Id { get; set; }
        public string? LastModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }

    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options) : base (options){}
        public DbSet<TestEntity> Entities { get; set; }
    }

    [Fact]
    public async Task SaveChangesAsync_Should_Populate_Audit_Fields_When_Adding_New_Entity()
    {
        //Arrange
        var interceptor = new AuditableEntityInterceptor();
        
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .AddInterceptors(interceptor)
            .Options;
        
        using var context = new TestDbContext(options);
        var entity = new TestEntity {Id = Guid.NewGuid()};
        
        //Act
        context.Add(entity);
        await context.SaveChangesAsync();
        
        //Assert
        Assert.Equal("mehmet", entity.CreatedBy);
        Assert.NotNull(entity.CreatedAt);
        
        Assert.True(entity.CreatedAt > DateTime.UtcNow.AddSeconds(-1));
    }

    [Fact]
    public async Task SaveChangesAsync_Should_Update_Modified_Fields_When_Editing_Entity()
    {
        //Arrange
        var interceptor = new AuditableEntityInterceptor();
        
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .AddInterceptors(interceptor)
            .Options;
        
        using var context = new TestDbContext(options);
        var entity = new TestEntity {Id = Guid.NewGuid(), CreatedBy = "antonio"};
        context.Entities.Add(entity);
        await context.SaveChangesAsync();
        
        //Act
        entity.CreatedBy = "Attempty modify the creator.";
        context.Entry(entity).State = EntityState.Modified;
        
        await context.SaveChangesAsync();
        
        //Assert
        Assert.Equal("mehmet", entity.LastModifiedBy);
        Assert.NotNull(entity.LastModifiedAt);
    }
}