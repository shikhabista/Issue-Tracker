using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations.Internal;

namespace Base.MigrationHistoryOverrides;

public class MigrationHistory : NpgsqlHistoryRepository
{
    public MigrationHistory(HistoryRepositoryDependencies dependencies) : base(dependencies)
    {
    }

    protected override void ConfigureTable(EntityTypeBuilder<HistoryRow> history)
    {
        base.ConfigureTable(history);
        history.Property(x => x.MigrationId).HasColumnName("MigrationId");
        history.Property(x => x.ProductVersion).HasColumnName("ProductVersion");
    }
}