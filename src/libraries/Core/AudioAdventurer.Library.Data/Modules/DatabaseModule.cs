using System.IO;
using AudioAdventurer.Library.Cache.Managers;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Data.Helpers;
using AudioAdventurer.Library.Data.Interfaces;
using AudioAdventurer.Library.Data.Repos;
using AudioAdventurer.Library.Data.Services;
using Autofac;
using LiteDB;

namespace AudioAdventurer.Library.Data.Modules;

public class DatabaseModule : Module
{
    private readonly IConfig _config;

    public DatabaseModule(IConfig config)
    {
        _config = config;
    }

    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        // Register LiteDB
        builder.Register<LiteDatabase>(_ =>
            {
                string dbFile = _config.Values["DB_FILE"];
                string dbPath = Path.GetDirectoryName(dbFile);

                DirectoryHelper.EnsureDirectory(dbPath);
                var db = new LiteDatabase(dbFile);
                return db;
            }).As<LiteDatabase>()
            .SingleInstance();

        // Register Repos
        builder.RegisterType<BehaviorInfoRepo>()
            .As<IBehaviorInfoRepo>();

        builder.RegisterType<RelationshipRepo>()
            .As<IRelationshipRepo>();

        builder.RegisterType<ThingInfoRepo>()
            .As<IThingInfoRepo>();

        // Register Services
        builder.RegisterType<ThingService>()
            .As<IThingService>();

        // Register Required Cache Manager
        builder.RegisterType<CacheManager<IThing>>()
            .As<ICacheManager<IThing>>();
    }
}