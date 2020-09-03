using EntityFrameworkCore.SimpleEntityAudit.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EntityFrameworkCore.SimpleEntityAudit.Test.SimpleActorAudit
{
    public partial class SimpleActorAuditTests
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
            var actorProvider = new ActorProvider();
            using var database = CreateDatabase(actorProvider);

            var author = new Author();

            Assert.Equal(default, author.CreatedBy);
            Assert.Null(author.LastModifiedBy);

            database.Authors.Add(author);
            await saveMethod(database);

            Assert.Equal(actorProvider.Provide(), author.CreatedBy);
            Assert.Null(author.LastModifiedBy);
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
            var actorProvider = new ActorProvider();
            using var database = CreateDatabase(actorProvider);

            var authors = new[] {
                new Author(),
                new Author(),
                new Author(),
                new Author(),
                new Author()
            };

            foreach (var author in authors)
            {
                Assert.Equal(default, author.CreatedBy);
                Assert.Null(author.LastModifiedBy);
            }

            database.Authors.AddRange(authors);
            await saveMethod(database);

            foreach (var author in authors)
            {
                Assert.Equal(actorProvider.Provide(), author.CreatedBy);
                Assert.Null(author.LastModifiedBy);
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
            var actorProvider = new ActorProvider();
            using var database = CreateDatabase(actorProvider);

            var author = new Author();

            database.Authors.Add(author);
            database.SaveChanges();

            Assert.Equal(actorProvider.Provide(), author.CreatedBy);
            Assert.Null(author.LastModifiedBy);

            author.Name = actorProvider.Provide().ToString();
            await saveMethod(database);

            Assert.Equal(actorProvider.Provide(), author.CreatedBy);
            Assert.Equal(actorProvider.Provide(), author.LastModifiedBy);
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
            var actorProvider = new ActorProvider();
            using var database = CreateDatabase(actorProvider);

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
                Assert.Equal(actorProvider.Provide(), author.CreatedBy);
                Assert.Null(author.LastModifiedBy);
            }

            foreach (var author in authors)
            {
                author.Name = actorProvider.Provide().ToString();
            }

            await saveMethod(database);

            foreach (var author in authors)
            {
                Assert.Equal(actorProvider.Provide(), author.CreatedBy);
                Assert.Equal(actorProvider.Provide(), author.LastModifiedBy);
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
            var actorProvider = new ActorProvider();
            using var database = CreateDatabase(actorProvider);

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
                Assert.Equal(actorProvider.Provide(), author.CreatedBy);
                Assert.Null(author.LastModifiedBy);
            }

            foreach (var author in existingAuthors)
            {
                author.Name = actorProvider.Provide().ToString();
            }

            database.Authors.AddRange(newAuthors);

            await saveMethod(database);

            foreach (var author in existingAuthors)
            {
                Assert.Equal(actorProvider.Provide(), author.CreatedBy);
                Assert.Equal(actorProvider.Provide(), author.LastModifiedBy);
            }

            foreach (var author in newAuthors)
            {
                Assert.Equal(actorProvider.Provide(), author.CreatedBy);
                Assert.Null(author.LastModifiedBy);
            }
        }

        private AuditDbContext CreateDatabase(IActorProvider<int> actorProvider)
        {
            var options = new DbContextOptionsBuilder<AuditDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AuditDbContext(options, actorProvider);
        }
    }
}