using HotelBooking.Data.Mongo.Repository;
using HotelBooking.Models.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace HotelBooking.Data.Mongo.Configuration
{
    public static class ServiceCollectionExtentions
    {
        public static void ConfigureDatabase(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddSingleton(x =>
                   new MongoSettings(configuration["MongoSettings:ConnectionString"], configuration["MongoSettings:Database"])
                  );

            services.AddSingleton<IMongoClient>(x =>
                new MongoClient(x.GetService<MongoSettings>().ConnectionString)
                );

            services.AddSingleton<IBookingRepository, BookingRepository>();
            services.AddSingleton<IRoomManagementRepository, RoomRepository>();
            services.AddSingleton<IHotelRepository, HotelRepository>();
        }
    }
}
