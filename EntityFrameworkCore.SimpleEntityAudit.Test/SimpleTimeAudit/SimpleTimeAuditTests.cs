using EntityFrameworkCore.SimpleEntityAudit.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EntityFrameworkCore.SimpleEntityAudit.Test.SimpleTimeAudit
{
    public partial class SimpleTimeAuditTests
    {
        [Fact]
        public async Task Single_New_SaveChanges()
        {
            await Single_New(x =>
            {
                var count = x.SaveChanges();

                return Task.FromResult(count);
            });
        }

        [Fact]
        public async Task Single_New_SaveChangesAsync()
        {
            await Single_New(x => x.SaveChangesAsync());
        }

        [Fact]
        public async Task Single_New_SaveChangesAccept()
        {
            await Single_New(x =>
            {
                var count = x.SaveChanges(true);

                return Task.FromResult(count);
            });
        }

        [Fact]
        public async Task Single_New_SaveChangesAsyncAccept()
        {
            await Single_New(x => x.SaveChangesAsync(true));
        }

        private async Task Single_New(Func<AuditDbContext, Task<int>> saveMethod)
        {
            var clock = new Clock();
            using var database = CreateDatabase(clock);

            var author = new Author();

            Assert.Equal(DateTime.MinValue, author.CreatedOn);
            Assert.Null(author.LastModifiedOn);

            database.Authors.Add(author);
            await saveMethod(database);

            Assert.Equal(clock.UtcNow, author.CreatedOn);
            Assert.Null(author.LastModifiedOn);
        }

        [Fact]
        public async Task Multiple_New_SaveChanges()
        {
            await Multiple_New(x =>
            {
                var count = x.SaveChanges();

                return Task.FromResult(count);
            });
        }

        [Fact]
        public async Task Multiple_New_SaveChangesAsync()
        {
            await Multiple_New(x => x.SaveChangesAsync());
        }

        [Fact]
        public async Task Multiple_New_SaveChangesAccept()
        {
            await Multiple_New(x =>
            {
                var count = x.SaveChanges(true);

                return Task.FromResult(count);
            });
        }

        [Fact]
        public async Task Multiple_New_SaveChangesAsyncAccept()
        {
            await Multiple_New(x => x.SaveChangesAsync(true));
        }

        private async Task Multiple_New(Func<AuditDbContext, Task<int>> saveMethod)
        {
            var clock = new Clock();
            using var database = CreateDatabase(clock);

            var authors = new[] {
                new Author(),
                new Author(),
                new Author(),
                new Author(),
                new Author()
            };

            foreach (var author in authors)
            {
                Assert.Equal(DateTime.MinValue, author.CreatedOn);
                Assert.Null(author.LastModifiedOn);
            }

            database.Authors.AddRange(authors);
            await saveMethod(database);

            foreach (var author in authors)
            {
                Assert.Equal(clock.UtcNow, author.CreatedOn);
                Assert.Null(author.LastModifiedOn);
            }
        }

        [Fact]
        public async Task Single_Existing_SaveChanges()
        {
            await Single_Existing(x =>
            {
                var count = x.SaveChanges();

                return Task.FromResult(count);
            });
        }

        [Fact]
        public async Task Single_Existing_SaveChangesAsync()
        {
            await Single_Existing(x => x.SaveChangesAsync());
        }

        [Fact]
        public async Task Single_Existing_SaveChangesAccept()
        {
            await Single_Existing(x =>
            {
                var count = x.SaveChanges(true);

                return Task.FromResult(count);
            });
        }

        [Fact]
        public async Task Single_Existing_SaveChangesAsyncAccept()
        {
            await Single_Existing(x => x.SaveChangesAsync(true));
        }

        private async Task Single_Existing(Func<AuditDbContext, Task<int>> saveMethod)
        {
            var clock = new Clock();
            using var database = CreateDatabase(clock);

            var author = new Author();

            database.Authors.Add(author);
            database.SaveChanges();

            Assert.Equal(clock.UtcNow, author.CreatedOn);
            Assert.Null(author.LastModifiedOn);

            author.Name = clock.UtcNow.ToShortDateString();
            await saveMethod(database);

            Assert.Equal(clock.UtcNow, author.CreatedOn);
            Assert.Equal(clock.UtcNow, author.LastModifiedOn);
        }

        [Fact]
        public async Task Multiple_Existing_SaveChanges()
        {
            await Multiple_Existing(x =>
            {
                var count = x.SaveChanges();

                return Task.FromResult(count);
            });
        }

        [Fact]
        public async Task Multiple_Existing_SaveChangesAsync()
        {
            await Multiple_Existing(x => x.SaveChangesAsync());
        }

        [Fact]
        public async Task Multiple_Existing_SaveChangesAccept()
        {
            await Multiple_Existing(x =>
            {
                var count = x.SaveChanges(true);

                return Task.FromResult(count);
            });
        }

        [Fact]
        public async Task Multiple_Existing_SaveChangesAsyncAccept()
        {
            await Multiple_Existing(x => x.SaveChangesAsync(true));
        }

        private async Task Multiple_Existing(Func<AuditDbContext, Task<int>> saveMethod)
        {
            var clock = new Clock();
            using var database = CreateDatabase(clock);

            var authors = new[] {
                new Author(),
                new Author(),
                new Author(),
                new Author(),
                new Author()
            };

            database.Authors.AddRange(authors);
            database.SaveChanges();

            foreach (var author in authors)
            {
                Assert.Equal(clock.UtcNow, author.CreatedOn);
                Assert.Null(author.LastModifiedOn);
            }

            foreach (var author in authors)
            {
                author.Name = clock.UtcNow.ToShortDateString();
            }

            await saveMethod(database);

            foreach (var author in authors)
            {
                Assert.Equal(clock.UtcNow, author.CreatedOn);
                Assert.Equal(clock.UtcNow, author.LastModifiedOn);
            }
        }

        [Fact]
        public async Task Multiple_Mixed_SaveChanges()
        {
            await Multiple_Mixed(x =>
            {
                var count = x.SaveChanges();

                return Task.FromResult(count);
            });
        }

        [Fact]
        public async Task Multiple_Mixed_SaveChangesAsync()
        {
            await Multiple_Mixed(x => x.SaveChangesAsync());
        }

        [Fact]
        public async Task Multiple_Mixed_SaveChangesAccept()
        {
            await Multiple_Mixed(x =>
            {
                var count = x.SaveChanges(true);

                return Task.FromResult(count);
            });
        }

        [Fact]
        public async Task Multiple_Mixed_SaveChangesAsyncAccept()
        {
            await Multiple_Mixed(x => x.SaveChangesAsync(true));
        }

        private async Task Multiple_Mixed(Func<AuditDbContext, Task<int>> saveMethod)
        {
            var clock = new Clock();
            using var database = CreateDatabase(clock);

            var newAuthors = new[] {
                new Author(),
                new Author(),
                new Author()
            };
            var existingAuthors = new[] {
                new Author(),
                new Author(),
                new Author(),
                new Author(),
                new Author()
            };

            database.Authors.AddRange(existingAuthors);
            database.SaveChanges();

            foreach (var author in existingAuthors)
            {
                Assert.Equal(clock.UtcNow, author.CreatedOn);
                Assert.Null(author.LastModifiedOn);
            }

            foreach (var author in existingAuthors)
            {
                author.Name = clock.UtcNow.ToShortDateString();
            }

            database.Authors.AddRange(newAuthors);

            await saveMethod(database);

            foreach (var author in existingAuthors)
            {
                Assert.Equal(clock.UtcNow, author.CreatedOn);
                Assert.Equal(clock.UtcNow, author.LastModifiedOn);
            }

            foreach (var author in newAuthors)
            {
                Assert.Equal(clock.UtcNow, author.CreatedOn);
                Assert.Null(author.LastModifiedOn);
            }
        }

        private AuditDbContext CreateDatabase(IClock clock)
        {
            var options = new DbContextOptionsBuilder<AuditDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AuditDbContext(options, clock);
        }
    }
}