using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Refit;
using RefitClientExample.ApiServices;
using RefitClientExample.Models;

namespace RefitClientExample
{
    public class Worker : BackgroundService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ILogger<Worker> _logger;

        public Worker(IHostApplicationLifetime hostApplicationLifetime, ILogger<Worker> logger)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await Main();
            }
            finally
            {
                _hostApplicationLifetime.StopApplication();
            }
        }

        private async Task Main()
        {
            var serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                Converters =
                {
                    new ObjectToInferredTypesConverter(),
                    new JsonStringEnumConverter()
                }
            };
            var serializer = new SystemTextJsonContentSerializer(
                new JsonSerializerOptions(serializerOptions)
            );
            var refitSettings = new RefitSettings(serializer);
            var apiService = RestService.For<IPersonApiService>("https://localhost:5001/api/v1", refitSettings);
            // var people = await apiService.GetPeople();
            // foreach (var person in people)
            // {
            //     _logger.LogInformation("Person : {0}", person);
            // }
            //
            // _logger.LogInformation("---------- Find PersonId=2 ------------- ");
            //
            // {
            //     var person = await apiService.FindPersonById(2);
            //     _logger.LogInformation("@ Found Person : {person}", person);
            // }


            _logger.LogInformation("---------- Add New Person ------------- ");
            Person addedPerson = null; 
            try
            {
                // await apiService.AddPerson(new PersonForCreation()
                //     {
                //         Email = "simson@acme.com",
                //         Gender = Gender.Male,
                //         FirstName = "Simpson",
                //         LastName = "Jade",
                //         DateOfBirth = new DateTimeOffset(1971, 12, 11, 0, 0, 0, TimeSpan.Zero),
                //     })
                //     ;
                addedPerson = await apiService.AddPerson(new PersonForCreation()
                {
                    Email = "simson@acme.com",
                    Gender = Gender.Male,
                    FirstName = "Simpson",
                    LastName = "Jade",
                    DateOfBirth = new DateTimeOffset(1971, 12, 11, 0, 0, 0, TimeSpan.Zero),
                });
                _logger.LogInformation("@ Added Person : {person}", addedPerson);
            }
            catch (ValidationApiException e)
            {
                var contents = await e.GetContentAsAsync<Dictionary<string, String>>();
                _logger.LogError(e, string.Join(", ", contents.Select(pair => $"{pair.Key} : {pair.Value}")));
            }

            // var lastPersonId = people.LastOrDefault()?.PersonId;
            try
            {
                if (addedPerson != null)
                {
                    await apiService.Delete(addedPerson.PersonId);
                    _logger.LogWarning("deleted Person(personId = {personId}", addedPerson.PersonId);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to delete person of personId with {personId}", addedPerson?.PersonId);
            }
        }
    }

    //
    // public sealed class JsonIntEnumConverter : JsonConverterFactory
    // {
    //     public override JsonConverter CreateConverter(
    //         Type typeToConvert,
    //         JsonSerializerOptions options)
    //     {
    //         return (JsonConverter)Activator.CreateInstance(typeof(EnumConverter<>).MakeGenericType(typeToConvert),
    //             BindingFlags.Instance | BindingFlags.Public, (Binder)null, new object[3]
    //             {
    //                 (object)this._converterOptions,
    //                 (object)this._namingPolicy,
    //                 (object)options
    //             }, (CultureInfo)null);
    //     }
    //
    //     public override bool CanConvert(Type typeToConvert)
    //     {
    //         return typeToConvert.IsEnum;
    //     }
    // }
    //
    // public class EnumNumberConverter<T>
    // {
    //     public string 
    // }
}