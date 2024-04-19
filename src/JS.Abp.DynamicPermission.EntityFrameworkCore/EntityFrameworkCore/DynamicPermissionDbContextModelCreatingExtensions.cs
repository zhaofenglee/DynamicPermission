using Volo.Abp.EntityFrameworkCore.Modeling;
using JS.Abp.DynamicPermission.PermissionDefinitions;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace JS.Abp.DynamicPermission.EntityFrameworkCore;

public static class DynamicPermissionDbContextModelCreatingExtensions
{
    public static void ConfigureDynamicPermission(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(DynamicPermissionDbProperties.DbTablePrefix + "Questions", DynamicPermissionDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
        if (builder.IsHostDatabase())
        {
            builder.Entity<PermissionDefinition>(b =>
            {
                b.ToTable(DynamicPermissionDbProperties.DbTablePrefix + "PermissionDefinitions", DynamicPermissionDbProperties.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.GroupName).HasColumnName(nameof(PermissionDefinition.GroupName)).IsRequired().HasMaxLength(PermissionDefinitionConsts.GroupNameMaxLength);
                b.Property(x => x.Name).HasColumnName(nameof(PermissionDefinition.Name)).IsRequired().HasMaxLength(PermissionDefinitionConsts.NameMaxLength);
                b.Property(x => x.ParentName).HasColumnName(nameof(PermissionDefinition.ParentName)).HasMaxLength(PermissionDefinitionConsts.ParentNameMaxLength);
                b.Property(x => x.DisplayName).HasColumnName(nameof(PermissionDefinition.DisplayName)).IsRequired().HasMaxLength(PermissionDefinitionConsts.DisplayNameMaxLength);
                b.Property(x => x.IsEnabled).HasColumnName(nameof(PermissionDefinition.IsEnabled));
            });

        }
    }
}